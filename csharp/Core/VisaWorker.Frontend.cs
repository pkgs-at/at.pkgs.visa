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

namespace Architector.Visa.Core
{

    public partial class VisaWorker
    {

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

            public IBuilder<Action> Command(string command)
            {
                return new Builder(this, () =>
                {
                    this.Worker.Connection.Command(command);
                });
            }

            public IBuilder<Action> Command(string format, params object[] arguments)
            {
                return new Builder(this, () =>
                {
                    this.Worker.Connection.Command(format, arguments);
                });
            }

            public IBuilder<Action> Command(string command, byte[] binary)
            {
                return new Builder(this, () =>
                {
                    this.Worker.Connection.Command(command, binary);
                });
            }

            public IBuilder<Action> Command(string format, byte[] binary, params object[] arguments)
            {
                return new Builder(this, () =>
                {
                    this.Worker.Connection.Command(format, binary, arguments);
                });
            }

            public IBuilder<Action<string>> QueryString(string query)
            {
                return new Builder<string>(this, () =>
                {
                    return this.Worker.Connection.QueryString(query);
                });
            }

            public IBuilder<Action<string>> QueryString(string format, params object[] arguments)
            {
                return new Builder<string>(this, () =>
                {
                    return this.Worker.Connection.QueryString(format, arguments);
                });
            }

            public IBuilder<Action<byte[]>> QueryBinary(string query)
            {
                return new Builder<byte[]>(this, () =>
                {
                    return this.Worker.Connection.QueryBinary(query);
                });
            }

            public IBuilder<Action<byte[]>> QueryBinary(string format, params object[] arguments)
            {
                return new Builder<byte[]>(this, () =>
                {
                    return this.Worker.Connection.QueryBinary(format, arguments);
                });
            }

            public IBuilder<Action> Shutdown()
            {
                return new Builder(this, () =>
                {
                    this.Worker.Shutdown();
                });
            }

        }

    }

}
