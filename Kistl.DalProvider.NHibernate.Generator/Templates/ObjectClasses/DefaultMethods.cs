
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Templates = Kistl.Generator.Templates;

    public class DefaultMethods
        : Templates.ObjectClasses.DefaultMethods
    {
        public DefaultMethods(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt)
            : base(_host, ctx, dt)
        {
        }

        protected override void ApplyPrePreSaveTemplate()
        {
            base.ApplyPrePreSaveTemplate();
            foreach (var propertyName in dt.Properties.Where(p => p.DefaultValue != null || p.IsCalculated()).Select(p => p.Name))
            {
                this.WriteObjects("            Fetch", propertyName, "OrDefault();");
                this.WriteLine();
            }
        }

        protected override void ApplyPostCreatedTemplate()
        {
            base.ApplyPostCreatedTemplate();
            foreach (var propertyName in dt.Properties.Where(p => p.IsCalculated()).Select(p => p.Name))
            {
                this.WriteObjects("            _", propertyName, "_IsDirty = true;");
                this.WriteLine();
            }
        }
    }
}
