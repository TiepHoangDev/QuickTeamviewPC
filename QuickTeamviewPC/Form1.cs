using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTeamviewPC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("UltraViewer_Desktop").Length == 0)
            {
                var path_ultra = "C:\\Program Files (x86)\\UltraViewer\\UltraViewer_Desktop.exe";
                if (File.Exists(path_ultra))
                {
                    Process.Start(path_ultra);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chương trình Ultraview tại " + path_ultra);
                }
            }

            if (Process.GetProcessesByName("TeamViewer").Length == 0)
            {
                var path_teamview = @"C:\Program Files (x86)\TeamViewer\TeamViewer.exe";
                if (File.Exists(path_teamview))
                {
                    Process.Start(path_teamview);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chương trình teamviewer tại " + path_teamview);
                }
            }

            _reload();
        }

        private void _reload()
        {
            if (File.Exists(file_ID_Pass))
            {
                flowLayoutPanel1.Controls.Clear();
                var regex = new Regex("\\s");
                foreach (var item in File.ReadAllLines(file_ID_Pass))
                {
                    var arr = item.Split(file_ID_Pass_Sperator);
                    if (arr.Length > 4)
                    {
                        var UseRDP = arr.Length > 8 ? Convert.ToBoolean(arr[8]) : false;
                        var uc = new ucConnect()
                        {
                            NamePC = arr[0],
                            ID_Ultra = regex.Replace(arr[1], string.Empty).ToString(),
                            Password_Ultra = arr[2],
                            ID_Teamview = regex.Replace(arr[3], string.Empty).ToString(),
                            Pass_Teamview = arr[4],
                            RDP_IP = arr.Length > 5 ? arr[5] : "",
                            RDP_Username = arr.Length > 6 ? arr[6] : "",
                            RDP_Pass = arr.Length > 7 ? arr[7] : "",
                        };
                        uc.UseRDP = UseRDP;
                        flowLayoutPanel1.Controls.Add(uc);
                    }
                    else
                    {
                        MessageBox.Show(item);
                    }
                }
            }
        }

        const string file_ID_Pass = "file_ID_Pass.txt";
        const char file_ID_Pass_Sperator = '\t';
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ucConnect item in flowLayoutPanel1.Controls)
            {
                if (string.IsNullOrWhiteSpace(item.NamePC) == false)
                {
                    sb.AppendLine(string.Join(file_ID_Pass_Sperator.ToString(), new[] {
                        item.NamePC,
                        item.ID_Ultra,
                        item.Password_Ultra,
                        item.ID_Teamview,
                        item.Pass_Teamview,
                        item.RDP_IP,
                        item.RDP_Username,
                        item.RDP_Pass,
                        Convert.ToString(item.UseRDP)
                    }));
                }
            }
            File.WriteAllText(file_ID_Pass, sb.ToString());

            _reload();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new ucConnect());
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Để xóa, để trống tên, sau đó lưu (save)");
        }
    }
}
