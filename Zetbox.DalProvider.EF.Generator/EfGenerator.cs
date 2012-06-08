
namespace Zetbox.DalProvider.Ef.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;
    using Zetbox.Generator;

    public class EntityFrameworkGenerator
        : AbstractBaseGenerator
    {
        public EntityFrameworkGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return "Ef"; } }
        public override string Description { get { return "Ef"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects.Ef"; } }
        public override string BaseName { get { return "Ef"; } }
        public override string ProjectGuid { get { return "{62B9344A-87D1-4715-9ABB-EAE0ACC4F523}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Implementation; } }

        public override IEnumerable<string> RequiredNamespaces
        {
            get
            {
                return new string[]{
                   "Zetbox.API.Server",
                   "Zetbox.DalProvider.Ef",
                   "System.Data.Objects",
                   "System.Data.Objects.DataClasses" 
                };
            }
        }

        protected override IEnumerable<string> Generate_Other(Zetbox.API.IZetboxContext ctx)
        {
            using (log4net.NDC.Push("EfGenerateOther"))
            {
                var otherFileNames = new List<string>();

                // those are not compilable, therefore don't add them to otherFileNames.
                // should be handled separately in the ProjectFile Template
                this.RunTemplate(ctx, "EfModel.ModelCsdl", "Model.csdl");
                this.RunTemplate(ctx, "EfModel.ModelMsl", "Model.msl");
                foreach (var schemaProvider in SchemaProviders.Where(sp => sp.IsStorageProvider))
                {
                    this.RunTemplate(ctx, "EfModel.ModelSsdl", String.Format("Model.{0}.ssdl", schemaProvider.ConfigName), schemaProvider);
                }
                otherFileNames.Add(this.RunTemplateWithExtension(ctx, "ObjectClasses.AssociationSetAttributes", "AssociationSetAttributes", "cs"));

                return base.Generate_Other(ctx).Concat(otherFileNames);
            }
        }
    }
}
