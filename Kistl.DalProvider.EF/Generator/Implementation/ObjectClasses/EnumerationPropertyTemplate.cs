

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Server.Generators.Templates.Implementation;

    public partial class EnumerationPropertyTemplate
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            SerializationMembersList list,
            EnumerationProperty prop,
            bool callGetterSetterEvents)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.EnumerationPropertyTemplate", ctx,
                list, prop, callGetterSetterEvents);
        }

        protected virtual void AddSerialization(SerializationMembersList list)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.EnumBinarySerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, this.prop);
        }
    }
}
