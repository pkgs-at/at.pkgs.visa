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
using System.Collections.Concurrent;
using System.Threading;
using System.Runtime.InteropServices;
using Ivi.Visa.Interop;

namespace Architector.Visa.InviniVision1000
{

    public class Backend
    {

        public class Context
        {

            public bool Active;

            public ResourceManager Manager;

            public FormattedIO488 Connection;

            public volatile bool Connected;

        }

        public abstract class AbstractEntry
        {

            private readonly Action<Exception> error;

            private readonly Action complete;

            public AbstractEntry(Action<Exception> error, Action complete)
            {
                this.error = error;
                this.complete = complete;
            }

            protected abstract void Execute(Context context);

            public void Run(Context context)
            {
                try
                {
                    this.Execute(context);
                }
                catch (Exception cause)
                {
                    this.error?.Invoke(cause);
                }
                finally
                {
                    this.complete?.Invoke();
                }
            }

        }

        public class ListInstrumentsEntry : AbstractEntry
        {

            private readonly Action<string[]> succeed;

            public ListInstrumentsEntry(Action<string[]> succeed, Action<Exception> error, Action complete)
                : base(error, complete)
            {
                this.succeed = succeed;
            }

            protected override void Execute(Context context)
            {
                this.succeed?.Invoke(context.Manager.FindRsrc("?*::INSTR"));
            }

        }

        public class ConnectEntry : AbstractEntry
        {

            private readonly string address;

            private readonly Action succeed;

            public ConnectEntry(string address, Action succeed, Action<Exception> error, Action complete)
                : base(error, complete)
            {
                this.address = address;
                this.succeed = succeed;
            }

            protected override void Execute(Context context)
            {
                FormattedIO488 connection;

                if (context.Connection != null)
                {
                    context.Connection.IO.Close();
                    Marshal.ReleaseComObject(context.Connection);
                    context.Connection = null;
                    context.Connected = false;
                }
                connection = new FormattedIO488();
                try
                {
                    connection.IO = (IMessage)context.Manager.Open(this.address);
                }
                catch (Exception)
                {
                    Marshal.ReleaseComObject(connection);
                    throw;
                }
                context.Connection = connection;
                context.Connected = true;
                this.succeed?.Invoke();
            }

        }

        public class QueryStringEntry : AbstractEntry
        {

            private readonly string query;

            private readonly Action<string> succeed;

            public QueryStringEntry(string query, Action<string> succeed, Action<Exception> error, Action complete)
                : base(error, complete)
            {
                this.query = query;
                this.succeed = succeed;
            }

            protected override void Execute(Context context)
            {
                string text;

                context.Connection.WriteString(query);
                text = context.Connection.ReadString();
                this.succeed?.Invoke(text);
            }

        }

        public class QueryBinaryEntry : AbstractEntry
        {

            private readonly string query;

            private readonly Action<byte[]> succeed;

            public QueryBinaryEntry(string query, Action<byte[]> succeed, Action<Exception> error, Action complete)
                : base(error, complete)
            {
                this.query = query;
                this.succeed = succeed;
            }

            protected override void Execute(Context context)
            {
                byte[] binary;

                context.Connection.WriteString(query);
                binary = context.Connection.ReadIEEEBlock(IEEEBinaryType.BinaryType_UI1);
                this.succeed?.Invoke(binary);
            }

        }

        public class ShutdownEntry : AbstractEntry
        {

            private readonly Action succeed;

            public ShutdownEntry(Action succeed, Action<Exception> error, Action complete)
                : base(error, complete)
            {
                this.succeed = succeed;
            }

            protected override void Execute(Context context)
            {
                context.Active = false;
                this.succeed?.Invoke();
            }

        }

        private readonly Context context;

        private readonly BlockingCollection<AbstractEntry> queue;

        private readonly Thread worker;

        public Backend()
        {
            this.context = new Context();
            this.queue = new BlockingCollection<AbstractEntry>(new ConcurrentQueue<AbstractEntry>());
            this.worker = new Thread(this.Run);
            this.worker.Start();
        }

        public bool Connected
        {
            get
            {
                return this.context.Connected;
            }
        }

        public void Run()
        {
            try
            {
                this.context.Active = true;
                this.context.Connected = false;
                this.context.Manager = new ResourceManager();
                while (this.context.Active)
                {
                    this.queue.Take().Run(this.context);
                }
            }
            finally
            {
                if (this.context.Connection != null)
                {
                    this.context.Connection.IO.Close();
                    Marshal.ReleaseComObject(this.context.Connection);
                    this.context.Connection = null;
                    this.context.Connected = false;
                }
                if (this.context.Manager != null)
                {
                    Marshal.ReleaseComObject(this.context.Manager);
                    this.context.Manager = null;
                }
            }
        }

        public void Invoke(AbstractEntry entry)
        {
            this.queue.Add(entry);
        }

        public void ListInstruments(Action<string[]> success, Action<Exception> error, Action complete)
        {
            this.Invoke(new ListInstrumentsEntry(success, error, complete));
        }

        public void Connect(string address, Action success, Action<Exception> error, Action complete)
        {
            this.Invoke(new ConnectEntry(address, success, error, complete));
        }

        public void QueryString(string query, Action<string> success, Action<Exception> error, Action complete)
        {
            this.Invoke(new QueryStringEntry(query, success, error, complete));
        }

        public void QueryBinary(string query, Action<byte[]> success, Action<Exception> error, Action complete)
        {
            this.Invoke(new QueryBinaryEntry(query, success, error, complete));
        }

        public void Shutdown(Action success, Action<Exception> error, Action complete)
        {
            this.Invoke(new ShutdownEntry(success, error, complete));
        }

    }

}
