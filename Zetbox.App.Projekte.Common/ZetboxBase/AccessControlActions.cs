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
    using Zetbox.App.Extensions;
    using Zetbox.API;
    using Zetbox.API.Utils;

    [Implementor]
    public static class AccessControlActions
    {
        [Invocation]
        public static void ToString(Zetbox.App.Base.AccessControl obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Description;
            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void postSet_Rights(AccessControl obj, PropertyPostSetterEventArgs<AccessRights?> e)
        {
            obj.Recalculate("Description");
        }

        [Invocation]
        public static void get_Description(AccessControl obj, PropertyGetterEventArgs<string> e)
        {
            if (obj is GroupMembership)
            {
                var grp = (GroupMembership)obj;
                e.Result = string.Format("Group {0} has {1} rights", grp.Group != null ? grp.Group.Name : "<null>", obj.Rights);
            }
            else if (obj is RoleMembership)
            {
                var role = (RoleMembership)obj;
                var navigators = new List<string>();
                ObjectClass nextType = obj.ObjectClass;
                foreach (var rel in role.Relations)
                {
                    if (rel == null)
                    {
                        Logging.Log.WarnFormat("Found a null relation in RoleMembership {0}", obj);
                        continue;
                    }
                    if (rel.A != null && rel.A.Type == nextType)
                    {
                        navigators.Add(rel.A.Navigator != null ? rel.A.Navigator.Name : "<?>");
                        nextType = rel.B.Type;
                    }
                    else if (rel.B != null && rel.B.Type == nextType)
                    {
                        navigators.Add(rel.B.Navigator != null ? rel.B.Navigator.Name : "<?>");
                        nextType = rel.A.Type;
                    }
                    else
                    {
                        navigators.Add("<?>");
                    }
                }
                e.Result = string.Format("{0} has {1} rights", string.Join(".", navigators), obj.Rights);
            }
        }
    }
}
