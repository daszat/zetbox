using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Server.ObjectClasses
{
    public class Template : Kistl.Server.Generators.Templates.Server.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.App.Base.ObjectClass cls)
            : base(_host, cls)
        {
        }

        protected override void ApplyIDPropertyTemplate()
        {
            base.ApplyIDPropertyTemplate();
            Host.CallTemplate("Server.ObjectClasses.IdProperty");
        }

        protected override string GetAdditionalImports()
        {
            return base.GetAdditionalImports() + "\n"
                + String.Join("\n", new string[] { "Kistl.DALProvider.EF", "System.Data.Objects", "System.Data.Objects.DataClasses" }
                    .Select(s => String.Format("\tusing {0};", s))
                    .ToArray());
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + "__Implementation__";
        }

        protected override string GetBaseClass()
        {
            if (DataType.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseServerDataObject_EntityFramework";
            }
        }

        protected override string[] GetInterfaces()
        {
            return new string[] { DataType.ClassName }.Concat(base.GetInterfaces()).ToArray();
        }
    }
}
