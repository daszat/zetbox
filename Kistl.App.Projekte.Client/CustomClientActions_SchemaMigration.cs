using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.Projekte.Client.Helper;
using Kistl.App.Extensions;
using MigraDoc.DocumentObjectModel;
using Kistl.Client.Presentables;

namespace ZBox.App.SchemaMigration
{
    public class CustomClientActions_SchemaMigration
    {
        private static IModelFactory _mdlFactory = null;

        public CustomClientActions_SchemaMigration(IModelFactory mdlFactory)
        {
            _mdlFactory = mdlFactory;
        }

        private static string Quote(string str, ZBox.App.SchemaMigration.MigrationProject obj)
        {
            return string.Format("{0}{1}{2}", obj.EtlQuotePrefix, str, obj.EtlQuoteSuffix);
        }

        public static void OnCreateEtlStatements_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Script " + obj.Description + ".sql", "SQL|*.sql");
            if (!string.IsNullOrEmpty(fileName))
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("-- Migration Script Template");
                sb.AppendLine("-- " + obj.Description);
                sb.AppendLine();

                sb.AppendLine("set dateformat dmy");
                sb.AppendLine();

                sb.AppendLine("-----------------------------------------------------------------");
                sb.AppendLine("-- delete all tables ");
                sb.AppendLine("-----------------------------------------------------------------");
                foreach (var cls in obj.SourceTables.Where(i => i.DestinationObjectClass != null).Select(i => i.DestinationObjectClass))
                {
                    sb.Append("DELETE FROM ");
                    sb.Append(obj.EtlDestDatabasePrefix);
                    sb.AppendLine(Quote(cls.TableName, obj));
                }
                sb.AppendLine("GO");
                sb.AppendLine();

                #region common functions
                sb.AppendLine(@"-----------------------------------------------------------------
-- common functions
-----------------------------------------------------------------
IF OBJECT_ID('[dbo].[fn_ConvertStr2Bit]') IS NOT NULL
DROP FUNCTION [dbo].fn_ConvertStr2Bit
GO
CREATE FUNCTION fn_ConvertStr2Bit(@input nvarchar(100))
RETURNS bit
BEGIN
	return case LOWER(@input)
		when 'ja' then 1
		when '-1' then 1
		when '1' then 1
		else 0 
		end
END
GO

IF OBJECT_ID('[dbo].[fn_ConvertStr2Date]') IS NOT NULL
DROP FUNCTION [dbo].fn_ConvertStr2Date
GO
CREATE FUNCTION fn_ConvertStr2Date(@input nvarchar(100))
RETURNS datetime2
BEGIN
	return case when ISDATE(@input)=1 then convert(datetime2, @input) else null end
END
GO

IF OBJECT_ID('[dbo].[fn_ConvertStr2DateErrorMsg]') IS NOT NULL
DROP FUNCTION [dbo].fn_ConvertStr2DateErrorMsg
GO
CREATE FUNCTION fn_ConvertStr2DateErrorMsg(@col nvarchar(100), @input nvarchar(100))
RETURNS nvarchar(1000)
BEGIN
	return case when ISDATE(@input)=0 AND @input is not null then @col + ' enhält kein gültiges Datum ' + @input else null end
END
GO

IF OBJECT_ID('[dbo].[IsInteger]') IS NOT NULL
DROP FUNCTION [dbo].IsInteger
GO
CREATE Function IsInteger(@Value VarChar(18))
Returns Bit
As 
Begin
  
  Return IsNull(
     (Select Case When CharIndex('.', @Value) > 0 
                  Then Case When Convert(int, ParseName(@Value, 1)) <> 0
                            Then 0
                            Else 1
                            End
                  Else 1
                  End
      Where IsNumeric(@Value + 'e0') = 1), 0)

End
GO

IF OBJECT_ID('[dbo].[fn_ConvertStr2Int]') IS NOT NULL
DROP FUNCTION [dbo].fn_ConvertStr2Int
GO
CREATE FUNCTION fn_ConvertStr2Int(@input nvarchar(100))
RETURNS int
BEGIN
	return case when dbo.IsInteger(@input)=1 then convert(int, @input) else null end
END
GO

IF OBJECT_ID('[dbo].[fn_ConvertStr2IntErrorMsg]') IS NOT NULL
DROP FUNCTION [dbo].fn_ConvertStr2IntErrorMsg
GO
CREATE FUNCTION fn_ConvertStr2IntErrorMsg(@col nvarchar(100), @input nvarchar(100))
RETURNS nvarchar(1000)
BEGIN
	return case when dbo.IsInteger(@input)=0 AND @input is not null then @col + ' enhält keine gültige Zahl ' + @input else null end
END
GO

");
                #endregion

                sb.AppendLine("-----------------------------------------------------------------");
                sb.AppendLine("-- enum conversion functions");
                sb.AppendLine("-----------------------------------------------------------------");

                foreach (var tbl in obj.SourceTables.Where(i => i.DestinationObjectClass != null))
                {
                    foreach (var col in tbl.SourceColumn.ToList()
                        .Where(i => i.DestinationProperty != null && typeof(EnumerationProperty).IsAssignableFrom(i.DestinationProperty.GetType())))
                    {
                        var e = (EnumerationProperty)col.DestinationProperty;
                        sb.AppendLine("-- " + e.Enumeration.GetDataTypeString());
                        sb.AppendLine(string.Format("IF OBJECT_ID('[dbo].[fn_mig_ConvertEnum{0}]') IS NOT NULL\nDROP FUNCTION [dbo].fn_mig_ConvertEnum{0}\nGO", e.Enumeration.Name));
                        sb.AppendLine(string.Format("CREATE FUNCTION fn_mig_ConvertEnum{0}(@val {1}{2})",
                            e.Enumeration.Name,
                            "nvarchar", // TODO!!
                            "(" + col.Size + ")" // TODO!!
                            )
                        );
                        sb.AppendLine("RETURNS INT\nBEGIN\n\tDECLARE @result int");
                        sb.AppendLine("\tSELECT @result = CASE @val");
                        foreach (var ev in e.Enumeration.EnumerationEntries)
                        {
                            sb.AppendLine(string.Format("\t\tWHEN '{0}' THEN {1}", ev.Name, ev.Value));
                        }
                        sb.AppendLine("\t\tELSE 0");
                        sb.AppendLine("\tEND\n\tRETURN @result\nEND\nGO\n");
                    }
                }

                sb.AppendLine("-----------------------------------------------------------------");
                sb.AppendLine("-- copy statements");
                sb.AppendLine("-----------------------------------------------------------------");
                foreach (var tbl in obj.SourceTables.Where(i => i.DestinationObjectClass != null))
                {
                    var hasExportGuid = tbl.DestinationObjectClass.ImplementsIExportable(); 

                    sb.AppendLine("-- " + tbl.Description);
                    sb.Append(string.Format("INSERT INTO {0}{1} (", obj.EtlDestDatabasePrefix, Quote(tbl.DestinationObjectClass.TableName, obj)));
                    foreach (var col in tbl.SourceColumn.Where(i => i.DestinationProperty != null))
                    {
                        sb.AppendLine();
                        sb.Append(string.Format("\t{0},", Quote(col.DestinationProperty.Name, obj)));
                    }

                    if (hasExportGuid)
                    {
                        sb.AppendLine();
                        sb.Append(string.Format("\t{0},", Quote("ExportGuid", obj)));
                    }

                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("\n)\nSELECT");
                    foreach (var col in tbl.SourceColumn.Where(i => i.DestinationProperty != null))
                    {
                        sb.AppendLine();
                        if (col.DestinationProperty is EnumerationProperty)
                        {
                            sb.Append(string.Format("\tdbo.fn_mig_ConvertEnum{0}({1}),", 
                                ((EnumerationProperty)col.DestinationProperty).Enumeration.Name, 
                                Quote(col.Name, obj)));
                        }
                        else if (col.DbType == ColumnType.String && col.DestinationProperty is IntProperty)
                        {
                            sb.Append(string.Format("\tdbo.fn_ConvertStr2Int({0}),", Quote(col.Name, obj)));
                        }
                        else if (col.DbType == ColumnType.String && col.DestinationProperty is DateTimeProperty)
                        {
                            sb.Append(string.Format("\tdbo.fn_ConvertStr2Date({0}),", Quote(col.Name, obj)));
                        }
                        else if (col.DbType == ColumnType.String && col.DestinationProperty is BoolProperty)
                        {
                            sb.Append(string.Format("\tdbo.fn_ConvertStr2Bit({0}),", Quote(col.Name, obj)));
                        }
                        else
                        {
                            sb.Append(string.Format("\t{0},", Quote(col.Name, obj)));
                        }
                    }

                    if (hasExportGuid)
                    {
                        sb.AppendLine();
                        sb.Append("\tnewid(),");
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine(string.Format("\nFROM {0}{1}", obj.EtlSrcDatabasePrefix, Quote(tbl.Name, obj)));
                    sb.AppendLine();
                }

                File.WriteAllText(fileName, sb.ToString(), Encoding.UTF8);
            }
        }

        public static void OnCreateMappingReport_MigrationProject(ZBox.App.SchemaMigration.MigrationProject obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Description + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new MigrationProjectMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                new FileInfo(fileName).ShellExecute();
            }
        }

        public static void OnCreateMappingReport_SourceTable(ZBox.App.SchemaMigration.SourceTable obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Name + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new SourceTableMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                new FileInfo(fileName).ShellExecute();
            }
        }

        #region Reports
        [CLSCompliant(false)]
        public class SourceTableMappingReport : PdfReport
        {
            public SourceTableMappingReport()
                : base()
            {
            }

            public SourceTableMappingReport(Document doc)
                : base(doc)
            {
            }

            protected override void DefineDocInfo()
            {
                Document.Info.Title = "Miration report table mapping";
                Document.Info.Author = "dasz.at";
                Document.Info.Subject = "Migration report table mapping";
            }

            private ZBox.App.SchemaMigration.SourceTable _obj = null;

            public void CreateReport(ZBox.App.SchemaMigration.SourceTable obj)
            {
                this._obj = obj;

                RenderSummary();
                RenderColumnMappings();
            }

            private void RenderColumnMappings()
            {
                NewHeading2("Summary");
                var t = NewTable();

                t.AddColumn("4cm");
                t.AddColumn("4cm");
                t.AddColumn("8cm");

                var r = t.AddRow();
                r.Cells[0].AddParagraph("Src Name").Style = styleTableHeader;
                r.Cells[1].AddParagraph("Dest Name").Style = styleTableHeader;
                r.Cells[2].AddParagraph("Description").Style = styleTableHeader;

                foreach (var c in _obj.SourceColumn.OrderBy(i => i.Name))
                {
                    r = t.AddRow();
                    r.Cells[0].AddParagraph(c.Name ?? string.Empty);
                    r.Cells[1].AddParagraph(c.DestinationProperty != null ? c.DestinationProperty.Name : string.Empty);
                    r.Cells[2].AddParagraph(c.Description ?? string.Empty);
                }

                foreach (var c in _obj.SourceColumn.OrderBy(i => i.Name))
                {
                    NewHeading2("Column " + c.Name);
                    if (!string.IsNullOrEmpty(c.Description)) Section.AddParagraph(c.Description).Format.SpaceAfter = "0.5cm";
                    t = NewTable();

                    t.AddColumn("6cm");
                    t.AddColumn("10cm");

                    r = t.AddRow();
                    r.Cells[0].AddParagraph("Source Name");
                    r.Cells[1].AddParagraph(c.Name ?? string.Empty);

                    r = t.AddRow();
                    r.Cells[0].AddParagraph("Dest Name");
                    r.Cells[1].AddParagraph(c.DestinationProperty != null ? c.DestinationProperty.Name : string.Empty);

                    r = t.AddRow();
                    r.Cells[0].AddParagraph("Source Type");
                    r.Cells[1].AddParagraph(c.DbType.ToString());

                    r = t.AddRow();
                    r.Cells[0].AddParagraph("Dest Type");
                    r.Cells[1].AddParagraph(c.DestinationProperty != null ? c.DestinationProperty.GetPropertyTypeString() : string.Empty);

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
                r.Cells[0].AddParagraph("DestinationObjectClass");
                r.Cells[1].AddParagraph(_obj.DestinationObjectClass != null ? _obj.DestinationObjectClass.Name : string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceColumns");
                r.Cells[1].AddParagraph(_obj.SourceColumn.Count.ToString());

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceColumns mapped");
                r.Cells[1].AddParagraph(_obj.SourceColumn.Count(i => i.DestinationProperty != null).ToString());
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

            private ZBox.App.SchemaMigration.MigrationProject _obj = null;

            public void CreateReport(ZBox.App.SchemaMigration.MigrationProject obj)
            {
                this._obj = obj;

                RenderTableMappings();

                foreach (var tbl in _obj.SourceTables.Where(i => i.DestinationObjectClass != null).OrderBy(i => i.Name))
                {
                    var r = new SourceTableMappingReport(Document);
                    r.CreateReport(tbl);
                }

                foreach (var tbl in _obj.SourceTables.Where(i => i.DestinationObjectClass == null).OrderBy(i => i.Name))
                {
                    var r = new SourceTableMappingReport(Document);
                    r.CreateReport(tbl);
                }
            }

            private void RenderTableMappings()
            {
                NewHeading1("Summary");
                var t = NewTable();

                t.AddColumn("4.5cm");
                t.AddColumn("4.5cm");
                t.AddColumn("5.5cm");
                t.AddColumn("1.5cm");

                var r = t.AddRow();
                r.Cells[0].AddParagraph("Src Table").Style = styleTableHeader;
                r.Cells[1].AddParagraph("Dest Class").Style = styleTableHeader;
                r.Cells[2].AddParagraph("Description").Style = styleTableHeader;
                r.Cells[3].AddParagraph("%").Style = styleTableHeader;

                foreach (var c in _obj.SourceTables.OrderBy(i => i.Name))
                {
                    r = t.AddRow();
                    r.Cells[0].AddParagraph(c.Name ?? string.Empty);
                    r.Cells[1].AddParagraph(c.DestinationObjectClass != null ? c.DestinationObjectClass.Name : string.Empty);
                    r.Cells[2].AddParagraph(c.Description ?? string.Empty);
                    r.Cells[3].AddParagraph((c.SourceColumn.Count > 0 ? 100 * c.SourceColumn.Count(i => i.DestinationProperty != null) / c.SourceColumn.Count : 0).ToString() + " %")
                        .Format.Alignment = ParagraphAlignment.Right;
                }
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

                r = t.AddRow();
                r.Cells[0].AddParagraph("SrcProvider");
                r.Cells[1].AddParagraph(_obj.SrcProvider ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("SrcConnectionString");
                r.Cells[1].AddParagraph(_obj.SrcConnectionString ?? string.Empty);

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceTables");
                r.Cells[1].AddParagraph(_obj.SourceTables.Count.ToString());

                r = t.AddRow();
                r.Cells[0].AddParagraph("SourceTables mapped");
                r.Cells[1].AddParagraph(_obj.SourceTables.Count(i => i.DestinationObjectClass != null).ToString());
            }
        }
        #endregion
    }
}