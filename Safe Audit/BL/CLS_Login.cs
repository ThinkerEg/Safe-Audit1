//using System;
//using System.Data;
//using System.Data.SqlClient;

//namespace Safe_Audit.BL
//{
//    public class CLS_Login
//    {
//        DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();

//        // 1. لجلب المستخدمين النشطين فقط (للكومبو بوكس في شاشة الدخول)
//        public DataTable GetActiveUsers()
//        {
//            // نبعت اسم الإجراء الجديد بدلاً من جملة SELECT النصية
//            return DAL.SelectData("SP_GET_ACTIVE_USERS_ONLY", null);
//        }

//        // 2. لجلب كل المستخدمين (لشاشة الإدارة والداتا جريد)
//        public DataTable GetAllUsers()
//        {
//            return DAL.SelectData("SP_GET_ALL_USERS", null);
//        }
//        // تعديل الدالة لتنادي الإجراء المخزن الجديد

//        // 3. التحقق من الدخول
//        public DataTable CheckLogin(string UserName, string Password)
//        {
//            SqlParameter[] param = new SqlParameter[2];
//            param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
//            param[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = Password };

//            return DAL.SelectData("SP_LOGIN", param);
//        }
//        // 2. إضافة مستخدم جديد
//        public void AddUser(string FullName, string UserName, string Password, string UserType)
//        {
//            SqlParameter[] param = new SqlParameter[4];
//            param[0] = new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = FullName };
//            param[1] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
//            param[2] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = Password };
//            param[3] = new SqlParameter("@UserType", SqlDbType.NVarChar, 50) { Value = UserType };

//            DAL.ExecuteCommand("SP_ADD_USER", param);
//        }

//        // 3. التعديل الشامل + التجميد (Master Edit)
//        // دي أهم دالة لأنها بتشيل التعديل العادي، تغيير الباسورد، وتغيير الحالة (نشط/مجمد)
//        public void EditUserMaster(int UserID, string FullName, string UserName, string Password, string UserType, bool IsActive)
//        {
//            SqlParameter[] param = new SqlParameter[6];
//            param[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID };
//            param[1] = new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = FullName };
//            param[2] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
//            // في دالة EditUserMaster، السطر ده تحديداً:
//            param[3] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = (object)Password ?? DBNull.Value };
//            // param[3] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = Password }; // لو فاضي الإجراء في SQL هيتجاهله
//            param[4] = new SqlParameter("@UserType", SqlDbType.NVarChar, 50) { Value = UserType };
//            param[5] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive }; // دي بتاعة التجميد

//            DAL.ExecuteCommand("SP_EDIT_USER_MASTER", param);
//        }


//    }
//}
using System;
using System.Data;
using System.Data.SqlClient;

namespace Safe_Audit.BL
{
    public class CLS_Login
    {
        DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();

        // 1. لجلب المستخدمين النشطين فقط
        public DataTable GetActiveUsers()
        {
            DAL.Open();
            DataTable dt = DAL.SelectData("SP_GET_ACTIVE_USERS_ONLY", null);
            DAL.Close();
            return dt;
        }

        // 2. لجلب كل المستخدمين
        public DataTable GetAllUsers()
        {
            DAL.Open();
            DataTable dt = DAL.SelectData("SP_GET_ALL_USERS", null);
            DAL.Close();
            return dt;
        }

        // 3. التحقق من الدخول
        public DataTable CheckLogin(string UserName, string Password)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
            param[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = Password };

            DAL.Open();
            DataTable dt = DAL.SelectData("SP_LOGIN", param);
            DAL.Close();
            return dt;
        }

        // 4. إضافة مستخدم جديد
        public void AddUser(string FullName, string UserName, string Password, string UserType)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = FullName };
            param[1] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
            param[2] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = Password };
            param[3] = new SqlParameter("@UserType", SqlDbType.NVarChar, 50) { Value = UserType };

            DAL.Open();
            DAL.ExecuteCommand("SP_ADD_USER", param);
            DAL.Close();
        }

        // 5. التعديل الشامل + التجميد (Master Edit)
        public void EditUserMaster(int UserID, string FullName, string UserName, string Password, string UserType, bool IsActive)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID };
            param[1] = new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = FullName };
            param[2] = new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = UserName };
            param[3] = new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = (object)Password ?? DBNull.Value };
            param[4] = new SqlParameter("@UserType", SqlDbType.NVarChar, 50) { Value = UserType };
            param[5] = new SqlParameter("@IsActive", SqlDbType.Bit) { Value = IsActive };

            DAL.Open();
            DAL.ExecuteCommand("SP_EDIT_USER_MASTER", param);
            DAL.Close();
        }
    }
}