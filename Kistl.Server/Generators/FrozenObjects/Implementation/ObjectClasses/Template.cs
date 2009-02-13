using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.FrozenObjects.Implementation.ObjectClasses
{
    public class Template
        : Templates.Implementation.ObjectClasses.Template
    {

        public Template(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            return base.GetAdditionalImports().Concat(new string[]{
                "Kistl.DalProvider.Frozen"
            });
        }

        protected override string MungeClassName(string name)
        {
            return base.MungeClassName(name) + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetBaseClass()
        {
            if (this.ObjectClass.BaseObjectClass != null)
            {
                return MungeClassName(base.GetBaseClass());
            }
            else
            {
                return "BaseFrozenDataObject";
            }
        }

        protected override void ApplyClassTailTemplate()
        {
            base.ApplyClassTailTemplate();
            // implement internal constructor to allow the FrozenContext to initialize the objects
            this.WriteObjects("        internal ", this.GetTypeName(), "(FrozenContext ctx, int id)");
            this.WriteLine();
            this.WriteObjects("            : base(ctx, id)");
            this.WriteLine();
            this.WriteLine("        { }");
         
            if (this.ObjectClass.IsFrozenObject)
            {

                this.Host.CallTemplate("Implementation.ObjectClasses.DataStore", ctx, this.ObjectClass);
            }
        }
    }
}
