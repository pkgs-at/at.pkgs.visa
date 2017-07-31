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
using Architector.Visa.Core;

namespace Architector.Visa.InfiniiVision1000X.Browser
{

    public partial class ConnectForm : Form
    {

        private readonly VisaManager manager;

        public ConnectForm(VisaManager manager)
        {
            this.manager = manager;
            this.InitializeComponent();
        }

        public string Address
        {
            get { return this.AddressComboBox.Text; }
        }

        private void RefreshAddress()
        {
            string text;

            text = this.AddressComboBox.Text;
            this.AddressComboBox.SelectedIndex = -1;
            this.AddressComboBox.Text = text;
            this.AddressComboBox.Items.Clear();
            this.AddressComboBox.Items.AddRange(this.manager.ListInstruments());
        }

        private void ConnectForm_Shown(object sender, EventArgs @event)
        {
            this.RefreshAddress();
        }

        private void RefreshButton_Click(object sender, EventArgs @event)
        {
            this.RefreshAddress();
        }

        private void ConnectButton_Click(object sender, EventArgs @event)
        {
            this.DialogResult = DialogResult.OK;
        }

    }

}
