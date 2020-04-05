namespace HexCode.Client
{
    partial class MapEditor
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
            this.grMap = new System.Windows.Forms.GroupBox();
            this.cmdRandom = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.cmdSaveMap = new System.Windows.Forms.Button();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.cmdNewMap = new System.Windows.Forms.Button();
            this.cmdLoadMap = new System.Windows.Forms.Button();
            this.grTile = new System.Windows.Forms.GroupBox();
            this.cmdRed = new System.Windows.Forms.Button();
            this.cmdBlue = new System.Windows.Forms.Button();
            this.cmdWater = new System.Windows.Forms.Button();
            this.cmdMountain = new System.Windows.Forms.Button();
            this.cmdTerrain = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRobotsPerTeam = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grMap.SuspendLayout();
            this.grTile.SuspendLayout();
            this.SuspendLayout();
            // 
            // skControl1
            // 
            this.skControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skControl1.Location = new System.Drawing.Point(0, 0);
            this.skControl1.Name = "skControl1";
            this.skControl1.Size = new System.Drawing.Size(878, 808);
            this.skControl1.TabIndex = 0;
            this.skControl1.Text = "skControl1";
            this.skControl1.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.skControl1_PaintSurface);
            this.skControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.skControl1_MouseDown);
            this.skControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.skControl1_MouseMove);
            this.skControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.skControl1_MouseUp);
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
            this.splitContainer1.SplitterDistance = 878;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(204, 808);
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
            this.splitContainer2.Panel1.Controls.Add(this.grMap);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grTile);
            this.splitContainer2.Size = new System.Drawing.Size(204, 783);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.TabIndex = 4;
            // 
            // grMap
            // 
            this.grMap.Controls.Add(this.label3);
            this.grMap.Controls.Add(this.txtRobotsPerTeam);
            this.grMap.Controls.Add(this.cmdRandom);
            this.grMap.Controls.Add(this.label2);
            this.grMap.Controls.Add(this.label1);
            this.grMap.Controls.Add(this.txtHeight);
            this.grMap.Controls.Add(this.txtWidth);
            this.grMap.Controls.Add(this.cmdSaveMap);
            this.grMap.Controls.Add(this.txtMapName);
            this.grMap.Controls.Add(this.cmdNewMap);
            this.grMap.Controls.Add(this.cmdLoadMap);
            this.grMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grMap.Location = new System.Drawing.Point(0, 0);
            this.grMap.Name = "grMap";
            this.grMap.Size = new System.Drawing.Size(204, 250);
            this.grMap.TabIndex = 2;
            this.grMap.TabStop = false;
            this.grMap.Text = "Map";
            // 
            // cmdRandom
            // 
            this.cmdRandom.AutoSize = true;
            this.cmdRandom.Location = new System.Drawing.Point(6, 172);
            this.cmdRandom.Name = "cmdRandom";
            this.cmdRandom.Size = new System.Drawing.Size(85, 23);
            this.cmdRandom.TabIndex = 12;
            this.cmdRandom.Text = "Random";
            this.cmdRandom.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Size (w/h)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "MapName";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(135, 112);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(47, 20);
            this.txtHeight.TabIndex = 9;
            this.txtHeight.Text = "35";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(70, 112);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(47, 20);
            this.txtWidth.TabIndex = 8;
            this.txtWidth.Text = "25";
            // 
            // cmdSaveMap
            // 
            this.cmdSaveMap.AutoSize = true;
            this.cmdSaveMap.Location = new System.Drawing.Point(97, 48);
            this.cmdSaveMap.Name = "cmdSaveMap";
            this.cmdSaveMap.Size = new System.Drawing.Size(85, 23);
            this.cmdSaveMap.TabIndex = 7;
            this.cmdSaveMap.Text = "save Map";
            this.cmdSaveMap.UseVisualStyleBackColor = true;
            // 
            // txtMapName
            // 
            this.txtMapName.Location = new System.Drawing.Point(70, 86);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(112, 20);
            this.txtMapName.TabIndex = 3;
            // 
            // cmdNewMap
            // 
            this.cmdNewMap.AutoSize = true;
            this.cmdNewMap.Location = new System.Drawing.Point(6, 19);
            this.cmdNewMap.Name = "cmdNewMap";
            this.cmdNewMap.Size = new System.Drawing.Size(85, 23);
            this.cmdNewMap.TabIndex = 5;
            this.cmdNewMap.Text = "new map";
            this.cmdNewMap.UseVisualStyleBackColor = true;
            // 
            // cmdLoadMap
            // 
            this.cmdLoadMap.AutoSize = true;
            this.cmdLoadMap.Location = new System.Drawing.Point(6, 48);
            this.cmdLoadMap.Name = "cmdLoadMap";
            this.cmdLoadMap.Size = new System.Drawing.Size(85, 23);
            this.cmdLoadMap.TabIndex = 6;
            this.cmdLoadMap.Text = "load Map";
            this.cmdLoadMap.UseVisualStyleBackColor = true;
            // 
            // grTile
            // 
            this.grTile.Controls.Add(this.cmdRed);
            this.grTile.Controls.Add(this.cmdBlue);
            this.grTile.Controls.Add(this.cmdWater);
            this.grTile.Controls.Add(this.cmdMountain);
            this.grTile.Controls.Add(this.cmdTerrain);
            this.grTile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grTile.Location = new System.Drawing.Point(0, 0);
            this.grTile.Name = "grTile";
            this.grTile.Size = new System.Drawing.Size(204, 529);
            this.grTile.TabIndex = 3;
            this.grTile.TabStop = false;
            this.grTile.Text = "Tile";
            // 
            // cmdRed
            // 
            this.cmdRed.AutoSize = true;
            this.cmdRed.Location = new System.Drawing.Point(6, 128);
            this.cmdRed.Name = "cmdRed";
            this.cmdRed.Size = new System.Drawing.Size(85, 23);
            this.cmdRed.TabIndex = 4;
            this.cmdRed.Text = "red Start";
            this.cmdRed.UseVisualStyleBackColor = true;
            // 
            // cmdBlue
            // 
            this.cmdBlue.AutoSize = true;
            this.cmdBlue.Location = new System.Drawing.Point(6, 105);
            this.cmdBlue.Name = "cmdBlue";
            this.cmdBlue.Size = new System.Drawing.Size(85, 23);
            this.cmdBlue.TabIndex = 3;
            this.cmdBlue.Text = "blue Start";
            this.cmdBlue.UseVisualStyleBackColor = true;
            // 
            // cmdWater
            // 
            this.cmdWater.AutoSize = true;
            this.cmdWater.Location = new System.Drawing.Point(6, 65);
            this.cmdWater.Name = "cmdWater";
            this.cmdWater.Size = new System.Drawing.Size(85, 23);
            this.cmdWater.TabIndex = 2;
            this.cmdWater.Text = "Water";
            this.cmdWater.UseVisualStyleBackColor = true;
            // 
            // cmdMountain
            // 
            this.cmdMountain.AutoSize = true;
            this.cmdMountain.Location = new System.Drawing.Point(6, 42);
            this.cmdMountain.Name = "cmdMountain";
            this.cmdMountain.Size = new System.Drawing.Size(85, 23);
            this.cmdMountain.TabIndex = 1;
            this.cmdMountain.Text = "Mountain";
            this.cmdMountain.UseVisualStyleBackColor = true;
            // 
            // cmdTerrain
            // 
            this.cmdTerrain.AutoSize = true;
            this.cmdTerrain.Location = new System.Drawing.Point(6, 19);
            this.cmdTerrain.Name = "cmdTerrain";
            this.cmdTerrain.Size = new System.Drawing.Size(85, 23);
            this.cmdTerrain.TabIndex = 0;
            this.cmdTerrain.Text = "Terrain";
            this.cmdTerrain.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(204, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Robots per team";
            // 
            // txtRobotsPerTeam
            // 
            this.txtRobotsPerTeam.Location = new System.Drawing.Point(135, 138);
            this.txtRobotsPerTeam.Name = "txtRobotsPerTeam";
            this.txtRobotsPerTeam.Size = new System.Drawing.Size(47, 20);
            this.txtRobotsPerTeam.TabIndex = 13;
            // 
            // MapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 808);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MapEditor";
            this.Text = "MapEditor";
            this.Load += new System.EventHandler(this.MapEditor_Load);
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
            this.grMap.ResumeLayout(false);
            this.grMap.PerformLayout();
            this.grTile.ResumeLayout(false);
            this.grTile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SkiaSharp.Views.Desktop.SKControl skControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox grTile;
        private System.Windows.Forms.GroupBox grMap;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdWater;
        private System.Windows.Forms.Button cmdMountain;
        private System.Windows.Forms.Button cmdTerrain;
        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.Button cmdSaveMap;
        private System.Windows.Forms.Button cmdLoadMap;
        private System.Windows.Forms.Button cmdNewMap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.Button cmdRed;
        private System.Windows.Forms.Button cmdBlue;
        private System.Windows.Forms.Button cmdRandom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRobotsPerTeam;
    }
}

