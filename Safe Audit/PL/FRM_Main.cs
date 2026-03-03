using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Safe_Audit.BL; // استدعاء الهيلبر

namespace Safe_Audit.PL
{
    public partial class FRM_Main : Form
    {
        public FRM_Main()
        {
            InitializeComponent();

            // ربط خلفية الفورم الرئيسية بلون الخلفية الموحد
            this.BackColor = HelperMethods.BackColor;

            // ربط لون الشريط العلوي (Header)
            pnlHeader.BackColor = Color.White; // أو HelperMethods.BackColor إذا أردت توحيدها

            // ربط لون القائمة الجانبية (Sidebar) باللون الأساسي للبرنامج
            pnlSidebar.BackColor = HelperMethods.HeaderColor;

            // تطبيق التنسيقات العامة
            HelperMethods.ApplyModernCorners(this, 25);
            HelperMethods.StyleButtons(this);
        }

        #region تحريك الفورم
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            HelperMethods.MoveForm(this.Handle); // تحريك الفورم من البانل العلوي
        }
        #endregion

        #region أزرار التنقل (فتح الشاشات)

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            // استخدام الطريقة التقليدية الآمنة لفتح الفورم
            HelperMethods.OpenChildForm(new FRM_Accounts_Mag());
        }

        private void btnAccountStatement_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_AccountStatement());
        }

        private void btnTransfers_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_Transfers());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // إغلاق البرنامج
        }
        #endregion

        #region لمسات إبداعية (تفاعل الأزرار)
        // حدث عند دخول الماوس على الزر
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                // نستخدم لون التحذير أو لون فاتح من اللون الأساسي عند الوقوف بالماوس
                btn.BackColor = Color.FromArgb(210, 230, 245); // لون فاتح مريح للعين
                btn.ForeColor = HelperMethods.HeaderColor;    // نص بلون الهيدر
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btn = (Button)sender;
                btn.BackColor = Color.Transparent; // العودة للحالة الأصلية
                btn.ForeColor = Color.White;
            }
        }
        //private void btn_MouseEnter(object sender, EventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button btn = (Button)sender; // Casting تقليدي وآمن
        //        btn.BackColor = Color.FromArgb(52, 152, 219); // تغيير اللون للأزرق الفاتح
        //        btn.ForeColor = Color.White;
        //    }
        //}

        //// حدث عند خروج الماوس من الزر
        //private void btn_MouseLeave(object sender, EventArgs e)
        //{
        //    if (sender is Button)
        //    {
        //        Button btn = (Button)sender;
        //        btn.BackColor = Color.Transparent; // العودة للشفافية
        //        btn.ForeColor = Color.Black;
        //    }
        //}
        #endregion

        private void btnSettlement_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_Add_Settlement());
        }

        private void btn_ExpensesList_Click(object sender, EventArgs e)
        {
             HelperMethods.OpenChildForm(new FRM_ExpensesList());
        }

        private void btn_Search_Settlements_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_Search_Settlements());
        }

        private void btn_EditLogs_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_EditLogs());
        }

        private void FRM_Main_Load(object sender, EventArgs e)
        {
            // السماح بتحريك النموذج من خلال الهيدر
            pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };

            // استدعاء دالة تحميل البيانات فور التشغيل
            RefreshDashboard();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // إعادة تحميل البيانات لتحديث الأرقام والجدول
            RefreshDashboard();

            // تنبيه بسيط للمستخدم (اختياري)
            // MessageBox.Show("تم تحديث البيانات بنجاح", "تحديث", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshDashboard()
        {
            try
            {
                // إنشاء كائن من كلاس العمليات
                BL.CLS_Accounts acc = new BL.CLS_Accounts();

                // جلب البيانات من الكلاس الوسيط
                DataTable dt = acc.GetAccountsBalanceSummary();

                // عرض البيانات في الجدول
                dgvBalances.DataSource = dt;
                // تحسين مظهر الجدول (اختياري)
                dgvBalances.Columns[0].Width = 150; // عرض اسم الحساب مثلاً

                // تحديث الليبلات (Labels) بناءً على القيم المحسوبة في الـ DataTable أو عبر استعلامات منفصلة
                // ملاحظة: يفضل أن يكون الإجراء المخزن يعيد الأرصدة الكلية أيضاً لسرعة الأداء
                UpdateBalanceLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحديث البيانات: " + ex.Message);
            }
        }
        private void UpdateBalanceLabels()
        {
            try
            {
                // إنشاء كائن من الكلاس الذي عدلناه فوق
                BL.CLS_Accounts acc = new BL.CLS_Accounts();

                // إسناد القيم لليبلات الموجودة على الواجهة
                lblCashVal.Text = acc.GetTotalCashBalance();
                lblDigitalVal.Text = acc.GetTotalDigitalBalance();
                lblExpVal.Text = acc.GetTotalExpenses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحديث الأرقام: " + ex.Message);
            }
        }

        private void btn_FinancialMovements_Click(object sender, EventArgs e)
        {
            
                  HelperMethods.OpenChildForm(new FRM_FinancialMovements());
        }

        private void btn_CashierDebts_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_CashierDebts());
        }

        private void btn_CASHIERS_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_CASHIERS());

            
        }

        private void btn_DEVICES_Click(object sender, EventArgs e)
        {
            HelperMethods.OpenChildForm(new FRM_DEVICES());
        }
    }
}