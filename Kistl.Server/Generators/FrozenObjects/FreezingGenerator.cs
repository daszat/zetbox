using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.Server.Generators.FrozenObjects
{
    public class FreezingGenerator
        : BaseDataObjectGenerator
    {

        public override string TemplateProviderPath { get { return this.GetType().Namespace; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Frozen"; } }
        public override string BaseName { get { return "Frozen"; } }
        public override string ProjectGuid { get { return "{CA615374-AEA3-4187-BF73-584CCD082766}"; } }

        protected override string Generate_ObjectClass(Kistl.API.IKistlContext ctx, ObjectClass objClass)
        {
            if (objClass.IsFrozenObject)
                return base.Generate_ObjectClass(ctx, objClass);
            else
                return null;
        }

        protected override IEnumerable<string> Generate_Other(Kistl.API.IKistlContext ctx)
        {
            var otherFileNames = new List<string>();

            var modules = ctx.GetQuery<Module>().ToList();
            otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenModuleRepository", "FrozenModuleRepository", "Designer.cs", modules));
            foreach (var m in modules)
            {
                otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenRepository", "Frozen" + m.ModuleName + "Repository", "Designer.cs", m));
            }

            return base.Generate_Other(ctx).Concat(otherFileNames);
        }
    }
}
