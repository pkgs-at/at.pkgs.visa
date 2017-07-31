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
using System.Text;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Ivi.Visa.Interop;

namespace Architector.Visa.Core
{

    public class VisaManager : IDisposable
    {

        private ResourceManager manager;

        private readonly ConcurrentBag<WeakReference<VisaConnection>> connections;

        private bool disposed;

        public VisaManager()
        {
            this.manager = new ResourceManager();
            this.connections = new ConcurrentBag<WeakReference<VisaConnection>>();
            this.disposed = false;
        }

        /*
         * rule of pattern:
         * 
         * ?               any one character
         * \               escape character
         * [list]          one of l, i, s, t
         * [^list]         none of l, i, s, t
         * *               0 or more occurrences
         * +               1 or more occurrences
         * ExpX|ExpY       ExpX or ExpY
         * (Exp)           grouping
         */
        public string[] FindResources(string pattern)
        {
            return this.manager.FindRsrc(pattern ?? "?*");
        }

        public string[] ListResources()
        {
            return this.FindResources(null);
        }

        public string[] FindInstruments(string pattern)
        {
            StringBuilder builder;

            builder = new StringBuilder(pattern ?? "?*");
            builder.Append("::INSTR");
            return this.FindResources(builder.ToString());
        }

        public string[] ListInstruments()
        {
            return this.FindInstruments(null);
        }

        public VisaConnection Connect(string address)
        {
            FormattedIO488 stream;
            IVisaSession session;
            VisaConnection connection;

            stream = null;
            session = null;
            try
            {
                stream = new FormattedIO488();
                session = this.manager.Open(address);
                stream.IO = (IMessage)session;
            }
            catch
            {
                if (session != null) session.Close();
                if (stream != null) Marshal.ReleaseComObject(stream);
                throw;
            }
            connection = null;
            try
            {
                connection = new VisaConnection(stream);
                this.connections.Add(new WeakReference<VisaConnection>(connection));
                return connection;
            }
            catch
            {
                if (connection != null) connection.Dispose();
                throw;
            }
        }

        protected virtual void DisposeManaged()
        {
            foreach (WeakReference<VisaConnection> reference in this.connections)
            {
                VisaConnection connection;

                if (!reference.TryGetTarget(out connection)) continue;
                connection.Dispose();
            }
        }

        protected virtual void DisposeUnmanaged()
        {
            Marshal.ReleaseComObject(this.manager);
            this.manager = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (this.disposed) return;
                if (disposing) this.DisposeManaged();
                this.DisposeUnmanaged();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

    }

}
