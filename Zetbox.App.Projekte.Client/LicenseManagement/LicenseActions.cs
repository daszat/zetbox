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
    using Zetbox.Client.GUI;

    [Implementor]
    public class LicenseActions
    {
        private static IViewModelFactory _vmf;
        private static IFrozenContext _frozenContext;

        public LicenseActions(IViewModelFactory vmf, IFrozenContext frozenContext)
        {
            _vmf = vmf;
            _frozenContext = frozenContext;
        }

        [Invocation]
        public static System.Threading.Tasks.Task ExportUI(License obj)
        {
            var file = _vmf.GetDestinationFileNameFromUser(Helper.GetLegalFileName($"{obj.Licensee?.Replace(".", "_")}-{obj.ValidFrom.ToString("yyyyMMdd")}-{obj.ValidThru.ToString("yyyyMMdd")}.license"));
            if (!string.IsNullOrWhiteSpace(file))
            {
                obj.Export(file);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task SignUI(License obj)
        {
            _vmf.CreateDialog(obj.Context, "Sign")
                .AddObjectReference("key", "Private key", NamedObjects.Base.Classes.Zetbox.App.LicenseManagement.PrivateKey.Find(_frozenContext))
                .AddPassword("pwd", "Password", "Optional a password. If empty, the password from the private key will be used.", "You should realy avoid storing passwords in the private key.")
                .OnAccept(values =>
                {
                    var key = (PrivateKey)values["key"];
                    var pwd = (string)values["pwd"];

                    if(key == null)
                    {
                        _vmf.ShowMessage("No private key selected", "Error");
                        return;
                    }

                    try
                    {
                        obj.Sign(key, pwd);
                    }
                    catch (System.Security.Cryptography.CryptographicException ex)
                    {
                        _vmf.ShowMessage(ex.Message, "Error");
                    }
                })
                .Show();

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
