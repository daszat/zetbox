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

        protected virtual string GetPropertyTypeString()
        {
            return prop.GetCollectionTypeString();
        }

        protected virtual string GetPropertyName()
        {
            return prop.PropertyName;
        }
    }
}
