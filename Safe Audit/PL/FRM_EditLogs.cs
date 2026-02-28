using Safe_Audit.BL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Safe_Audit.PL
{
    public partial class FRM_EditLogs : Form
    {
        // استدعاء كلاس العمليات
       CLS_Settlements settlement = new CLS_Settlements();

        public FRM_EditLogs()
        {
            InitializeComponent();
            // ضبط التواريخ لتبدأ من بداية اليوم الحالي
            dtpFrom.Value = DateTime.Now.Date;
            dtpTo.Value = DateTime.Now;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // جلب البيانات باستخدام الدالة التي أنشأناها في BL
                DataTable dt = settlement.GetEditLogs(dtpFrom.Value, dtpTo.Value);

                dgvLogs.DataSource = dt;

                // تحسينات شكلية للأعمدة بعد الربط
                if (dgvLogs.Columns.Count > 0)
                {
                    dgvLogs.Columns["مسلسل"].Visible = false; // إخفاء المعرف الداخلي
                    dgvLogs.Columns["تاريخ التعديل"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في جلب البيانات: " + ex.Message);
            }
        }

        // إضافة لمسة احترافية: تلوين الخلية إذا كان هناك فارق بين المبلغ القديم والجديد
        private void dgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // نطبق التلوين فقط على أعمدة "العجز" و "رصيد النظام" و "الكاش"
            if (dgvLogs.Columns[e.ColumnIndex].Name == "عجز حالي" ||
                dgvLogs.Columns[e.ColumnIndex].Name == "رصيد النظام الحالي" ||
                dgvLogs.Columns[e.ColumnIndex].Name == "الكاش الحالي")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    // تحديد اسم العمود المقابل (السابق) للمقارنة
                    string currentColumn = dgvLogs.Columns[e.ColumnIndex].Name;
                    string previousColumn = "";

                    if (currentColumn == "عجز حالي") previousColumn = "عجز سابق";
                    else if (currentColumn == "رصيد النظام الحالي") previousColumn = "رصيد النظام السابق";
                    else if (currentColumn == "الكاش الحالي") previousColumn = "الكاش السابق";

                    // الحصول على القيمة السابقة من نفس الصف
                    var prevValue = dgvLogs.Rows[e.RowIndex].Cells[previousColumn].Value;

                    if (prevValue != null && prevValue != DBNull.Value)
                    {
                        decimal current = Convert.ToDecimal(e.Value);
                        decimal previous = Convert.ToDecimal(prevValue);

                        // إذا اختلف الرقم الحالي عن السابق، قم بتلوين الخلية
                        if (current != previous)
                        {
                            e.CellStyle.BackColor = Color.Yellow; // لون الخلفية
                            e.CellStyle.ForeColor = Color.Red;    // لون الخط
                            e.CellStyle.Font = new Font(dgvLogs.Font, FontStyle.Bold); // خط عريض
                        }
                    }
                }
            }
        }
        //private void dgvLogs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (dgvLogs.Columns[e.ColumnIndex].Name == "الفارق")
        //    {
        //        if (e.Value != null && e.Value != DBNull.Value)
        //        {
        //            decimal diff = Convert.ToDecimal(e.Value);
        //            if (diff != 0)
        //            {
        //                // تلوين الخلية باللون البرتقالي إذا حدث تغيير
        //                e.CellStyle.BackColor = Color.MistyRose;
        //                e.CellStyle.ForeColor = Color.Red;
        //                e.CellStyle.Font = new Font(dgvLogs.Font, FontStyle.Bold);
        //            }
        //        }
        //    }
        //}

        private void btnClose_Click(object sender, EventArgs e)
        {
            // استدعاء دالة الاختفاء التدريجي ثم الإغلاق
            HelperMethods.FormFadeOut(this);
        }
    }
}