namespace Safe_Audit.PL
{
    partial class FRM_Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btn_CASHIERS = new System.Windows.Forms.Button();
            this.btn_CashierDebts = new System.Windows.Forms.Button();
            this.btn_FinancialMovements = new System.Windows.Forms.Button();
            this.btn_EditLogs = new System.Windows.Forms.Button();
            this.btn_Search_Settlements = new System.Windows.Forms.Button();
            this.btn_ExpensesList = new System.Windows.Forms.Button();
            this.btnSettlement = new System.Windows.Forms.Button();
            this.btnTransfers = new System.Windows.Forms.Button();
            this.btnStatement = new System.Windows.Forms.Button();
            this.btnAccounts = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCash = new System.Windows.Forms.Panel();
            this.lblCashVal = new System.Windows.Forms.Label();
            this.lblCashTitle = new System.Windows.Forms.Label();
            this.pnlDigital = new System.Windows.Forms.Panel();
            this.lblDigitalVal = new System.Windows.Forms.Label();
            this.lblDigitalTitle = new System.Windows.Forms.Label();
            this.pnlExpenses = new System.Windows.Forms.Panel();
            this.lblExpVal = new System.Windows.Forms.Label();
            this.lblExpTitle = new System.Windows.Forms.Label();
            this.dgvBalances = new System.Windows.Forms.DataGridView();
            this.btn_DEVICES = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.flowLayoutCards.SuspendLayout();
            this.pnlCash.SuspendLayout();
            this.pnlDigital.SuspendLayout();
            this.pnlExpenses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlSidebar.Controls.Add(this.btn_DEVICES);
            this.pnlSidebar.Controls.Add(this.btn_CASHIERS);
            this.pnlSidebar.Controls.Add(this.btn_CashierDebts);
            this.pnlSidebar.Controls.Add(this.btn_FinancialMovements);
            this.pnlSidebar.Controls.Add(this.btn_EditLogs);
            this.pnlSidebar.Controls.Add(this.btn_Search_Settlements);
            this.pnlSidebar.Controls.Add(this.btn_ExpensesList);
            this.pnlSidebar.Controls.Add(this.btnSettlement);
            this.pnlSidebar.Controls.Add(this.btnTransfers);
            this.pnlSidebar.Controls.Add(this.btnStatement);
            this.pnlSidebar.Controls.Add(this.btnAccounts);
            this.pnlSidebar.Controls.Add(this.btnExit);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(957, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 728);
            this.pnlSidebar.TabIndex = 1;
            // 
            // btn_CASHIERS
            // 
            this.btn_CASHIERS.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_CASHIERS.FlatAppearance.BorderSize = 0;
            this.btn_CASHIERS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CASHIERS.ForeColor = System.Drawing.Color.White;
            this.btn_CASHIERS.Location = new System.Drawing.Point(0, 360);
            this.btn_CASHIERS.Name = "btn_CASHIERS";
            this.btn_CASHIERS.Size = new System.Drawing.Size(200, 40);
            this.btn_CASHIERS.TabIndex = 10;
            this.btn_CASHIERS.Text = "إدارة بيانات الكاشير";
            this.btn_CASHIERS.Click += new System.EventHandler(this.btn_CASHIERS_Click);
            // 
            // btn_CashierDebts
            // 
            this.btn_CashierDebts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_CashierDebts.FlatAppearance.BorderSize = 0;
            this.btn_CashierDebts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CashierDebts.ForeColor = System.Drawing.Color.White;
            this.btn_CashierDebts.Location = new System.Drawing.Point(0, 320);
            this.btn_CashierDebts.Name = "btn_CashierDebts";
            this.btn_CashierDebts.Size = new System.Drawing.Size(200, 40);
            this.btn_CashierDebts.TabIndex = 9;
            this.btn_CashierDebts.Text = "كشف حساب مديونيات الكاشير";
            this.btn_CashierDebts.Click += new System.EventHandler(this.btn_CashierDebts_Click);
            // 
            // btn_FinancialMovements
            // 
            this.btn_FinancialMovements.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_FinancialMovements.FlatAppearance.BorderSize = 0;
            this.btn_FinancialMovements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FinancialMovements.ForeColor = System.Drawing.Color.White;
            this.btn_FinancialMovements.Location = new System.Drawing.Point(0, 280);
            this.btn_FinancialMovements.Name = "btn_FinancialMovements";
            this.btn_FinancialMovements.Size = new System.Drawing.Size(200, 40);
            this.btn_FinancialMovements.TabIndex = 8;
            this.btn_FinancialMovements.Text = "\"الحركات المالية\" (قبض وصرف)";
            this.btn_FinancialMovements.Click += new System.EventHandler(this.btn_FinancialMovements_Click);
            // 
            // btn_EditLogs
            // 
            this.btn_EditLogs.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_EditLogs.FlatAppearance.BorderSize = 0;
            this.btn_EditLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_EditLogs.ForeColor = System.Drawing.Color.White;
            this.btn_EditLogs.Location = new System.Drawing.Point(0, 240);
            this.btn_EditLogs.Name = "btn_EditLogs";
            this.btn_EditLogs.Size = new System.Drawing.Size(200, 40);
            this.btn_EditLogs.TabIndex = 7;
            this.btn_EditLogs.Text = "تقرير محذوفات التسويات";
            this.btn_EditLogs.Click += new System.EventHandler(this.btn_EditLogs_Click);
            // 
            // btn_Search_Settlements
            // 
            this.btn_Search_Settlements.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Search_Settlements.FlatAppearance.BorderSize = 0;
            this.btn_Search_Settlements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search_Settlements.ForeColor = System.Drawing.Color.White;
            this.btn_Search_Settlements.Location = new System.Drawing.Point(0, 200);
            this.btn_Search_Settlements.Name = "btn_Search_Settlements";
            this.btn_Search_Settlements.Size = new System.Drawing.Size(200, 40);
            this.btn_Search_Settlements.TabIndex = 6;
            this.btn_Search_Settlements.Text = "تقرير التسويات المالية";
            this.btn_Search_Settlements.Click += new System.EventHandler(this.btn_Search_Settlements_Click);
            // 
            // btn_ExpensesList
            // 
            this.btn_ExpensesList.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ExpensesList.FlatAppearance.BorderSize = 0;
            this.btn_ExpensesList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ExpensesList.ForeColor = System.Drawing.Color.White;
            this.btn_ExpensesList.Location = new System.Drawing.Point(0, 160);
            this.btn_ExpensesList.Name = "btn_ExpensesList";
            this.btn_ExpensesList.Size = new System.Drawing.Size(200, 40);
            this.btn_ExpensesList.TabIndex = 5;
            this.btn_ExpensesList.Text = "إدارة المصروفات";
            this.btn_ExpensesList.Click += new System.EventHandler(this.btn_ExpensesList_Click);
            // 
            // btnSettlement
            // 
            this.btnSettlement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSettlement.FlatAppearance.BorderSize = 0;
            this.btnSettlement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettlement.ForeColor = System.Drawing.Color.White;
            this.btnSettlement.Location = new System.Drawing.Point(0, 120);
            this.btnSettlement.Name = "btnSettlement";
            this.btnSettlement.Size = new System.Drawing.Size(200, 40);
            this.btnSettlement.TabIndex = 4;
            this.btnSettlement.Text = "إضافة تسوية مالية";
            this.btnSettlement.Click += new System.EventHandler(this.btnSettlement_Click);
            // 
            // btnTransfers
            // 
            this.btnTransfers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransfers.FlatAppearance.BorderSize = 0;
            this.btnTransfers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfers.ForeColor = System.Drawing.Color.White;
            this.btnTransfers.Location = new System.Drawing.Point(0, 80);
            this.btnTransfers.Name = "btnTransfers";
            this.btnTransfers.Size = new System.Drawing.Size(200, 40);
            this.btnTransfers.TabIndex = 1;
            this.btnTransfers.Text = "التحويلات المالية";
            this.btnTransfers.Click += new System.EventHandler(this.btnTransfers_Click);
            // 
            // btnStatement
            // 
            this.btnStatement.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStatement.FlatAppearance.BorderSize = 0;
            this.btnStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatement.ForeColor = System.Drawing.Color.White;
            this.btnStatement.Location = new System.Drawing.Point(0, 40);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(200, 40);
            this.btnStatement.TabIndex = 2;
            this.btnStatement.Text = "كشف الحساب";
            this.btnStatement.Click += new System.EventHandler(this.btnAccountStatement_Click);
            // 
            // btnAccounts
            // 
            this.btnAccounts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAccounts.FlatAppearance.BorderSize = 0;
            this.btnAccounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccounts.ForeColor = System.Drawing.Color.White;
            this.btnAccounts.Location = new System.Drawing.Point(0, 0);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Size = new System.Drawing.Size(200, 40);
            this.btnAccounts.TabIndex = 3;
            this.btnAccounts.Text = "إدارة الحسابات";
            this.btnAccounts.Click += new System.EventHandler(this.btnAccounts_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Salmon;
            this.btnExit.Location = new System.Drawing.Point(0, 668);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 60);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "خروج";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(957, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(400, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(181, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "نظام الخزينة والتدقيق";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 60);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(957, 60);
            this.pnlTop.TabIndex = 3;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(20, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "تحديث البيانات";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(715, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "ملخص الأرصدة الحالية";
            // 
            // flowLayoutCards
            // 
            this.flowLayoutCards.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutCards.Controls.Add(this.pnlCash);
            this.flowLayoutCards.Controls.Add(this.pnlDigital);
            this.flowLayoutCards.Controls.Add(this.pnlExpenses);
            this.flowLayoutCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutCards.Location = new System.Drawing.Point(0, 120);
            this.flowLayoutCards.Name = "flowLayoutCards";
            this.flowLayoutCards.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutCards.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutCards.Size = new System.Drawing.Size(957, 160);
            this.flowLayoutCards.TabIndex = 4;
            // 
            // pnlCash
            // 
            this.pnlCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(166)))), ((int)(((byte)(154)))));
            this.pnlCash.Controls.Add(this.pictureBox1);
            this.pnlCash.Controls.Add(this.lblCashVal);
            this.pnlCash.Controls.Add(this.lblCashTitle);
            this.pnlCash.Location = new System.Drawing.Point(634, 13);
            this.pnlCash.Name = "pnlCash";
            this.pnlCash.Size = new System.Drawing.Size(300, 120);
            this.pnlCash.TabIndex = 0;
            // 
            // lblCashVal
            // 
            this.lblCashVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblCashVal.ForeColor = System.Drawing.Color.White;
            this.lblCashVal.Location = new System.Drawing.Point(0, 45);
            this.lblCashVal.Name = "lblCashVal";
            this.lblCashVal.Size = new System.Drawing.Size(300, 75);
            this.lblCashVal.TabIndex = 0;
            this.lblCashVal.Text = "0.00";
            this.lblCashVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCashTitle
            // 
            this.lblCashTitle.AutoSize = true;
            this.lblCashTitle.ForeColor = System.Drawing.Color.White;
            this.lblCashTitle.Location = new System.Drawing.Point(200, 15);
            this.lblCashTitle.Name = "lblCashTitle";
            this.lblCashTitle.Size = new System.Drawing.Size(69, 13);
            this.lblCashTitle.TabIndex = 1;
            this.lblCashTitle.Text = "إجمالي الكاش";
            // 
            // pnlDigital
            // 
            this.pnlDigital.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlDigital.Controls.Add(this.pictureBox2);
            this.pnlDigital.Controls.Add(this.lblDigitalVal);
            this.pnlDigital.Controls.Add(this.lblDigitalTitle);
            this.pnlDigital.Location = new System.Drawing.Point(328, 13);
            this.pnlDigital.Name = "pnlDigital";
            this.pnlDigital.Size = new System.Drawing.Size(300, 120);
            this.pnlDigital.TabIndex = 1;
            // 
            // lblDigitalVal
            // 
            this.lblDigitalVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblDigitalVal.ForeColor = System.Drawing.Color.White;
            this.lblDigitalVal.Location = new System.Drawing.Point(0, 45);
            this.lblDigitalVal.Name = "lblDigitalVal";
            this.lblDigitalVal.Size = new System.Drawing.Size(300, 75);
            this.lblDigitalVal.TabIndex = 0;
            this.lblDigitalVal.Text = "0.00";
            this.lblDigitalVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDigitalTitle
            // 
            this.lblDigitalTitle.AutoSize = true;
            this.lblDigitalTitle.ForeColor = System.Drawing.Color.White;
            this.lblDigitalTitle.Location = new System.Drawing.Point(180, 15);
            this.lblDigitalTitle.Name = "lblDigitalTitle";
            this.lblDigitalTitle.Size = new System.Drawing.Size(103, 13);
            this.lblDigitalTitle.TabIndex = 1;
            this.lblDigitalTitle.Text = "إجمالي الفيزا/الرقمي";
            // 
            // pnlExpenses
            // 
            this.pnlExpenses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this.pnlExpenses.Controls.Add(this.pictureBox3);
            this.pnlExpenses.Controls.Add(this.lblExpVal);
            this.pnlExpenses.Controls.Add(this.lblExpTitle);
            this.pnlExpenses.Location = new System.Drawing.Point(22, 13);
            this.pnlExpenses.Name = "pnlExpenses";
            this.pnlExpenses.Size = new System.Drawing.Size(300, 120);
            this.pnlExpenses.TabIndex = 2;
            // 
            // lblExpVal
            // 
            this.lblExpVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblExpVal.ForeColor = System.Drawing.Color.White;
            this.lblExpVal.Location = new System.Drawing.Point(0, 45);
            this.lblExpVal.Name = "lblExpVal";
            this.lblExpVal.Size = new System.Drawing.Size(300, 75);
            this.lblExpVal.TabIndex = 0;
            this.lblExpVal.Text = "0.00";
            this.lblExpVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExpTitle
            // 
            this.lblExpTitle.AutoSize = true;
            this.lblExpTitle.ForeColor = System.Drawing.Color.White;
            this.lblExpTitle.Location = new System.Drawing.Point(149, 15);
            this.lblExpTitle.Name = "lblExpTitle";
            this.lblExpTitle.Size = new System.Drawing.Size(116, 13);
            this.lblExpTitle.TabIndex = 1;
            this.lblExpTitle.Text = "إجمالي المصروفات اليوم";
            // 
            // dgvBalances
            // 
            this.dgvBalances.AllowUserToAddRows = false;
            this.dgvBalances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBalances.BackgroundColor = System.Drawing.Color.White;
            this.dgvBalances.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBalances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBalances.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBalances.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBalances.Location = new System.Drawing.Point(0, 280);
            this.dgvBalances.Name = "dgvBalances";
            this.dgvBalances.ReadOnly = true;
            this.dgvBalances.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvBalances.RowHeadersVisible = false;
            this.dgvBalances.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBalances.Size = new System.Drawing.Size(957, 448);
            this.dgvBalances.TabIndex = 5;
            // 
            // btn_DEVICES
            // 
            this.btn_DEVICES.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_DEVICES.FlatAppearance.BorderSize = 0;
            this.btn_DEVICES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DEVICES.ForeColor = System.Drawing.Color.White;
            this.btn_DEVICES.Location = new System.Drawing.Point(0, 400);
            this.btn_DEVICES.Name = "btn_DEVICES";
            this.btn_DEVICES.Size = new System.Drawing.Size(200, 40);
            this.btn_DEVICES.TabIndex = 11;
            this.btn_DEVICES.Text = "إدارة بيانات الأجهزة";
            this.btn_DEVICES.Click += new System.EventHandler(this.btn_DEVICES_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Safe_Audit.Properties.Resources.إجمالي_الكاش;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Safe_Audit.Properties.Resources.إجمالي_الفيزا;
            this.pictureBox2.Location = new System.Drawing.Point(3, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Safe_Audit.Properties.Resources.إجمالي_المصروفات_اليوم;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(100, 50);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // FRM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 728);
            this.Controls.Add(this.dgvBalances);
            this.Controls.Add(this.flowLayoutCards);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FRM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FRM_Main_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.flowLayoutCards.ResumeLayout(false);
            this.pnlCash.ResumeLayout(false);
            this.pnlCash.PerformLayout();
            this.pnlDigital.ResumeLayout(false);
            this.pnlDigital.PerformLayout();
            this.pnlExpenses.ResumeLayout(false);
            this.pnlExpenses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBalances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnAccounts;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.Button btnTransfers;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSettlement;
        private System.Windows.Forms.Button btn_ExpensesList;
        private System.Windows.Forms.Button btn_Search_Settlements;
        private System.Windows.Forms.Button btn_EditLogs;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutCards;
        private System.Windows.Forms.Panel pnlCash;
        private System.Windows.Forms.Label lblCashVal;
        private System.Windows.Forms.Label lblCashTitle;
        private System.Windows.Forms.Panel pnlDigital;
        private System.Windows.Forms.Label lblDigitalVal;
        private System.Windows.Forms.Label lblDigitalTitle;
        private System.Windows.Forms.Panel pnlExpenses;
        private System.Windows.Forms.Label lblExpVal;
        private System.Windows.Forms.Label lblExpTitle;
        private System.Windows.Forms.DataGridView dgvBalances;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btn_FinancialMovements;
        private System.Windows.Forms.Button btn_CashierDebts;
        private System.Windows.Forms.Button btn_CASHIERS;
        private System.Windows.Forms.Button btn_DEVICES;
    }
}