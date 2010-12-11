
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Server;
    using Kistl.App.Base;

    public class InterfaceGenerator
        : AbstractBaseGenerator
    {
        public InterfaceGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return String.Empty; } }
        public override string Description { get { return "Interfaces"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects"; } }
        public override string TemplateProviderNamespace { get { return "Kistl.Generator.InterfaceTemplates"; } }
        public override string BaseName { get { return "Interface"; } }
        public override string ProjectGuid { get { return "{0C9E6E69-309F-46F7-A936-D5762229DEB9}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Interface; } }

        protected override string Generate_ObjectClass(IKistlContext ctx, ObjectClass objClass)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", objClass.Name, "Designer.cs", objClass);
        }

        protected override string Generate_CollectionEntries(IKistlContext ctx)
        {
            return RunTemplateWithExtension(ctx, "CollectionEntries.CollectionEntries", "CollectionEntries", "Designer.cs");
        }

        protected override string Generate_Enumeration(IKistlContext ctx, Enumeration e)
        {
            return RunTemplateWithExtension(ctx, "Enumerations.Template", e.Name, "Designer.cs", e);
        }

        protected override string Generate_CompoundObject(IKistlContext ctx, CompoundObject s)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", s.Name, "Designer.cs", s);
        }

        protected override string Generate_Interface(IKistlContext ctx, Kistl.App.Base.Interface i)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", i.Name, "Designer.cs", i);
        }

        //protected override IEnumerable<string> Generate_Other(IKistlContext ctx)
        //{
        //    var otherFileNames = new List<string>();

        //    var modules = ctx.GetQuery<Module>().OrderBy(m => m.Name).ToList();
        //    otherFileNames.Add(RunTemplateWithExtension(ctx, "Interface.Repositories.ModuleRepository", "ModuleRepository", "Designer.cs", modules));
        //    foreach (var m in modules)
        //    {
        //        otherFileNames.Add(RunTemplateWithExtension(ctx, "Interface.Repositories.Repository", m.Name + "Repository", "Designer.cs", m));
        //    }

        //    return base.Generate_Other(ctx).Concat(otherFileNames);
        //}

        public override IEnumerable<string> RequiredNamespaces
        {
            get { return new List<string>(); }
        }
    }
}
