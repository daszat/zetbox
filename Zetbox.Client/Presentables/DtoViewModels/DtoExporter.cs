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

    public class DtoExporter
    {
        private readonly IFileOpener _fileOpener;
        private readonly ITempFileService _tmpService;

        public DtoExporter(IFileOpener fileOpener, ITempFileService tmpService)
        {
            if (fileOpener == null) throw new ArgumentNullException("fileOpener");
            if (tmpService == null) throw new ArgumentNullException("tmpService");

            _fileOpener = fileOpener;
            _tmpService = tmpService;
        }

        public void Export(DtoBaseViewModel dto)
        {
            if (dto is DtoTableViewModel)
            {
                ExportTable((DtoTableViewModel)dto);
            }
        }

        private void ExportTable(DtoTableViewModel dto)
        {
            var tmpFile = _tmpService.CreateWithExtension("_Export.csv"); // Excel can't open two files with the same name, located in another folder!
            StreamWriter sw;
            // http://stackoverflow.com/questions/545666/how-to-translate-ms-windows-os-version-numbers-into-product-names-in-net
            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major <= 5) // assuming Windox XP or lower
                sw = new StreamWriter(tmpFile, false, Encoding.Default);
            else
                sw = new StreamWriter(tmpFile, false, Encoding.UTF8); // use this constructor to ensure BOM
            using (sw)
            {
                // Header
                sw.WriteLine(string.Join(";",
                    dto.Columns.Select(i => "\"" + i.Title.Replace("\"", "\"\"") + "\"").ToArray()));

                // Data
                var maxRow = dto.Rows.Count - 1;
                var maxCol = dto.Columns.Count - 1;
                for (int rowIdx = 0; rowIdx <= maxRow; rowIdx++)
                {
                    var myRow = dto.Rows[rowIdx];
                    for (int colIdx = 0; colIdx <= maxCol; colIdx++)
                    {
                        var myCol = dto.Columns[colIdx];
                        var myCell = dto.Cells.SingleOrDefault(cell => cell.Row == myRow && cell.Column == myCol);
                        if (myCell != null)
                        {
                            string val = myCell.Value.ToString();
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
                        }
                        if (colIdx < maxCol)
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

            _fileOpener.ShellExecute(tmpFile);
        }
    }
}
