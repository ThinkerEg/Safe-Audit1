using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Safe_Audit.BL
{
    public class CLS_Settlements
    {
        DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();

        // جلب الكاشيرات
        public DataTable GET_ALL_CASHIERS()
        {
            // استخدمنا SqlCommand مباشر بدل SelectData عشان نقدر نبعت نص SQL عادي
            SqlDataAdapter da = new SqlDataAdapter("SELECT CashierID, CashierName FROM Cashiers", DAL.sqlconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // جلب طرق الدفع
        public DataTable GET_PAYMENT_METHODS()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MethodID, MethodName FROM PaymentMethods", DAL.sqlconnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // الدالة الشاملة التي ترسل كل شيء للإجراء المخزن
        public void SaveFullSettlement(
       decimal sID, int cashierID, int deviceID, int uID, DateTime date, string type,
       decimal sysAmt, decimal actCash, decimal actDigi, decimal totalExp, decimal diff, string status,
       int f200, int f100, int f50, int f20, int f10, int f5, int f1,
       DataTable dtPayments, DataTable dtExpenses)
        {
            DAL.Open();

            SqlCommand cmd = new SqlCommand("SP_SAVE_FULL_SETTLEMENT", DAL.sqlconnection);
            cmd.CommandType = CommandType.StoredProcedure;

            // 1. بيانات الوردية (الرأس) + الجهاز الجديد
            cmd.Parameters.AddWithValue("@ShiftID", sID);
            cmd.Parameters.AddWithValue("@CashierID", cashierID);
            cmd.Parameters.AddWithValue("@DeviceID", deviceID); // التعديل هنا
            cmd.Parameters.AddWithValue("@UserID", uID);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@SysAmt", sysAmt);
            cmd.Parameters.AddWithValue("@ActCash", actCash);
            cmd.Parameters.AddWithValue("@ActDigi", actDigi);
            cmd.Parameters.AddWithValue("@TotalExp", totalExp);
            cmd.Parameters.AddWithValue("@Diff", diff);
            cmd.Parameters.AddWithValue("@Status", status);

            // 2. بيانات التفنيدة
            cmd.Parameters.AddWithValue("@f200", f200);
            cmd.Parameters.AddWithValue("@f100", f100);
            cmd.Parameters.AddWithValue("@f50", f50);
            cmd.Parameters.AddWithValue("@f20", f20);
            cmd.Parameters.AddWithValue("@f10", f10);
            cmd.Parameters.AddWithValue("@f5", f5);
            cmd.Parameters.AddWithValue("@f1", f1);

            // 3. إرسال الجداول (TVP)
            SqlParameter pPay = cmd.Parameters.AddWithValue("@Payments", dtPayments);
            pPay.SqlDbType = SqlDbType.Structured;
            pPay.TypeName = "PaymentTableType";

            SqlParameter pExp = cmd.Parameters.AddWithValue("@Expenses", dtExpenses);
            pExp.SqlDbType = SqlDbType.Structured;
            pExp.TypeName = "ExpenseTableType";

            cmd.ExecuteNonQuery();
            DAL.Close();
        }
        public DataTable GET_ALL_DEVICES()
        {
            // بننادي على اسم الإجراء اللي عملناه في SQL
            return DAL.SelectData("SP_GET_ALL_DEVICES", null);
        }
        public DataTable GET_LAST_SHIFT_ID()
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            // اسم الـ Procedure لازم يطابق اللي كتبناه في SQL
            DataTable dt = DAL.SelectData("GET_LAST_SHIFT_ID", null);
            return dt;
        }
        public DataTable GetSettlementByID(int shiftID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Int);
            param[0].Value = shiftID;

            return DAL.SelectData("SP_GET_SETTLEMENT_BY_ID", param);
        }
        // جلب المدفوعات
        public DataTable GetPaymentsByID(int shiftID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Int) { Value = shiftID };
            return DAL.SelectData("SP_GET_PAYMENTS_BY_ID", param);
        }

        // جلب المصروفات
        public DataTable GetExpensesByID(int shiftID)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Int) { Value = shiftID };
            return DAL.SelectData("SP_GET_EXPENSES_BY_ID", param);
        }
        public DataTable GetCashDenominations(decimal shiftID)
        {
            SqlParameter[] param = new SqlParameter[1];
            // تأكد أن نوع البيانات Decimal ليطابق السكربت الخاص بك
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Decimal) { Value = shiftID };

            // استدعاء الإجراء المخزن الجديد من خلال طبقة الـ DAL
            return DAL.SelectData("SP_GET_CASH_DENOMINATIONS_BY_ID", param);
        }
        // دالة جديدة مخصصة لجلب المدفوعات مع أسمائها لتجنب أخطاء البحث
        public DataTable GetPaymentsForEdit(decimal shiftID)
        {
            SqlParameter[] param = new SqlParameter[1];
            // نستخدم Decimal ليطابق نوع الـ ShiftID في قاعدة البيانات عندك
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Decimal) { Value = shiftID };

            // استدعاء الإجراء المخزن الجديد الذي أنشأناه (الذي يحتوي على الـ JOIN)
            return DAL.SelectData("SP_GET_SETTLEMENT_PAYMENTS_FOR_EDIT", param);
        }
        // دالة البحث الشاملة في الورديات
        public DataTable SearchSettlements(DateTime fromDate, DateTime toDate, int? shiftID, int? cashierID, string shiftType, int? deviceID)
        {
            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@From", SqlDbType.DateTime) { Value = fromDate };
            param[1] = new SqlParameter("@To", SqlDbType.DateTime) { Value = toDate };

            // التعامل مع القيم التي قد تكون Null (إذا لم يختارها المستخدم)
            param[2] = new SqlParameter("@ID", SqlDbType.Int) { Value = (object)shiftID ?? DBNull.Value };
            param[3] = new SqlParameter("@Cashier", SqlDbType.Int) { Value = (object)cashierID ?? DBNull.Value };
            param[4] = new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = (object)shiftType ?? DBNull.Value };
            param[5] = new SqlParameter("@Device", SqlDbType.Int) { Value = (object)deviceID ?? DBNull.Value };

            // استدعاء الإجراء المخزن من خلال طبقة الوصول للبيانات DAL
            return DAL.SelectData("SP_SEARCH_SETTLEMENTS", param);
        }
        public void UpdateFullSettlement(decimal shiftID, int cashierID, int deviceID, int userID,
                                 DateTime date, string type, decimal sysAmt,
                                 decimal actCash, decimal actDigi, decimal totalExp,
                                 decimal diff, string status,
                                 int f200, int f100, int f50, int f20, int f10, int f5, int f1,
                                 DataTable dtPay, DataTable dtExp)
        {
            DAL.Open();
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@ShiftID", SqlDbType.Decimal) { Value = shiftID };
            param[1] = new SqlParameter("@CashierID", SqlDbType.Int) { Value = cashierID };
            param[2] = new SqlParameter("@DeviceID", SqlDbType.Int) { Value = deviceID };
            param[3] = new SqlParameter("@UserID", SqlDbType.Int) { Value = userID };
            param[4] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = date };
            param[5] = new SqlParameter("@Type", SqlDbType.NVarChar, 20) { Value = type };
            param[6] = new SqlParameter("@SysAmt", SqlDbType.Decimal) { Value = sysAmt };
            param[7] = new SqlParameter("@ActCash", SqlDbType.Decimal) { Value = actCash };
            param[8] = new SqlParameter("@ActDigi", SqlDbType.Decimal) { Value = actDigi };
            param[9] = new SqlParameter("@TotalExp", SqlDbType.Decimal) { Value = totalExp };
            param[10] = new SqlParameter("@Diff", SqlDbType.Decimal) { Value = diff };
            param[11] = new SqlParameter("@Status", SqlDbType.NVarChar, 50) { Value = status };
            param[12] = new SqlParameter("@f200", SqlDbType.Int) { Value = f200 };
            param[13] = new SqlParameter("@f100", SqlDbType.Int) { Value = f100 };
            param[14] = new SqlParameter("@f50", SqlDbType.Int) { Value = f50 };
            param[15] = new SqlParameter("@f20", SqlDbType.Int) { Value = f20 };
            param[16] = new SqlParameter("@f10", SqlDbType.Int) { Value = f10 };
            param[17] = new SqlParameter("@f5", SqlDbType.Int) { Value = f5 };
            param[18] = new SqlParameter("@f1", SqlDbType.Int) { Value = f1 };
            param[19] = new SqlParameter("@Payments", SqlDbType.Structured) { TypeName = "dbo.PaymentTableType", Value = dtPay };
            param[20] = new SqlParameter("@Expenses", SqlDbType.Structured) { TypeName = "dbo.ExpenseTableType", Value = dtExp };

            DAL.ExecuteCommand("SP_UPDATE_FULL_SETTLEMENT", param);
            DAL.Close();
        }
        public DataTable GetEditLogs(DateTime from, DateTime to)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer(); // تأكد من اسم الكلاس عندك
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@From", SqlDbType.DateTime) { Value = from };
            param[1] = new SqlParameter("@To", SqlDbType.DateTime) { Value = to };

            DAL.Open();
            DataTable dt = DAL.SelectData("SP_GET_EDIT_LOGS", param);
            DAL.Close();
            return dt;
        }
    }
}