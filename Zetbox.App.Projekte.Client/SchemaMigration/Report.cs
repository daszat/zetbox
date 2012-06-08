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
namespace Zetbox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Base;
    using Zetbox.App.Projekte.Client.Helper;
    using Zetbox.App.Extensions;
    using MigraDoc.DocumentObjectModel;
    using Zetbox.Client.Presentables;
    using Zetbox.App.SchemaMigration;

    internal class ReportHelper
    {
        public static string GetStatusString(MappingStatus? s)
        {
            switch (s)
            {
                case MappingStatus.Mapped:
                    return "OK";
                case MappingStatus.Questions:
                    return "?";
                case MappingStatus.CustomSource:
                    return "CS";
                case MappingStatus.Ignored:
                    return "I";
                default:
                    return string.Empty;
            }
        }
    }


    [CLSCompliant(false)]
    public class SourceTableMappingReport : PdfReport
    {
        public SourceTableMappingReport()
            : base()
        {
        }

        public SourceTableMappingReport(Section s)
            : base(s)
        {
        }

        protected override void DefineDocInfo()
        {
            Document.Info.Title = "Miration report table mapping";
            Document.Info.Author = "dasz.at";
            Document.Info.Subject = "Migration report table mapping";
        }

        private Zetbox.App.SchemaMigration.SourceTable _obj = null;

        public void CreateReport(Zetbox.App.SchemaMigration.SourceTable obj)
        {
            this._obj = obj;

            RenderSummary();
            RenderColumnMappings();
        }

        private void RenderColumnMappings()
        {
            NewHeading2("Summary");
            var t = NewTable();

            t.AddColumn("1cm");
            t.AddColumn("4cm");
            t.AddColumn("4cm");
            t.AddColumn("7cm");

            var r = t.AddRow();
            r.Cells[0].AddParagraph("Stat.").Style = styleTableHeader;
            r.Cells[1].AddParagraph("Src Name").Style = styleTableHeader;
            r.Cells[2].AddParagraph("Dest Name").Style = styleTableHeader;
            r.Cells[3].AddParagraph("Description").Style = styleTableHeader;

            foreach (var c in _obj.SourceColumn.OrderBy(i => i.Name))
            {
                r = t.AddRow();
                r.Cells[0].AddParagraph(ReportHelper.GetStatusString(c.Status));
                r.Cells[1].AddParagraph(c.Name ?? string.Empty);
                r.Cells[2].AddParagraph(c.DestinationProperty.Count > 0 ? c.DestinationProperty.Last().Name : string.Empty);
                r.Cells[3].AddParagraph(c.Description ?? string.Empty);
            }

            foreach (var c in _obj.SourceColumn.Where(i => i.Status != MappingStatus.Ignored).OrderBy(i => i.Name))
            {
                NewHeading2(string.Format("{1}Column {0}", c.Name, c.Status == MappingStatus.Questions ? "** " : string.Empty));
                if (!string.IsNullOrEmpty(c.Description)) Section.AddParagraph(c.Description).Format.SpaceAfter = "0.5cm";
                t = NewTable();

                t.AddColumn("6cm");
                t.AddColumn("10cm");

                r = t.AddRow();
                r.Cells[0].AddParagraph("Status");
                r.Cells[1].AddParagraph(c.Status.HasValue ? c.Status.Value.ToString() : string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("Source Name");
                r.Cells[1].AddParagraph(c.Name ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("Dest Name");
                r.Cells[1].AddParagraph(c.DestinationProperty.Count > 0 ? c.DestinationProperty.Last().Name : string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("Source Type");
                r.Cells[1].AddParagraph(c.DbType.ToString());

                r = t.AddRow();
                r.Cells[0].AddParagraph("Dest Type");
                r.Cells[1].AddParagraph(c.DestinationProperty.Count > 0 ? c.DestinationProperty.Last().GetPropertyTypeString() : string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("IsNullable");
                r.Cells[1].AddParagraph((c.IsNullable ?? true).ToString());

                r = t.AddRow();
                r.Cells[0].AddParagraph("Size");
                r.Cells[1].AddParagraph(c.Size.ToString());

                if (!string.IsNullOrEmpty(c.Comment)) Section.AddParagraph(c.Comment).Format.SpaceBefore = "0.5cm";

                if (c.EnumEntries.Count > 0)
                {
                    var p = Section.AddParagraph("Enum Mapping");
                    p.Format.Font.Italic = true;
                    p.Format.SpaceBefore = "0.5cm";
                    p.Format.SpaceAfter = "0.5cm";
                    t = NewTable();

                    t.AddColumn("6cm");
                    t.AddColumn("10cm");

                    r = t.AddRow();
                    r.Cells[0].AddParagraph("Src. Value").Style = styleTableHeader;
                    r.Cells[1].AddParagraph("Dest. Value").Style = styleTableHeader;

                    foreach (var e in c.EnumEntries)
                    {
                        r = t.AddRow();
                        r.Cells[0].AddParagraph(e.SourceValue);
                        r.Cells[1].AddParagraph(e.DestinationValue.Name + (string.IsNullOrEmpty(e.DestinationValue.Description) ? ", " + e.DestinationValue.Description : string.Empty));
                    }
                }
            }
        }

        private void RenderSummary()
        {
            base.NewHeading1("Table " + _obj.Name);
            if (!string.IsNullOrEmpty(_obj.Description)) Section.AddParagraph(_obj.Description).Format.SpaceAfter = "0.5cm";
            if (!string.IsNullOrEmpty(_obj.Comment)) Section.AddParagraph(_obj.Comment).Format.SpaceAfter = "0.5cm";

            var t = NewTable();
            t.AddColumn("6cm");
            t.AddColumn("10cm");

            var r = t.AddRow();
            r.Cells[0].AddParagraph("Status");
            r.Cells[1].AddParagraph(_obj.Status.HasValue ? _obj.Status.Value.ToString() : string.Empty);

            r = t.AddRow();
            r.Cells[0].AddParagraph("DestinationObjectClass");
            r.Cells[1].AddParagraph(_obj.DestinationObjectClass != null ? _obj.DestinationObjectClass.Name : string.Empty);

            r = t.AddRow();
            r.Cells[0].AddParagraph("SourceColumns");
            r.Cells[1].AddParagraph(_obj.SourceColumn.Count.ToString());

            r = t.AddRow();
            r.Cells[0].AddParagraph("SourceColumns mapped");
            r.Cells[1].AddParagraph(_obj.SourceColumn.Count(i => i.DestinationProperty.Count > 0).ToString());
        }

        protected override void NewDocument()
        {
            base.NewDocument();

            DefineHeader();
            DefineFooter();

            RenderFirstPage();
        }

        private void DefineHeader()
        {
            Section.PageSetup.DifferentFirstPageHeaderFooter = true;
            var p = Section.Headers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.AddText("Migration report for " + _obj.Name);
            p.Format.Borders.Bottom.Visible = true;
        }

        private void DefineFooter()
        {
            var p = Section.Footers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.Format.AddTabStop("16cm", TabAlignment.Right);

            p.AddText(DateTime.Today.ToShortDateString());
            p.AddTab();
            p.AddText("Page ");
            p.AddPageField();
            p.AddText("/");
            p.AddNumPagesField();

            p.Format.Borders.Top.Visible = true;
        }

        private void RenderFirstPage()
        {
            // Aktenzeichen
            var p = Section.AddParagraph("Migration Report for table ");
            p.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
            p.Format.SpaceBefore = "2cm";

            p = Section.AddParagraph(_obj.Name ?? string.Empty);
            p.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
            p.Format.SpaceBefore = "2cm";
            p.Format.Font.Bold = true;

            p = Section.AddParagraph(_obj.Description ?? string.Empty);
            p.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
        }
    }

    [CLSCompliant(false)]
    public class MigrationProjectMappingReport : PdfReport
    {
        protected override void DefineDocInfo()
        {
            Document.Info.Title = "Miration report";
            Document.Info.Author = "dasz.at";
            Document.Info.Subject = "Migration report";
        }

        private Zetbox.App.SchemaMigration.MigrationProject _obj = null;

        public void CreateReport(Zetbox.App.SchemaMigration.MigrationProject obj)
        {
            this._obj = obj;

            NewHeading1("Summary");
            foreach (var s in obj.StagingDatabases)
            {
                RenderTableMappings(s);
            }

            foreach (var s in obj.StagingDatabases)
            {
                foreach (var tbl in s.SourceTables.Where(i => i.DestinationObjectClass != null).OrderBy(i => i.Name))
                {
                    var r = new SourceTableMappingReport(Section);
                    r.CreateReport(tbl);
                }

                foreach (var tbl in s.SourceTables.Where(i => i.DestinationObjectClass == null && i.Status != MappingStatus.Ignored).OrderBy(i => i.Name))
                {
                    var r = new SourceTableMappingReport(Section);
                    r.CreateReport(tbl);
                }
            }
        }

        private void RenderTableMappings(Zetbox.App.SchemaMigration.StagingDatabase s)
        {
            var p = Section.AddParagraph(s.Description);
            var t = NewTable();

            t.AddColumn("1cm");
            t.AddColumn("4.5cm");
            t.AddColumn("4.5cm");
            t.AddColumn("4.5cm");
            t.AddColumn("1.5cm");

            var r = t.AddRow();
            r.Cells[0].AddParagraph("Stat.").Style = styleTableHeader;
            r.Cells[1].AddParagraph("Src Table").Style = styleTableHeader;
            r.Cells[2].AddParagraph("Dest Class").Style = styleTableHeader;
            r.Cells[3].AddParagraph("Description").Style = styleTableHeader;
            r.Cells[4].AddParagraph("%").Style = styleTableHeader;

            foreach (var c in s.SourceTables.OrderBy(i => i.Name))
            {
                r = t.AddRow();
                r.Cells[0].AddParagraph(ReportHelper.GetStatusString(c.Status));
                r.Cells[1].AddParagraph(c.Name ?? string.Empty);
                r.Cells[2].AddParagraph(c.DestinationObjectClass != null ? c.DestinationObjectClass.Name : string.Empty);
                r.Cells[3].AddParagraph(c.Description ?? string.Empty);

                if (c.Status != MappingStatus.Ignored)
                {
                    var cols_ok = c.SourceColumn.Count(i => i.Status.HasValue && i.Status.In(MappingStatus.Mapped, MappingStatus.CustomSource, MappingStatus.Ignored));
                    r.Cells[4].AddParagraph((c.SourceColumn.Count > 0 ? (100 * cols_ok / c.SourceColumn.Count) : 0).ToString() + " %")
                        .Format.Alignment = ParagraphAlignment.Right;
                }
                else
                {
                    r.Cells[4].AddParagraph("-");
                }
            }

            p = Section.AddParagraph();
            p.Format.SpaceAfter = "1cm";
        }

        protected override void NewDocument()
        {
            base.NewDocument();

            DefineHeader();
            DefineFooter();

            RenderFirstPage();
        }

        private void DefineHeader()
        {
            Section.PageSetup.DifferentFirstPageHeaderFooter = true;
            var p = Section.Headers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.AddText("Migration Report for " + _obj.Description);
            p.Format.Borders.Bottom.Visible = true;
        }

        private void DefineFooter()
        {
            var p = Section.Footers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.Format.AddTabStop("16cm", TabAlignment.Right);

            p.AddText(DateTime.Today.ToShortDateString());
            p.AddTab();
            p.AddText("Page ");
            p.AddPageField();
            p.AddText("/");
            p.AddNumPagesField();

            p.Format.Borders.Top.Visible = true;
        }

        private void RenderFirstPage()
        {
            // Aktenzeichen
            var p = Section.AddParagraph("Migration Report for");
            p.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
            p.Format.SpaceBefore = "2cm";

            p = Section.AddParagraph(_obj.Description ?? string.Empty);
            p.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center;
            p.Format.SpaceBefore = "2cm";
            p.Format.SpaceAfter = "2cm";
            p.Format.Font.Bold = true;

            var t = NewTable();
            t.AddColumn("6cm");
            t.AddColumn("10cm");

            var r = t.AddRow();
            r.Cells[0].AddParagraph("Destination module");
            r.Cells[1].AddParagraph(_obj.DestinationModule != null ? _obj.DestinationModule.Name : string.Empty);

            foreach (var s in _obj.StagingDatabases)
            {
                p = Section.AddParagraph("Staging Database " + s.Description);
                p.Format.Font.Italic = true;
                p.Format.SpaceBefore = "1cm";
                t = NewTable();
                t.AddColumn("6cm");
                t.AddColumn("10cm");

                r = t.AddRow();
                r.Cells[0].AddParagraph("Source ConnectionString Key");
                r.Cells[1].AddParagraph(s.OriginConnectionStringKey ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("Dest ConnectionString Key");
                r.Cells[1].AddParagraph(s.ConnectionStringKey ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("Dest Schema");
                r.Cells[1].AddParagraph(s.Schema ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceTables");
                r.Cells[1].AddParagraph(s.SourceTables.Count.ToString());

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceTables mapped");
                r.Cells[1].AddParagraph(s.SourceTables.Count(i => i.DestinationObjectClass != null).ToString());
            }
        }
    }
}
