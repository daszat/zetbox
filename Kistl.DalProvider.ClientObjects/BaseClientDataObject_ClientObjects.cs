
namespace Kistl.DalProvider.ClientObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    /// <summary>
    /// ClientObjects specific BaseClientDataObject functionality.
    /// </summary>
    public abstract class BaseClientDataObject_ClientObjects
        : BaseClientDataObject
    {
        /// <inheritdoc />
        public override bool IsValid()
        {
            ObjectClass oc = this.GetInterfaceType().GetObjectClass(FrozenContext.Single);
            return oc.Properties.Aggregate(
                true,
                (acc, prop) =>
                    acc && prop.Constraints.All(c =>
                        c.IsValid(this, this.GetPropertyValue<object>(prop.Name))));
        }

        /// <summary>
        /// A BaseClientDataObject is always valid.
        /// </summary>
        /// <param name="prop">The name of the porperty to check</param>
        /// <returns>Always returns String.Empty</returns>
        /// This stub also helps simplifying the template, since it provides a simple default.
        protected override string GetPropertyError(string prop)
        {
            return String.Empty;
        }
    }
}
