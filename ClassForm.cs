using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloLabel
{
    public partial class ClassForm : Form
    {
        public ClassForm(string _labelPth)
        {
            InitializeComponent();
            if (!Directory.Exists(_labelPth))
            {
                Directory.CreateDirectory(_labelPth);
            }
            labelPath = _labelPth;
            var tblF = Path.Combine(labelPath, "classes.txt");
            if (File.Exists(tblF))
            {
                var tbl = File.ReadAllLines(tblF);
                foreach (var c in tbl)
                {
                    if (!String.IsNullOrEmpty(c))
                    {
                        Classes.Add(c);
                    }
                }
                listBox1.Items.Clear();
                foreach (var c in Classes)
                {
                    listBox1.Items.Add(c);
                }
            }
        }

        public int Class = 0;
        public string labelPath;

        public void SetFolder(string _labelPth)
        {
            if (!Directory.Exists(_labelPth))
            {
                Directory.CreateDirectory(_labelPth);
            }
            labelPath = _labelPth;
            var tblF = Path.Combine(labelPath, "classes.txt");
            if (File.Exists(tblF))
            {
                var tbl = File.ReadAllLines(tblF);
                foreach (var c in tbl)
                {
                    if (!String.IsNullOrEmpty(c))
                    {
                        Classes.Add(c);
                    }
                }
                listBox1.Items.Clear();
                foreach (var c in Classes)
                {
                    listBox1.Items.Add(c);
                }
            }
        }

        public List<string> Classes = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                Class = -1;
                for(int i = 0; i<Classes.Count; i++)
                {
                    if(textBox1.Text == Classes[i])
                    {
                        Class = i; break;
                    }
                }
                if (Class >= 0)
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
                Class = -1;
                for (int i = 0; i < Classes.Count; i++)
                {
                    if (textBox1.Text == Classes[i])
                    {
                        Class = i; break;
                    }
                }
                if (Class >= 0)
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        public void setMode(int mode)
        {
            if(mode == 0)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                bool bex = false;
                for (int i = 0; i < Classes.Count; i++)
                {
                    if (Classes[i].Equals(textBox1.Text))
                    {
                        bex = true;
                        break;
                    }
                }
                if (!bex)
                {
                    Classes.Add(textBox1.Text);
                    string contents = "";
                    Classes.Sort((a, b) =>
                    {
                        return a.CompareTo(b);
                    });
                    foreach (var c in Classes)
                    {
                        contents += c.ToString() + "\n";
                    }
                    File.WriteAllText(Path.Combine(labelPath, "classes.txt"), contents);

                    listBox1.Items.Clear();
                    foreach (var c in Classes)
                    {
                        listBox1.Items.Add(c);
                    }
                }
            }
        }
    }
}
