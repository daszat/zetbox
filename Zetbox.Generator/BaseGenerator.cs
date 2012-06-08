
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;

    public class BaseGenerator
        : AbstractBaseGenerator
    {
        public BaseGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string BaseName { get { return "Base"; } }
        public override string Description { get { return "Base"; } }
        public override string ExtraSuffix { get { return "Base"; } }
        public override string ProjectGuid { get { return "{18AA936D-BA5E-47A1-9F43-0473D88E10A5}"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Base"; } }
        public override string TemplateProviderNamespace { get { return "Kistl.Generator.Templates"; } }
        public override IEnumerable<string> RequiredNamespaces { get { return new string[] { "Kistl.DalProvider.Base" }; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        protected override string Generate_ProjectFile(API.IKistlContext ctx, string projectGuid, List<string> generatedFileNames, IEnumerable<ISchemaProvider> schemaProviders)
        {
            return RunTemplate(ctx, "ProjectFile",
                         TargetNameSpace + ".csproj",
                         projectGuid,
                         generatedFileNames.Where(s => !String.IsNullOrEmpty(s)).ToList(),
                         schemaProviders);
        }
    }
}
