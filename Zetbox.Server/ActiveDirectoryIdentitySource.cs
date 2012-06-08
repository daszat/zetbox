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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using Zetbox.API;

namespace Zetbox.Server
{
    public class ActiveDirectoryIdentitySource : IIdentitySource
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server");

        public IEnumerable<IdentitySourceItem> GetAllIdentities()
        {
            var userList = new Dictionary<string, IdentitySourceItem>();

            ReadUsers(Environment.UserDomainName, userList);
            ReadUsers(Environment.MachineName, userList);

            return userList.Values;
        }

        private void ReadUsers(string machine, Dictionary<string, IdentitySourceItem> userList)
        {
            try
            {
                using (DirectoryEntry root = new DirectoryEntry("WinNT://" + machine))
                {
                    root.Children.SchemaFilter.Add("User");
                    foreach (DirectoryEntry d in root.Children)
                    {
                        var login = machine + "\\" + d.Name;
                        userList[login] = new IdentitySourceItem() { UserName = login, DisplayName = (d.Properties["FullName"].Value ?? login).ToString() };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error reading users from " + machine, ex);
            }
        }
    }
}
