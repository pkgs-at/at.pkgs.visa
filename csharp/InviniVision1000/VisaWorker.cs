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
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading;

namespace Architector.Visa.InviniVision1000
{

    public partial class VisaWorker
    {

        public interface ICallerThread
        {

            void Invoke(Action action);

        }

        public class Frontend
        {

            private readonly VisaWorker worker;

            private readonly ICallerThread caller;

            public Frontend(VisaWorker worker, ICallerThread caller)
            {
                this.worker = worker;
                this.caller = caller;
            }

            public VisaWorker Worker
            {
                get { return this.worker; }
            }

            public ICallerThread Caller
            {
                get { return this.caller; }
            }

            public IBuilder<Action<int>> TInt()
            {
                return new Builder<int>(this, () =>
                {
                    return 1;
                });
            }

        }

        public Frontend For(ICallerThread caller)
        {
            return new Frontend(this, caller);
        }

        private readonly BlockingCollection<IFuture> queue;

        private readonly Thread worker;

        public void Invoke(IFuture future)
        {
            this.queue.Add(future);
        }

    }

}
