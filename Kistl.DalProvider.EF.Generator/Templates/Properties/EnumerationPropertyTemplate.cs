
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class EnumerationPropertyTemplate
    {
        //public static void Call(
        //    Arebis.CodeGeneration.IGenerationHost host,
        //    IKistlContext ctx,
        //    Templates.Serialization.SerializationMembersList list,
        //    EnumerationProperty prop,
        //    bool callGetterSetterEvents)
        //{
        //    if (host == null) { throw new ArgumentNullException("host"); }

        //    host.CallTemplate("Properties.EnumerationPropertyTemplate", ctx,
        //        list, prop, callGetterSetterEvents);
        //}

        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list)
        {
            if (list != null)
                list.Add("Serialization.EnumBinarySerialization", Templates.Serialization.SerializerType.All, this.prop.Module.Namespace, this.prop.Name, this.prop);
        }
    }
}
