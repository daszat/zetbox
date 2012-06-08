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
