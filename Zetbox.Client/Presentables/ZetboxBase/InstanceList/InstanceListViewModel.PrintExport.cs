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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.App.Extensions;
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
            SetBusy(string.Format(InstanceListViewModelResources.ExportBusyMessageFormat, 1));

            var tmpFile = tmpService.CreateWithExtension("_Export.csv"); // Excel can't open two files with the same name, located in another folder!
            StreamWriter sw;
            // http://stackoverflow.com/questions/545666/how-to-translate-ms-windows-os-version-numbers-into-product-names-in-net
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major <= 5) // assuming Windox XP or lower
                sw = new StreamWriter(tmpFile, false, Encoding.Default);
            else
                sw = new StreamWriter(tmpFile, false, Encoding.UTF8); // use this constructor to ensure BOM

            var cols = DisplayedColumns.Columns
                    .Where(i => i.Type != ColumnDisplayModel.ColumnType.MethodModel)
                    .ToList();
            // Header
            sw.WriteLine(string.Join(";",
                cols.Select(i => i.Header).ToArray()));

            var unpagedQuery = GetUnpagedQuery();

            GetPagedQuery(0, Helper.MAXLISTCOUNT, unpagedQuery)
                .OnError(ex => OnExportPageError(sw, ex))
                .OnResult(OnExportPageResultFactory(sw, cols, 0, Helper.MAXLISTCOUNT, unpagedQuery, tmpFile));

        }

        private ICommandViewModel _ExportXMLCommand = null;
        public ICommandViewModel ExportXMLCommand
        {
            get
            {
                if (_ExportXMLCommand == null)
                {
                    _ExportXMLCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this,
                        InstanceListViewModelResources.ExportXmlCommand,
                        InstanceListViewModelResources.ExportXmlommand_Tooltip,
                        ExportXML,
                        CanExportXML,
                        null);
                }
                return _ExportXMLCommand;
            }
        }

        private bool? _canExportXML;
        public bool CanExportXML()
        {
            if (_canExportXML == null)
            {
                _canExportXML = DataType.ImplementsIExportable();
            }
            return _canExportXML.Value;
        }

        public void ExportXML()
        {
            var fileName = ViewModelFactory.GetDestinationFileNameFromUser("Zetbox.xml", "XML|*.xml");
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                SetBusy(string.Format(InstanceListViewModelResources.ExportBusyMessageFormat, 1));
                var unpagedQuery = GetUnpagedQuery();

                var objects = new List<IDataObject>();
                GetPagedQuery(0, Helper.MAXLISTCOUNT, unpagedQuery)
                    .OnError(ex => OnExportPageError(ex))
                    .OnResult(OnExportPageResultFactory(objects, 0, Helper.MAXLISTCOUNT, unpagedQuery, fileName));
            }
        }

        private Action<ZbTask<IEnumerable>> OnExportPageResultFactory(List<IDataObject> objects, int page, int pageSize, IQueryable unpagedQuery, string fileName)
        {
            return t =>
            {
                var result = t.Result.Cast<IDataObject>().ToList();
                objects.AddRange(result);
                bool queryAgain = result.Count == pageSize;

                if (queryAgain)
                {
                    CurrentBusyMessage = string.Format(InstanceListViewModelResources.ExportBusyMessageFormat, page + 1);
                    GetPagedQuery(page + 1, pageSize, unpagedQuery)
                        .OnError(ex => OnExportPageError(ex))
                        .OnResult(OnExportPageResultFactory(objects, page + 1, pageSize, unpagedQuery, fileName));
                }
                else
                {
                    Zetbox.App.Packaging.Exporter.Export(DataContext, fileName, objects);
                    fileOpener.ShellExecute(Path.GetDirectoryName(fileName));
                    ClearBusy();
                }
            };
        }

        private Action<ZbTask<IEnumerable>> OnExportPageResultFactory(StreamWriter sw, List<ColumnDisplayModel> cols, int page, int pageSize, IQueryable unpagedQuery, string tmpFile)
        {
            return t =>
            {
                bool queryAgain = ExportPage(sw, cols, t.Result.Cast<IDataObject>()) == pageSize;

                if (queryAgain)
                {
                    CurrentBusyMessage = string.Format(InstanceListViewModelResources.ExportBusyMessageFormat, page + 1);
                    GetPagedQuery(page + 1, pageSize, unpagedQuery)
                        .OnError(ex => OnExportPageError(sw, ex))
                        .OnResult(OnExportPageResultFactory(sw, cols, page + 1, pageSize, unpagedQuery, tmpFile));
                }
                else
                {
                    sw.Dispose();
                    fileOpener.ShellExecute(tmpFile);
                    ClearBusy();
                }
            };
        }

        private void OnExportPageError(Exception ex)
        {
            OnExportPageError(null, ex);
        }

        private void OnExportPageError(IDisposable d, Exception ex)
        {
            if(d != null) d.Dispose();

            ClearBusy();
            errorReporter.Value.Show(ex);
        }

        private int ExportPage(StreamWriter sw, List<ColumnDisplayModel> cols, IEnumerable<IDataObject> instances)
        {
            try
            {
                var dmvos = instances
                    .Select(obj => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj));

                int count = 0;
                // Data
                foreach (var obj in dmvos)
                {
                    count += 1;

                    // This check has to be done AFTER counting
                    // to avoid the edgecase of pageSize deleted objects followed by more data
                    if (obj.ObjectState == DataObjectState.Deleted)
                        continue;

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

                return count;
            }
            catch (Exception ex)
            {
                OnExportPageError(sw, ex);
                return 0;
            }
        }

        private static ZbTask<System.Collections.IEnumerable> GetPagedQuery(int page, int pageSize, IQueryable unpagedQuery)
        {
            var qryTask = unpagedQuery
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return qryTask;
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
                        ExportCommand, PrintCommand, ExportXMLCommand);
                }
                return _ExportContainerCommand;
            }
        }
    }
}
