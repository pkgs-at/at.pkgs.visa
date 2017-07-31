/*
 * Copyright (c) 2009-2017, Architector Inc., Japan
 * All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Architector.Visa.Core;

namespace Architector.Visa.InfiniiVision1000X.Browser
{

    public partial class MainForm : Form, VisaWorker.ICallerThread
    {

        private readonly ErrorForm errorForm;

        private readonly ImageConverter imageConverter;

        private readonly VisaWorker.Frontend frontend;

        private bool teardown;

        public MainForm(VisaConnection connection)
        {
            this.errorForm = new ErrorForm();
            this.imageConverter = new ImageConverter();
            this.frontend = new VisaWorker(connection, (Exception cause) =>
            {
                this.ShowErrorDialog(cause);
                return true;
            }).For(this);
            this.teardown = false;
            this.InitializeComponent();
            this.SaveDisplayAsToolStripMenuItem.Enabled = false;
        }

        public void Invoke(Action action)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    action();
                }));
            }
            else
            {
                action();
            }
        }

        public void ShowErrorDialog(Exception cause)
        {
            this.Invoke(() =>
            {
                this.errorForm.ShowDialog(cause.ToString(), this);
            });
        }

        private void ShowSaveFileDialog(SaveFileDialog dialog)
        {
            dialog.FileName = null;
            dialog.InitialDirectory = Properties.Settings.Default.Program_FilePath;
            if (dialog.ShowDialog(this) == DialogResult.OK)
                Properties.Settings.Default.Program_FilePath = Path.GetDirectoryName(Path.GetFullPath(dialog.FileName));
        }

        private void UpdateDisplay()
        {
            this.UpdateDisplayButton.Enabled = false;
            this.frontend.QueryBinary(":DISPlay:DATA? PNG").OnFail(this.ShowErrorDialog).OnSuccess((byte[] value) =>
            {
                this.DisplayPictureBox.Image = (Image)this.imageConverter.ConvertFrom(value);
                this.SaveDisplayAsToolStripMenuItem.Enabled = true;
            }).OnComplete(() =>
            {
                this.UpdateDisplayButton.Enabled = true;
            }).Invoke();
        }

        private void Settings_SettingChanging(object sender, SettingChangingEventArgs @event)
        {
            switch (@event.SettingName)
            {
                case "MainForm_Location":
                    break;
                case "MainForm_ClientSize":
                    break;
                default:
                    return;
            }
            @event.Cancel = (this.WindowState != FormWindowState.Normal);
        }

        private void MainForm_Load(object sender, EventArgs @event)
        {
            Properties.Settings.Default.SettingChanging += this.Settings_SettingChanging;
        }

        private void MainForm_Shown(object sender, EventArgs @event)
        {
            this.frontend.QueryString("*IDN?").OnFail(this.ShowErrorDialog).OnSuccess((string value) =>
            {
                this.Text += String.Format("{0} | {1}", this.Text, value);
            }).Invoke();
            this.UpdateDisplay();
        }

        private void SaveDisplayAsToolStripMenuItem_Click(object sender, EventArgs @event)
        {
            this.ShowSaveFileDialog(this.DisplaySaveFileDialog);
        }

        private void DisplaySaveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs @event)
        {
            string file;

            file = Path.GetFullPath(this.DisplaySaveFileDialog.FileName);
            this.DisplayPictureBox.Image.Save(file, ImageFormat.Png);
        }

        private void SaveSetupAsToolStripMenuItem_Click(object sender, EventArgs @event)
        {
            this.ShowSaveFileDialog(this.SetupSaveFileDialog);
        }

        private void SetupSaveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs @event)
        {
            string file;

            file = Path.GetFullPath(this.SetupSaveFileDialog.FileName);
            this.frontend.QueryBinary(":SYSTem:SETup?").OnFail(this.ShowErrorDialog).OnSuccess((byte[] value) =>
            {
                File.WriteAllBytes(file, value);
            }).Invoke();
        }

        private void UpdateDisplayButton_Click(object sender, EventArgs @event)
        {
            this.UpdateDisplay();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs @event)
        {
            if (this.teardown)
            {
                Properties.Settings.Default.SettingChanging -= this.Settings_SettingChanging;
                return;
            }
            else
            {
                @event.Cancel = true;
                this.frontend.Shutdown().OnFail(this.ShowErrorDialog).OnSuccess(() =>
                {
                    this.teardown = true;
                    this.Close();
                }).Invoke();
            }
        }

    }

}
