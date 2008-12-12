using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;


namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class SimplePropertyListTemplate
    {

#if INTELLISENSE
        protected Property prop;
#endif

        protected virtual string GetPropertyTypeString()
        {
            if (prop.IsIndexed)
            {
                return string.Format("IList<{0}>", prop.GetPropertyTypeString());
            }
            else
            {
                return string.Format("ICollection<{0}>", prop.GetPropertyTypeString());
            }
        }

        protected virtual string GetPropertyName()
        {
            return prop.PropertyName;
        }
    }
}
