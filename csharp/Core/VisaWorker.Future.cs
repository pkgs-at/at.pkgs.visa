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

namespace Architector.Visa.Core
{

    public partial class VisaWorker
    {

        public interface IFuture
        {

            void Run();

        }

        public class FutureAction : IFuture
        {

            private readonly Action action;

            public FutureAction(Action action)
            {
                this.action = action;
            }

            public void Run()
            {
                this.action();
            }

        }

        public abstract class AbstractFuture<ProcessType, ActionType> : IFuture
        {

            private readonly Frontend frontend;

            private readonly ProcessType process;

            private readonly IReadOnlyList<ActionType> succeed;

            private readonly IReadOnlyList<Action<Exception>> failed;

            private readonly IReadOnlyList<Action> completed;

            public AbstractFuture(
                Frontend frontend,
                ProcessType process,
                IEnumerable<ActionType> succeed,
                IEnumerable<Action<Exception>> failed,
                IEnumerable<Action> completed)
            {
                this.frontend = frontend;
                this.process = process;
                this.succeed = new ReadOnlyCollection<ActionType>(new List<ActionType>(succeed));
                this.failed = new ReadOnlyCollection<Action<Exception>>(new List<Action<Exception>>(failed));
                this.completed = new ReadOnlyCollection<Action>(new List<Action>(completed));
            }

            public Frontend Frontend
            {
                get { return this.frontend; }
            }

            public ProcessType Process
            {
                get { return this.process; }
            }

            public IReadOnlyList<ActionType> Succeed
            {
                get { return this.succeed; }
            }

            public IReadOnlyList<Action<Exception>> Failed
            {
                get { return this.failed; }
            }

            public IReadOnlyList<Action> Completed
            {
                get { return this.completed; }
            }

            protected void Callback(Action action)
            {
                this.Frontend.Caller.Invoke(action);
            }

            protected abstract void Execute();

            public void Run()
            {
                try
                {
                    this.Execute();
                }
                catch (Exception cause)
                {
                    if (this.failed.Count > 0)
                    {
                        this.Callback(() =>
                        {
                            foreach (Action<Exception> failed in this.Failed)
                                failed(cause);
                        });
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    if (this.Completed.Count > 0)
                    {
                        this.Callback(() =>
                        {
                            foreach (Action completed in this.Completed)
                                completed();
                        });
                    }
                }
            }

        }

        public class Future : AbstractFuture<Action, Action>
        {

            public Future(
                Frontend frontend,
                Action process,
                IEnumerable<Action> succeed,
                IEnumerable<Action<Exception>> failed,
                IEnumerable<Action> completed)
                : base(
                      frontend,
                      process,
                      succeed,
                      failed,
                      completed)
            { }

            protected override void Execute()
            {
                this.Process();
                if (this.Succeed.Count > 0)
                {
                    this.Callback(() =>
                    {
                        foreach (Action succeed in this.Succeed)
                            succeed();
                    });
                }
            }

        }

        public class Future<Type> : AbstractFuture<Func<Type>, Action<Type>>
        {

            public Future(
                Frontend frontend,
                Func<Type> process,
                IEnumerable<Action<Type>> succeed,
                IEnumerable<Action<Exception>> failed,
                IEnumerable<Action> completed)
                : base(
                      frontend,
                      process,
                      succeed,
                      failed,
                      completed)
            { }

            protected override void Execute()
            {
                Type result;

                result = this.Process();
                if (this.Succeed.Count > 0)
                {
                    this.Callback(() =>
                    {
                        foreach (Action<Type> succeed in this.Succeed)
                            succeed(result);
                    });
                }
            }

        }

    }

}
