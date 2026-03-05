namespace Safe_Audit.PL
{
    partial class FRM_ColorSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlColors = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnPickHeader = new System.Windows.Forms.Button();
            this.btnPickPrimary = new System.Windows.Forms.Button();
            this.btnPickBack = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvSample = new System.Windows.Forms.DataGridView();
            this.col1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSample = new System.Windows.Forms.Button();
            this.pnlHeaderPreview = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlColors.SuspendLayout();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSample)).BeginInit();
            this.pnlHeaderPreview.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.btnReset);
            this.pnlColors.Controls.Add(this.btnPickHeader);
            this.pnlColors.Controls.Add(this.btnPickPrimary);
            this.pnlColors.Controls.Add(this.btnPickBack);
            this.pnlColors.Controls.Add(this.btnSave);
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlColors.Location = new System.Drawing.Point(380, 0);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Padding = new System.Windows.Forms.Padding(10);
            this.pnlColors.Size = new System.Drawing.Size(220, 400);
            this.pnlColors.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReset.Location = new System.Drawing.Point(10, 145);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(200, 45);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "اعادة تعيين الوضع الافتراضي";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnPickHeader
            // 
            this.btnPickHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPickHeader.Location = new System.Drawing.Point(10, 100);
            this.btnPickHeader.Name = "btnPickHeader";
            this.btnPickHeader.Size = new System.Drawing.Size(200, 45);
            this.btnPickHeader.TabIndex = 0;
            this.btnPickHeader.Text = "لون الهيدر والشرائط";
            this.btnPickHeader.Click += new System.EventHandler(this.btnPickHeader_Click);
            // 
            // btnPickPrimary
            // 
            this.btnPickPrimary.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPickPrimary.Location = new System.Drawing.Point(10, 55);
            this.btnPickPrimary.Name = "btnPickPrimary";
            this.btnPickPrimary.Size = new System.Drawing.Size(200, 45);
            this.btnPickPrimary.TabIndex = 1;
            this.btnPickPrimary.Text = "لون الأزرار الرئيسية";
            this.btnPickPrimary.Click += new System.EventHandler(this.btnPickPrimary_Click);
            // 
            // btnPickBack
            // 
            this.btnPickBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPickBack.Location = new System.Drawing.Point(10, 10);
            this.btnPickBack.Name = "btnPickBack";
            this.btnPickBack.Size = new System.Drawing.Size(200, 45);
            this.btnPickBack.TabIndex = 2;
            this.btnPickBack.Text = "لون خلفية البرنامج";
            this.btnPickBack.Click += new System.EventHandler(this.btnPickBack_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(10, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 50);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "حفظ وإعتماد الألوان";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.button3);
            this.grpPreview.Controls.Add(this.button2);
            this.grpPreview.Controls.Add(this.button1);
            this.grpPreview.Controls.Add(this.dgvSample);
            this.grpPreview.Controls.Add(this.btnSample);
            this.grpPreview.Controls.Add(this.pnlHeaderPreview);
            this.grpPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPreview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpPreview.Location = new System.Drawing.Point(0, 0);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(380, 400);
            this.grpPreview.TabIndex = 0;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = " معاينة التصميم (Live Preview) ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(347, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "الأزرار التنبيهات";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(347, 35);
            this.button2.TabIndex = 4;
            this.button2.Text = "الأزرار التحذيرات";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(347, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "الأزرار العمليات الناجحة";
            // 
            // dgvSample
            // 
            this.dgvSample.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSample.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvSample.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSample.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col1,
            this.col2});
            this.dgvSample.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvSample.Location = new System.Drawing.Point(3, 61);
            this.dgvSample.Name = "dgvSample";
            this.dgvSample.RowHeadersWidth = 10;
            this.dgvSample.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSample.Size = new System.Drawing.Size(374, 150);
            this.dgvSample.TabIndex = 0;
            // 
            // col1
            // 
            this.col1.HeaderText = "العمود الأول";
            this.col1.Name = "col1";
            // 
            // col2
            // 
            this.col2.HeaderText = "العمود الثاني";
            this.col2.Name = "col2";
            // 
            // btnSample
            // 
            this.btnSample.Location = new System.Drawing.Point(21, 226);
            this.btnSample.Name = "btnSample";
            this.btnSample.Size = new System.Drawing.Size(347, 35);
            this.btnSample.TabIndex = 1;
            this.btnSample.Text = "الأزرار الرئيسية";
            // 
            // pnlHeaderPreview
            // 
            this.pnlHeaderPreview.Controls.Add(this.btnClose);
            this.pnlHeaderPreview.Controls.Add(this.lblHeaderTitle);
            this.pnlHeaderPreview.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderPreview.Location = new System.Drawing.Point(3, 21);
            this.pnlHeaderPreview.Name = "pnlHeaderPreview";
            this.pnlHeaderPreview.Size = new System.Drawing.Size(374, 40);
            this.pnlHeaderPreview.TabIndex = 2;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(374, 40);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "شريط العنوان / الهيدر";
            this.lblHeaderTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colorDialog1
            // 
            this.colorDialog1.ShowHelp = true;
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(3, -5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(42, 42);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FRM_ColorSettings
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.pnlColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FRM_ColorSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إعدادات الألوان والمظهر";
            this.Load += new System.EventHandler(this.FRM_ColorSettings_Load);
            this.pnlColors.ResumeLayout(false);
            this.grpPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSample)).EndInit();
            this.pnlHeaderPreview.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        // تعريف الأدوات (Controls)
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.Button btnPickHeader;
        private System.Windows.Forms.Button btnPickPrimary;
        private System.Windows.Forms.Button btnPickBack;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.Panel pnlHeaderPreview;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Button btnSample;
        private System.Windows.Forms.DataGridView dgvSample;
        private System.Windows.Forms.DataGridViewTextBoxColumn col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn col2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnClose;
    }
}