namespace HexCode.Client
{
    partial class frmOptions
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
            if (disposing && (components != null))
            {
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
            this.txtBlue = new System.Windows.Forms.TextBox();
            this.cmdBlue = new System.Windows.Forms.Button();
            this.cmdRed = new System.Windows.Forms.Button();
            this.txtRed = new System.Windows.Forms.TextBox();
            this.cboMap = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cboBlue = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboRed = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBlue
            // 
            this.txtBlue.Location = new System.Drawing.Point(77, 19);
            this.txtBlue.Name = "txtBlue";
            this.txtBlue.Size = new System.Drawing.Size(266, 20);
            this.txtBlue.TabIndex = 0;
            this.txtBlue.TextChanged += new System.EventHandler(this.txtBlue_TextChanged);
            // 
            // cmdBlue
            // 
            this.cmdBlue.Location = new System.Drawing.Point(349, 17);
            this.cmdBlue.Name = "cmdBlue";
            this.cmdBlue.Size = new System.Drawing.Size(95, 23);
            this.cmdBlue.TabIndex = 2;
            this.cmdBlue.Text = "Durchsuchen";
            this.cmdBlue.UseVisualStyleBackColor = true;
            this.cmdBlue.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdRed
            // 
            this.cmdRed.Location = new System.Drawing.Point(349, 17);
            this.cmdRed.Name = "cmdRed";
            this.cmdRed.Size = new System.Drawing.Size(95, 23);
            this.cmdRed.TabIndex = 5;
            this.cmdRed.Text = "Durchsuchen";
            this.cmdRed.UseVisualStyleBackColor = true;
            this.cmdRed.Click += new System.EventHandler(this.cmdRed_Click);
            // 
            // txtRed
            // 
            this.txtRed.Location = new System.Drawing.Point(80, 19);
            this.txtRed.Name = "txtRed";
            this.txtRed.Size = new System.Drawing.Size(266, 20);
            this.txtRed.TabIndex = 3;
            this.txtRed.TextChanged += new System.EventHandler(this.txtRed_TextChanged);
            // 
            // cboMap
            // 
            this.cboMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMap.FormattingEnabled = true;
            this.cboMap.Location = new System.Drawing.Point(92, 217);
            this.cboMap.Name = "cboMap";
            this.cboMap.Size = new System.Drawing.Size(330, 21);
            this.cboMap.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Map";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(336, 301);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(123, 29);
            this.cmdOK.TabIndex = 8;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.button3_Click);
            // 
            // cboBlue
            // 
            this.cboBlue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBlue.FormattingEnabled = true;
            this.cboBlue.Location = new System.Drawing.Point(77, 47);
            this.cboBlue.Name = "cboBlue";
            this.cboBlue.Size = new System.Drawing.Size(170, 21);
            this.cboBlue.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtRed);
            this.groupBox1.Controls.Add(this.cboRed);
            this.groupBox1.Controls.Add(this.cmdRed);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(453, 93);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spieler 1 (Team Rot)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cboBlue);
            this.groupBox2.Controls.Add(this.txtBlue);
            this.groupBox2.Controls.Add(this.cmdBlue);
            this.groupBox2.Location = new System.Drawing.Point(12, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 100);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spieler 2 (Team Blau)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pfad";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Typ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Typ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Pfad";
            // 
            // cboRed
            // 
            this.cboRed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRed.FormattingEnabled = true;
            this.cboRed.Location = new System.Drawing.Point(80, 45);
            this.cboRed.Name = "cboRed";
            this.cboRed.Size = new System.Drawing.Size(167, 21);
            this.cboRed.TabIndex = 12;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 342);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboMap);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.label3);
            this.Name = "frmOptions";
            this.Text = "frmOptions";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBlue;
        private System.Windows.Forms.Button cmdBlue;
        private System.Windows.Forms.Button cmdRed;
        private System.Windows.Forms.TextBox txtRed;
        private System.Windows.Forms.ComboBox cboMap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.ComboBox cboBlue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboRed;
    }
}