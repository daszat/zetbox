// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using System.IO;
    using System.ComponentModel;
    using Zetbox.API.Utils;
    using PdfSharp.Pdf;
    using PdfSharp.Pdf.IO;

    public interface ITextExtractor
    {
        string GetText(Stream data, string mimeType);
    }

    public interface ITextExtractorProvider
    {
        string GetText(Stream data, int limit);
    }


    public class TextExtractor : ITextExtractor
    {
        public static readonly int TextLengthLimit = 100 * 1024; // 100 KB

        [Description("Default Text Extractor service registration")]
        public class Module : Autofac.Module
        {
            protected override void Load(Autofac.ContainerBuilder builder)
            {
                base.Load(builder);

                builder
                    .RegisterType<TextExtractor>()
                    .As<ITextExtractor>()
                    .SingleInstance();

                builder
                    .RegisterType<TextTextExtractorProvider>()
                    .As<ITextExtractorProvider>()
                    .Named<ITextExtractorProvider>("text/plain")
                    .SingleInstance();

                builder
                    .RegisterType<PdfTextExtractorProvider>()
                    .As<ITextExtractorProvider>()
                    .Named<ITextExtractorProvider>("application/pdf")
                    .SingleInstance();

                builder
                    .RegisterType<WordTextExtractorProvider>()
                    .As<ITextExtractorProvider>()
                    .Named<ITextExtractorProvider>("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    .SingleInstance();
            }
        }

        private readonly ILifetimeScope _scope;
        public TextExtractor(ILifetimeScope scope)
        {
            if (scope == null) throw new ArgumentNullException("scope");

            _scope = scope;
        }

        public string GetText(Stream data, string mimeType)
        {
            if(data == null) throw new ArgumentNullException("data");
            if(string.IsNullOrEmpty(mimeType)) return string.Empty;
            mimeType = mimeType.ToLower().Trim();

            if (_scope.IsRegisteredWithName<ITextExtractorProvider>(mimeType))
            {
                return _scope.ResolveNamed<ITextExtractorProvider>(mimeType).GetText(data, TextLengthLimit);
            }
            else
            {
                return string.Empty;
            }
        }
    }

    #region ITextExtractorProvider
    public class TextTextExtractorProvider : ITextExtractorProvider
    {
        public string GetText(Stream data, int limit)
        {
            if (data == null) throw new ArgumentNullException("data");

            bool isUtf8 = true;
            if (data.CanSeek)
            {
                isUtf8 = Utf8Checker.IsUtf8(data);
                data.Seek(0, SeekOrigin.Begin);
            }
            var sr = new StreamReader(data, isUtf8 ? Encoding.UTF8 : Encoding.Default);
            var result = new StringBuilder();

            var buffer = new char[1024];
            int cnt;
            while ((cnt = sr.ReadBlock(buffer, 0, buffer.Length)) > 0 && result.Length < limit)
            {
                result.Append(buffer, 0, cnt);
            }

            return result.ToString();
        }
    }

    public class PdfTextExtractorProvider : ITextExtractorProvider
    {
        public string GetText(Stream data, int limit)
        {
            if (data == null) throw new ArgumentNullException("data");

            var inputDocument = PdfReader.Open(data, PdfDocumentOpenMode.ReadOnly);
            var result = new StringBuilder();

            foreach (PdfPage page in inputDocument.Pages)
            {
                for (int index = 0; index < page.Contents.Elements.Count; index++)
                {
                    var stream = page.Contents.Elements.GetDictionary(index).Stream;
                    result.AppendLine(PDFParser.ExtractTextFromPDFBytes(stream.Value));
                }
            }

            return result.ToString();
        }
    }

    #region PDFParser
    class PDFParser
    {
        /*
         * Inspired by:
         * http://forum.pdfsharp.net/viewtopic.php?f=2&t=527&p=1603
         * http://stackoverflow.com/questions/248768/how-do-i-walk-through-tree-of-pdf-objects-in-pdfsharp
         * 
         * BT = Beginning of a text object operator 
         * ET = End of a text object operator
         * Td move to the start of next line
         * 5 Ts = superscript
         * -5 Ts = subscript
         */

        /// <summary>
        /// The number of characters to keep, when extracting text.
        /// </summary>
        private const int _numberOfCharsToKeep = 15;


        /// <summary>
        /// This method processes an uncompressed Adobe (text) object 
        /// and extracts text.
        /// </summary>
        /// <param name="input">uncompressed</param>
        /// <returns></returns>
        public static string ExtractTextFromPDFBytes(byte[] input)
        {
            if (input == null || input.Length == 0) return string.Empty;

            try
            {
                var result = new StringBuilder();

                // Flag showing if we are we currently inside a text object
                var inTextObject = false;

                // Flag showing if the next character is literal 
                // e.g. '\\' to get a '\' character or '\(' to get '('
                var nextLiteral = false;

                // () Brackets. Text appears inside ()
                var inBrackets = false;

                // <> angle brackets. HEX Text appears inside <>
                var inAnglebrackets = false;
                var hexStr = new StringBuilder();

                // Keep previous chars to get extract numbers etc.:
                var previousCharacters = Enumerable.Repeat(' ', _numberOfCharsToKeep).ToArray();

                for (int i = 0; i < input.Length; i++)
                {
                    char c = (char)input[i];

                    if (inTextObject)
                    {
                        // Position the text
                        if (inBrackets == false)
                        {
                            if (CheckToken(new string[] { "TD", "Td" }, previousCharacters))
                            {
                                result.AppendLine();
                            }
                            else if (CheckToken(new string[] { "'", "T*", "\"" }, previousCharacters))
                            {
                                result.AppendLine();
                            }
                            else if (CheckToken(new string[] { "Tj" }, previousCharacters))
                            {
                                result.Append(" ");
                            }
                        }

                        // End of a text object, also go to a new line.
                        if (inBrackets == false &&
                            CheckToken(new string[] { "ET" }, previousCharacters))
                        {

                            inTextObject = false;
                            result.Append(" ");
                        }
                        else if ((c == '(') && (inBrackets == false) && (!nextLiteral)) // Start outputting text
                        {
                            inBrackets = true;
                        }
                        else if ((c == ')') && (inBrackets == true) && (!nextLiteral)) // Stop outputting text
                        {
                            inBrackets = false;
                        }
                        else if ((c == '<') && (inAnglebrackets == false) && (!nextLiteral)) // Start outputting text
                        {
                            inAnglebrackets = true;
                            hexStr.Clear();
                        }
                        else if ((c == '>') && (inAnglebrackets == true) && (!nextLiteral)) // Stop outputting text
                        {
                            inAnglebrackets = false;
                            var hex = hexStr.ToString();
                            for (int j = 0; j <= hex.Length - 2; j += 2)
                            {
                                result.Append(Convert.ToChar(int.Parse(hex.Substring(j, 2), System.Globalization.NumberStyles.HexNumber)));
                            }
                        }
                        else if (inBrackets == true) // Just a normal text character:
                        {
                            // Only print out next character no matter what. 
                            // Do not interpret.
                            if (c == '\\' && !nextLiteral)
                            {
                                nextLiteral = true;
                            }
                            else
                            {
                                if (((c >= ' ') && (c <= '~')) ||
                                    ((c >= 128) && (c < 255)))
                                {
                                    result.Append(c);
                                }

                                nextLiteral = false;
                            }
                        }
                        else if (inAnglebrackets == true)
                        {
                            hexStr.Append(c);
                        }
                    }

                    // Store the recent characters for 
                    // when we have to go back for a checking
                    for (int j = 0; j < _numberOfCharsToKeep - 1; j++)
                    {
                        previousCharacters[j] = previousCharacters[j + 1];
                    }
                    previousCharacters[_numberOfCharsToKeep - 1] = c;

                    // Start of a text object
                    if (!inTextObject && CheckToken(new string[] { "BT" }, previousCharacters))
                    {
                        inTextObject = true;
                    }
                }
                return result.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check if a certain 2 character token just came along (e.g. BT)
        /// </summary>
        /// <param name="tokens">the searched token</param>
        /// <param name="recent">the recent character array</param>
        /// <returns></returns>
        private static bool CheckToken(string[] tokens, char[] recent)
        {
            foreach (string token in tokens)
            {
                if (token.Length > 1)
                {
                    if ((recent[_numberOfCharsToKeep - 3] == token[0]) &&
                        (recent[_numberOfCharsToKeep - 2] == token[1]) &&
                        ((recent[_numberOfCharsToKeep - 1] == ' ') ||
                        (recent[_numberOfCharsToKeep - 1] == 0x0d) ||
                        (recent[_numberOfCharsToKeep - 1] == 0x0a)) &&
                        ((recent[_numberOfCharsToKeep - 4] == ' ') ||
                        (recent[_numberOfCharsToKeep - 4] == 0x0d) ||
                        (recent[_numberOfCharsToKeep - 4] == 0x0a))
                        )
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            return false;
        }
    }
    #endregion

    public class WordTextExtractorProvider : ITextExtractorProvider
    {
        public string GetText(Stream data, int limit)
        {
            return string.Empty;
        }
    }
    #endregion
}
