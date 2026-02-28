using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Safe_Audit.BL
{
    class CLS_Accounts
    {
        DAL.DataAccessLayer dal = new DAL.DataAccessLayer();

        // 1. جلب بيانات الحسابات والأرصدة الحالية
        public DataTable Get_All_Accounts()
        {
            DataTable Dt = dal.SelectData("SP_GET_ALL_ACCOUNTS", null);
            return Dt;
        }

        // 2. تنفيذ عملية تحويل مالي
        public void SP_MAKE_TRANSFER(int FromID, int ToID, decimal Amount, DateTime Date, string Note)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@FromID", SqlDbType.Int) { Value = FromID };
            param[1] = new SqlParameter("@ToID", SqlDbType.Int) { Value = ToID };
            param[2] = new SqlParameter("@Amount", SqlDbType.Decimal) { Value = Amount };
            param[3] = new SqlParameter("@Date", SqlDbType.DateTime) { Value = Date };
            param[4] = new SqlParameter("@Note", SqlDbType.NVarChar, 250) { Value = Note };

            dal.ExecuteCommand("SP_MAKE_TRANSFER", param);
            dal.Close();
        }

        // 3. جلب سجل التحويلات بناءً على التاريخ (الدالة المفقودة)
        public DataTable Get_Transfers_By_Date(DateTime SearchDate)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SearchDate", SqlDbType.Date) { Value = SearchDate };

            DataTable Dt = dal.SelectData("SP_GET_TRANSFERS_BY_DATE", param);
            return Dt;
        }

        // 4. إضافة حساب جديد
        public void Add_Account(string name, string type, decimal initialBalance)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = name };
            param[1] = new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = type };
            param[2] = new SqlParameter("@Balance", SqlDbType.Decimal) { Value = initialBalance };

            dal.Open();
            dal.ExecuteCommand("SP_ADD_ACCOUNT", param);
            dal.Close();
        }

        // 5. تعديل بيانات حساب
        public void Update_Account(int id, string name, string type)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
            param[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = name };
            param[2] = new SqlParameter("@Type", SqlDbType.NVarChar, 50) { Value = type };

            dal.Open();
            dal.ExecuteCommand("SP_UPDATE_ACCOUNT", param);
            dal.Close();
        }
        // دالة جلب كشف الحساب التفصيلي من قاعدة البيانات
        public DataTable GetAccountStatement(int MethodID, DateTime From, DateTime To)
        {
          
            SqlParameter[] param = new SqlParameter[3];

            // تأكد من ترتيب الـ Index هنا: 0، 1، 2
            param[0] = new SqlParameter("@MethodID", SqlDbType.Int) { Value = MethodID };
            param[1] = new SqlParameter("@FromDate", SqlDbType.Date) { Value = From };
            param[2] = new SqlParameter("@ToDate", SqlDbType.Date) { Value = To };

            return dal.SelectData("SP_GET_ACCOUNT_STATEMENT", param);
        }
        // داخل كلاس CLS_Accounts
        public DataTable GetAccountsBalanceSummary()
        {
            // هنا نستدعي الدالة العامة من الداتا لاير التي تتعامل مع الإجراءات
            // لنفترض أن اسم الإجراء في قاعدة البيانات هو "SP_GetDashboardSummary"
            return dal.SelectData("SP_GetDashboardSummary", null);
        }
        // 1. جلب إجمالي النقدية (الكاش)
        public string GetTotalCashBalance()
        {
            DataTable dt = dal.SelectData("SP_GetTotalCash", null);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToDecimal(dt.Rows[0][0]).ToString("N2");
            }
            return "0.00";
        }

        // 2. جلب إجمالي الفيزا/الرقمي
        public string GetTotalDigitalBalance()
        {
            DataTable dt = dal.SelectData("SP_GetTotalDigital", null);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToDecimal(dt.Rows[0][0]).ToString("N2");
            }
            return "0.00";
        }

        // 3. جلب إجمالي المصروفات
        public string GetTotalExpenses()
        {
            DataTable dt = dal.SelectData("SP_GetTotalExpenses", null);
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToDecimal(dt.Rows[0][0]).ToString("N2");
            }
            return "0.00";
        }
        public void AddFinancialTransaction(int accID, string type, string category, string person, decimal amount, DateTime date, string notes)
        {
            DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@AccountID", SqlDbType.Int) { Value = accID };
            param[1] = new SqlParameter("@Type", SqlDbType.NVarChar, 20) { Value = type };
            param[2] = new SqlParameter("@Category", SqlDbType.NVarChar, 50) { Value = category };
            param[3] = new SqlParameter("@ResponsiblePerson", SqlDbType.NVarChar, 100) { Value = person };
            param[4] = new SqlParameter("@Amount", SqlDbType.Decimal) { Value = amount };
            param[5] = new SqlParameter("@TransDate", SqlDbType.DateTime) { Value = date };
            param[6] = new SqlParameter("@Notes", SqlDbType.NVarChar) { Value = notes };

            DAL.Open();
            DAL.ExecuteCommand("SP_AddFinancialTransaction", param);
            DAL.Close();
        }
    }
}