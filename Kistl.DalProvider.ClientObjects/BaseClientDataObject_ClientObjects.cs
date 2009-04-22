using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.DalProvider.ClientObjects
{
    public abstract class BaseClientDataObject_ClientObjects 
        : BaseClientDataObject
    {
        public override bool IsValid()
        {
            ObjectClass oc = this.GetObjectClass(FrozenContext.Single);
            return oc.Properties.Aggregate(true, (acc, prop) =>
                acc && prop.Constraints.All(c =>
                    c.IsValid(this, this.GetPropertyValue<object>(prop.PropertyName))));
        }

        // stub to get everything to compile
        protected override string GetPropertyError(string prop)
        {
            return String.Empty;
        }
    }

}
