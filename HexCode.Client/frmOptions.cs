using HexCode.Common;
using HexCode.Engine.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexCode.Client
{
    public partial class frmOptions : Form
    {
        public frmOptions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectDll(txtBlue);
        }

        private void selectDll(TextBox tb)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = ".dll|*.dll";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            if (tb.Text.Length > 0) {
                openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(System.IO.Path.GetDirectoryName(tb.Text));
            } else {
                System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
                openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(ass.Location);
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                tb.Text = openFileDialog.FileName;
            }


        }

        private void cmdRed_Click(object sender, EventArgs e)
        {
            selectDll(txtRed);
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            var settings = Settings.Load();
            txtBlue.Text = settings.PlayerBlueDll;
            txtRed.Text = settings.PlayerRedDll;

            cboRed.SelectedItem = settings.PlayerRedTypeName;
            cboBlue.SelectedItem = settings.PlayerBlueTypeName;
        

            cboMap.Items.AddRange(MapLoader.GetMapNames().ToArray());
            cboMap.SelectedItem = settings.MapName;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            var settings = Settings.Load();
            settings.PlayerRedDll = txtRed.Text;
            settings.PlayerBlueDll = txtBlue.Text;
            settings.PlayerRedTypeName = (string)cboRed.SelectedItem;
            settings.PlayerBlueTypeName = (string)cboBlue.SelectedItem;
            settings.MapName = (string)cboMap.SelectedItem;

            settings.Save();
            this.Close();
        }

        private void txtBlue_TextChanged(object sender, EventArgs e)
        {
            findRobot(txtBlue, cboBlue);
        }

        private void findRobot(TextBox tb, ComboBox cb)
        {
            if (System.IO.File.Exists(tb.Text)) {
                cb.Items.Clear();
                cb.Items.AddRange(LibraryRobotFactory.FindTypes(tb.Text).Select(x => x.Name).ToArray());
                if (cb.Items.Count > 0) {
                    cb.SelectedIndex = 0;
                }
            }
        }

        private void txtRed_TextChanged(object sender, EventArgs e)
        {
            findRobot(txtRed, cboRed);
        }
    }
}
