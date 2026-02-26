using Safe_Audit.BL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Safe_Audit.PL
{
    public partial class FRM_Search_Settlements : Form
    {
        CLS_Settlements settlement = new CLS_Settlements();

        public FRM_Search_Settlements()
        {
            InitializeComponent();
            // مسحنا LoadDataToComboBoxes من هنا عشان ميتكررش مع الـ Load
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. رقم الوردية
                int? sID = string.IsNullOrEmpty(txtShiftID.Text) ? (int?)null : int.Parse(txtShiftID.Text);

                // 2. الكاشير والجهاز (نستخدم SelectedValue)
                int? cID = (cmbCashiers.SelectedValue == null) ? (int?)null : Convert.ToInt32(cmbCashiers.SelectedValue);
                int? dID = (cmbDevices.SelectedValue == null) ? (int?)null : Convert.ToInt32(cmbDevices.SelectedValue);

                // 3. الشيفت (لو "الكل" نبعت null عشان SQL يفهم)
                string sType = (cmbShiftType.Text == "الكل" || string.IsNullOrEmpty(cmbShiftType.Text)) ? null : cmbShiftType.Text;

                // تنفيذ الاستعلام
                dgvResults.DataSource = settlement.SearchSettlements(dtpFrom.Value, dtpTo.Value, sID, cID, sType, dID);

                // تنسيق بسيط للجدول بعد البحث
                if (dgvResults.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد نتائج تطابق هذا البحث");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("تأكد من إدخال البيانات بشكل صحيح: " + ex.Message);
            }
        }

        private void FRM_Search_Settlements_Load(object sender, EventArgs e)
        {
            try
            {
                // تعبئة الكاشيرات
                DataTable dtCashiers = settlement.GET_ALL_CASHIERS();
                cmbCashiers.DataSource = dtCashiers;
                cmbCashiers.DisplayMember = "CashierName";
                cmbCashiers.ValueMember = "CashierID";
                cmbCashiers.SelectedIndex = -1;

                // تعبئة الأجهزة
                DataTable dtDevices = settlement.GET_ALL_DEVICES();
                cmbDevices.DataSource = dtDevices;
                cmbDevices.DisplayMember = "DeviceName";
                cmbDevices.ValueMember = "DeviceID";
                cmbDevices.SelectedIndex = -1;

                // تعبئة نوع الشيفت
                cmbShiftType.Items.Clear();
                cmbShiftType.Items.Add("الكل");
                cmbShiftType.Items.Add("صباحي");
                cmbShiftType.Items.Add("مسائي");
                cmbShiftType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات الأساسية: " + ex.Message);
            }
        }

        private void dgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // نتأكد إننا واقفين على عمود "الحالة" (تأكد من كتابة الاسم كما هو في الـ SQL)
            if (dgvResults.Columns[e.ColumnIndex].Name == "الحالة")
            {
                if (e.Value != null)
                {
                    string status = e.Value.ToString();

                    // 1. لو الحالة فيها كلمة "عجز" تلون السطر بالأحمر الباهت (عشان الكلام يبان)
                    if (status.Contains("عجز"))
                    {
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.MistyRose;
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    // 2. لو الحالة "تمام" تلون بالأخضر
                    else if (status.Contains("تمام"))
                    {
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Honeydew;
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkGreen;
                    }
                    // 3. لو الحالة "زيادة" تلون بالأزرق أو الأخضر الفاتح
                    else if (status.Contains("زيادة"))
                    {
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;
                        dgvResults.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkBlue;
                    }
                }
            }
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. استخراج رقم الوردية من الصف المختار
                decimal sID = Convert.ToDecimal(dgvResults.Rows[e.RowIndex].Cells["رقم الوردية"].Value);

                // 2. فتح فورم الإضافة
                FRM_Add_Settlement frm = new FRM_Add_Settlement();

                // 3. استدعاء دالة البحث الموجودة داخل فورم الإضافة وإعطاؤها الرقم
                // نمرر null للـ sender و e لأننا نناديها برمجياً
                frm.btnSearchShift_Click(null, null, sID);

                // 4. عرض الفورم
                frm.ShowDialog();
            }
        }
    }
}