using Safe_Audit.DAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Safe_Audit.BL
{
    class CLS_CASHIERS
    {
        DataAccessLayer DAL = new DataAccessLayer();

        // جلب البيانات
        public DataTable GetAllCashiers()
        {
            return DAL.SelectData("SP_GET_ALL_CASHIERS", null);
        }

        // حفظ وتعديل
        public void SaveCashier(int id, string name, string phone)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ID", SqlDbType.Int) { Value = id };
            param[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 100) { Value = name };
            param[2] = new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = phone };

            DAL.Open();
            // تنفيذ الإجراء الموحد (إضافة/تعديل)
            DAL.ExecuteCommand("SP_SAVE_CASHIER", param);
            DAL.Close();
        }      
    }
}