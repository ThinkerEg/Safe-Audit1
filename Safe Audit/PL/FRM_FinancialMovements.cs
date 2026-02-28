using System;
using System.Data;
using System.Windows.Forms;
using Safe_Audit.BL;

namespace Safe_Audit.PL
{
    public partial class FRM_FinancialMovements : Form
    {
        CLS_Accounts acc = new CLS_Accounts();

        public FRM_FinancialMovements()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            // تحميل الحسابات (خزنة، فيزا، الخ)
            cmbAccount.DataSource = acc.Get_All_Accounts();
            cmbAccount.DisplayMember = "MethodName";
            cmbAccount.ValueMember = "MethodID";
            cmbAccount.SelectedIndex = -1;

            // أنواع العمليات المحددة
            cmbTransType.Items.Clear();
            cmbTransType.Items.Add("سداد عجز (قبض)");
            cmbTransType.Items.Add("توريد لصاحب المال (صرف)");
            cmbTransType.Items.Add("مصروفات من العهدة (صرف)");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. التحقق من البيانات الأساسية
                if (cmbAccount.SelectedValue == null || cmbTransType.SelectedIndex == -1 || numAmount.Value <= 0)
                {
                    MessageBox.Show("من فضلك أكمل كافة البيانات المطلوبة (الحساب، النوع، المبلغ)", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. التحقق من اسم المسؤول (الكاشير) - إضافة للأمان
                if (string.IsNullOrWhiteSpace(txtResponsible.Text))
                {
                    MessageBox.Show("يجب كتابة اسم المسؤول أو الكاشير القائم بالعملية", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtResponsible.Focus();
                    return;
                }

                // 3. تحديد نوع الحركة (Deposit / Withdraw)
                string dbType = "Withdraw";
                string category = cmbTransType.SelectedItem.ToString();

                if (category.Contains("قبض") || category.Contains("سداد عجز"))
                {
                    dbType = "Deposit";
                }

                // 4. التحقق من الرصيد في حالة الصرف (Withdraw)
                if (dbType == "Withdraw")
                {
                    DataRowView drv = (DataRowView)cmbAccount.SelectedItem;
                    // تأكد أن الكولوم اسمه CurrentBalance في الـ DataTable العائد من Get_All_Accounts
                    decimal currentBal = Convert.ToDecimal(drv["CurrentBalance"]);
                    if (numAmount.Value > currentBal)
                    {
                        MessageBox.Show($"عفواً، الرصيد الحالي ({currentBal}) لا يكفي لصرف هذا المبلغ", "عجز رصيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // 5. التنفيذ النهائي
                if (MessageBox.Show("هل أنت متأكد من حفظ هذه الحركة المالية؟", "تأكيد الحفظ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    acc.AddFinancialTransaction(
                        Convert.ToInt32(cmbAccount.SelectedValue),
                        dbType,
                        category,
                        txtResponsible.Text.Trim(),
                        numAmount.Value,
                        dtpDate.Value,
                        txtNotes.Text.Trim()
                    );

                    MessageBox.Show("تم تسجيل الحركة وتحديث رصيد الحساب بنجاح", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message, "خطأ تقني", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
    }
}