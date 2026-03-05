using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Safe_Audit.BL
{
    public static class HelperMethods
    {
        ////#region الألوان الأساسية
        ////public static Color HeaderColor = Color.FromArgb(41, 128, 185);
        ////public static Color PrimaryColor = Color.FromArgb(41, 128, 185);
        ////public static Color SuccessColor = Color.FromArgb(39, 174, 96);
        ////public static Color DangerColor = Color.FromArgb(192, 57, 43);
        ////public static Color WarningColor = Color.FromArgb(243, 156, 18);
        ////public static Color BackColor = Color.FromArgb(236, 240, 241);
        ////#endregion
        //#region الألوان الأساسية الجديدة
        //// كحلي غامق وقور جداً للهيدر
        //public static Color HeaderColor = Color.FromArgb(44, 62, 80);

        //// أزرق ملكي للأزرار الرئيسية
        //public static Color PrimaryColor = Color.FromArgb(52, 152, 219);

        //// أخضر زمردي للعمليات الناجحة
        //public static Color SuccessColor = Color.FromArgb(46, 204, 113);

        //// أحمر هادي للتحذيرات
        //public static Color DangerColor = Color.FromArgb(231, 76, 60);

        //// برتقالي شيك للتنبيهات
        //public static Color WarningColor = Color.FromArgb(241, 196, 15);

        //// رمادي "ثلجي" مريح جداً للعين في الخلفية
        //public static Color BackColor = Color.FromArgb(244, 247, 249);
        //#endregion
        public static void ResetToDefault()
        {
            HeaderColor = Color.FromArgb(44, 62, 80);
            PrimaryColor = Color.FromArgb(52, 152, 219);
            BackColor = Color.FromArgb(244, 247, 249);
            Properties.Settings.Default.Save();
        }
        #region الألوان الأساسية الديناميكية
        // نستخدم الـ Getter لجعل المتغير يقرأ من الإعدادات المحفوظة في كل مرة يُستدعى فيها
        public static Color HeaderColor
        {
            get { return Properties.Settings.Default.HeaderColor; }
            set { Properties.Settings.Default.HeaderColor = value; }
        }

        public static Color PrimaryColor
        {
            get { return Properties.Settings.Default.PrimaryColor; }
            set { Properties.Settings.Default.PrimaryColor = value; }
        }

        public static Color BackColor
        {
            get { return Properties.Settings.Default.BackColor; }
            set { Properties.Settings.Default.BackColor = value; }
        }

        // لو اللون فاتح خلي الخط أسود، لو غامق خليه أبيض
        public static Color GetContrastColor(Color backgroundColor)
        {
            // معادلة رياضية لحساب سطوع اللون
            double luminance = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White; // لو اللون فاتح خلي الخط أسود، لو غامق خليه أبيض
        }
        // الألوان الوظيفية (يفضل بقاؤها ثابتة للاتساق أو ربطها بالإعدادات بنفس الطريقة)
        public static Color SuccessColor = Color.FromArgb(46, 204, 113);
        public static Color DangerColor = Color.FromArgb(231, 76, 60);
        public static Color WarningColor = Color.FromArgb(241, 196, 15);
        #endregion
        #region تحريك الفورم
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        public static void MoveForm(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region رسم الحواف الدائرية
        public static void ApplyModernCorners(Control control, int radius)
        {
            if (control == null || control.Width <= 0 || control.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                float r = (float)radius;
                path.StartFigure();
                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(control.Width - r, 0, r, r, 270, 90);
                path.AddArc(control.Width - r, control.Height - r, r, r, 0, 90);
                path.AddArc(0, control.Height - r, r, r, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }
        #endregion

        #region إدارة الفورمات (الظهور، الاختفاء، الفتح)
        public static void PrepareForm(Form frm)
        {
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.BackColor = BackColor;
            ApplyModernCorners(frm, 20);

            Control[] headers = frm.Controls.Find("pnlHeader", true);
            if (headers.Length > 0)
            {
                Panel pnlHeader = headers[0] as Panel;
                if (pnlHeader != null)
                    pnlHeader.MouseDown += delegate { MoveForm(frm.Handle); };
                pnlHeader.BackColor = HeaderColor;
            }

            StyleAllControls(frm);
            FormFadeIn(frm);
        }

        public static void OpenChildForm(Form childForm)
        {
            childForm.StartPosition = FormStartPosition.CenterScreen;
            // السطر السحري: لازم نضمن إن الفورم رسمت عناصرها في الذاكرة الأول
            childForm.CreateControl();
            PrepareForm(childForm);
            childForm.Show();
        }

        public static void FormFadeIn(Form frm)
        {
            frm.Opacity = 0;
            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += delegate {
                if (frm.Opacity < 1) frm.Opacity += 0.05;
                else { timer.Stop(); timer.Dispose(); }
            };
            timer.Start();
        }

        public static void FormFadeOut(Form frm)
        {
            if (frm == null) return;
            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += delegate {
                if (frm.Opacity > 0) frm.Opacity -= 0.05;
                else { timer.Stop(); timer.Dispose(); frm.Close(); }
            };
            timer.Start();
        }
        #endregion

        #region تنسيق العناصر (الأزرار والجداول)
        public static void StyleButtons(Form frm)
        {
            StyleAllControls(frm);
        }
        // النسخة دي هي اللي هتحل المشكلة لأنها بتقبل Panel أو أي حاوية
        public static void StyleButtons(Control container)
        {
            StyleAllControls(container);
        }
        public static void StyleAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                Button btn = c as Button;
                if (btn != null)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;

                    if (btn.Name.ToLower().Contains("close") || btn.Text.Trim() == "X" || btn.Text.Trim() == "خروج")
                    {
                        btn.FlatAppearance.MouseOverBackColor = Color.Red;
                        btn.FlatAppearance.MouseDownBackColor = Color.DarkRed;
                    }
                    else if (parent.Name.ToLower().Contains("sidebar"))
                    {
                        btn.BackColor = Color.Transparent;
                        btn.ForeColor = Color.White;
                        // هنا مش بنعمل ApplyModernCorners عشان القائمة تفضل متساوية
                    }
                    else if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent)
                    {
                        btn.BackColor = PrimaryColor;
                        btn.ForeColor = Color.White;
                        ApplyModernCorners(btn, 10); // خلي الحواف الدائرية للأزرار العادية فقط
                    }
                }

                      // الجداول
                DataGridView dgv = c as DataGridView;
                if (dgv != null) FormatDataGridView(dgv);

                if (c.HasChildren) StyleAllControls(c);
            }
        }
        //public static void StyleAllControls(Control parent)
        //{
        //    foreach (Control c in parent.Controls)
        //    {
        //        // الأزرار
        //        Button btn = c as Button;
        //        if (btn != null)
        //        {
        //            btn.FlatStyle = FlatStyle.Flat;
        //            btn.FlatAppearance.BorderSize = 0;
        //            btn.Cursor = Cursors.Hand;
        //            if (btn.Name.ToLower().Contains("close") || btn.Text.Trim() == "X")
        //            {
        //                btn.FlatAppearance.MouseOverBackColor = Color.Red;
        //                btn.FlatAppearance.MouseDownBackColor = Color.DarkRed;
        //            }
        //            else if (parent.Name.ToLower().Contains("sidebar"))
        //            {
        //                btn.BackColor = Color.Transparent;
        //                btn.ForeColor = Color.White;
        //                // هنا لا نطبق ApplyModernCorners للحفاظ على استقامة القائمة
        //            }
        //            else if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent)
        //            {
        //                btn.BackColor = PrimaryColor;
        //                btn.ForeColor = Color.White;
        //            }
        //            ApplyModernCorners(btn, 10);
        //        }

        //        // الجداول
        //        DataGridView dgv = c as DataGridView;
        //        if (dgv != null) FormatDataGridView(dgv);

        //        if (c.HasChildren) StyleAllControls(c);
        //    }
        //}

        public static void FormatDataGridView(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
        }
        #endregion

        #region مسح الحقول والتحقق
        public static void ClearFields(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox) ((TextBox)c).Clear();
                else if (c is ComboBox) ((ComboBox)c).SelectedIndex = -1;
                else if (c is NumericUpDown) ((NumericUpDown)c).Value = 0;

                if (c.HasChildren) ClearFields(c);
            }
        }

        public static bool IsValid(Control container)
        {
            foreach (Control c in container.Controls)
            {
                TextBox txt = c as TextBox;
                if (txt != null)
                {
                    if (txt.Tag != null && txt.Tag.ToString() == "Optional") continue;
                    if (string.IsNullOrEmpty(txt.Text.Trim()))
                    {
                        txt.BackColor = Color.MistyRose;
                        return false;
                    }
                    txt.BackColor = Color.White;
                }
                if (c.HasChildren) { if (!IsValid(c)) return false; }
            }
            return true;
        }
        #endregion
    }
}