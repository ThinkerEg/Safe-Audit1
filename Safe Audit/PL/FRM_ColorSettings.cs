using System;
using System.Drawing;
using System.Windows.Forms;
using Safe_Audit.BL; // لاستدعاء الهيلبر

namespace Safe_Audit.PL
{
    public partial class FRM_ColorSettings : Form
    {
        // متغيرات مؤقتة لتخزين الألوان المختارة قبل الحفظ النهائي
        private Color tempHeader = HelperMethods.HeaderColor;
        private Color tempPrimary = HelperMethods.PrimaryColor;
        private Color tempBack = HelperMethods.BackColor;

        public FRM_ColorSettings()
        {
            InitializeComponent();

            // تطبيق التنسيق الأولي فور فتح الشاشة
            ApplyInitialStyles();
            RefreshPreview();
        }

        private void ApplyInitialStyles()
        {
            // تنسيق الأزرار الجانبية باستخدام الهيلبر
            HelperMethods.StyleButtons(pnlColors);
            this.BackColor = HelperMethods.BackColor;
            grpPreview.BackColor = Color.White;
        }

        // --- أحداث اختيار الألوان (تحديث عند الضغط على OK فقط) ---
        private void btnPickHeader_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = tempHeader;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tempHeader = colorDialog1.Color;
                RefreshPreview();
            }
        }

        private void btnPickPrimary_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = tempPrimary;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tempPrimary = colorDialog1.Color;
                RefreshPreview();
            }
        }

        private void btnPickBack_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = tempBack;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                tempBack = colorDialog1.Color;
                RefreshPreview();
            }
        }

        // --- دالة التحديث في المعاينة ---
        private void RefreshPreview()
        {
            // 1. تحديث الهيدر في المعاينة (لون ذكي للخط)
            pnlHeaderPreview.BackColor = tempHeader;
            lblHeaderTitle.ForeColor = HelperMethods.GetContrastColor(tempHeader);

            // 2. تحديث الزر التجريبي الأساسي (لون ذكي للخط)
            btnSample.BackColor = tempPrimary;
            btnSample.ForeColor = HelperMethods.GetContrastColor(tempPrimary);
            HelperMethods.ApplyModernCorners(btnSample, 8);

            // 3. تحديث جدول المعاينة
            dgvSample.EnableHeadersVisualStyles = false;
            dgvSample.ColumnHeadersDefaultCellStyle.BackColor = tempHeader;
            dgvSample.ColumnHeadersDefaultCellStyle.ForeColor = HelperMethods.GetContrastColor(tempHeader);
            dgvSample.DefaultCellStyle.SelectionBackColor = tempPrimary;
            dgvSample.DefaultCellStyle.SelectionForeColor = HelperMethods.GetContrastColor(tempPrimary);

            // 4. تحديث خلفية الجروب بوكس لتمثيل خلفية البرنامج
            grpPreview.BackColor = tempBack;

            // 5. تحديث الأزرار الإضافية (نجاح، خطر، تنبيه)
            button1.BackColor = HelperMethods.SuccessColor;
            button1.ForeColor = Color.White;
            HelperMethods.ApplyModernCorners(button1, 8);

            button2.BackColor = HelperMethods.DangerColor;
            button2.ForeColor = Color.White;
            HelperMethods.ApplyModernCorners(button2, 8);

            button3.BackColor = HelperMethods.WarningColor;
            button3.ForeColor = Color.White;
            HelperMethods.ApplyModernCorners(button3, 8);
        }

        // --- الحفظ النهائي ---
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. تحديث قيم الهيلبر
                HelperMethods.HeaderColor = tempHeader;
                HelperMethods.PrimaryColor = tempPrimary;
                HelperMethods.BackColor = tempBack;

                // 2. الحفظ الدائم في إعدادات المشروع
                Properties.Settings.Default.HeaderColor = tempHeader;
                Properties.Settings.Default.PrimaryColor = tempPrimary;
                Properties.Settings.Default.BackColor = tempBack;
                Properties.Settings.Default.Save();

                MessageBox.Show("تم حفظ وتطبيق الثيم الجديد بنجاح!", "تحديث المظهر", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد استعادة الإعدادات الأصلية للبرنامج؟", "تأكيد", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                HelperMethods.ResetToDefault();

                tempHeader = HelperMethods.HeaderColor;
                tempPrimary = HelperMethods.PrimaryColor;
                tempBack = HelperMethods.BackColor;

                RefreshPreview();
                MessageBox.Show("تمت إعادة التعيين بنجاح.", "إشعار", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void FRM_ColorSettings_Load(object sender, EventArgs e)
        {
            lblHeaderTitle.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };
        }

        // ملاحظة: تم إزالة Load event و HelpRequest لعدم الحاجة لهما الآن.
    }
}