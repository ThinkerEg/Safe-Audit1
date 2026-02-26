using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Safe_Audit.BL;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
namespace Safe_Audit.PL
{
    public partial class FRM_Accounts_Mag : Form
    {
        // استدعاء كلاس الحسابات من طبقة البزنس
        CLS_Accounts acc = new CLS_Accounts();
        BindingSource bs = new BindingSource();
        public FRM_Accounts_Mag()
        {
            InitializeComponent();
   
            LoadData(); // تحميل البيانات فور فتح الشاشة
        }

        // دالة لجلب البيانات وتنسيق الجدول
        void LoadData()
        {
            try
            {
                //dgvAccounts.DataSource = acc.Get_All_Accounts();
                bs.DataSource  = acc.Get_All_Accounts(); ; // الجدول اللي جاي من قاعدة البيانات
                dgvAccounts.DataSource = bs;
                bindingNavigator1.BindingSource = bs;
                // تنسيق أسماء الأعمدة (تأكد أنها نفس أسماء الـ View)
                dgvAccounts.Columns["MethodID"].HeaderText = "الكود";
                dgvAccounts.Columns["MethodName"].HeaderText = "اسم الحساب";
                dgvAccounts.Columns["AccountType"].HeaderText = "النوع";
                dgvAccounts.Columns["OpeningBalance"].HeaderText = "الرصيد الافتتاحي";
                dgvAccounts.Columns["CurrentBalance"].HeaderText = "الرصيد الحالي 🔥";

                // تلوين عمود الرصيد الحالي لتمييزه
                dgvAccounts.Columns["CurrentBalance"].DefaultCellStyle.ForeColor = Color.Blue;
                dgvAccounts.Columns["CurrentBalance"].DefaultCellStyle.Font = new Font(dgvAccounts.Font, FontStyle.Bold);
                CalculateFooterTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات: " + ex.Message);
            }
        }

        //// 1. استدعاءات الويندوز لقص الحواف وتحريك الفورم
        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        //private extern static void ReleaseCapture();

        //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
        //private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //// 2. دالة تطبيق الحواف المستديرة
        //private void ApplyRoundedCorners(Control control, int radius)
        //{
        //    if (control != null && control.Width > 0 && control.Height > 0)
        //        control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, radius, radius));
        //}

        // 3. تحديث التصميم (نادي هذه الدالة في Load وفي OnResize)
        private void UpdateLayoutDesign()
        {
            // استدعاء مباشر من الكلاس الجديد
            HelperMethods.ApplyModernCorners(grpInputs, 25);
            HelperMethods.ApplyModernCorners(dgvAccounts, 20);
            HelperMethods.ApplyModernCorners(this, 30);
        }

        // 4. لضمان ثبات الحواف عند تغيير الحجم
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayoutDesign();
        }
        // زر جديد (تنظيف الخانات)
        private void btnNew_Click(object sender, EventArgs e)
        {
            txtAccName.Clear();
            cmbAccType.SelectedIndex = -1;
            numInitialBal.Value = 0;
            numInitialBal.Enabled = true; // إعادة تفعيله عند الإضافة الجديدة
            txtAccName.Focus();
        }

        // زر إضافة حساب جديد
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccName.Text) || cmbAccType.SelectedIndex == -1)
            {
                MessageBox.Show("من فضلك أدخل اسم الحساب ونوعه أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                acc.Add_Account(txtAccName.Text, cmbAccType.Text, numInitialBal.Value);
                MessageBox.Show("تم حفظ الحساب الجديد بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                btnNew.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        // زر التعديل
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvAccounts.CurrentRow.Cells["MethodID"].Value);
                acc.Update_Account(id, txtAccName.Text, cmbAccType.Text);

                MessageBox.Show("تم تحديث البيانات بنجاح", "تعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء التعديل: " + ex.Message);
            }
        }

        // نقل البيانات من الجدول للحقول عند الضغط على صف
        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAccounts.CurrentRow != null)
            {
                txtAccName.Text = dgvAccounts.CurrentRow.Cells["MethodName"].Value.ToString();
                cmbAccType.Text = dgvAccounts.CurrentRow.Cells["AccountType"].Value.ToString();
                numInitialBal.Value = Convert.ToDecimal(dgvAccounts.CurrentRow.Cells["OpeningBalance"].Value);

                // الرصيد الافتتاحي لا يُعدل بعد الحفظ لضمان سلامة الحسابات
                numInitialBal.Enabled = false;
            }
        }
        private void FRM_Accounts_Mag_Load(object sender, EventArgs e)
        {
            // 1. الظهور التدريجي (بسيط وسريع)
            HelperMethods.FormFadeIn(this);

            // 2. تفعيل تحريك الفورم عن طريق الهيدر
            pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };

            // 3. تنسيق الأزرار والجداول دفعة واحدة من الهيلبر
            HelperMethods.StyleButtons(this);
            //btnClose.MouseEnter += (s, ev) => { btnClose.BackColor = Color.Red; };
            //btnClose.MouseLeave += (s, ev) => { btnClose.BackColor = Color.Transparent; };
            // 4. تحميل البيانات الأساسية
            LoadData();
        }
        //private void FRM_Accounts_Mag_Load(object sender, EventArgs e)
        //{
        //    // 1. تأثيرات زر الإغلاق (ممكن تسيبها هنا لأنها تخص الزر ده بس)
        //    btnClose.MouseEnter += (s, ev) => { btnClose.BackColor = Color.Red; };
        //    btnClose.MouseLeave += (s, ev) => { btnClose.BackColor = Color.Transparent; };

        //    // 2. تحديث التصميم والحواف
        //    UpdateLayoutDesign();

        //    // 3. تحريك الفورم باستخدام الهيلبر (كود سطر واحد بدل اللخبطة)
        //    pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };

        //    // 4. تحميل البيانات (تأكد أنك تناديها مرة واحدة فقط)
        //    LoadData();

        //    cmbAccType.SelectedIndex = 0;
        //}
        // تأثيرات زر الإغلاق
      
private void btnClose_Click(object sender, EventArgs e)
        {
           // Close();
            HelperMethods.FormFadeOut(this);
        }


        //private void btnAccTypeSerch_Click(object sender, EventArgs e)
        //{
        //    if (bs.DataSource != null)
        //    {
        //        bs.Filter = string.Format("AccountType = '{0}'", cmbAccTypeSerch.Text);
        //    }
        //}
        private void btnAccTypeSerch_Click(object sender, EventArgs e)
        {
            if (bs.DataSource != null)
            {
                if (cmbAccTypeSerch.SelectedIndex != -1)
                {
                    // فلترة البيانات بناءً على النوع المختار
                    bs.Filter = string.Format("AccountType LIKE '%{0}%'", cmbAccTypeSerch.Text);
                }
                else
                {
                    bs.RemoveFilter(); // إلغاء الفلتر إذا لم يتم اختيار شيء
                }
                CalculateFooterTotal(); // تحديث الإجمالي في الفوتر بعد الفلترة
            }
        }
        void CalculateFooterTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvAccounts.Rows)
            {
                if (row.Cells["CurrentBalance"].Value != null)
                    total += Convert.ToDecimal(row.Cells["CurrentBalance"].Value);
            }
            lblFinalBalance.Text = "إجمالي أرصدة الحسابات الظاهرة: " + total.ToString("N2");
        }
    }
}