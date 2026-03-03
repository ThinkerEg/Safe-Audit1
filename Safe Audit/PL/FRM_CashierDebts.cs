using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Safe_Audit.BL;

namespace Safe_Audit.PL
{
    public partial class FRM_CashierDebts : Form
    {
        CLS_Settlements stl = new CLS_Settlements();

        public FRM_CashierDebts()
        {
            InitializeComponent();
            // تفعيل السحب والحواف إذا كنت تستخدم الـ Helper
            // HelperMethods.PrepareForm(this); 
            LoadCashiers();
        }

        // 1. تحميل قائمة الكاشيرية عند فتح الشاشة
        void LoadCashiers()
        {
            try
            {
                DataTable dt = stl.GET_ALL_CASHIERS();
                cmbCashiers.DataSource = dt;
                cmbCashiers.DisplayMember = "CashierName";
                cmbCashiers.ValueMember = "CashierID";
                cmbCashiers.SelectedIndex = -1; // البدء بدون اختيار
            }
            catch (Exception ex) { MessageBox.Show("خطأ في تحميل الكاشيرية: " + ex.Message); }
        }

        // 2. الحدث الذي يعمل عند اختيار اسم كاشير
        private void cmbCashiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // التحقق من وجود اختيار حقيقي وليس مجرد تحميل للبيانات
            if (cmbCashiers.SelectedValue == null || cmbCashiers.SelectedValue is DataRowView) return;

            try
            {
                int cashierID = Convert.ToInt32(cmbCashiers.SelectedValue);

                // أ- جلب جدول الحركات وعرضه في الـ DataGridView
                DataTable dtReport = stl.GetCashierTransactionsReport(cashierID);
                dgvTransactions.DataSource = dtReport;
                FormatGrid();

                // ب- حساب الإجماليات من الـ DataTable لعرضها في الـ Labels
                CalculateSummary(dtReport, cashierID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في جلب البيانات: " + ex.Message);
            }
        }

        // 3. دالة حساب الملخص (الصافي، المسدد، العجز)
        void CalculateSummary(DataTable dt, int cashierID)
        {
            decimal totalShortage = 0;
            decimal totalPaid = 0;

            foreach (DataRow row in dt.Rows)
            {
                // استخدام الرقم (Index) أضمن من الاسم العربي
                decimal amount = Convert.ToDecimal(row[2]); // عمود المبلغ
                string type = row[1].ToString();           // عمود نوع الحركة

                if (type.Contains("عجز"))
                    totalShortage += amount;
                else if (type.Contains("سداد نقدي"))
                    totalPaid += amount;
            }

            lblTotalShortage.Text = "إجمالي العجز: " + totalShortage.ToString("N2");
            lblTotalPaid.Text = "إجمالي المسدد: " + totalPaid.ToString("N2");

            decimal netDebt = stl.GetCashierRemainingDebt(cashierID);
            lblNetDebt.Text = "صافي المديونية: " + netDebt.ToString("N2");

            lblNetDebt.ForeColor = netDebt > 0 ? Color.FromArgb(192, 57, 43) : Color.FromArgb(39, 174, 96);
        }
        //void CalculateSummary(DataTable dt, int cashierID)
        //{
        //    decimal totalShortage = 0;
        //    decimal totalPaid = 0;

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        decimal amount = Convert.ToDecimal(row["المبلغ"]);
        //        string type = row["نوع الحركة"].ToString();

        //        if (type.Contains("عجز")) // تأكد أن الكلمة مطابقة لما يخرج من الـ Database
        //            totalShortage += amount;
        //        else if (type.Contains("سداد"))
        //            totalPaid += amount;
        //    }

        //    // تحديث الأدوات الموجودة في الـ Designer فعلياً
        //    lblTotalShortage.Text = "إجمالي العجز: " + totalShortage.ToString("N2");
        //    lblTotalPaid.Text = "إجمالي المسدد: " + totalPaid.ToString("N2");

        //    // جلب الصافي المتبقي من الدالة المخصصة في الـ BL
        //    decimal netDebt = stl.GetCashierRemainingDebt(cashierID);
        //    lblNetDebt.Text = "صافي المديونية: " + netDebt.ToString("N2");

        //    // تلوين الصافي (أحمر لو عليه فلوس، أخضر لو خالص)
        //    lblNetDebt.ForeColor = netDebt > 0 ? Color.FromArgb(192, 57, 43) : Color.FromArgb(39, 174, 96);
        //}

        // 4. تنسيق جدول البيانات
        void FormatGrid()
        {
            if (dgvTransactions.Columns.Count > 0)
            {
                dgvTransactions.Columns[0].HeaderText = "التاريخ";
                dgvTransactions.Columns[1].HeaderText = "نوع الحركة";
                dgvTransactions.Columns[2].HeaderText = "المبلغ";
                dgvTransactions.Columns[3].HeaderText = "ملاحظات";

                // تحسين المظهر
                dgvTransactions.EnableHeadersVisualStyles = false;
                dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
                dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
        }

        // 5. زر إضافة سداد جديد (F2)
        private void btnPayShortcut_Click(object sender, EventArgs e)
        {
            if (cmbCashiers.SelectedValue == null || cmbCashiers.SelectedValue is DataRowView)
            {
                MessageBox.Show("من فضلك اختر الكاشير أولاً");
                return;
            }

            // نفتح فورم الحركات المالية
            FRM_FinancialMovements frm = new FRM_FinancialMovements();

            // تمرير الكاشير المختار للفورم الآخر (تأكد من وجود هذه الدالة في FRM_FinancialMovements)
            // frm.SetExternalCashier(Convert.ToInt32(cmbCashiers.SelectedValue));

            frm.ShowDialog(); // ShowDialog تجبر المستخدم على إنهاء العملية قبل العودة هنا

            // تحديث البيانات تلقائياً بعد إغلاق شاشة السداد
            cmbCashiers_SelectedIndexChanged(null, null);
        }

        // 6. زر التحديث
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbCashiers_SelectedIndexChanged(null, null);
        }

        // 7. زر الإغلاق
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_CashierDebts_Load(object sender, EventArgs e)
        {
            pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };
        }
    }
}