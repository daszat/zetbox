using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.ObjectClass
{
    public partial class Template
    {
        protected virtual string GetBaseClass()
        {
            if (objClass.BaseObjectClass != null)
            {
                return " : " + objClass.BaseObjectClass.Module.Namespace + "." + objClass.BaseObjectClass.ClassName;
            }
            return "";
        }

        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (p.IsValueTypePropertySingle())
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.ObjectClass.SimplePropertyTemplate.cst"), p);
            }
            else if (p.IsValueTypePropertyList())
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.ObjectClass.SimplePropertyListTemplate.cst"), p);
            }
        }
    }
}
