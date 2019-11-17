using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbLinkCheck.Text = "Path/To/CheckFolder";
            tbNewData.Text = "Path/To/NewData";
            tbOutPut.Text = "Path/To/OutputFolder";
        }


        private void btnChangeCheckFolder_Click(object sender, EventArgs e)
        {

            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    ProcessCenter.getInstance().SetCheckFolder(fldrDlg.SelectedPath);
                    tbLinkCheck.Text = fldrDlg.SelectedPath;
                }
            }
        }

        private void btnChangeNewDataFolder_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    ProcessCenter.getInstance().SetInputFolder(fldrDlg.SelectedPath);
                    tbNewData.Text = fldrDlg.SelectedPath;
                }
            }
        }

        private void btnChangeOutPutFolder_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    ProcessCenter.getInstance().SetOutPutFolder(fldrDlg.SelectedPath);
                    tbOutPut.Text = fldrDlg.SelectedPath;
                }
            }
        }

        private void btnAddEffect_Click(object sender, EventArgs e)
        {
            ProcessCenter.getInstance().SetLogView(this.gvLogEffect);
            ProcessCenter.getInstance().ProcessEffect(tbLsNameEffect.Text.Split('|'));
        }

        private void btnAddMonster_Click(object sender, EventArgs e)
        {
            ProcessCenter.getInstance().SetLogView(this.gvLogMonster);
            ProcessCenter.getInstance().AddMonster(tbMonsterName.Text);
        }
    }
}
