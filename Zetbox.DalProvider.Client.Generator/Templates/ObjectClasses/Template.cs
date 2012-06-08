
namespace Zetbox.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public class Template : Templates.ObjectClasses.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, Zetbox.API.IZetboxContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyCompoundObjectListTemplate(CompoundObjectProperty prop)
        {
            this.WriteLine("        // CompoundObject list property");
            Templates.Properties.ValueCollectionProperty.Call(Host, ctx,
                this.MembersToSerialize,
                prop,
                "ClientValueCollectionAsListWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyValueTypeListTemplate(ValueTypeProperty prop)
        {
            this.WriteLine("        // value list property");
            Templates.Properties.ValueCollectionProperty.Call(Host, ctx,
                MembersToSerialize,
                prop,
                "ClientValueCollectionAsListWrapper",
                "ClientValueListWrapper");
        }

        protected override void ApplyMethodTemplate(Method m, int index)
        {
            if (m.InvokeOnServer == true)
            {
                ObjectClasses.InvokeServerMethod.Call(Host, ctx, this.DataType, m, index);                
            }
            else
            {
                base.ApplyMethodTemplate(m, index);
            }
        }
    }
}
