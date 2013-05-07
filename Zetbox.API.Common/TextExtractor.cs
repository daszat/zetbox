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

    public interface ITextExtractor
    {
        string GetText(Stream data, string mimeType);
    }

    public interface ITextExtractorProvider
    {
        string GetText(Stream data);
    }


    public class TextExtractor : ITextExtractor
    {
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
                return _scope.ResolveNamed<ITextExtractorProvider>(mimeType).GetText(data);
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
        public string GetText(Stream data)
        {
            bool isUtf8 = true;
            if (data.CanSeek)
            {
                isUtf8 = Utf8Checker.IsUtf8(data);
                data.Seek(0, SeekOrigin.Begin);
            }
            var sr = new StreamReader(data, isUtf8 ? Encoding.UTF8 : Encoding.Default);
            return sr.ReadToEnd();
        }
    }

    public class PdfTextExtractorProvider : ITextExtractorProvider
    {
        public string GetText(Stream data)
        {
            return string.Empty;
        }
    }

    public class WordTextExtractorProvider : ITextExtractorProvider
    {
        public string GetText(Stream data)
        {
            return string.Empty;
        }
    }
    #endregion
}
