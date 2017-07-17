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
using System.Drawing;
using System.Windows.Forms;

namespace Architector.Visa.InviniVision1000
{

    public partial class MainForm : Form
    {

        private readonly Backend backend;

        private readonly ErrorForm errorForm;

        private readonly ConnectForm connectForm;

        private readonly ImageConverter imageConverter;

        private bool shuttingDown;

        public MainForm()
        {
            this.backend = new Backend();
            this.errorForm = new ErrorForm();
            this.connectForm = new ConnectForm(backend);
            this.imageConverter = new ImageConverter();
            this.shuttingDown = false;
            this.InitializeComponent();
            this.toolStripMenuItem_saveImageAs.Enabled = false;
        }

        protected void Invoke(Action action)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                action();
            }));
        }

        protected void ShowBackendError(Exception cause)
        {
            this.Invoke(() =>
            {
                this.errorForm.ShowDialog(this, cause);
            });
        }

        protected void Connect()
        {
            this.toolStripMenuItem_connect.Enabled = false;
            this.connectForm.ShowDialog(this);
            if (!this.backend.Connected)
            {
                this.toolStripMenuItem_connect.Enabled = true;
                this.Close();
                return;
            }
            this.toolStripStatusLabel_identity.Text = "(querying...)";
            this.UpdateDisplay();
            this.backend.QueryString(
                "*IDN?",
                (string value) => this.Invoke(() =>
                {
                    this.toolStripStatusLabel_identity.Text = value.Trim();
                }),
                this.ShowBackendError,
                () => this.Invoke(() =>
                {
                    this.toolStripMenuItem_connect.Enabled = true;
                }));
        }

        protected void UpdateDisplay()
        {
            DateTime start;

            start = DateTime.Now;
            this.button_updateDisplay.Enabled = false;
            this.backend.QueryBinary(
                ":DISPlay:DATA? PNG",
                (byte[] binary) => this.Invoke(() =>
                {
                    this.pictureBox_display.Image = (Image)this.imageConverter.ConvertFrom(binary);
                    this.toolStripMenuItem_saveImageAs.Enabled = true;
                    this.toolStripStatusLabel_time.Text = (DateTime.Now - start).ToString();
                }),
                this.ShowBackendError,
                () => this.Invoke(() =>
                {
                    this.button_updateDisplay.Enabled = true;
                }));
        }

        private void MainForm_Shown(object sender, EventArgs @event)
        {
            this.Connect();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs @event)
        {
            if (this.shuttingDown) return;
            @event.Cancel = true;
            this.backend.Shutdown(
                null,
                this.ShowBackendError,
                () => this.Invoke(() =>
                {
                    this.shuttingDown = true;
                    this.Close();
                }));
        }

        private void button_updateDisplay_Click(object sender, EventArgs @event)
        {
            this.UpdateDisplay();
        }

        private void toolStripMenuItem_connect_Click(object sender, EventArgs e)
        {
            this.Connect();
        }

        private void toolStripMenuItem_saveImageAs_Click(object sender, EventArgs e)
        {
            this.saveFileDialog_saveImageAs.ShowDialog(this);
        }

        private void saveFileDialog_saveImageAs_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.pictureBox_display.Image.Save(this.saveFileDialog_saveImageAs.FileName);
        }

    }

}
