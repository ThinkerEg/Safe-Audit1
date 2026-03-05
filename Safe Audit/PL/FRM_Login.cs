using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using Safe_Audit.DAL;
using Safe_Audit.BL;

namespace Safe_Audit.PL
{
    public partial class FRM_Login : Form
    {
        // تم إلغاء سطر الـ SqlConnection اليدوي والاعتماد على DbHelper لتوحد المسار

       
        public FRM_Login()
        {
            InitializeComponent();

        }
        private void FRM_Login_Load(object sender, EventArgs e)
        {
            //pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };
            LoadUsers();
            // في الـ Load بتاع الفورم
        }

        private void btn_connect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PL.FRM_CONFIG frm = new PL.FRM_CONFIG();
            frm.ShowDialog();
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "كلمة المرور")
            {
                txtPassword.Text = "";
                txtPassword.PasswordChar = '●';
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "كلمة المرور";
                txtPassword.PasswordChar = '\0';
            }
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin.PerformClick();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void LoadUsers()
        {
            try
            {
                BL.CLS_Login log = new BL.CLS_Login();
                // جلب النشطين فقط للكومبو
                DataTable dt = log.GetActiveUsers();

                if (dt.Rows.Count > 0)
                {
                    comboBoxUsers.DataSource = dt;
                    comboBoxUsers.DisplayMember = "FullName"; // يظهر للعين
                    comboBoxUsers.ValueMember = "UserName";   // يُستخدم في الكود
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل القائمة: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. التحقق الأولي: هل الكومبو بوكس فيه داتا أصلاً؟
            if (comboBoxUsers.Items.Count == 0)
            {
                MessageBox.Show("لا يوجد مستخدمين مسجلين في النظام، يرجى مراجعة قاعدة البيانات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. التحقق من الاختيار والباسورد
            if (comboBoxUsers.SelectedValue == null ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                txtPassword.Text == "كلمة المرور") // إضافة هذا الشرط الصغير
            {
                return;
            }
            try
            {
                BL.CLS_Login log = new BL.CLS_Login();
                // نأخذ الـ ValueMember اللي هو الـ UserName
                DataTable dt = log.CheckLogin(comboBoxUsers.SelectedValue.ToString(), txtPassword.Text);

                if (dt.Rows.Count > 0)
                {
                    GlobalUser.ID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    GlobalUser.FullName = dt.Rows[0]["FullName"].ToString();
                    GlobalUser.UserType = dt.Rows[0]["UserType"].ToString();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("بيانات الدخول غير صحيحة أو الحساب مجمد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ: " + ex.Message);
            }
        }
    }
}