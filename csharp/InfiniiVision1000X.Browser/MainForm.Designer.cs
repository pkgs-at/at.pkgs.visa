namespace Architector.Visa.InfiniiVision1000X.Browser
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveDisplayAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveSetupAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.DisplaySaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SetupSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.DisplayPictureBox = new System.Windows.Forms.PictureBox();
            this.UpdateDisplayButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(960, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveDisplayAsToolStripMenuItem,
            this.SaveSetupAsToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.FileToolStripMenuItem.Text = "File(&F)";
            // 
            // SaveDisplayAsToolStripMenuItem
            // 
            this.SaveDisplayAsToolStripMenuItem.Name = "SaveDisplayAsToolStripMenuItem";
            this.SaveDisplayAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SaveDisplayAsToolStripMenuItem.Text = "Save Display As...(&D)";
            this.SaveDisplayAsToolStripMenuItem.Click += new System.EventHandler(this.SaveDisplayAsToolStripMenuItem_Click);
            // 
            // SaveSetupAsToolStripMenuItem
            // 
            this.SaveSetupAsToolStripMenuItem.Name = "SaveSetupAsToolStripMenuItem";
            this.SaveSetupAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.SaveSetupAsToolStripMenuItem.Text = "Save Setup As...(&S)";
            this.SaveSetupAsToolStripMenuItem.Click += new System.EventHandler(this.SaveSetupAsToolStripMenuItem_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Location = new System.Drawing.Point(0, 618);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(960, 22);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "StatusStrip";
            // 
            // DisplaySaveFileDialog
            // 
            this.DisplaySaveFileDialog.DefaultExt = "png";
            this.DisplaySaveFileDialog.Filter = "PNG (*.png)|*.png";
            this.DisplaySaveFileDialog.Title = "Save Display As...";
            this.DisplaySaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.DisplaySaveFileDialog_FileOk);
            // 
            // SetupSaveFileDialog
            // 
            this.SetupSaveFileDialog.DefaultExt = "dat";
            this.SetupSaveFileDialog.Filter = "DAT (*.dat)|*.dat|All Files (*.*)|*.*";
            this.SetupSaveFileDialog.Title = "Save Setup As...";
            this.SetupSaveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SetupSaveFileDialog_FileOk);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.UpdateDisplayButton);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlPanel.Location = new System.Drawing.Point(840, 24);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(120, 594);
            this.ControlPanel.TabIndex = 2;
            // 
            // DisplayPictureBox
            // 
            this.DisplayPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayPictureBox.Location = new System.Drawing.Point(0, 24);
            this.DisplayPictureBox.Name = "DisplayPictureBox";
            this.DisplayPictureBox.Size = new System.Drawing.Size(840, 594);
            this.DisplayPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.DisplayPictureBox.TabIndex = 3;
            this.DisplayPictureBox.TabStop = false;
            // 
            // UpdateDisplayButton
            // 
            this.UpdateDisplayButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.UpdateDisplayButton.Location = new System.Drawing.Point(0, 0);
            this.UpdateDisplayButton.Name = "UpdateDisplayButton";
            this.UpdateDisplayButton.Size = new System.Drawing.Size(120, 23);
            this.UpdateDisplayButton.TabIndex = 0;
            this.UpdateDisplayButton.Text = "Update Display";
            this.UpdateDisplayButton.UseVisualStyleBackColor = true;
            this.UpdateDisplayButton.Click += new System.EventHandler(this.UpdateDisplayButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Architector.Visa.InfiniiVision1000X.Browser.Properties.Settings.Default.MainForm_ClientSize;
            this.Controls.Add(this.DisplayPictureBox);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Architector.Visa.InfiniiVision1000X.Browser.Properties.Settings.Default, "MainForm_Location", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Architector.Visa.InfiniiVision1000X.Browser.Properties.Settings.Default, "MainForm_ClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Architector.Visa.InfiniiVision1000X.Browser.Properties.Settings.Default.MainForm_Location;
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "MainForm";
            this.Text = "InviniiVision 1000X Briwser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.MenuStrip MenuStrip;
        protected System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem SaveDisplayAsToolStripMenuItem;
        protected System.Windows.Forms.StatusStrip StatusStrip;
        protected System.Windows.Forms.ToolStripMenuItem SaveSetupAsToolStripMenuItem;
        protected System.Windows.Forms.SaveFileDialog DisplaySaveFileDialog;
        protected System.Windows.Forms.SaveFileDialog SetupSaveFileDialog;
        protected System.Windows.Forms.Panel ControlPanel;
        protected System.Windows.Forms.PictureBox DisplayPictureBox;
        protected System.Windows.Forms.Button UpdateDisplayButton;
    }
}