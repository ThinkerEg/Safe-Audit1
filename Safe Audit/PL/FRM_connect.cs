using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Net.Sockets;
using Safe_Audit.Properties;
using Safe_Audit.BL; // تأكد من وجود المتغيرات في Settings

namespace Safe_Audit.PL
{
    public partial class FRM_CONFIG : Form
    {
        public FRM_CONFIG()
        {
            InitializeComponent();
            LoadSettings();
        }

        // تحميل الإعدادات عند الفتح
        private void LoadSettings()
        {
            txtServer.Text = Settings.Default.Server;
            txtDatabase.Text = Settings.Default.Database;
            txtBackup.Text = Settings.Default.Backup;

            if (Settings.Default.Mode == "SQL")
            {
                rbSQL.Checked = true;
                pl_SQL.Visible = true;
                txt_ID_con.Text = Settings.Default.ID;
                txt_PWD_con.Text = Settings.Default.Password;
            }
            else
            {
                rbWindows.Checked = true;
                pl_SQL.Visible = false;
            }
        }

        // بناء جملة الاتصال بشكل ديناميكي للاختبار
        private string BuildConnectionString()
        {
            if (rbSQL.Checked)
                return $@"Data Source={txtServer.Text};Initial Catalog={txtDatabase.Text};User ID={txt_ID_con.Text};Password={txt_PWD_con.Text};Integrated Security=False;TrustServerCertificate=True;Connect Timeout=15;";
            else
                return $@"Data Source={txtServer.Text};Initial Catalog={txtDatabase.Text};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=15;";
        }

        // زر اختبار الاتصال (الاحترافي)
        private void btn_test_con_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. اختبار الشبكة (Ping على البورت)
                using (TcpClient client = new TcpClient())
                {
                    // محاولة الاتصال بالآي بي والبورت 1433
                    var result = client.BeginConnect(txtServer.Text.Split('\\')[0], 1433, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));

                    if (!success)
                    {
                        MessageBox.Show("تحذير: البورت 1433 مغلق على السيرفر، قد ينجح الاتصال محلياً ويفشل عبر الشبكة.", "تنبيه الشبكة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // 2. اختبار صلاحيات SQL
                using (SqlConnection con = new SqlConnection(BuildConnectionString()))
                {
                    con.Open();
                    MessageBox.Show("تم الاتصال بقاعدة البيانات بنجاح ✔", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("فشل الاتصال: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // زر حفظ الإعدادات
        private void btn_Save_con_Click(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.Server = txtServer.Text;
                Settings.Default.Database = txtDatabase.Text;
                Settings.Default.Mode = rbSQL.Checked ? "SQL" : "Windows";
                Settings.Default.ID = txt_ID_con.Text;
                Settings.Default.Password = txt_PWD_con.Text;
                Settings.Default.Backup = txtBackup.Text;
                Settings.Default.Save();

                MessageBox.Show("تم حفظ إعدادات السيرفر بنجاح. سيتم تطبيقها عند العمليات القادمة.", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء الحفظ: " + ex.Message);
            }
        }

        private void rbSQL_CheckedChanged(object sender, EventArgs e)
        {
            pl_SQL.Visible = rbSQL.Checked;
        }

        private void btn_SelectBackupPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_CONFIG_Load(object sender, EventArgs e)
        {
            //pnlHeader.MouseDown += (s, ev) => { HelperMethods.MoveForm(this.Handle); };
        }
    }
}