using Newtonsoft.Json.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Buffers.Text;
using System.Xml;

namespace YoloLabel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string cfgFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "config.cfg");
            if (File.Exists(cfgFile))
            {
                string contents = File.ReadAllText(cfgFile);
                JObject config = JObject.Parse(contents);
                var imgF = config.GetValue("imgFolder");
                if (imgF != null)
                {
                    imgFolder = imgF.ToString();
                }
                var lblF = config.GetValue("lblFolder");
                if (lblF != null)
                {
                    lblFolder = lblF.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(lblFolder))
            {
                if (classFrm == null)
                {
                    classFrm = new ClassForm(lblFolder);
                }
                labelTbl.AddRange(classFrm.Classes);

                foreach (var c in labelTbl)
                {
                    var item = contextMenuStrip1.Items.Add(c);
                    contextMenuStrip1.Items.Add(item);
                }
            }
            else
            {
                lblFolder = "";
            }
            if (Directory.Exists(imgFolder))
            {
                loadImageList();
                if (fileList.Count > 0)
                {
                    openImage(fileList[0]);
                }
            }
            else
            {
                imgFolder = "";
            }
        }

        private void writeConfig(string imgPth, string lblPth)
        {
            string cfgFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "config.cfg");
            string oimgPth = "", olblPth = "";
            if (File.Exists(cfgFile))
            {
                string contents = File.ReadAllText(cfgFile);
                try
                {
                    JObject config = JObject.Parse(contents);
                    var imgF = config.GetValue("imgFolder");
                    if (imgF != null)
                    {
                        oimgPth = imgF.ToString();
                    }
                    var lblF = config.GetValue("lblFolder");
                    if (lblF != null)
                    {
                        olblPth = lblF.ToString();
                    }
                }
                catch (Exception ex) { }
            }
            if (String.IsNullOrEmpty(imgPth))
            {
                imgPth = oimgPth;
            }
            if (String.IsNullOrEmpty(lblPth))
            {
                lblPth = olblPth;
            }
            JObject outC = new JObject();
            outC.Add("imgFolder", imgPth);
            outC.Add("lblFolder", lblPth);
            var oO = outC.ToString();
            File.WriteAllText(cfgFile, oO);
        }

        private string imgFolder = System.AppDomain.CurrentDomain.BaseDirectory;
        private string lblFolder = System.AppDomain.CurrentDomain.BaseDirectory;
        private string currentImage = "";
        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = imgFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                imgFolder = folderBrowserDialog1.SelectedPath;
                loadImageList();
                if (fileList.Count > 0)
                {
                    openImage(fileList[0]);
                }
                writeConfig(imgFolder, "");
            }
        }

        private List<string> fileList = new List<string>();
        private void loadImageList()
        {
            var files = Directory.GetFiles(imgFolder);
            fileList.Clear();
            foreach (var f in files)
            {
                if (f.EndsWith(".jpg") || f.EndsWith(".png"))
                {
                    fileList.Add(f);
                }
            }
            listBox1.Items.Clear();
            foreach (var f in fileList)
            {
                listBox1.Items.Add(f);
            }
        }

        private string labelFileName(string imgFile)
        {
            var filename = Path.GetFileNameWithoutExtension(imgFile);
            return Path.Combine(lblFolder, filename + ".txt");
        }

        private void readLabels(string lblF, List<SLabel> list)
        {
            list.Clear();
            if (File.Exists(lblF))
            {
                string[] lines = File.ReadAllLines(lblF);
                foreach (var line in lines)
                {
                    var cr = line.Split(" ");
                    if (cr.Length == 5)
                    {
                        SLabel sl = new SLabel();
                        sl.Id = -1;
                        sl.rect = new RectangleF();
                        int cidx = -1;
                        if (int.TryParse(cr[0], out cidx))
                        {
                            sl.Id = cidx;
                        }
                        if (sl.Id >= 0 && sl.Id < labelTbl.Count)
                        {
                            sl.Label = labelTbl[sl.Id];
                        }
                        float left = -1, top = -1, wd = 0, ht = 0;

                        float.TryParse(cr[1], out left);
                        float.TryParse(cr[2], out top);
                        float.TryParse(cr[3], out wd);
                        float.TryParse(cr[4], out ht);
                        if (left >= 0 && top >= 0 && wd > 0 && ht > 0)
                        {
                            sl.rect = new RectangleF(left, top, wd, ht);
                            list.Add(sl);
                        }
                    }
                }
            }
        }
        private void openLabel(string imgFile)
        {
            var lblF = labelFileName(imgFile);
            readLabels(lblF, labels);
            listLabels();
        }

        private void listLabels()
        {
            listBox2.Items.Clear();
            for (int i = 0; i < labels.Count; i++)
            {
                listBox2.Items.Add(labels[i].Label);
            }
        }

        private System.Drawing.Point leftTop;
        private OpenCvSharp.Size resizeImage(int imgw, int imgh)
        {
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            int h = height;
            int w = 0;
            w = h * imgw / imgh;
            if (w > width)
            {
                h = w * imgh / imgw;
                w = width;
            }
            rRect = new RectangleF((pictureBox1.Width - w) / 2, (pictureBox1.Height - h) / 2, w, h);
            leftTop = new System.Drawing.Point((int)(pictureBox1.Width - rRect.Width) / 2, (int)(pictureBox1.Height - rRect.Height) / 2);
            OpenCvSharp.Size osize = new OpenCvSharp.Size(w, h);
            return osize;
        }

        private void openImage(string imgFile)
        {
            currentImage = imgFile;
            selectedIdx = -1;

            Mat img = Cv2.ImRead(imgFile, ImreadModes.Grayscale);

            var osize = resizeImage(img.Width, img.Height);
            srcMat = img.Resize(osize);

            openLabel(imgFile);

            pictureBox1.Refresh();

            int idx = Array.IndexOf(fileList.ToArray(), imgFile);
            listBox1.SelectedIndex = idx;
            this.Text = imgFile + $" - [{idx + 1}/{fileList.Count}]";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = lblFolder;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblFolder = folderBrowserDialog1.SelectedPath;
                classFrm = new ClassForm(lblFolder);
                labelTbl.Clear();
                labelTbl.AddRange(classFrm.Classes);

                if (currentImage != "")
                {
                    openLabel(currentImage);
                    pictureBox1.Refresh();
                }

                writeConfig("", lblFolder);
            }
        }

        private class SLabel
        {
            public string Label { get; set; }
            public int Id { get; set; }
            public RectangleF rect { get; set; }
        }
        private static Font font1 = new Font(new FontFamily("华文宋体"), 17, GraphicsUnit.Pixel);
        private Pen boxPen = Pens.Red;
        private Mat srcMat;
        private PointF cursorPoint;
        private RectangleF rRect;
        private List<string> labelTbl = new List<string>();
        private List<SLabel> labels = new List<SLabel>();
        private int cutmode = 0;
        private RectangleF tempRect;
        private int cClass = 0;
        /// <summary>
        /// 0编辑（选择），1新的类型，2单一类型
        /// </summary>
        private int workMode = 0;
        private void saveLabelFile(string lblFileName, List<SLabel> list)
        {
            var content = "";
            for (int i = 0; i < list.Count; i++)
            {
                content += $"{list[i].Id} {list[i].rect.X} {list[i].rect.Y} {list[i].rect.Width} {list[i].rect.Height}\n";
            }
            File.WriteAllText(lblFileName, content);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (srcMat == null)
            {
                return;
            }
            var srcBmp = srcMat.ToBitmap();
            e.Graphics.DrawImage(srcBmp, rRect, new RectangleF(0, 0, srcBmp.Width, srcBmp.Height), GraphicsUnit.Pixel);
            if (workMode > 0)
            {
                e.Graphics.DrawLine(Pens.White, new PointF(0, cursorPoint.Y), new PointF(pictureBox1.Width, cursorPoint.Y));
                e.Graphics.DrawLine(Pens.White, new PointF(cursorPoint.X, 0), new PointF(cursorPoint.X, pictureBox1.Height));
            }

            for (int i = 0; i < labels.Count; i++)
            {
                Rectangle lr = labelRect(labels[i]);
                if (selectedIdx == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(125, Color.LightGreen)), lr);
                }
                else
                {
                    e.Graphics.DrawRectangle(Pens.LightGreen, lr);
                }
                e.Graphics.DrawString(labels[i].Label, font1, Brushes.LightGreen, new PointF(lr.Left, lr.Top));
            }

            if (cutmode == 1)
            {
                e.Graphics.DrawLine(boxPen, new System.Drawing.Point((int)tempRect.Left, (int)tempRect.Top), new System.Drawing.Point((int)tempRect.Right, (int)tempRect.Top));
                e.Graphics.DrawLine(boxPen, new System.Drawing.Point((int)tempRect.Right, (int)tempRect.Top), new System.Drawing.Point((int)tempRect.Right, (int)tempRect.Bottom));
                e.Graphics.DrawLine(boxPen, new System.Drawing.Point((int)tempRect.Right, (int)tempRect.Bottom), new System.Drawing.Point((int)tempRect.Left, (int)tempRect.Bottom));
                e.Graphics.DrawLine(boxPen, new System.Drawing.Point((int)tempRect.Left, (int)tempRect.Bottom), new System.Drawing.Point((int)tempRect.Left, (int)tempRect.Top));
            }
        }

        private Rectangle labelRect(SLabel lbl)
        {
            var xc = lbl.rect.X * rRect.Width;
            var yc = lbl.rect.Y * rRect.Height;
            var lw = lbl.rect.Width * rRect.Width;
            var lh = lbl.rect.Height * rRect.Height;
            Rectangle lr = new Rectangle((int)(Math.Round(xc - lw / 2.0) + rRect.X), (int)(Math.Round(yc - lh / 2.0) + rRect.Y), (int)Math.Round(lw), (int)Math.Round(lh));
            return lr;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = new PointF(e.X, e.Y);
            if (workMode > 0 && cutmode == 1)
            {
                var tr = tempRect;

                if (e.X < tempRect.Left)
                {
                    tempRect.X = e.X;
                    tempRect.Width = tr.Width + tr.X - tempRect.X;
                }
                else
                {
                    tempRect.Width = e.X - tempRect.X;
                }
                if (e.Y < tempRect.Top)
                {
                    tempRect.Y = e.Y;
                    tempRect.Height = tr.Height + tr.Y - tempRect.Y;
                }
                else
                {
                    tempRect.Height = e.Y - tempRect.Y;
                }
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (cutmode == 1)
            {
                cutmode = 0;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (workMode > 0 && cutmode == 1)
            {
                cutmode = 0;
                var xc = ((tempRect.Right + tempRect.Left) / 2 - leftTop.X) / srcMat.Width;
                var yc = ((tempRect.Top + tempRect.Bottom) / 2 - leftTop.Y) / srcMat.Height;
                var tr = new RectangleF(xc, yc, tempRect.Width / srcMat.Width, tempRect.Height / srcMat.Height);

                if (workMode == 1)
                {
                    if (classFrm == null)
                    {
                        classFrm = new ClassForm(lblFolder);
                    }
                    classFrm.setMode(1);
                    if (classFrm.ShowDialog() == DialogResult.OK)
                    {
                        cClass = classFrm.Class;
                    }
                    else
                    {
                        return;
                    }
                }
                SLabel l = new SLabel();
                l.Id = cClass;
                l.rect = tr;
                l.Label = labelTbl[l.Id];
                labels.Add(l);
                var lblFileName = labelFileName(currentImage);
                saveLabelFile(lblFileName, labels);
                listLabels();

                pictureBox1.Refresh();
            }
        }

        private int selectedIdx = -1;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (workMode == 0)
            {
                selectedIdx = -1;
                var x = e.X;
                var y = e.Y;
                for (int i = 0; i < labels.Count; i++)
                {
                    Rectangle lr = labelRect(labels[i]);
                    if (x > lr.X && x < lr.Right)
                    {
                        if (y > lr.Y && y < lr.Bottom)
                        {
                            selectedIdx = i;
                            break;
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (selectedIdx >= 0)
                    {
                        contextMenuStrip1.Show(Form1.MousePosition);
                    }
                }
                pictureBox1.Refresh();
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    cutmode = 1;
                    tempRect.X = e.X;
                    tempRect.Y = e.Y;
                    tempRect.Width = 0;
                    tempRect.Height = 0;
                }
            }
        }

        private ClassForm? classFrm = null;
        private void button8_Click(object sender, EventArgs e)
        {
            if (classFrm == null)
            {
                classFrm = new ClassForm(lblFolder);
            }
            classFrm.setMode(0);
            classFrm.ShowDialog();
            labelTbl.Clear();
            labelTbl.AddRange(classFrm.Classes);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = SystemColors.Highlight;
            button4.BackColor = SystemColors.Control;
            button7.BackColor = SystemColors.Control;
            workMode = 1;
            cutmode = 0;
            selectedIdx = -1;
            pictureBox1.Cursor = Cursors.Cross;
            pictureBox1.Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            button7.BackColor = SystemColors.Highlight;
            workMode = 2;
            cutmode = 0;
            selectedIdx = -1;
            pictureBox1.Cursor = Cursors.Cross;
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Highlight;
            button7.BackColor = SystemColors.Control;
            workMode = 0;
            cutmode = 0;
            selectedIdx = -1;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (File.Exists(currentImage))
            {
                File.Delete(currentImage);
                var lblF = labelFileName(currentImage);
                if (File.Exists(lblF))
                {
                    File.Delete(lblF);
                }
                string nimgF = "";
                for (int i = 0; i < fileList.Count; i++)
                {
                    if (fileList[i] == currentImage)
                    {
                        if (i == fileList.Count - 1)
                        {
                            nimgF = fileList[i - 1];
                        }
                        else
                        {
                            nimgF = fileList[i + 1];
                        }
                        fileList.RemoveAt(i);
                        break;
                    }
                }
                if (File.Exists(nimgF))
                {
                    openImage(nimgF);
                }
            }
        }

        private void editClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (classFrm == null)
            {
                classFrm = new ClassForm(lblFolder);
            }
            classFrm.setMode(1);
            if (classFrm.ShowDialog() == DialogResult.OK)
            {
                labels[selectedIdx].Id = classFrm.Class;
                labels[selectedIdx].Label = labelTbl[classFrm.Class];
                var lblFileName = labelFileName(currentImage);
                saveLabelFile(lblFileName, labels);
                listLabels();
                pictureBox1.Refresh();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedIdx >= 0)
            {
                labels.RemoveAt(selectedIdx);
                var lblFileName = labelFileName(currentImage);
                saveLabelFile(lblFileName, labels);
                listLabels();
                pictureBox1.Refresh();
            }
            selectedIdx = -1;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                newSrc(1);
                button6.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                newSrc(-1);
                button5.Focus();
                e.Handled = true;
            }else if(e.KeyCode == Keys.W)
            {
                button3_Click(sender, null);
                e.Handled = true;
            }else if(e.KeyCode == Keys.S)
            {
                button7_Click(sender, null);
                e.Handled = true;
            }else if(e.KeyCode == Keys.E)
            {
                button4_Click(sender, null);
                e.Handled = true;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            newSrc(-1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            newSrc(1);
        }
        private void newSrc(int r)
        {
            int cidx = 0;
            for (int i = 0; i < fileList.Count; i++)
            {
                if (Path.GetFileName(fileList[i]) == Path.GetFileName(currentImage))
                {
                    cidx = i;
                    break;
                }
            }
            if (r < 0)
            {
                cidx--;
            }
            else if (r > 0)
            {
                cidx++;
            }
            if (cidx >= 0 && cidx < fileList.Count)
            {
                var nf = fileList[cidx];
                openImage(nf);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (srcMat != null)
            {
                var osize = resizeImage(srcMat.Width, srcMat.Height);

                srcMat = srcMat.Resize(osize);
                pictureBox1.Refresh();
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (labelTbl.Contains(e.ClickedItem.Text))
            {
                for (int i = 0; i < labelTbl.Count; i++)
                {
                    if (labelTbl[i] == e.ClickedItem.Text)
                    {
                        labels[selectedIdx].Id = i;
                        labels[selectedIdx].Label = labelTbl[i];
                        break;
                    }
                }
                var lblFileName = labelFileName(currentImage);
                saveLabelFile(lblFileName, labels);
                listLabels();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            int idx = listBox1.SelectedIndex;
            if (idx >= 0 && idx < fileList.Count)
            {
                openImage(fileList[idx]);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                selectedIdx = listBox2.SelectedIndex;
                pictureBox1.Refresh();
            }
        }

        private int hoveredIndex = -1;
        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // See which row is currently under the mouse:
            int newHoveredIndex = listBox1.IndexFromPoint(e.Location);

            // If the row has changed since last moving the mouse:
            if (hoveredIndex != newHoveredIndex)
            {
                // Change the variable for the next time we move the mouse:
                hoveredIndex = newHoveredIndex;

                // If over a row showing data (rather than blank space):
                if (hoveredIndex > -1)
                {
                    //Set tooltip text for the row now under the mouse:
                    toolTip1.Active = false;
                    toolTip1.SetToolTip(listBox1, fileList[hoveredIndex]);
                    toolTip1.Active = true;
                }
            }
        }

        /*
        // 类别排序错了，调整一下顺序
        private void button10_Click(object sender, EventArgs e)
        {
            var lbls = Directory.GetFiles(lblFolder);
            foreach ( var lbl in lbls )
            {
                if(lbl.EndsWith(".txt") && !lbl.EndsWith("classes.txt"))
                {
                    List<SLabel> list = new List<SLabel>();
                    readLabels(lbl, list);
                    for(int i = 0; i<list.Count; i++)
                    {
                        if (list[i].Id == 2)
                        {
                            list[i].Id = 3;
                        }else if (list[i].Id == 3)
                        {
                            list[i].Id = 2;
                        }
                    }
                    saveLabelFile(lbl, list);
                }
            }
        }*/
    }
}
