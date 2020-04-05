namespace HexCode.Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.skControl1 = new SkiaSharp.Views.Desktop.SKControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.s = new System.Windows.Forms.GroupBox();
            this.chkShowRobotId = new System.Windows.Forms.CheckBox();
            this.cboIndicator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.chkRandomSeed = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSeed = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.s.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // skControl1
            // 
            this.skControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skControl1.Location = new System.Drawing.Point(0, 0);
            this.skControl1.Name = "skControl1";
            this.skControl1.Size = new System.Drawing.Size(760, 808);
            this.skControl1.TabIndex = 0;
            this.skControl1.Text = "skControl1";
            this.skControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.skControl1_PaintSurface);
            this.skControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.skControl1_KeyUp);
            this.skControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.skControl1_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.skControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1086, 808);
            this.splitContainer1.SplitterDistance = 760;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 808);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.s);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(322, 783);
            this.splitContainer2.SplitterDistance = 133;
            this.splitContainer2.TabIndex = 4;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            // 
            // s
            // 
            this.s.Controls.Add(this.txtSeed);
            this.s.Controls.Add(this.label2);
            this.s.Controls.Add(this.chkRandomSeed);
            this.s.Controls.Add(this.chkShowRobotId);
            this.s.Controls.Add(this.cboIndicator);
            this.s.Controls.Add(this.label1);
            this.s.Dock = System.Windows.Forms.DockStyle.Fill;
            this.s.Location = new System.Drawing.Point(0, 0);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(322, 133);
            this.s.TabIndex = 2;
            this.s.TabStop = false;
            this.s.Text = "Options";
            // 
            // chkShowRobotId
            // 
            this.chkShowRobotId.AutoSize = true;
            this.chkShowRobotId.Location = new System.Drawing.Point(13, 97);
            this.chkShowRobotId.Name = "chkShowRobotId";
            this.chkShowRobotId.Size = new System.Drawing.Size(97, 17);
            this.chkShowRobotId.TabIndex = 2;
            this.chkShowRobotId.Text = "show Robot-ID";
            this.chkShowRobotId.UseVisualStyleBackColor = true;
            this.chkShowRobotId.CheckedChanged += new System.EventHandler(this.chkShowRobotId_CheckedChanged);
            // 
            // cboIndicator
            // 
            this.cboIndicator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIndicator.FormattingEnabled = true;
            this.cboIndicator.Location = new System.Drawing.Point(98, 70);
            this.cboIndicator.Name = "cboIndicator";
            this.cboIndicator.Size = new System.Drawing.Size(120, 21);
            this.cboIndicator.TabIndex = 1;
            this.cboIndicator.SelectedIndexChanged += new System.EventHandler(this.cboIndicator_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Debug Indicator";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 646);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Log";
            // 
            // richTextBox1
            // 
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBox1.Size = new System.Drawing.Size(316, 627);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(322, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // chkRandomSeed
            // 
            this.chkRandomSeed.AutoSize = true;
            this.chkRandomSeed.Location = new System.Drawing.Point(13, 19);
            this.chkRandomSeed.Name = "chkRandomSeed";
            this.chkRandomSeed.Size = new System.Drawing.Size(177, 17);
            this.chkRandomSeed.TabIndex = 3;
            this.chkRandomSeed.Text = "Generate new seed every game";
            this.chkRandomSeed.UseVisualStyleBackColor = true;
            this.chkRandomSeed.CheckedChanged += new System.EventHandler(this.chkRandomSeed_CheckedChanged);
            this.chkRandomSeed.Checked = true;

            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Random Seed";
            // 
            // txtSeed
            // 
            this.txtSeed.Location = new System.Drawing.Point(98, 41);
            this.txtSeed.Name = "txtSeed";
            this.txtSeed.Size = new System.Drawing.Size(74, 20);
            this.txtSeed.TabIndex = 5;
            this.txtSeed.Enabled = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 808);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "HexCode Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.s.ResumeLayout(false);
            this.s.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKControl skControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox s;
        private System.Windows.Forms.ComboBox cboIndicator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowRobotId;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox txtSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRandomSeed;
    }
}

