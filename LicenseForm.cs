using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Compute;

namespace FlexeraTest
{
    public partial class FlexeraTest : Form
    {
        StreamReader fileCSV;

        public FlexeraTest()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "csv files (*.csv)|*.csv";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        fileCSV = new StreamReader(openFileDialog1.FileName);
                        SetText(openFileDialog1.SafeFileName);
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                    }
                }
            }
        }

        private void SetText(string text)
        {
            txtFile.Text = text;
        }

        private void txtAppID_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtAppID.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAppID.Text = txtAppID.Text.Remove(txtAppID.Text.Length - 1);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Double licenses = 0; 
            try
            {
                if(string.IsNullOrEmpty(txtAppID.Text))
                {
                    throw new Exception("Please input Application ID");
                }
                else if(string.IsNullOrEmpty(txtFile.Text))
                {
                    throw new Exception("Please insert file");
                }
                else
                {
                    licenses = Compute.ComputeCSV(txtAppID.Text, fileCSV);

                    MessageBox.Show("Application ID: " + txtAppID.Text + " requires " + licenses + " number of licenses.");

                    fileCSV.DiscardBufferedData();
                    fileCSV.BaseStream.Position = 0;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }
    }
}
