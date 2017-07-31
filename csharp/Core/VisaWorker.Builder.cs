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

namespace Architector.Visa.Core
{

    public partial class VisaWorker
    {

        public interface IBuilder<ActionType>
        {

            IBuilder<ActionType> OnSuccess(ActionType action);

            IBuilder<ActionType> OnFail(Action<Exception> action);

            IBuilder<ActionType> OnComplete(Action action);

            void Invoke();

        }

        public abstract class AbstractBuilder<ProcessType, ActionType> : IBuilder<ActionType>
        {

            private readonly Frontend frontend;

            private readonly ProcessType process;

            private readonly IList<ActionType> succeed;

            private readonly IList<Action<Exception>> failed;

            private readonly IList<Action> completed;

            public AbstractBuilder(
                Frontend frontend,
                ProcessType process)
            {
                this.frontend = frontend;
                this.process = process;
                this.succeed = new List<ActionType>();
                this.failed = new List<Action<Exception>>();
                this.completed = new List<Action>();
            }

            public Frontend Frontend
            {
                get { return this.frontend; }
            }

            public ProcessType Process
            {
                get { return this.process; }
            }

            public IList<ActionType> Succeed
            {
                get { return this.succeed; }
            }

            public IList<Action<Exception>> Failed
            {
                get { return this.failed; }
            }

            public IList<Action> Completed
            {
                get { return this.completed; }
            }

            public virtual IBuilder<ActionType> OnSuccess(ActionType action)
            {
                this.succeed.Add(action);
                return this;
            }

            public virtual IBuilder<ActionType> OnFail(Action<Exception> action)
            {
                this.failed.Add(action);
                return this;
            }

            public virtual IBuilder<ActionType> OnComplete(Action action)
            {
                this.completed.Add(action);
                return this;
            }

            public abstract IFuture Build();

            public void Invoke()
            {
                this.Frontend.Worker.Invoke(this.Build());
            }

        }

        public class Builder : AbstractBuilder<Action, Action>
        {

            public Builder(
                Frontend frontend,
                Action process)
                : base(
                      frontend,
                      process)
            { }

            public override IFuture Build()
            {
                return new Future(
                    this.Frontend,
                    this.Process,
                    this.Succeed,
                    this.Failed,
                    this.Completed);
            }

        }

        public class Builder<Type> : AbstractBuilder<Func<Type>, Action<Type>>
        {

            public Builder(
                Frontend frontend,
                Func<Type> process)
                : base(
                      frontend,
                      process)
            { }

            public override IFuture Build()
            {
                return new Future<Type>(
                    this.Frontend,
                    this.Process,
                    this.Succeed,
                    this.Failed,
                    this.Completed);
            }

        }

    }

}
