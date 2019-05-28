using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrueConfScanerManager;

namespace ScannerTest
{
    public partial class Form1 : Form, IMessageFilter
    {
        private List<string> files;
        private ScannerManager scannerManager;
        private int imageIndex = -1;
        public Form1()
        {
            InitializeComponent();
            files = new List<string>();
            scannerManager = new ScannerManager(Handle);
            cbScanner.DataSource = scannerManager.GetAllScanners();
            cbScanner.SelectedIndex = scannerManager.DefaultScanner;
        }

        private void CbScanner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbScanner.SelectedIndex >= 0)
                scannerManager.CurrentScanner = cbScanner.SelectedIndex;
        }

        private void BtnScan_Click(object sender, EventArgs e)
        {
            if (cbScanner.SelectedIndex >= 0)
            {
                Application.AddMessageFilter(this);
                scannerManager.Scan();
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            ScannerMessages msg = scannerManager.PassMessage(ref m);
            if (msg == ScannerMessages.Undefined)
                return false;

            switch (msg)
            {
                case ScannerMessages.FinishScanning:
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = "Scan";
                        sfd.Title = "Save bitmap as...";
                        sfd.Filter = "Bitmap file (*.bmp)|*.bmp|TIFF file (*.tif)|*.tif|JPEG file (*.jpg)|*.jpg|PNG file (*.png)|*.png|GIF file (*.gif)|*.gif|All files (*.*)|*.*";
                        sfd.FilterIndex = 1;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            string[] savedFiles = scannerManager.SaveScanImages(sfd.FileName);
                            if (savedFiles.Length > 0)
                            {
                                files.AddRange(savedFiles);
                                if (imageIndex < 0)
                                {
                                    pictureBox1.Image = Image.FromFile(files[0]);
                                    imageIndex = 0;
                                }
                            }
                        }                        
                        break;
                case ScannerMessages.CloseScannerOk:
                    Application.RemoveMessageFilter(this);
                    break;
            }
            return true;
        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            if (files.Count > 0)
                pictureBox1.Image = Image.FromFile(files[(imageIndex + 5) % files.Count]);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (files.Count > 0)
                pictureBox1.Image = Image.FromFile(files[(imageIndex + 1) % files.Count]);
        }
    }
}
