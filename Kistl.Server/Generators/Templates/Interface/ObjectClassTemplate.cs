using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;

namespace Kistl.Server.Generators.Templates.Interface
{
    public partial class ObjectClassTemplate
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
                this.Host.CallTemplate(ResolveResourceUrl("Interface.SimplePropertyTemplate.cst"), p);
            }
            else if (p.IsValueTypePropertyList())
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.SimplePropertyListTemplate.cst"), p);
            }
        }
    }
}
