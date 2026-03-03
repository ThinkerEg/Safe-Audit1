using Safe_Audit.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Safe_Audit.PL
{
    public partial class FRM_CASHIERS : Form
    {
        BL.CLS_CASHIERS cashier = new BL.CLS_CASHIERS();
        int currentID = 0; // 0 تعني إضافة، أي رقم آخر يعني تعديل

        public FRM_CASHIERS()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            dgvCashiers.DataSource = cashier.GetAllCashiers();
            ResetFields();
        }

        void ResetFields()
        {
            currentID = 0;
            txtName.Clear();
            txtPhone.Clear();
            btnSave.Text = "حفظ جديد";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // استخدام التحقق الموحد من الهلبير بدلاً من الـ IF التقليدية
            if (!HelperMethods.IsValid(this))
            {
                MessageBox.Show("يرجى إدخال البيانات المطلوبة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // إرسال currentID (لو 0 يحفظ جديد، لو غير كدة يعدل)
                cashier.SaveCashier(currentID, txtName.Text, txtPhone.Text);

                MessageBox.Show("تمت العملية بنجاح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // لإعادة تحميل الجدول وتصفير الحقول
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تنفيذ العملية: " + ex.Message);
            }
        }

        // عند الضغط مرتين على سطر في الجدول للتعديل
        private void dgvCashiers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCashiers.CurrentRow != null)
            {
                // جلب البيانات من الصف المختار
                currentID = Convert.ToInt32(dgvCashiers.CurrentRow.Cells[0].Value);
                txtName.Text = dgvCashiers.CurrentRow.Cells[1].Value.ToString();
                txtPhone.Text = dgvCashiers.CurrentRow.Cells[2].Value.ToString();

                // تغيير شكل الزرار ولونه ليدل على وضع "التعديل"
                btnSave.Text = "تعديل البيانات";
                btnSave.BackColor = HelperMethods.WarningColor; // اللون البرتقالي المعتمد
            }
        }
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtName.Text))
        //    {
        //        MessageBox.Show("يرجى إدخال اسم الكاشير");
        //        return;
        //    }

        //    try
        //    {
        //        cashier.SaveCashier(currentID, txtName.Text, txtPhone.Text);
        //        MessageBox.Show("تم الحفظ بنجاح", "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("خطأ: " + ex.Message);
        //    }
        //}

        //// عند الضغط مرتين على سطر في الجدول للتعديل
        //private void dgvCashiers_DoubleClick(object sender, EventArgs e)
        //{
        //    if (dgvCashiers.CurrentRow != null)
        //    {
        //        currentID = Convert.ToInt32(dgvCashiers.CurrentRow.Cells[0].Value);
        //        txtName.Text = dgvCashiers.CurrentRow.Cells[1].Value.ToString();
        //        txtPhone.Text = dgvCashiers.CurrentRow.Cells[2].Value.ToString();
        //        btnSave.Text = "تعديل البيانات";
        //    }
        //}
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetFields();
        }
    }
}