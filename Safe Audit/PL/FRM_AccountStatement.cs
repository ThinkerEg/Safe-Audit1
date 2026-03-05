using Safe_Audit.BL;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Safe_Audit.PL
{
    public partial class FRM_AccountStatement : Form
    {
        // استدعاء كلاس الحسابات من طبقة الـ Business Layer
        BL.CLS_Accounts acc = new BL.CLS_Accounts();

        public FRM_AccountStatement()
        {
            InitializeComponent();
        }

        // 1. دالة تحميل الحسابات في الـ ComboBox
        void LoadAccounts()
        {
            try
            {
                DataTable dt = acc.Get_All_Accounts();
                cmbAccount.DataSource = dt;
                cmbAccount.DisplayMember = "MethodName";
                cmbAccount.ValueMember = "MethodID";
                cmbAccount.SelectedIndex = -1; // البدء بدون اختيار
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل الحسابات: " + ex.Message);
            }
        }

        // 2. تنسيق مظهر الجدول (الألوان والخطوط)
        private void FormatGrid()
        {
            if (dgvStatement.Columns.Count > 0)
            {
                // تلوين الأعمدة بناءً على نوع الحركة
                if (dgvStatement.Columns.Contains("وارد"))
                    dgvStatement.Columns["وارد"].DefaultCellStyle.ForeColor = HelperMethods.SuccessColor;

                if (dgvStatement.Columns.Contains("منصرف"))
                    dgvStatement.Columns["منصرف"].DefaultCellStyle.ForeColor = HelperMethods.DangerColor;

                if (dgvStatement.Columns.Contains("الرصيد التراكمي"))
                {
                    dgvStatement.Columns["الرصيد التراكمي"].DefaultCellStyle.BackColor = Color.FromArgb(245, 249, 252);
                    dgvStatement.Columns["الرصيد التراكمي"].DefaultCellStyle.Font = new Font(dgvStatement.Font, FontStyle.Bold);
                }
            }
        }

        //private void FRM_AccountStatement_Load(object sender, EventArgs e)
        //{
        //    // تأثيرات الدخول والحواف
        //    HelperMethods.FormFadeIn(this);
        //    HelperMethods.ApplyRoundedCorners(this, 30);
        //    HelperMethods.ApplyRoundedCorners(dgvStatement, 20);
        //    HelperMethods.ApplyRoundedCorners(grpFilters, 20);

        //    // تحريك الفورم من الهيدر
        //    ////pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };

        //    // تطبيق الهوية البصرية (الألوان المعتمدة)
        //    this.BackColor = HelperMethods.BackColor;
        //    pnlHeader.BackColor = HelperMethods.HeaderColor;
        //    pnlFooter.BackColor = Color.FromArgb(220, 230, 235); // أغمق قليلاً للتمييز

        //    // تنسيق الأزرار
        //    btnShowReport.BackColor = HelperMethods.SuccessColor;
        //    btnShowReport.ForeColor = Color.White;
        //    btnShowReport.Cursor = Cursors.Hand;

        //    // تأثير زر الإغلاق (طلبك الخاص)
        //    btnClose.MouseEnter += (s, ev) => { btnClose.BackColor = Color.Red; };
        //    btnClose.MouseLeave += (s, ev) => { btnClose.BackColor = Color.Transparent; };

        //    // تنسيق Label الإجمالي
        //    lblFinalBalance.ForeColor = HelperMethods.DangerColor;

        //    // ضبط التواريخ الافتراضية (من بداية الشهر الحالي)
        //    dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //    dtpTo.Value = DateTime.Now;

        //    LoadAccounts();
        //}
        private void FRM_AccountStatement_Load(object sender, EventArgs e)
        {
            // 1. استدعاء الهيلبر لعمل (التدريج، الألوان، تنسيق الأزرار والجداول، الحواف المستديرة)
            // كل ده بيتم في سطرين بس بفضل الدوال اللي عملناها
            HelperMethods.FormFadeIn(this);
            HelperMethods.StyleButtons(this); // دي بتنسق الأزرار والجداول والحواف داخلياً

            // 2. تفعيل تحريك الفورم
            //////pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };

            // 3. ضبط الإعدادات الخاصة بالبيانات فقط
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            LoadAccounts();
        }
        private void btnShowReport_Click(object sender, EventArgs e)
        {
            // التأكد من اختيار حساب أولاً
            if (cmbAccount.SelectedValue == null)
            {
                MessageBox.Show("يرجى اختيار الحساب أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. جلب البيانات من الدالة الوسيطة (التي تستدعي الإجراء المطور SP_GET_ACCOUNT_STATEMENT)
                DataTable dt = acc.GetAccountStatement(
                    Convert.ToInt32(cmbAccount.SelectedValue),
                    dtpFrom.Value.Date, // نرسل التاريخ فقط بدون الوقت لضمان دقة الفلترة
                    dtpTo.Value.Date
                );

                // 2. عرض البيانات في الـ DataGridView
                dgvStatement.DataSource = dt;
                FormatGrid(); // استدعاء دالة تنسيق الأعمدة

                // 3. حساب وعرض الإجماليات إذا كان هناك بيانات
                if (dt.Rows.Count > 0)
                {
                    // حساب إجمالي الوارد والمنصرف من الـ DataTable باستخدام LINQ
                    decimal totalIn = dt.AsEnumerable().Sum(row => row.Field<decimal>("وارد"));
                    decimal totalOut = dt.AsEnumerable().Sum(row => row.Field<decimal>("منصرف"));

                    // الرصيد النهائي هو الرصيد التراكمي لآخر عملية في الجدول
                    decimal finalBal = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["الرصيد التراكمي"]);

                    // تحديث الليبلات لتشمل الوصف بجانب القيمة الرقمية
                    lblTotalIn.Text = "إجمالي الوارد: " + totalIn.ToString("N2") + " ج.م";
                    lblTotalOut.Text = "إجمالي المنصرف: " + totalOut.ToString("N2") + " ج.م";
                    lblFinalBalance.Text = "إجمالي الرصيد الحالي: " + finalBal.ToString("N2") + " ج.م";
                }
                else
                {
                    // تصفير الليبلات في حال عدم وجود حركات
                    lblTotalIn.Text = "0.00";
                    lblTotalOut.Text = "0.00";
                    lblFinalBalance.Text = "لا توجد حركات لهذا الحساب في الفترة المختارة";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء عرض كشف الحساب: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            HelperMethods.FormFadeOut(this);
        }
    }
}