using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Safe_Audit.BL;

namespace Safe_Audit.PL
{
    public partial class FRM_DEVICES : Form
    {
        CLS_DEVICES dev = new CLS_DEVICES();
        int ID = 0; // متغير المعرف للتحكم في الإضافة أو التعديل

        public FRM_DEVICES()
        {
            InitializeComponent();
            LoadData(); // تحميل البيانات عند الفتح
        }

        // 1. دالة جلب البيانات وعرضها في الجدول
        void LoadData()
        {
            try
            {
                dgvDevices.DataSource = dev.GET_ALL_DEVICES();
                if (dgvDevices.Columns.Count > 0)
                {
                    dgvDevices.Columns[0].HeaderText = "كود الجهاز";
                    dgvDevices.Columns[1].HeaderText = "اسم الجهاز";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات: " + ex.Message);
            }
        }

        // 2. زر الحفظ (إضافة أو تعديل بناءً على الـ ID)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeviceName.Text))
            {
                MessageBox.Show("من فضلك أدخل اسم الجهاز");
                txtDeviceName.Focus();
                return;
            }

            try
            {
                if (ID == 0) // إضافة
                {
                    dev.ADD_DEVICE(txtDeviceName.Text);
                    MessageBox.Show("تمت إضافة الجهاز بنجاح", "إضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // تعديل
                {
                    dev.EDIT_DEVICE(ID, txtDeviceName.Text);
                    MessageBox.Show("تم تعديل بيانات الجهاز بنجاح", "تعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnNew_Click(null, null); // تفريغ الحقول بعد الحفظ
                LoadData(); // تحديث الجدول
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }

        // 3. اختيار جهاز من الجدول للتعديل (DoubleClick)
        private void dgvDevices_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDevices.CurrentRow != null)
            {
                ID = Convert.ToInt32(dgvDevices.CurrentRow.Cells[0].Value);
                txtDeviceName.Text = dgvDevices.CurrentRow.Cells[1].Value.ToString();

                btnSave.Text = "تحديث البيانات ✔";
                //btnSave.BackColor = Color.FromArgb(39, 174, 96); // تغيير اللون للأخضر عند التعديل
                btnSave.BackColor = HelperMethods.WarningColor; // اللون البرتقالي المعتمد
                grpInputs.Text = "تعديل جهاز رقم: " + ID;
            }
        }

        // 4. زر جديد / مسح الحقول
        private void btnNew_Click(object sender, EventArgs e)
        {
            ID = 0;
            txtDeviceName.Clear();
            btnSave.Text = "حفظ البيانات +";
            btnSave.BackColor = Color.FromArgb(41, 128, 185);
            grpInputs.Text = "بيانات الجهاز";
            txtDeviceName.Focus();
        }

        // 5. زر الخروج
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_DEVICES_Load(object sender, EventArgs e)
        {
            //pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };
        }
    }
}