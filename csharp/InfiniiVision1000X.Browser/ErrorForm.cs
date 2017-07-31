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

using System.Windows.Forms;

namespace Architector.Visa.InfiniiVision1000X.Browser
{

    public partial class ErrorForm : Form
    {

        public ErrorForm()
        {
            this.InitializeComponent();
        }

        public DialogResult ShowDialog(string message, IWin32Window parent)
        {
            this.MessageTextBox.Text = message;
            return this.ShowDialog(parent);
        }

        public DialogResult ShowDialog(string message)
        {
            return this.ShowDialog(message, null);
        }

    }

}
