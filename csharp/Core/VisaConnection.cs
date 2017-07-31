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
using System.Runtime.InteropServices;
using Ivi.Visa.Interop;

namespace Architector.Visa.Core
{

    public class VisaConnection : IDisposable
    {

        private IFormattedIO488 stream;

        private bool disposed;

        public VisaConnection(IFormattedIO488 stream)
        {
            this.stream = stream;
            this.disposed = false;
        }

        public VisaError.Status QuerySystemError()
        {
            this.stream.WriteString(":SYSTem:ERRor?");
            return VisaError.Status.Parse(this.stream.ReadString());
        }

        public void CheckSystemError()
        {
            IList<VisaError.Status> statuses;

            statuses = new List<VisaError.Status>();
            while (true)
            {
                VisaError.Status status;

                status = this.QuerySystemError();
                if (!status.IsError) break;
                statuses.Add(status);
            }
            if (statuses.Count > 0) throw new VisaError(statuses);
        }

        public void Command(string command)
        {
            this.stream.WriteString(command);
            this.CheckSystemError();
        }

        public void Command(string format, params object[] arguments)
        {
            this.Command(String.Format(format, arguments));
        }

        public void Command(string command, byte[] binary)
        {
            this.stream.WriteIEEEBlock(command, binary);
            this.CheckSystemError();
        }

        public void Command(string format, byte[] binary, params object[] arguments)
        {
            this.Command(String.Format(format, arguments), binary);
        }

        public string QueryString(string query)
        {
            string value;

            this.stream.WriteString(query);
            value = this.stream.ReadString();
            this.CheckSystemError();
            return value;
        }

        public string QueryString(string format, params object[] arguments)
        {
            return this.QueryString(String.Format(format, arguments));
        }

        public byte[] QueryBinary(string query)
        {
            byte[] value;

            this.stream.WriteString(query);
            value = this.stream.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1);
            this.CheckSystemError();
            return value;
        }

        public byte[] QueryBinary(string format, params object[] arguments)
        {
            return this.QueryBinary(String.Format(format, arguments));
        }

        protected virtual void DisposeManaged() { }

        protected virtual void DisposeUnmanaged()
        {
            this.stream.IO.Close();
            Marshal.ReleaseComObject(this.stream);
            this.stream = null;
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
