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
namespace Zetbox.App.LicenseManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;

    [Implementor]
    public class LicenseActions
    {
        private static IViewModelFactory _vmdl;

        public LicenseActions(IViewModelFactory vmdl)
        {
            _vmdl = vmdl;
        }

        [Invocation]
        public static void ExportUI(License obj)
        {
            var file = _vmdl.GetDestinationFileNameFromUser(Helper.GetLegalFileName($"{obj.Licensee?.Replace(".", "_")}-{obj.ValidFrom.ToString("yyyyMMdd")}-{obj.ValidThru.ToString("yyyyMMdd")}.license"));
            if(!string.IsNullOrWhiteSpace(file))
            {
                obj.Export(file);
            }
        }
    }
}
