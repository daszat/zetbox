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
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables;
    using System.IO;

    /// <summary>
    /// Client implementation
    /// </summary>    
    [Implementor]
    public class SourceTableActions
    {
        private static IViewModelFactory _mdlFactory = null;
        private static IFileOpener _fileOpener = null;

        public SourceTableActions(IViewModelFactory mdlFactory, IFileOpener fileOpener)
        {
            _mdlFactory = mdlFactory;
            _fileOpener = fileOpener;

        }
        [Invocation]
        public static void CreateMappingReport(Zetbox.App.SchemaMigration.SourceTable obj)
        {
            var fileName = _mdlFactory.GetDestinationFileNameFromUser("Migration Report " + obj.Name + ".pdf", "PDF|*.pdf");
            if (!string.IsNullOrEmpty(fileName))
            {
                var r = new SourceTableMappingReport();
                r.CreateReport(obj);
                r.Save(fileName);
                _fileOpener.ShellExecute(fileName);
            }
        }
    }
}
