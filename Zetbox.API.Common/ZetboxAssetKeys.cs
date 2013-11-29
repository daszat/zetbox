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

namespace Zetbox.API.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    public static class ZetboxAssetKeys
    {
        #region DataTypes

        public static readonly string DataTypes = "ZetboxBase.DataTypes";

        public static string ConstructNameKey(DataType dt)
        {
            return dt.Module.Namespace + "." + dt.Name;
        }
        public static string ConstructDescriptionKey(DataType dt)
        {
            return dt.Module.Namespace + "." + dt.Name + "_description";
        }

        #endregion

        #region Modules

        public static readonly string Modules = "ZetboxBase.Modules";

        public static string ConstructNameKey(Module m)
        {
            return m.Name;
        }
        public static string ConstructDescriptionKey(Module m)
        {
            return m.Name + "_description";
        }

        #endregion

        #region Applications

        public static readonly string Applications = "GUI.Applications";

        public static string ConstructNameKey(Application app)
        {
            return app.Module.Namespace + "." + app.Name;
        }
        public static string ConstructDescriptionKey(Application app)
        {
            return app.Module.Namespace + "." + app.Name + "_description";
        }

        #endregion

        #region Properties

        public static string ConstructBaseName(Property prop)
        {
            return "ZetboxBase.Properties." + prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name;
        }

        public static string ConstructLabelKey(Property prop)
        {
            return prop.Name + "_label";
        }

        public static string ConstructDescriptionKey(Property prop)
        {
            return prop.Name + "_description";
        }

        #endregion

        #region Methods
        public static string ConstructBaseName(Method meth)
        {
            return "ZetboxBase.Methods." + meth.ObjectClass.Module.Namespace + "." + meth.ObjectClass.Name;
        }

        public static string ConstructLabelKey(Method meth)
        {
            return meth.Name + "_label";
        }

        public static string ConstructDescriptionKey(Method meth)
        {
            return meth.Name + "_description";
        }

        public static string ConstructLabelKey(BaseParameter param)
        {
            return param.Method.Name + "(" + param.Name + ")_label";
        }

        public static string ConstructDescriptionKey(BaseParameter param)
        {
            return param.Method.Name + "(" + param.Name + ")_description";
        }

        #endregion

        #region Enumerations

        public static string ConstructBaseName(Enumeration enumeration)
        {
            return "ZetboxBase.Enumerations." + enumeration.Module.Namespace + "." + enumeration.Name;
        }

        public static string ConstructLabelKey(EnumerationEntry entry)
        {
            return entry.Name + "_label";
        }

        public static string ConstructDescriptionKey(EnumerationEntry entry)
        {
            return entry.Name + "_description";
        }

        #endregion

        #region CategoryTags
        public static readonly string CategoryTags = "ZetboxBase.CategoryTags";
        #endregion

        #region NavigationEntry
        public static readonly string NavigationEntries = "GUI.NavigationEntries";
        public static string ConstructTitleKey(NavigationEntry nav)
        {
            return nav.Module.Namespace + "." + nav.Title;
        }

        public static string ConstructColorKey(NavigationEntry nav)
        {
            return nav.Module.Namespace + "." + nav.Title + "_color";
        }
        #endregion
    }
}
