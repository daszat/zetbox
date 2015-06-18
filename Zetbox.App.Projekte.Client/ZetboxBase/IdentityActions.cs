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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.GUI;
    using Zetbox.App.Projekte.Client.ZetboxBase;
    using Zetbox.Client.Presentables;

    [Implementor]
    public class IdentityActions
    {
        private static IViewModelFactory _vmf;

        public IdentityActions(IViewModelFactory vmf)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            _vmf = vmf;
        }

        [Invocation]
        public static void SetPasswordUI(Identity obj)
        {
            _vmf.CreateDialog(obj.Context, Strings.SetPasswordDlgTitle)
                .AddPassword("pwd", Strings.Password)
                .AddPassword("repeat", Strings.RepeatPassword)
                .Show(values =>
                {
                    var pwd = (string)values["pwd"];
                    var repeat = (string)values["repeat"];

                    if (string.IsNullOrEmpty(pwd))
                    {
                        _vmf.ShowMessage(Strings.PasswordEmpty, Strings.SetPasswordDlgTitle);
                        return;
                    }

                    if(pwd != repeat)
                    {
                        _vmf.ShowMessage(Strings.PasswordDoesNotMatch, Strings.SetPasswordDlgTitle);
                        return;
                    }

                    obj.SetPassword(pwd);
                });
        }
    }
}
