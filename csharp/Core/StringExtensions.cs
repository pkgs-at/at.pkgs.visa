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

namespace Architector.Visa.Core
{

    public static class StringExtensions
    {

        public class Scanner
        {

            private readonly string text;

            private int position;

            public Scanner(string text)
            {
                this.text = text;
                this.position = 0;
            }

            public string Text
            {
                get { return this.text; }
            }

            public int Position
            {
                get { return this.position; }
            }

            public bool EndOfText
            {
                get { return this.position >= this.text.Length; }
            }

            public char Peek()
            {
                return this.text[this.position];
            }

            public void Skip()
            {
                this.position++;
            }

            public void SkipSpaces()
            {
                while (!this.EndOfText && this.Peek() == ' ') this.Skip();
            }

            public char Read()
            {
                return this.text[this.position++];
            }

            public string ReadAscii()
            {
                char quote;
                StringBuilder builder;

                this.SkipSpaces();
                if (this.EndOfText) return null;
                quote = this.Peek();
                switch (quote)
                {
                    case '\'':
                        break;
                    case '\"':
                        break;
                    default:
                        return null;
                }
                this.Skip();
                builder = new StringBuilder();
                while (!this.EndOfText)
                {
                    if (this.Peek() != quote)
                    {
                        builder.Append(this.Read());
                        continue;
                    }
                    this.Skip();
                    if (this.EndOfText || this.Peek() != quote) return builder.ToString();
                    builder.Append(this.Read());
                }
                throw new ArgumentException(String.Format("Invalid ascii termination: {0} at: {1}", this.text, this.position));
            }

        }

        public static string ParseScpiAscii(this string text)
        {
            return new Scanner(text).ReadAscii();
        }

    }

}
