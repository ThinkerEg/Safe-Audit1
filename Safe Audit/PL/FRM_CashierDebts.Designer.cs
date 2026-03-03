namespace Safe_Audit.PL
{
    partial class FRM_CashierDebts
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblNetDebt = new System.Windows.Forms.Label();
            this.lblTotalPaid = new System.Windows.Forms.Label();
            this.lblTotalShortage = new System.Windows.Forms.Label();
            this.cmbCashiers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.btnPayShortcut = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 45);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(250, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(270, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "كشف حساب مديونيات الكاشير";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(10, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlSummary.Controls.Add(this.lblNetDebt);
            this.pnlSummary.Controls.Add(this.lblTotalPaid);
            this.pnlSummary.Controls.Add(this.lblTotalShortage);
            this.pnlSummary.Location = new System.Drawing.Point(20, 60);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(760, 80);
            this.pnlSummary.TabIndex = 2;
            // 
            // lblNetDebt
            // 
            this.lblNetDebt.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblNetDebt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblNetDebt.Location = new System.Drawing.Point(20, 25);
            this.lblNetDebt.Name = "lblNetDebt";
            this.lblNetDebt.Size = new System.Drawing.Size(250, 30);
            this.lblNetDebt.TabIndex = 0;
            this.lblNetDebt.Text = "صافي المديونية: 0.00";
            this.lblNetDebt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalPaid
            // 
            this.lblTotalPaid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblTotalPaid.Location = new System.Drawing.Point(300, 25);
            this.lblTotalPaid.Name = "lblTotalPaid";
            this.lblTotalPaid.Size = new System.Drawing.Size(200, 30);
            this.lblTotalPaid.TabIndex = 1;
            this.lblTotalPaid.Text = "إجمالي المسدد: 0.00";
            this.lblTotalPaid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalShortage
            // 
            this.lblTotalShortage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalShortage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(84)))), ((int)(((byte)(0)))));
            this.lblTotalShortage.Location = new System.Drawing.Point(540, 25);
            this.lblTotalShortage.Name = "lblTotalShortage";
            this.lblTotalShortage.Size = new System.Drawing.Size(200, 30);
            this.lblTotalShortage.TabIndex = 2;
            this.lblTotalShortage.Text = "إجمالي العجز: 0.00";
            this.lblTotalShortage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCashiers
            // 
            this.cmbCashiers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCashiers.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbCashiers.FormattingEnabled = true;
            this.cmbCashiers.Location = new System.Drawing.Point(450, 152);
            this.cmbCashiers.Name = "cmbCashiers";
            this.cmbCashiers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbCashiers.Size = new System.Drawing.Size(230, 28);
            this.cmbCashiers.TabIndex = 3;
            this.cmbCashiers.SelectedIndexChanged += new System.EventHandler(this.cmbCashiers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(685, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = ":اختر الكاشير";
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Location = new System.Drawing.Point(20, 195);
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvTransactions.Size = new System.Drawing.Size(760, 280);
            this.dgvTransactions.TabIndex = 1;
            // 
            // btnPayShortcut
            // 
            this.btnPayShortcut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnPayShortcut.FlatAppearance.BorderSize = 0;
            this.btnPayShortcut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayShortcut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayShortcut.ForeColor = System.Drawing.Color.White;
            this.btnPayShortcut.Location = new System.Drawing.Point(20, 485);
            this.btnPayShortcut.Name = "btnPayShortcut";
            this.btnPayShortcut.Size = new System.Drawing.Size(180, 40);
            this.btnPayShortcut.TabIndex = 0;
            this.btnPayShortcut.Text = "إضافة سداد جديد (F2)";
            this.btnPayShortcut.UseVisualStyleBackColor = false;
            this.btnPayShortcut.Click += new System.EventHandler(this.btnPayShortcut_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(210, 485);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "تحديث البيانات";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FRM_CashierDebts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 540);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPayShortcut);
            this.Controls.Add(this.dgvTransactions);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.cmbCashiers);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FRM_CashierDebts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FRM_CashierDebts_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalShortage;
        private System.Windows.Forms.Label lblTotalPaid;
        private System.Windows.Forms.Label lblNetDebt;
        private System.Windows.Forms.ComboBox cmbCashiers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.Button btnPayShortcut;
        private System.Windows.Forms.Button btnRefresh;
    }
}