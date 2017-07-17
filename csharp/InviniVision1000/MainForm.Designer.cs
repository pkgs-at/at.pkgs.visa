namespace Architector.Visa.InviniVision1000
{
    partial class MainForm
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_file = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_saveImageAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_connect = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_identity = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_time = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel_control = new System.Windows.Forms.Panel();
            this.button_updateDisplay = new System.Windows.Forms.Button();
            this.pictureBox_display = new System.Windows.Forms.PictureBox();
            this.saveFileDialog_saveImageAs = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panel_control.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_display)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_file,
            this.toolStripMenuItem_connect});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(944, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem_file
            // 
            this.toolStripMenuItem_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_saveImageAs});
            this.toolStripMenuItem_file.Name = "toolStripMenuItem_file";
            this.toolStripMenuItem_file.Size = new System.Drawing.Size(51, 20);
            this.toolStripMenuItem_file.Text = "File(&F)";
            // 
            // toolStripMenuItem_saveImageAs
            // 
            this.toolStripMenuItem_saveImageAs.Name = "toolStripMenuItem_saveImageAs";
            this.toolStripMenuItem_saveImageAs.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItem_saveImageAs.Text = "Save Image As...(&S)";
            this.toolStripMenuItem_saveImageAs.Click += new System.EventHandler(this.toolStripMenuItem_saveImageAs_Click);
            // 
            // toolStripMenuItem_connect
            // 
            this.toolStripMenuItem_connect.Name = "toolStripMenuItem_connect";
            this.toolStripMenuItem_connect.Size = new System.Drawing.Size(89, 20);
            this.toolStripMenuItem_connect.Text = "Connect...(&C)";
            this.toolStripMenuItem_connect.Click += new System.EventHandler(this.toolStripMenuItem_connect_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_identity,
            this.toolStripStatusLabel_time});
            this.statusStrip.Location = new System.Drawing.Point(0, 540);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(944, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_identity
            // 
            this.toolStripStatusLabel_identity.Name = "toolStripStatusLabel_identity";
            this.toolStripStatusLabel_identity.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel_identity.Text = "-";
            // 
            // toolStripStatusLabel_time
            // 
            this.toolStripStatusLabel_time.Name = "toolStripStatusLabel_time";
            this.toolStripStatusLabel_time.Size = new System.Drawing.Size(12, 17);
            this.toolStripStatusLabel_time.Text = "-";
            // 
            // panel_control
            // 
            this.panel_control.Controls.Add(this.button_updateDisplay);
            this.panel_control.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_control.Location = new System.Drawing.Point(824, 24);
            this.panel_control.Name = "panel_control";
            this.panel_control.Size = new System.Drawing.Size(120, 516);
            this.panel_control.TabIndex = 2;
            // 
            // button_updateDisplay
            // 
            this.button_updateDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_updateDisplay.Location = new System.Drawing.Point(0, 0);
            this.button_updateDisplay.Name = "button_updateDisplay";
            this.button_updateDisplay.Size = new System.Drawing.Size(120, 23);
            this.button_updateDisplay.TabIndex = 0;
            this.button_updateDisplay.Text = "Update Display";
            this.button_updateDisplay.UseVisualStyleBackColor = true;
            this.button_updateDisplay.Click += new System.EventHandler(this.button_updateDisplay_Click);
            // 
            // pictureBox_display
            // 
            this.pictureBox_display.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_display.Location = new System.Drawing.Point(0, 24);
            this.pictureBox_display.Name = "pictureBox_display";
            this.pictureBox_display.Size = new System.Drawing.Size(824, 516);
            this.pictureBox_display.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox_display.TabIndex = 3;
            this.pictureBox_display.TabStop = false;
            // 
            // saveFileDialog_saveImageAs
            // 
            this.saveFileDialog_saveImageAs.DefaultExt = "png";
            this.saveFileDialog_saveImageAs.Filter = "PNG(*.png)|*.png";
            this.saveFileDialog_saveImageAs.Title = "Save Image As...";
            this.saveFileDialog_saveImageAs.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog_saveImageAs_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 562);
            this.Controls.Add(this.pictureBox_display);
            this.Controls.Add(this.panel_control);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "VISA Remote Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel_control.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_display)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_identity;
        private System.Windows.Forms.Panel panel_control;
        private System.Windows.Forms.PictureBox pictureBox_display;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_time;
        private System.Windows.Forms.Button button_updateDisplay;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_file;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_connect;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_saveImageAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_saveImageAs;
    }
}