using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.App.Extensions;

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
            if (objClass.IsFrozen())
                return base.Generate_ObjectClass(ctx, objClass);
            else
                return null;
        }

        protected override IEnumerable<string> Generate_Other(Kistl.API.IKistlContext ctx)
        {
            var otherFileNames = new List<string>();

            // TODO: IsFrozenObject doesn't contain enough information, should check parents too
            var modulesWithFrozenClasses = ctx.GetQuery<Module>()
                .ToList()
                .Where(m => m.DataTypes.OfType<ObjectClass>().Any(cls => cls.IsFrozenObject))
                .OrderBy(m => m.ModuleName)
                .ToList();

            otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenContextImplementation", "FrozenContextImplementation", "Designer.cs", modulesWithFrozenClasses));
            foreach (var m in modulesWithFrozenClasses)
            {
                otherFileNames.Add(RunTemplateWithExtension(ctx, "Repositories.FrozenRepository", "Frozen" + m.ModuleName + "Repository", "Designer.cs", m));
            }

            return base.Generate_Other(ctx).Concat(otherFileNames);
        }
    }
}
