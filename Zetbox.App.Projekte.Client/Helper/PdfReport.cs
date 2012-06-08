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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MigraDoc.DocumentObjectModel;
using System.IO;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;

namespace Zetbox.App.Projekte.Client.Helper
{
    [CLSCompliant(false)]
    public abstract class PdfReport : IDisposable
    {
        public PdfReport()
        {
        }

        public PdfReport(Document doc)
        {
            if (doc == null) throw new ArgumentNullException("doc");
            _Document = doc;
        }

        public PdfReport(Section s)
        {
            if (s == null) throw new ArgumentNullException("s");
            _Document = s.Document;
            _Section = s;
        }

        protected static readonly string styleTableHeader = "TableHeader";
        protected static readonly string styleAppendixHeading = "AppendixHeading";

        private Document _Document = null;
        private Section _Section = null;

        protected Document Document
        {
            get
            {
                if (_Document == null)
                {
                    NewDocument();
                }
                return _Document;
            }
        }
        protected Section Section
        {
            get
            {
                if (_Section == null)
                {
                    NewSection();
                }
                return _Section;
            }
        }

        protected abstract void DefineDocInfo();

        protected virtual void NewDocument()
        {
            _Document = new Document();

            DefineStyles();
            DefineDocInfo();

            NewSection();
        }

        protected virtual void DefineStyles()
        {
            Style s;
            s = Document.Styles[StyleNames.Normal];
            s.Font.Name = "Arial";
            s.Font.Size = 10;

            s = Document.Styles[StyleNames.Heading1];
            s.Font.Bold = true;
            s.Font.Size = 13;
            s.ParagraphFormat.SpaceBefore = "1cm";
            s.ParagraphFormat.SpaceAfter = "0.5cm";
            s.ParagraphFormat.ListInfo.ListType = ListType.NumberList1;
            s.ParagraphFormat.ListInfo.ContinuePreviousList = true;

            s = Document.Styles[StyleNames.Heading2];
            s.Font.Bold = true;
            s.Font.Size = 11;
            s.ParagraphFormat.SpaceBefore = "1cm";
            s.ParagraphFormat.SpaceAfter = "0.5cm";
            s.ParagraphFormat.ListInfo.ListType = ListType.BulletList1;
            s.ParagraphFormat.ListInfo.ContinuePreviousList = false;

            s = Document.Styles.AddStyle(styleTableHeader, StyleNames.Normal);
            s.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            s.ParagraphFormat.Font.Bold = true;

            s = Document.Styles.AddStyle(styleAppendixHeading, StyleNames.Heading1);
            s.ParagraphFormat.ListInfo.ListType = ListType.BulletList1;
        }

        protected virtual void NewSection()
        {
            _Section = Document.AddSection();
        }

        protected virtual Paragraph NewHeading1(string text)
        {
            return Section.AddParagraph(text, StyleNames.Heading1);
        }
        protected virtual Paragraph NewHeading2(string text)
        {
            return Section.AddParagraph(text, StyleNames.Heading2);
        }

        protected Table NewTable()
        {
            var t = Section.AddTable();
            t.Borders.Visible = true;
            t.TopPadding = 2;
            t.BottomPadding = 2;
            return t;
        }

        public virtual Stream GetStream()
        {
            MemoryStream stream = new MemoryStream();
            PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = Document;
            pdf.RenderDocument();
            pdf.Save(stream, false);
            return stream;
        }

        public virtual void Save(string filename)
        {
            PdfDocumentRenderer pdf = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = Document;
            pdf.RenderDocument();
            pdf.Save(filename);
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
