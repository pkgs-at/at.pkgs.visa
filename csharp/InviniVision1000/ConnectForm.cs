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
using System.Windows.Forms;

namespace Architector.Visa.InviniVision1000
{

    public partial class ConnectForm : Form
    {

        private readonly Backend backend;

        private readonly ErrorForm errorForm;

        public ConnectForm(Backend backend)
        {
            this.backend = backend;
            this.errorForm = new ErrorForm();
            this.InitializeComponent();
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

        private void UpdateInstruments()
        {
            this.button_refresh.Enabled = false;
            this.backend.ListInstruments(
                (string[] list) => this.Invoke(() =>
                {
                    this.comboBox_instruments.Items.Clear();
                    this.comboBox_instruments.Items.AddRange(list);
                }),
                this.ShowBackendError,
                () => this.Invoke(() =>
                {
                    this.button_refresh.Enabled = true;
                }));
        }

        private void ConnectForm_Shown(object sender, EventArgs @event)
        {
            this.UpdateInstruments();
        }

        private void button_refresh_Click(object sender, EventArgs @event)
        {
            this.UpdateInstruments();
        }

        private void button_connect_Click(object sender, EventArgs @event)
        {
            bool succeed;

            succeed = false;
            this.button_connect.Enabled = false;
            this.backend.Connect(
                this.comboBox_instruments.Text,
                () => this.Invoke(() =>
                {
                    succeed = true;
                }),
                this.ShowBackendError,
                () => this.Invoke(() =>
                {
                    this.button_connect.Enabled = true;
                    if (succeed) this.DialogResult = DialogResult.OK;
                }));
        }

    }

}
