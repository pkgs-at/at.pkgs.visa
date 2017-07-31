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
using System.Text;
using System.Text.RegularExpressions;

namespace Architector.Visa.Core
{

    public class VisaError : Exception
    {

        public class Status
        {

            private static readonly Regex Pattern = new Regex("^([+-]\\d+),(.*)$");

            private readonly int code;

            private readonly string message;

            public Status(int code, string message)
            {
                this.code = code;
                this.message = message;
            }

            public int Code
            {
                get { return this.code; }
            }

            public bool IsError
            {
                get { return this.Code != 0; }
            }

            public string Message
            {
                get { return this.message; }
            }

            public override string ToString()
            {
                StringBuilder builder;

                builder = new StringBuilder();
                if (this.Code >= 0) builder.Append('+');
                builder.Append(this.Code);
                while (builder.Length < 8) builder.Insert(0, ' ');
                builder.Append(", ");
                builder.Append(this.Message);
                return builder.ToString();
            }

            public static Status Parse(string message)
            {
                Match match;

                match = Status.Pattern.Match(message);
                if (!match.Success) throw new ArgumentException(String.Format("{0} is not valid message", message));
                return new Status(Int32.Parse(match.Groups[1].Value), match.Groups[2].Value.ParseScpiAscii());
            }

        }

        private readonly IReadOnlyList<Status> statuses;

        public VisaError(IList<Status> statuses)
            : base(String.Format("{0} of errors", statuses.Count))
        {
            this.statuses = new ReadOnlyCollection<Status>(statuses);
        }

        public VisaError(IEnumerable<Status> statuses)
            : this(new List<Status>(statuses))
        { }

        public IReadOnlyList<Status> Statuses
        {
            get { return this.statuses; }
        }

        public override string ToString()
        {
            StringBuilder builder;

            builder = new StringBuilder();
            builder.AppendLine(this.Message);
            foreach (Status status in this.Statuses)
                builder.AppendLine(status.ToString());
            return builder.ToString();
        }

    }

}
