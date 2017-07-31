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

namespace Architector.Visa.Core
{

    public partial class VisaWorker
    {

        public interface ICallerThread
        {

            void Invoke(Action action);

        }

        private readonly VisaConnection connection;

        private readonly Func<Exception, bool> error;

        private readonly BlockingCollection<IFuture> queue;

        private readonly Thread worker;

        private bool active;

        public VisaWorker(
            VisaConnection connection,
            Func<Exception, bool> error)
        {
            this.connection = connection;
            this.error = error;
            this.queue = new BlockingCollection<IFuture>(new ConcurrentQueue<IFuture>());
            this.worker = new Thread(this.Run);
            this.worker.Start();
        }

        public VisaWorker(
            VisaConnection connection)
            : this(connection, null)
        { }

        public VisaConnection Connection
        {
            get { return this.connection; }
        }

        public void Invoke(IFuture future)
        {
            if (Thread.CurrentThread.ManagedThreadId == this.worker.ManagedThreadId)
                future.Run();
            else
                this.queue.Add(future);
        }

        public void Shutdown()
        {
            this.Invoke(new FutureAction(() =>
            {
                this.active = false;
            }));
        }

        public void Run()
        {
            try
            {
                this.active = true;
                while (this.active)
                {
                    try
                    {
                        this.queue.Take().Run();
                    }
                    catch (Exception cause)
                    {
                        if (this.error != null && this.error(cause)) continue;
                        throw;
                    }
                }
            }
            finally
            {
                this.Connection.Dispose();
            }
        }

        public Frontend For(ICallerThread caller)
        {
            return new Frontend(this, caller);
        }

    }

}
