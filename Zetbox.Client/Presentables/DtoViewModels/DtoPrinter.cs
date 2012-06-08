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

namespace Zetbox.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Dtos;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;
    using MigraDoc.Rendering;

    public class DtoPrinter
    {
        private readonly IFileOpener _fileOpener;
        private readonly ITempFileService _tmpService;

        public DtoPrinter(IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");
            if (tmpService == null) throw new ArgumentNullException("tmpService");

            _fileOpener = fileOpener;
            _tmpService = tmpService;
        }

        /// <summary>
        /// DTOs as lists are rendered as a sequence of sections containing paragraphs and tables with various different parameters.
        /// This class keeps track of the current end of document and is able to append new values, groups and tables with minimal changes.
        /// </summary>
        private class DocumentListState
        {
            public DocumentListState()
            {
                CurrentHeadingLevel = 0;
            }

            private Document _currentDocument;
            private Style _titleStyle;
            private Style _heading1Style;
            private Style _heading2Style;
            private Style _heading3Style;
            private Style _heading4Style;
            private Style _descriptionStyle;
            public Document CurrentDocument
            {
                get
                {
                    return _currentDocument;
                }
                set
                {
                    if (_currentDocument != value)
                    {
                        _currentDocument = value;

                        _titleStyle = FindStyle("Title");
                        _titleStyle.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                        _titleStyle.ParagraphFormat.Font.Bold = true;
                        _titleStyle.ParagraphFormat.Font.Size = 17;
                        _titleStyle.ParagraphFormat.SpaceAfter = "0.25cm";

                        _heading1Style = FindStyle("Heading1");
                        _heading1Style.ParagraphFormat.Font.Bold = true;
                        _heading1Style.ParagraphFormat.Font.Size = 17;
                        _heading1Style.ParagraphFormat.SpaceBefore = "0.5cm";
                        _heading1Style.ParagraphFormat.SpaceAfter = "0.1cm";

                        _heading2Style = FindStyle("Heading2");
                        _heading2Style.ParagraphFormat.Font.Bold = true;
                        _heading2Style.ParagraphFormat.Font.Size = 17;
                        _heading2Style.ParagraphFormat.SpaceBefore = "0.5cm";
                        _heading2Style.ParagraphFormat.SpaceAfter = "0.1cm";

                        _heading3Style = FindStyle("Heading3");
                        _heading3Style.ParagraphFormat.Font.Bold = true;
                        _heading3Style.ParagraphFormat.Font.Size = 15;
                        _heading3Style.ParagraphFormat.SpaceBefore = "0.25cm";
                        _heading3Style.ParagraphFormat.SpaceAfter = "0.1cm";

                        _heading4Style = FindStyle("Heading4");
                        _heading4Style.ParagraphFormat.Font.Bold = true;
                        _heading4Style.ParagraphFormat.SpaceBefore = "0.1cm";
                        _heading4Style.ParagraphFormat.SpaceAfter = "0.1cm";

                        _descriptionStyle = FindStyle("Description");
                        _descriptionStyle.ParagraphFormat.Font.Size = 8;
                    }
                }
            }

            private Style FindStyle(string styleName)
            {
                return _currentDocument.Styles.OfType<Style>().SingleOrDefault(s => s.Name == styleName) ?? _currentDocument.AddStyle(styleName, "Normal");
            }

            public Section CurrentSection { get; set; }
            public Table CurrentTable { get; set; }
            public int CurrentHeadingLevel { get; set; }

            private int _heading1 = 0;
            private int _heading2 = 0;
            private int _heading3 = 0;

            public void ForceOrientation(Orientation orientation)
            {
                if (this.CurrentSection == null || this.CurrentSection.PageSetup.Orientation != orientation)
                {
                    this.CurrentSection = this.CurrentDocument.AddSection();
                    this.CurrentSection.PageSetup.Orientation = orientation;
                }
            }

            public void AddTitle(string text)
            {
                this.CurrentTable = null;

                _heading1 = 0;
                _heading2 = 0;
                _heading3 = 0;
                this.CurrentSection.AddParagraph(text).Style = _titleStyle.Name;
            }

            public void AddHeading1(string text)
            {
                this.CurrentTable = null;

                _heading1 += 1;
                _heading2 = 0;
                _heading3 = 0;

                this.CurrentSection.AddParagraph(string.Format("{0} {1}", _heading1, text)).Style = _heading1Style.Name;
            }

            public void AddHeading2(string text)
            {
                this.CurrentTable = null;

                //_heading1;
                _heading2 += 1;
                _heading3 = 0;
                this.CurrentSection.AddParagraph(string.Format("{0}.{1} {2}", _heading1, _heading2, text)).Style = _heading2Style.Name;
            }

            public void AddHeading3(string text)
            {
                this.CurrentTable = null;

                //_heading1;
                //_heading2;
                _heading3 += 1;
                this.CurrentSection.AddParagraph(string.Format("{0}.{1}.{2} {3}", _heading1, _heading2, _heading3, text)).Style = _heading3Style.Name;
            }

            public void AddHeadingOther(string text)
            {
                this.CurrentTable = null;

                this.CurrentSection.AddParagraph(text).Style = _heading4Style.Name;
            }

            public void AddHeading(int level, string text)
            {
                switch (level)
                {
                    case 0:
                        AddTitle(text);
                        break;
                    case 1:
                        AddHeading1(text);
                        break;
                    case 2:
                        AddHeading2(text);
                        break;
                    case 3:
                        AddHeading3(text);
                        break;
                    default:
                        AddHeadingOther(text);
                        break;
                }
            }

            public void AddHeading(string text, object debugInfo)
            {
                AddHeading(CurrentHeadingLevel, text);
                // DEBUG: CurrentSection.AddParagraph(string.Format("from {0}", debugInfo.ToString()));
            }

            public void AddDescription(string text)
            {
                if (!string.IsNullOrEmpty(text))
                    CurrentSection.AddParagraph(text).Style = _descriptionStyle.Name;
            }
        }

        public void PrintAsList(DtoBaseViewModel dto)
        {
            var doc = GetListFormattedDocument(dto);
            PostProcess(dto, doc);
            //DebugDump(doc);
            PrintToPdf(doc);
        }

        private static void PostProcess(DtoBaseViewModel dto, Document doc)
        {
            var postProc = dto.Data.GetType().GetMethods().Where(mi => mi.GetCustomAttributes(typeof(GuiPrintPostProcessorAttribute), false).Length > 0).SingleOrDefault();
            if (postProc != null)
            {
                postProc.Invoke(dto.Data, new object[] { dto.Root, doc });
            }
            // else: ignore, nothing to do
        }

        //private static void DebugDump(Document doc)
        //{
        //    var dw = new MigraDoc.DocumentObjectModel.IO.DdlWriter(@"C:\tmp\debug.mdddl");
        //    dw.WriteDocument(doc);
        //    dw.Close();
        //}

        private void PrintToPdf(Document doc)
        {
            var pdf = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = doc;
            pdf.RenderDocument();

            var tmp = _tmpService.CreateWithExtension(".pdf");
            pdf.Save(tmp);
            _fileOpener.ShellExecute(tmp);
        }

        private Document GetListFormattedDocument(DtoBaseViewModel dto)
        {
            var result = new Document();
            AppendAsList(new DocumentListState() { CurrentDocument = result }, dto);

            return result;
        }

        private void AppendAsList(DocumentListState dls, DtoBaseViewModel dto)
        {
            var dtoTable = dto as DtoTableViewModel;
            if (dtoTable != null)
            {
                if (dtoTable.IsDataTable)
                {
                    AppendAsTable(dls, dtoTable);
                }
                else // append contents of structural table as lists
                {
                    dls.ForceOrientation(Orientation.Portrait);
                    dls.AddHeading(dto.Title, dto.DebugInfo);
                    dls.AddDescription(dto.Description);
                    dls.CurrentHeadingLevel += 1;

                    foreach (var item in dtoTable.Cells)
                    {
                        var baseVM = item.Value as DtoBaseViewModel;
                        if (baseVM != null)
                        {
                            AppendAsList(dls, baseVM);
                        }
                        else if (item.Value != null)
                        {
                            throw new NotImplementedException(string.Format("Cannot print {0} ({1}) as cell contents", item.Value, item.Value.GetType()));
                        }
                    }
                    dls.CurrentHeadingLevel -= 1;
                }
            }
            else if (typeof(DtoGroupedViewModel).IsAssignableFrom(dto.GetType()))
            {
                dls.ForceOrientation(Orientation.Portrait);
                dls.AddHeading(dto.Title, dto.DebugInfo);

                dls.CurrentHeadingLevel += 1;
                foreach (var item in ((DtoGroupedViewModel)dto).Items)
                {
                    AppendAsList(dls, item);
                }
                dls.CurrentHeadingLevel -= 1;
            }
            else if (typeof(DtoValueViewModel).IsAssignableFrom(dto.GetType()))
            {
                var valVM = dto as DtoValueViewModel;
                if (dls.CurrentTable == null)
                {
                    dls.CurrentTable = dls.CurrentSection.AddTable();
                    dls.CurrentTable.AddColumn("8cm");  // title
                    dls.CurrentTable.AddColumn("4cm");  // value
                    dls.CurrentTable.AddColumn("4cm");  // alternate representation
                }
                var row = dls.CurrentTable.AddRow();
                row.Cells[0].AddParagraph(dto.Title);
                if (!string.IsNullOrEmpty(valVM.Value))
                    row.Cells[1].AddParagraph(valVM.Value).Format.Alignment = ConvertParagraphAlignment(valVM.ValueAlignment);
                if (!string.IsNullOrEmpty(valVM.AlternateRepresentation))
                    row.Cells[2].AddParagraph(valVM.AlternateRepresentation).Format.Alignment = ConvertParagraphAlignment(valVM.AlternateRepresentationAlignment);
                if (!string.IsNullOrEmpty(valVM.Description))
                {
                    dls.AddDescription(valVM.Description);
                    dls.CurrentTable = null; // reset table after description
                }
            }
            else if (dto != null)
            {
                throw new NotImplementedException(string.Format("Cannot print {0} ({1}) as list", dto, dto.GetType()));
            }
        }

        private ParagraphAlignment ConvertParagraphAlignment(ContentAlignment contentAlignment)
        {
            switch (contentAlignment)
            {
                case ContentAlignment.BottomCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.TopCenter:
                    return MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
                case 0: // uninitialized value
                case ContentAlignment.BottomLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.TopLeft:
                    return MigraDoc.DocumentObjectModel.ParagraphAlignment.Left;
                case ContentAlignment.BottomRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.TopRight:
                    return MigraDoc.DocumentObjectModel.ParagraphAlignment.Right;
                default:
                    throw new ArgumentOutOfRangeException("contentAlignment");
            }
        }

        private void AppendAsTable(DocumentListState dls, DtoTableViewModel dto)
        {
            dls.ForceOrientation(Orientation.Landscape);
            dls.AddHeading(dto.Title, dto.DebugInfo);

            dls.CurrentTable = dls.CurrentSection.AddTable();
            dls.CurrentTable.KeepTogether = true;

            var maxCol = dto.Columns.Count - 1;
            for (int i = 0; i <= maxCol; i++)
            {
                dls.CurrentTable.AddColumn();
            }
            dls.CurrentTable.Columns[0].Width = "4cm";

            var header = dls.CurrentTable.AddRow();
            for (int i = 0; i <= maxCol; i++)
            {
                header.Cells[i].AddParagraph(dto.Columns[i].Title);
                header.Cells[i].Format.Alignment = ParagraphAlignment.Center;
            }

            var maxRow = dto.Rows.Count - 1;
            for (int rowIdx = 0; rowIdx <= maxRow; rowIdx++)
            {
                var row = dls.CurrentTable.AddRow();
                if (rowIdx % 2 == 0)
                    row.Format.Shading.Color = Colors.LightGray;
                var myRow = dto.Rows[rowIdx];
                for (int colIdx = 0; colIdx <= maxCol; colIdx++)
                {
                    var myCol = dto.Columns[colIdx];
                    var myCell = dto.Cells.SingleOrDefault(cell => cell.Row == myRow && cell.Column == myCol);
                    if (myCell != null)
                    {
                        row.Cells[colIdx].AddParagraph(myCell.Value.ToString());
                        if (myCell.Value is DtoValueViewModel)
                            row.Cells[colIdx].Format.Alignment = ConvertParagraphAlignment(((DtoValueViewModel)myCell.Value).ValueAlignment);
                    }
                }
            }

            dls.AddDescription(dto.Description);
            dls.CurrentTable = null;
        }
    }
}
