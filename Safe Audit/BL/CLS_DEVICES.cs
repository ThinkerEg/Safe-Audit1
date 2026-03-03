using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Safe_Audit.BL
{
    class CLS_DEVICES
    {
        DAL.DataAccessLayer DAL = new DAL.DataAccessLayer();

        // 1. جلب كل الأجهزة (اسم الإجراء لازم يكون مطابق للـ SQL)
        public DataTable GET_ALL_DEVICES()
        {
            return DAL.SelectData("SP_GET_ALL_DEVICES", null);
        }

        // 2. إضافة جهاز (باراميتر واحد فقط: الاسم)
        public void ADD_DEVICE(string DeviceName)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@DeviceName", SqlDbType.NVarChar, 100);
            param[0].Value = DeviceName;
            DAL.Open();
            DAL.ExecuteCommand("SP_ADD_DEVICE", param);
            DAL.Close();
        }

        // 3. تعديل جهاز (2 باراميتر: ID والاسم الجديد)
        public void EDIT_DEVICE(int DeviceID, string DeviceName)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@DeviceID", SqlDbType.Int);
            param[0].Value = DeviceID;

            param[1] = new SqlParameter("@DeviceName", SqlDbType.NVarChar, 100);
            param[1].Value = DeviceName;
            DAL.Open();
            DAL.ExecuteCommand("SP_EDIT_DEVICE", param);
            DAL.Close();
        }

        // ملاحظة: تم إلغاء دالة الحذف بناءً على طلبك لمنع مشاكل الحسابات
    }
}