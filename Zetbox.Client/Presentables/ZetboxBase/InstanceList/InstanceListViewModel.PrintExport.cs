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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Models;

    public partial class InstanceListViewModel
    {
        private ICommandViewModel _PrintCommand = null;
        public ICommandViewModel PrintCommand
        {
            get
            {
                if (_PrintCommand == null)
                {
                    _PrintCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.PrintCommand,
                        InstanceListViewModelResources.PrintCommand_Tooltip,
                        Print, null, null);
                    _PrintCommand.Icon = IconConverter.ToImage(Zetbox.NamedObjects.Gui.Icons.ZetboxBase.Printer_png.Find(FrozenContext));
                }
                return _PrintCommand;
            }
        }

        public void Print()
        {
            var doc = new MigraDoc.DocumentObjectModel.Document();
            var s = doc.AddSection();
            s.PageSetup.Orientation = MigraDoc.DocumentObjectModel.Orientation.Landscape;
            s.PageSetup.PageFormat = MigraDoc.DocumentObjectModel.PageFormat.A4;
            s.PageSetup.TopMargin = "2cm";
            s.PageSetup.BottomMargin = "2cm";
            s.PageSetup.LeftMargin = "2cm";
            s.PageSetup.RightMargin = "3cm";
            var tbl = s.AddTable();
            tbl.Borders.Visible = true;

            // Footer
            var p = s.Footers.Primary.AddParagraph();
            p.Format.Font.Size = 10;
            p.Format.AddTabStop("245mm", MigraDoc.DocumentObjectModel.TabAlignment.Right);

            p.AddText(DateTime.Today.ToShortDateString());
            p.AddTab();
            p.AddPageField();
            p.AddText("/");
            p.AddNumPagesField();

            var cols = DisplayedColumns.Columns
                .Where(i => i.Type != ColumnDisplayModel.ColumnType.MethodModel)
                .ToList();

            // TODO: Calc width more sophisticated
            var width = new MigraDoc.DocumentObjectModel.Unit(250.0 * (1.0 / (double)cols.Count), MigraDoc.DocumentObjectModel.UnitType.Millimeter);

            // Header
            for (int colIdx = 0; colIdx < cols.Count; colIdx++)
            {
                //var col = cols[colIdx];
                tbl.AddColumn(width);
            }

            var row = tbl.AddRow();
            row.HeadingFormat = true;
            for (int colIdx = 0; colIdx < cols.Count; colIdx++)
            {
                var col = cols[colIdx];
                p = row.Cells[colIdx].AddParagraph(col.Header ?? string.Empty);
                p.Format.Font.Bold = true;
            }


            // Data
            foreach (var obj in Instances)
            {
                row = tbl.AddRow();
                for (int colIdx = 0; colIdx < cols.Count; colIdx++)
                {
                    string val = cols[colIdx].ExtractFormattedValue(obj);
                    p = row.Cells[colIdx].AddParagraph(val ?? string.Empty);
                }
            }

            var filename = tmpService.Create("Export.pdf");

            var pdf = new MigraDoc.Rendering.PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.None);
            pdf.Document = doc;
            pdf.RenderDocument();
            pdf.Save(filename);

            fileOpener.ShellExecute(filename);
        }

        private ICommandViewModel _ExportCommand = null;
        public ICommandViewModel ExportCommand
        {
            get
            {
                if (_ExportCommand == null)
                {
                    _ExportCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.ExportCSVCommand,
                        InstanceListViewModelResources.ExportCSVCommand_Tooltip,
                        Export, null, null);
                }
                return _ExportCommand;
            }
        }

        public void Export()
        {
            var tmpFile = tmpService.CreateWithExtension("_Export.csv"); // Excel can't open two files with the same name, located in another folder!
            StreamWriter sw;
            // http://stackoverflow.com/questions/545666/how-to-translate-ms-windows-os-version-numbers-into-product-names-in-net
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major <= 5) // assuming Windox XP or lower
                sw = new StreamWriter(tmpFile, false, Encoding.Default);
            else
                sw = new StreamWriter(tmpFile, false, Encoding.UTF8); // use this constructor to ensure BOM
            using (sw)
            {
                var cols = DisplayedColumns.Columns
                    .Where(i => i.Type != ColumnDisplayModel.ColumnType.MethodModel)
                    .ToList();
                // Header
                sw.WriteLine(string.Join(";",
                    cols.Select(i => i.Header).ToArray()));

                // Data
                foreach (var obj in Instances)
                {
                    for (int colIdx = 0; colIdx < cols.Count; colIdx++)
                    {
                        string val = cols[colIdx].ExtractFormattedValue(obj);
                        if (val != null)
                        {
                            var needsQuoting = val.IndexOfAny(new[] { ';', '\n', '\r', '"' }) >= 0;
                            if (needsQuoting)
                            {
                                val = val.Replace("\"", "\"\"");
                                val = "\"" + val + "\"";
                            }
                            sw.Write(val);
                        }
                        if (colIdx < cols.Count - 1)
                        {
                            sw.Write(";");
                        }
                        else
                        {
                            sw.WriteLine();
                        }
                    }
                }
            }

            fileOpener.ShellExecute(tmpFile);
        }

        private ICommandViewModel _ExportContainerCommand = null;
        public ICommandViewModel ExportContainerCommand
        {
            get
            {
                if (_ExportContainerCommand == null)
                {
                    _ExportContainerCommand = ViewModelFactory.CreateViewModel<ContainerCommand.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.ExportContainerCommand,
                        InstanceListViewModelResources.ExportContainerCommand_Tooltip,
                        ExportCommand, PrintCommand);
                }
                return _ExportContainerCommand;
            }
        }
    }
}
