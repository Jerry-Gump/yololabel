namespace YoloLabel
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            contextMenuStrip1 = new ContextMenuStrip(components);
            editClassToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            toolTip1 = new ToolTip(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(90, 717);
            panel1.TabIndex = 0;
            // 
            // label5
            // 
            label5.BackColor = SystemColors.ScrollBar;
            label5.Location = new Point(0, 306);
            label5.Name = "label5";
            label5.Size = new Size(87, 2);
            label5.TabIndex = 13;
            // 
            // label4
            // 
            label4.BackColor = SystemColors.ScrollBar;
            label4.Location = new Point(0, 423);
            label4.Name = "label4";
            label4.Size = new Size(87, 2);
            label4.TabIndex = 12;
            // 
            // label3
            // 
            label3.BackColor = SystemColors.ScrollBar;
            label3.Location = new Point(3, 129);
            label3.Name = "label3";
            label3.Size = new Size(87, 2);
            label3.TabIndex = 3;
            // 
            // button9
            // 
            button9.Location = new Point(3, 487);
            button9.Name = "button9";
            button9.Size = new Size(84, 50);
            button9.TabIndex = 11;
            button9.Text = "Remove Image";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(3, 428);
            button8.Name = "button8";
            button8.Size = new Size(84, 50);
            button8.TabIndex = 10;
            button8.Text = "Class List";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(3, 193);
            button7.Name = "button7";
            button7.Size = new Size(84, 50);
            button7.TabIndex = 9;
            button7.Text = "Single Class Rect";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new Point(3, 370);
            button6.Name = "button6";
            button6.Size = new Size(84, 50);
            button6.TabIndex = 8;
            button6.Text = "Next Img";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new Point(3, 311);
            button5.Name = "button5";
            button5.Size = new Size(84, 50);
            button5.TabIndex = 7;
            button5.Text = "Prev Img";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.Highlight;
            button4.Location = new Point(3, 252);
            button4.Name = "button4";
            button4.Size = new Size(84, 50);
            button4.TabIndex = 6;
            button4.Text = "Edit Rect";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(3, 134);
            button3.Name = "button3";
            button3.Size = new Size(84, 50);
            button3.TabIndex = 5;
            button3.Text = "New Class Rect";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(3, 71);
            button2.Name = "button2";
            button2.Size = new Size(84, 53);
            button2.TabIndex = 4;
            button2.Text = "Label Dir";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(3, 12);
            button1.Name = "button1";
            button1.Size = new Size(84, 53);
            button1.TabIndex = 3;
            button1.Text = "Image Dir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(listBox2);
            panel2.Controls.Add(listBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(935, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(223, 717);
            panel2.TabIndex = 1;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 17;
            listBox2.Location = new Point(0, 29);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(223, 293);
            listBox2.TabIndex = 5;
            listBox2.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new Point(0, 358);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(223, 361);
            listBox1.TabIndex = 4;
            listBox1.DoubleClick += listBox1_DoubleClick;
            listBox1.MouseMove += listBox1_MouseMove;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 9);
            label2.Name = "label2";
            label2.Size = new Size(62, 17);
            label2.TabIndex = 2;
            label2.Text = "Label List";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 338);
            label1.Name = "label1";
            label1.Size = new Size(50, 17);
            label1.TabIndex = 1;
            label1.Text = "File List";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Location = new Point(96, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(833, 717);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { editClassToolStripMenuItem, deleteToolStripMenuItem, toolStripMenuItem1 });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(149, 54);
            contextMenuStrip1.ItemClicked += contextMenuStrip1_ItemClicked;
            // 
            // editClassToolStripMenuItem
            // 
            editClassToolStripMenuItem.Name = "editClassToolStripMenuItem";
            editClassToolStripMenuItem.Size = new Size(148, 22);
            editClassToolStripMenuItem.Text = "Edit Class";
            editClassToolStripMenuItem.Click += editClassToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(148, 22);
            deleteToolStripMenuItem.Text = "Delete Label";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(145, 6);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1158, 717);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            KeyPreview = true;
            Name = "Form1";
            Text = "YoloLabel";
            Load += Form1_Load;
            KeyUp += Form1_KeyUp;
            Resize += Form1_Resize;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button button1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Label label2;
        private Label label1;
        private Button button6;
        private Button button5;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button7;
        private Button button8;
        private ListBox listBox1;
        private ContextMenuStrip contextMenuStrip1;
        private Button button9;
        private ToolStripMenuItem editClassToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ListBox listBox2;
        private Label label5;
        private Label label4;
        private Label label3;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolTip toolTip1;
    }
}
