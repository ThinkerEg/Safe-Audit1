using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Safe_Audit.BL;

namespace Safe_Audit.PL
{
    public partial class FRM_Add_Settlement : Form
    {
        // استدعاء طبقة العمليات
        CLS_Settlements settlement = new CLS_Settlements();

        public FRM_Add_Settlement()
        {
            InitializeComponent();
            this.Padding = new Padding(1);
            LoadDataToCombos(); // تعبئة الكومبو بوكس عند فتح الشاشة
        }

        // 1. دالة لجلب البيانات الأساسية للكاشيرات وطرق الدفع
        private void LoadDataToCombos()
        {
            try
            {
                // تعبئة الكاشيرات
                cmbCashier.DataSource = settlement.GET_ALL_CASHIERS();
                cmbCashier.DisplayMember = "CashierName";
                cmbCashier.ValueMember = "CashierID";

                // تعبئة طرق الدفع (فيزا/انستا..)
                cmbInstaAccName.DataSource = settlement.GET_PAYMENT_METHODS();
                cmbInstaAccName.DisplayMember = "MethodName";
                cmbInstaAccName.ValueMember = "MethodID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل البيانات الأساسية: " + ex.Message);
            }
        }
        int Row_Exp = 1;
        int Row_Paym = 1;
        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtExpenseReason.Text) || numExpenses.Value <= 0)
            {
                MessageBox.Show("من فضلك ادخل سبب المصروف والمبلغ أولاً", "تنبيه");
                return;
            }

            // الترتيب الصحيح حسب صورتك (6053df):
            // Index 0: MethodID (نضع 0 للمصروفات)
            // Index 1: سبب الصرف (النص)
            // Index 2: المبلغ (الرقم)
            dgvExpenses.Rows.Add(Row_Exp, txtExpenseReason.Text, numExpenses.Value);
            Row_Exp++;
            CalculateTotals();
            txtExpenseReason.Clear();
            numExpenses.Value = 0;
        }
        // 2. زر إضافة مصروف للجدول (قبل الحفظ النهائي)
        //private void btnAddExpense_Click(object sender, EventArgs e)
        //{
        //    // 1. التأكد إن المستخدم كتب سبب ومبلغ قبل الإضافة
        //    //if (string.IsNullOrEmpty(txtExpenseReason.Text) || numExpenses.Value <= 0)
        //    //{
        //    //    MessageBox.Show("من فضلك ادخل سبب المصروف والمبلغ أولاً", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    //    return;
        //    //}

        //    //// 2. إضافة البيانات للـ DataGridView
        //    //dgvExpenses.Rows.Add(txtExpenseReason.Text, numExpenses.Value);

        //    //// 3. مسح الخانات عشان العملية اللي بعدها
        //    //txtExpenseReason.Clear();
        //    //numExpenses.Value = 0;
        //    //txtExpenseReason.Focus();

        //    //// 4. استدعاء دالة الحسابات الشاملة (عشان تحدث إجمالي المصروفات والفرق والوضع)
        //    //CalculateTotals();
        //}

        // 3. زر إضافة دفع إلكتروني للجدول (قبل الحفظ النهائي)
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            // 1. التحقق لو المبلغ صفر أو أقل
            if (numInstaAmount.Value <= 0)
            {
                MessageBox.Show("عفواً، لا يمكن إضافة مبلغ قيمته صفر. يرجى إدخال قيمة المبلغ أولاً.",
                                "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numInstaAmount.Focus(); // وضع المؤشر على خانة المبلغ لتسهيل الكتابة
                return; // الخروج من الدالة وعدم تنفيذ الإضافة
            }

            // 2. في حالة كان المبلغ أكبر من صفر، يتم التنفيذ كالمعتاد
            // إضافة الاسم والقيمة والـ ID المخفي
            //dgvPayments.Rows.Add(cmbInstaAccName.Text, numInstaAmount.Value, cmbInstaAccName.SelectedValue);
            dgvPayments.Rows.Add(Row_Paym, cmbInstaAccName.SelectedValue, cmbInstaAccName.Text, numInstaAmount.Value);
           
            Row_Paym++;
            // استدعاء دالة الجمع عشان "السيستم يجمع" فوراً
            CalculateTotals();

            // تصفير الخانة للعملية التالية
            numInstaAmount.Value = 0;
        }

        // 4. زر الحفظ النهائي لقاعدة البيانات
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. التحقق الذكي من الحقول وتلوين الفارغ منها
            if (HelperMethods.IsValid(this))
            {
                try
                {
                    // 2. تجهيز جداول البيانات (TVP)

                    // --- جدول المدفوعات ---
                    DataTable dtPayments = new DataTable();
                    dtPayments.Columns.Add("MethodID", typeof(int));
                    dtPayments.Columns.Add("Amount", typeof(decimal));

                    foreach (DataGridViewRow row in dgvPayments.Rows)
                    {
                        if (row.IsNewRow) continue;
                        // نأخذ MethodID من الخلية [0] والمبلغ من الخلية [3] حسب تصميمك
                        if (row.Cells[0].Value != null && row.Cells[3].Value != null)
                        {
                            dtPayments.Rows.Add(row.Cells[0].Value, row.Cells[3].Value);
                        }
                    }

                    // --- جدول المصروفات ---
                    DataTable dtExpenses = new DataTable();
                    dtExpenses.Columns.Add("Reason", typeof(string));
                    dtExpenses.Columns.Add("Amount", typeof(decimal));

                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        if (row.IsNewRow) continue;
                        // نأخذ السبب من الخلية [1] والمبلغ من الخلية [2]
                        if (row.Cells[1].Value != null && row.Cells[2].Value != null)
                        {
                            dtExpenses.Rows.Add(row.Cells[1].Value, row.Cells[2].Value);
                        }
                    }

                    // 3. استدعاء الدالة الشاملة للحفظ
                    settlement.SaveFullSettlement(
                        numShiftID.Value,
                        Convert.ToInt32(cmbCashier.SelectedValue),
                        Convert.ToInt32(cmbDevices.SelectedValue),
                        1, // يمكن استبداله بـ GlobalUserID.ID لاحقاً
                        date_P.Value,
                        cmbShift.Text,
                        numSystemAmount.Value,
                        Convert.ToDecimal(lblTotal.Text),
                        Convert.ToDecimal(lblDigitalTotal.Text),
                        Convert.ToDecimal(lblExpensesTotal.Text),
                        Convert.ToDecimal(lblFinalDiff.Text),
                        lblStatus.Text,
                        (int)txt200.Value, (int)txt100.Value, (int)txt50.Value, (int)txt20.Value,
                        (int)txt10.Value, (int)txt5.Value, (int)txt1.Value,
                        dtPayments,
                        dtExpenses
                    );

                    //// 4. إنهاء العملية بنجاح
                    //MessageBox.Show("تم حفظ وإتمام تصفية الوردية بنجاح وتحديث أرصدة الخزينة والحسابات", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //// تنظيف الحقول للعملية التالية
                    //HelperMethods.ClearFields(this);
                    //dgvPayments.Rows.Clear();
                    //dgvExpenses.Rows.Clear();
                    //numSearchID.Focus();
                    // 4. إنهاء العملية بنجاح
                    MessageBox.Show("تم حفظ وإتمام تصفية الوردية بنجاح وتحديث أرصدة الخزينة والحسابات", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- الحل هنا ---

                    // 5. تنظيف الحقول 
                    HelperMethods.ClearFields(this);
                    dgvPayments.Rows.Clear();
                    dgvExpenses.Rows.Clear();

                    // 6. تحديث رقم الوردية الجديد تلقائياً (عشان ميبقاش صفر)
                    // افترضنا إن عندك دالة اسمها GetNextShiftID أو بتنادي الـ SP اللي اسمها GET_LAST_SHIFT_ID
                    // 1. جلب الـ ID الجديد (ID الوردية اللي عليها الدور)
                    DataTable dt = settlement.GET_LAST_SHIFT_ID();
                    numShiftID.Value = (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                                       ? Convert.ToInt32(dt.Rows[0][0]) + 1 : 1;

                    // 7. إعادة ضبط الألوان (عشان نشيل اللون الأحمر اللي ظهر غلط)
                    foreach (Control c in this.Controls)
                    {
                        if (c is TextBox || c is NumericUpDown)
                            c.BackColor = Color.White; // أو اللون الطبيعي للفورم
                    }

                    numSearchID.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message, "خطأ تقني", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // رسالة تنبيه في حال وجود حقول ناقصة (سيقوم الهيلبر بتلوينها بالأحمر تلقائياً)
                MessageBox.Show("يرجى ملء الحقول المطلوبة الملونة بالأحمر أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    Row_Paym = 1; Row_Exp = 1;
        //    try
        //    {
        //        // 1. تحويل جدول المدفوعات (dgvPayments)
        //        DataTable dtPayments = new DataTable();
        //        dtPayments.Columns.Add("MethodID", typeof(int));
        //        dtPayments.Columns.Add("Amount", typeof(decimal));

        //        foreach (DataGridViewRow row in dgvPayments.Rows)
        //        {
        //            if (row.IsNewRow) continue; // تخطي الصف الفارغ الأخير

        //            // تحقق من أن الجريد فيها 3 أعمدة على الأقل قبل طلب الخلية [2]
        //            if (row.Cells.Count >= 3 && row.Cells[2].Value != null)
        //            {

        //                dtPayments.Rows.Add(row.Cells[1].Value, row.Cells[3].Value);
        //            }
        //            else if (row.Cells.Count == 2) // إذا كان الـ ID مخفي في عمود غير موجود، أبلغ المستخدم
        //            {
        //                throw new Exception("جدول المدفوعات يفتقد لعمود المعرف (MethodID). تأكد من إضافة الـ SelectedValue في العمود الثالث.");
        //            }
        //        }

        //        // 2. تحويل جدول المصروفات (dgvExpenses)
        //        DataTable dtExpenses = new DataTable();
        //        dtExpenses.Columns.Add("Reason", typeof(string));
        //        dtExpenses.Columns.Add("Amount", typeof(decimal));

        //        foreach (DataGridViewRow row in dgvExpenses.Rows)
        //        {
        //            if (row.IsNewRow) continue;

        //            if (row.Cells.Count >= 2 && row.Cells[0].Value != null)
        //            {
        //                dtExpenses.Rows.Add(row.Cells[1].Value, row.Cells[2].Value);
        //            }
        //        }

        //        // 3. استدعاء الدالة الشاملة (تطابق الإجراء المخزن المرفق)
        //        settlement.SaveFullSettlement(
        //            numShiftID.Value,
        //            Convert.ToInt32(cmbCashier.SelectedValue),
        //            Convert.ToInt32(cmbDevices.SelectedValue),
        //            1, // CreatedBy_UserID
        //            date_P.Value,
        //            cmbShift.Text,
        //            numSystemAmount.Value,
        //            Convert.ToDecimal(lblTotal.Text),
        //            Convert.ToDecimal(lblDigitalTotal.Text),
        //            Convert.ToDecimal(lblExpensesTotal.Text),
        //            Convert.ToDecimal(lblFinalDiff.Text),
        //            lblStatus.Text,
        //            (int)txt200.Value, (int)txt100.Value, (int)txt50.Value, (int)txt20.Value,
        //            (int)txt10.Value, (int)txt5.Value, (int)txt1.Value,
        //            dtPayments,
        //            dtExpenses
        //        );

        //        MessageBox.Show("تم الحفظ بنجاح وتحديث الخزنة!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        ClearForm();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("خطأ: " + ex.Message);
        //    }
        //}
        private void CalculateTotals()
        {
            // 1. حساب الكاش الفعلي
            decimal cashTotal = (txt200.Value * 200) + (txt100.Value * 100) + (txt50.Value * 50) +
                                (txt20.Value * 20) + (txt10.Value * 10) + (txt5.Value * 5) + (txt1.Value * 1);
            lblTotal.Text = cashTotal.ToString("N2");

            // 2. حساب المدفوعات الرقمية - باستخدام حلقة آمنة
            decimal digitalTotal = 0;
            foreach (DataGridViewRow row in dgvPayments.Rows)
            {
                if (row.IsNewRow || row.Cells[3].Value == null) continue;
                digitalTotal += Convert.ToDecimal(row.Cells[3].Value);
            }
            lblDigitalTotal.Text = digitalTotal.ToString("N2");

            // 3. حساب المصروفات
            decimal expensesTotal = 0;
            foreach (DataGridViewRow row in dgvExpenses.Rows)
            {
                if (row.IsNewRow || row.Cells[2].Value == null) continue;
                expensesTotal += Convert.ToDecimal(row.Cells[2].Value);
            }
            lblExpensesTotal.Text = expensesTotal.ToString("N2");

            // 4. الحسابات النهائية (المراية)
            // إجمالي المقبوضات (كاش + فيزا)
            lblTotalDisplay.Text = (cashTotal + digitalTotal).ToString("N2");

            // المبلغ الذي يجب أن يكون موجوداً (صافي الوردية)
            decimal actualTotal = (cashTotal + digitalTotal + expensesTotal);
            decimal finalDiff = actualTotal - numSystemAmount.Value;

            lblFinalDiff.Text = Math.Abs(finalDiff).ToString("N2"); // عرض الرقم الموجب

            // تحديد الحالة
            if (finalDiff == 0)
            {
                lblStatus.Text = "تمام (مطابق)";
                lblStatus.ForeColor = Color.Green;
            }
            else if (finalDiff < 0)
            {
                lblStatus.Text = "عجز بمبلغ: " + Math.Abs(finalDiff).ToString("N2");
                lblStatus.ForeColor = Color.Red;
            }
            else
            {
                lblStatus.Text = "زيادة بمبلغ: " + finalDiff.ToString("N2");
                lblStatus.ForeColor = Color.Blue;
            }
        }
        //private void CalculateTotals()
        //{
        //    // 1. حساب الكاش الفعلي
        //    decimal cashTotal = (txt200.Value * 200) + (txt100.Value * 100) + (txt50.Value * 50) +
        //                        (txt20.Value * 20) + (txt10.Value * 10) + (txt5.Value * 5) + (txt1.Value * 1);
        //    lblTotal.Text = cashTotal.ToString("N2");

        //    // 2. حساب المدفوعات الرقمية (الفيزا/انستا)
        //    decimal digitalTotal = 0;
        //    foreach (DataGridViewRow row in dgvPayments.Rows)
        //    {
        //        if (row.IsNewRow) continue;
        //        // تأكد أن المبلغ في العمود رقم 3 (الرابع)
        //        digitalTotal += Convert.ToDecimal(row.Cells[3].Value);
        //    }
        //    lblDigitalTotal.Text = digitalTotal.ToString("N2");

        //    // 3. حساب المصروفات
        //    decimal expensesTotal = 0;
        //    foreach (DataGridViewRow row in dgvExpenses.Rows)
        //    {
        //        if (row.IsNewRow) continue;
        //        // بناءً على تصميمك (image_6053df): المبلغ هو العمود رقم 2 (الثالث)
        //        expensesTotal += Convert.ToDecimal(row.Cells[2].Value);
        //    }
        //    lblExpensesTotal.Text = expensesTotal.ToString("N2");

        //    // 4. التحديثات النهائية
        //    lblTotalDisplay.Text = (cashTotal + digitalTotal).ToString("N2");
        //    decimal actualTotal = cashTotal + digitalTotal + expensesTotal;
        //    decimal finalDiff = actualTotal - numSystemAmount.Value;

        //    lblFinalDiff.Text = finalDiff.ToString("N2");
        //    lblStatus.Text = finalDiff == 0 ? "تمام" : (finalDiff < 0 ? "عجز" : "زيادة");
        //    lblStatus.ForeColor = finalDiff == 0 ? Color.Green : (finalDiff < 0 ? Color.Red : Color.Blue);
        //}
        private void ClearForm()
        {
            // 1. جلب الـ ID الجديد (ID الوردية اللي عليها الدور)
            DataTable dt = settlement.GET_LAST_SHIFT_ID();
            numShiftID.Value = (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                               ? Convert.ToInt32(dt.Rows[0][0]) + 1 : 1;

            // 2. تصفير كل شيء (جداول، فئات، مبالغ)
            dgvExpenses.Rows.Clear();
            dgvPayments.Rows.Clear();
            Row_Exp = 1; Row_Paym = 1;
            txt200.Value = txt100.Value = txt50.Value = txt20.Value = 0;
            txt10.Value = txt5.Value = txt1.Value = 0;
            numSystemAmount.Value = 0;
            numSearchID.Value = 0;

            // 3. (مهم) إعادة التاريخ لتاريخ اللحظة الحالية
            // عشان الموظف يبدأ وردية جديدة وهو "عارف" إنه على تاريخ النهارده
            date_P.Value = DateTime.Now;

            // 4. تحديث الواجهة وتصفير الليبلات
            CalculateTotals();
            cmbCashier.Focus();
        }

        private void txt200_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotals();

        }

        private void FRM_Add_Settlement_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. تعبئة البيانات الأساسية (الأجهزة)
                cmbDevices.DataSource = settlement.GET_ALL_DEVICES();
                cmbDevices.DisplayMember = "DeviceName";
                cmbDevices.ValueMember = "DeviceID";

                // 2. ضبط القائمة المنسدلة للوردية (صباحي/مسائي)
                if (cmbShift.Items.Count > 0) cmbShift.SelectedIndex = 0;

                // 3. استدعاء ClearForm (الجوكر بتاعنا)
                // دي هتعمل 3 حاجات: هتجيب آخر ID + 1، وتصفر الجداول، وتصفر المبالغ
                ClearForm();

                // 4. ضبط التاريخ على تاريخ اليوم لحظة فتح الشاشة
                date_P.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء تحميل البيانات: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTotal_TextChanged(object sender, EventArgs e)
        {

          //  lblTotalDisplay.Text = Convert.ToDecimal(lblTotal.Text) + Convert.ToDecimal(lblDigitalTotal.Text) + "";

        }

        private void lblExpensesTotal_TextChanged(object sender, EventArgs e)
        {
           // lblTotalDisplay.Text = Convert.ToDecimal(lblTotal.Text) + Convert.ToDecimal(lblDigitalTotal.Text) + "";
        }
        private void btnSearchShift_Click(object sender, EventArgs e)
        {
            try
            {
                decimal sID = numSearchID.Value;
                if (sID <= 0) return;

                // 1. جلب البيانات الأساسية (استدعاء SP_GET_SETTLEMENT_BY_ID)
                DataTable dtMain = settlement.GetSettlementByID((int)sID);

                if (dtMain.Rows.Count > 0)
                {
                    numSearchID.BackColor = Color.White;

                    DataRow dr = dtMain.Rows[0];
                    numShiftID.Value = Convert.ToDecimal(dr["ShiftID"]);
                    cmbCashier.SelectedValue = dr["CashierID"];
                    cmbDevices.SelectedValue = dr["DeviceID"];
                    cmbShift.Text = dr["ShiftType"].ToString();
                    numSystemAmount.Value = Convert.ToDecimal(dr["SystemAmount"]);

                    // 2. فئات النقدية (استدعاء SP_GET_CASH_DENOMINATIONS_BY_ID)
                    DataTable dtCash = settlement.GetCashDenominations(sID);
                    if (dtCash.Rows.Count > 0)
                    {
                        DataRow drCash = dtCash.Rows[0];
                        txt200.Value = Convert.ToInt32(drCash["F200"]);
                        txt100.Value = Convert.ToInt32(drCash["F100"]);
                        txt50.Value = Convert.ToInt32(drCash["F50"]);
                        txt20.Value = Convert.ToInt32(drCash["F20"]);
                        txt10.Value = Convert.ToInt32(drCash["F10"]);
                        txt5.Value = Convert.ToInt32(drCash["F5"]);
                        txt1.Value = Convert.ToInt32(drCash["F1"]);
                    }

                    // 3. المدفوعات (استدعاء الإجراء الجديد SP_GET_SETTLEMENT_PAYMENTS_FOR_EDIT)
                    dgvPayments.Rows.Clear();
                    // ملاحظة: استدعاء الدالة الجديدة التي أضفناها للكلاس لضمان وجود MethodName
                    DataTable dtPay = settlement.GetPaymentsForEdit(sID);
                    foreach (DataRow row in dtPay.Rows)
                    {
                        // الآن MethodName مضمونة الوجود بفضل الـ JOIN في SQL
                        dgvPayments.Rows.Add(row["MethodID"], row["DetailID"], row["MethodName"], row["Amount"]);
                    }

                    // 4. المصروفات (استدعاء SP_GET_EXPENSES_BY_ID)
                    dgvExpenses.Rows.Clear();
                    DataTable dtExp = settlement.GetExpensesByID((int)sID);
                    foreach (DataRow row in dtExp.Rows)
                    {
                        dgvExpenses.Rows.Add(row["ExpenseID"], row["Reason"], row["Amount"]);
                    }

                    CalculateTotals();
                    MessageBox.Show("تم استرجاع البيانات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    numSearchID.BackColor = Color.IndianRed;
                    MessageBox.Show($"عفواً، رقم الوردية ({sID}) غير مسجل في النظام.", "بيانات غير موجودة", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    ClearForm();
                    numSearchID.Focus();
                    numSearchID.Select(0, numSearchID.Value.ToString().Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ تقني: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void btnSearchShift_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        decimal sID = numSearchID.Value;
        //        if (sID <= 0) return;

        //        // 1. جلب البيانات الأساسية (Shift_Settlements)
        //        DataTable dtMain = settlement.GetSettlementByID((int)sID);

        //        if (dtMain.Rows.Count > 0)
        //        {
        //            // --- في حالة النجاح ---
        //            numSearchID.BackColor = Color.White; // إعادة اللون للطبيعي

        //            DataRow dr = dtMain.Rows[0];
        //            numShiftID.Value = Convert.ToDecimal(dr["ShiftID"]);
        //            cmbCashier.SelectedValue = dr["CashierID"];
        //            cmbDevices.SelectedValue = dr["DeviceID"];
        //            cmbShift.Text = dr["ShiftType"].ToString();
        //            numSystemAmount.Value = Convert.ToDecimal(dr["SystemAmount"]);

        //            // 2. فئات النقدية
        //            DataTable dtCash = settlement.GetCashDenominations(sID);
        //            if (dtCash.Rows.Count > 0)
        //            {
        //                DataRow drCash = dtCash.Rows[0];
        //                txt200.Value = Convert.ToInt32(drCash["F200"]);
        //                txt100.Value = Convert.ToInt32(drCash["F100"]);
        //                txt50.Value = Convert.ToInt32(drCash["F50"]);
        //                txt20.Value = Convert.ToInt32(drCash["F20"]);
        //                txt10.Value = Convert.ToInt32(drCash["F10"]);
        //                txt5.Value = Convert.ToInt32(drCash["F5"]);
        //                txt1.Value = Convert.ToInt32(drCash["F1"]);
        //            }

        //            // 3. المدفوعات (مع جلب الاسم)
        //            dgvPayments.Rows.Clear();
        //            DataTable dtPay = settlement.GetPaymentsByID((int)sID);
        //            foreach (DataRow row in dtPay.Rows)
        //            {
        //                dgvPayments.Rows.Add(row["MethodID"], row["DetailID"], row["MethodName"], row["Amount"]);
        //            }

        //            // 4. المصروفات
        //            dgvExpenses.Rows.Clear();
        //            DataTable dtExp = settlement.GetExpensesByID((int)sID);
        //            foreach (DataRow row in dtExp.Rows)
        //            {
        //                dgvExpenses.Rows.Add(row["ExpenseID"], row["Reason"], row["Amount"]);
        //            }

        //            CalculateTotals();
        //            MessageBox.Show("تم استرجاع البيانات بنجاح", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            // --- في حالة عدم وجود بيانات (التحذير) ---
        //            numSearchID.BackColor = Color.IndianRed; // تغيير اللون للأحمر الخفيف (تحذير)
        //            MessageBox.Show($"عفواً، رقم الوردية ({sID}) غير مسجل في النظام.", "بيانات غير موجودة", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //            ClearForm(); // تنظيف الشاشة من أي أرقام قديمة
        //            numSearchID.Focus();
        //            numSearchID.Select(0, numSearchID.Value.ToString().Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("خطأ تقني: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void numSystemAmount_ValueChanged(object sender, EventArgs e)
        {
            // استدعاء دالة الحسابات فوراً عند تغيير الكمية
            CalculateTotals();
        }
        private void dgvExpenses_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            // 1. التأكد أن المستخدم ضغط على زر الحذف وأن هناك صفاً واحداً على الأقل مختاراً
            if (e.KeyCode == Keys.Delete && dgv.SelectedRows.Count > 0)
            {
                // إيقاف المعالجة الافتراضية للمفتاح لمنع الحذف المزدوج أو العشوائي
                e.Handled = true;

                // 2. رسالة تأكيد لحماية البيانات من الحذف الخطأ
                if (MessageBox.Show("هل تريد حذف السجلات المحددة؟", "تأكيد حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgv.SelectedRows)
                    {
                        // 3. منع حذف الصف الفارغ المخصص للإضافة (IsNewRow)
                        if (!row.IsNewRow)
                        {
                            dgv.Rows.Remove(row);
                        }
                    }

                    // 4. تحديث إجمالي الحسابات فوراً بعد الحذف لتظهر النتائج صحيحة في المراية
                    CalculateTotals();
                }
            }
        }
        private void numSearchID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnSearchShift.PerformClick();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // استدعاء دالة الاختفاء التدريجي ثم الإغلاق
            HelperMethods.FormFadeOut(this);
        }
    }
}