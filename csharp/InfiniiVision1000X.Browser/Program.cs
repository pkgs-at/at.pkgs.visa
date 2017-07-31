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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Architector.Visa.Core;

namespace Architector.Visa.InfiniiVision1000X.Browser
{

    public class Program : IDisposable
    {

        private readonly IReadOnlyList<string> arguments;

        private readonly VisaManager manager;

        private readonly ConnectForm connectForm;

        private bool disposed;

        private Program(string[] arguments)
        {
            this.arguments = new ReadOnlyCollection<string>(arguments);
            this.manager = new VisaManager();
            this.connectForm = new ConnectForm(this.manager);
            this.disposed = false;
        }

        public IReadOnlyList<string> Arguments
        {
            get { return this.arguments; }
        }

        private VisaConnection Connect(string address)
        {
            try
            {
                return this.manager.Connect(address);
            }
            catch (Exception cause)
            {
                using (ErrorForm form = new Browser.ErrorForm())
                {
                    form.ShowDialog(cause.ToString(), this.connectForm);
                }
                return null;
            }
        }

        private int Main()
        {
            VisaConnection connection;

            connection = this.Arguments.Count > 0 ? this.Connect(this.arguments[0]) : null;
            while (connection == null)
            {
                if (this.connectForm.ShowDialog() != DialogResult.OK) return 0;
                connection = this.Connect(this.connectForm.Address);
            }
            using (connection)
            {
                Application.Run(new MainForm(connection));
            }
            return 0;
        }

        public void Dispose()
        {
            if (this.disposed) return;
            this.manager.Dispose();
            Properties.Settings.Default.Save();
            this.disposed = true;
        }

        [STAThread]
        public static int Main(params string[] arguments)
        {
            if (Properties.Settings.Default.Program_SettingsUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Program_SettingsUpgrade = false;
                Properties.Settings.Default.Save();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Program program = new Program(arguments))
            {
                return program.Main();
            }
        }

    }

}
