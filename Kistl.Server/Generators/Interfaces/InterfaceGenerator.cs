using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Interfaces
{
    public class InterfaceGenerator
        : BaseDataObjectGenerator
    {
        public override string TemplateProviderPath { get { return this.GetType().Namespace; } }
        public override string TargetNameSpace { get { return "Kistl.Objects"; } }
        public override string BaseName { get { return "Interface"; } }
        public override string ProjectGuid { get { return "{0C9E6E69-309F-46F7-A936-D5762229DEB9}"; } }

        protected override string Generate_ObjectClass(IKistlContext ctx, ObjectClass objClass)
        {
            return RunTemplateWithExtension(ctx, "Interface.DataTypes.Template", objClass.ClassName, "Designer.cs", objClass);
        }

        protected override string Generate_CollectionEntries(IKistlContext ctx)
        {
            // collection entries have no specific interfaces; See Kistl.API.ICollectionEntry and friends
            return null;
        }

        protected override string Generate_Enumeration(IKistlContext ctx, Enumeration e)
        {
            return RunTemplateWithExtension(ctx, "Interface.Enumerations.Template", e.ClassName, "Designer.cs", e);
        }

        protected override string Generate_Struct(IKistlContext ctx, Struct s)
        {
            return RunTemplateWithExtension(ctx, "Interface.DataTypes.Template", s.ClassName, "Designer.cs", s);
        }

        protected override string Generate_Interface(IKistlContext ctx, Kistl.App.Base.Interface i)
        {
            return RunTemplateWithExtension(ctx, "Interface.DataTypes.Template", i.ClassName, "Designer.cs", i);
        }

        protected override IEnumerable<string> Generate_Other(IKistlContext ctx)
        {
            var otherFileNames = new List<string>();

            var modules = ctx.GetQuery<Module>().ToList();
            otherFileNames.Add(RunTemplateWithExtension(ctx, "Interface.Repositories.ModuleRepository", "ModuleRepository", "Designer.cs", modules));
            foreach (var m in modules)
            {
                otherFileNames.Add(RunTemplateWithExtension(ctx, "Interface.Repositories.Repository", m.ModuleName + "Repository", "Designer.cs", m));
            }

            return base.Generate_Other(ctx).Concat(otherFileNames);
        }

    }
}
