using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Safe_Audit.BL; // كلاسات الشغل

namespace Safe_Audit.PL
{
    public partial class FRM_Users : Form
    {
        // استدعاء الكلاس اللي أنت تعبت في كتابته
        CLS_Login userBus = new CLS_Login();

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public FRM_Users()
        {
            InitializeComponent();
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 25, 25));
        }

        private void RefreshGrid()
        {
            try
            {
                // استخدام دالة الـ BL لجلب البيانات عبر SP_GET_ALL_USERS
                DataTable dt = userBus.GetAllUsers();
                dgvUsers.DataSource = dt;

                if (dgvUsers.Columns.Count > 0)
                {
                    dgvUsers.Columns["UserID"].Visible = false;
                    dgvUsers.Columns["FullName"].HeaderText = "الاسم بالكامل";
                    dgvUsers.Columns["UserName"].HeaderText = "اسم المستخدم";
                    dgvUsers.Columns["UserType"].HeaderText = "نوع الصلاحية";
                    dgvUsers.Columns["IsActive"].HeaderText = "الحالة";

                    // الثيم الموحد
                    dgvUsers.EnableHeadersVisualStyles = false;
                    dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = HelperMethods.HeaderColor;
                    dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = HelperMethods.GetContrastColor(HelperMethods.HeaderColor);
                    dgvUsers.DefaultCellStyle.SelectionBackColor = HelperMethods.PrimaryColor;
                    dgvUsers.DefaultCellStyle.SelectionForeColor = HelperMethods.GetContrastColor(HelperMethods.PrimaryColor);

                    dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvUsers.RowHeadersVisible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("خطأ في التحميل: " + ex.Message); }
        }

        private void FRM_Users_Load(object sender, EventArgs e)
        {
            comboUserType.Items.Clear();
            comboUserType.Items.AddRange(new string[] { "مدير نظام", "مدخل بيانات" });
            RefreshGrid();
        }

        // 2. زر الحفظ (نادينا دالة الـ BL مباشرة)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("برجاء إدخال اسم المستخدم وكلمة المرور"); return;
            }

            try
            {
                // استدعاء دالة الإضافة اللي بتنادي SP_ADD_USER
                userBus.AddUser(txtFullName.Text, txtUserName.Text, txtPassword.Text, comboUserType.SelectedItem.ToString());

                MessageBox.Show("تم حفظ المستخدم بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshGrid();
                ClearFields();
            }
            catch (Exception ex) { MessageBox.Show("خطأ في الحفظ: " + ex.Message); }
        }

        // 3. زر التعديل (استخدمنا الـ Master Edit اللي أنت عملته)
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            try
            {
                int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
                // هنا بنبعت true للـ IsActive عشان يفضل شغال، والباسورد لو فاضي الـ SP هيتعامل
                userBus.EditUserMaster(id, txtFullName.Text, txtUserName.Text, txtPassword.Text, comboUserType.SelectedItem.ToString(), true);

                MessageBox.Show("تم التعديل بنجاح");
                RefreshGrid();
                ClearFields();
            }
            catch (Exception ex) { MessageBox.Show("خطأ في التعديل: " + ex.Message); }
        }

        // 4. زر التجميد (Master Edit مع تغيير الحالة لـ false)
        private void btnIsActive_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            string name = dgvUsers.CurrentRow.Cells["FullName"].Value.ToString();
            if (MessageBox.Show($"هل تريد تجميد حساب ({name})؟", "تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(dgvUsers.CurrentRow.Cells["UserID"].Value);
                    // بننادي نفس دالة التعديل بس بنغير آخر بارامتر لـ False (مجمد)
                    userBus.EditUserMaster(id,
                                           dgvUsers.CurrentRow.Cells["FullName"].Value.ToString(),
                                           dgvUsers.CurrentRow.Cells["UserName"].Value.ToString(),
                                           null, // مش هنغير باسورد
                                           dgvUsers.CurrentRow.Cells["UserType"].Value.ToString(),
                                           false); // الحالة: مجمد

                    MessageBox.Show("تم تجميد الحساب");
                    RefreshGrid();
                }
                catch (Exception ex) { MessageBox.Show("خطأ: " + ex.Message); }
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                txtFullName.Text = row.Cells["FullName"].Value?.ToString();
                txtUserName.Text = row.Cells["UserName"].Value?.ToString();
                comboUserType.SelectedItem = row.Cells["UserType"].Value?.ToString();
                txtPassword.Clear();
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void ClearFields()
        {
            txtFullName.Clear(); txtUserName.Clear(); txtPassword.Clear();
            comboUserType.SelectedIndex = -1;
        }

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}