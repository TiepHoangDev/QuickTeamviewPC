using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ClassLibraryHelper;

namespace QuickTeamviewPC
{
    public partial class ucConnect : UserControl, IConnection
    {
        public string ID_Ultra { get => maskedTextBox_ID_ultra.Text; set => maskedTextBox_ID_ultra.Text = value; }
        public string Password_Ultra { get => textBox_pass_ultra.Text; set => textBox_pass_ultra.Text = value; }
        public string ID_Teamview { get => maskedTextBox_ID_teamview.Text; set => maskedTextBox_ID_teamview.Text = value; }
        public string Pass_Teamview { get => textBox_pass_teamview.Text; set => textBox_pass_teamview.Text = value; }
        public string NamePC { get => textBox_name.Text; set => textBox_name.Text = value; }
        public string RDP_IP { get => textBox_rdp_ip.Text; set => textBox_rdp_ip.Text = value; }
        public string RDP_Username { get => textBox_rdp_username.Text; set => textBox_rdp_username.Text = value; }
        public string RDP_Pass { get => textBox_rdp_pass.Text; set => textBox_rdp_pass.Text = value; }

        public bool UseRDP { get => checkBox_rdp.Checked; set => checkBox_rdp.Checked = value; }

        public ucConnect()
        {
            InitializeComponent();
            this.Size = new Size(this.Size.Width, 288 - panel_rdp.Height - 11);
        }

        private void button_open_ultra_Click(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName("UltraViewer_Desktop").Length == 0)
                {
                    var path_ultra = "C:\\Program Files (x86)\\UltraViewer\\UltraViewer_Desktop.exe";
                    if (File.Exists(path_ultra))
                    {
                        Process.Start(path_ultra);
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chương trình Ultraview tại " + path_ultra);
                        return;
                    }
                }

                var cmd = $"/C UltraViewer_Desktop.exe -i:{ID_Ultra} -p:{Password_Ultra}";
                var proc = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmd,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = "C:\\Program Files (x86)\\UltraViewer\\"
                });
                proc.WaitForExit(20000);
                var output = proc.StandardOutput.ReadToEnd();
                var err = proc.StandardError.ReadToEnd();
                var msg = output + err;
                if (string.IsNullOrWhiteSpace(msg) == false)
                {
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Name}: {ID_Ultra} + {Password_Ultra} . {ex.Message}");
            }
        }

        private void button_open_teamview_Click(object sender, EventArgs e)
        {
            try
            {
                if (Process.GetProcessesByName("TeamViewer").Length == 0)
                {
                    var path_teamview = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
                    if (File.Exists(path_teamview))
                    {
                        Process.Start(path_teamview);
                        Thread.Sleep(5000);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chương trình teamviewer tại " + path_teamview);
                        return;
                    }
                }

                var cmd = $"/C teamviewer.exe -i \"{ID_Teamview}\" -P \"{Pass_Teamview}\"";
                var proc = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = cmd,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = "C:\\Program Files (x86)\\TeamViewer\\"
                });
                proc.WaitForExit(20000);
                var output = proc.StandardOutput.ReadToEnd();
                var err = proc.StandardError.ReadToEnd();
                var msg = output + err;
                if (string.IsNullOrWhiteSpace(msg) == false)
                {
                    MessageBox.Show(msg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Name}: {ID_Teamview} + {Pass_Teamview} . {ex.Message}");
            }
        }

        public string StringSaveToFile()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RDPHelper.Connect(textBox_rdp_ip.Text, textBox_rdp_username.Text, textBox_rdp_pass.Text);
        }

        private void checkBox_rdp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_rdp.Checked)
            {
                this.Size = new Size(this.Size.Width, 288);
            }
            else
            {
                this.Size = new Size(this.Size.Width, 288 - panel_rdp.Height - 11);
            }
        }
    }
}
