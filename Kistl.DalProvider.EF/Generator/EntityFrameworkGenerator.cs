
namespace Kistl.DalProvider.EF.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.Server.Generators;

    public class EntityFrameworkGenerator
        : BaseDataObjectGenerator
    {
        public override string Description { get { return "EfObjects"; } }
        public override string TargetNameSpace { get { return "Kistl.Objects.Server"; } }
        public override string BaseName { get { return "Server"; } }
        public override string ProjectGuid { get { return "{62B9344A-87D1-4715-9ABB-EAE0ACC4F523}"; } }

        protected override IEnumerable<string> Generate_Other(Kistl.API.IKistlContext ctx)
        {
            using (log4net.NDC.Push("EfGenerateOther"))
            {
                var otherFileNames = new List<string>();

                // those are not compilable, therefore don't add them to otherFileNames.
                // should be handled separately in the ProjectFile Template
                this.RunTemplate(ctx, "Implementation.EfModel.ModelCsdl", "Model.csdl");
                this.RunTemplate(ctx, "Implementation.EfModel.ModelMsl", "Model.msl");
                this.RunTemplate(ctx, "Implementation.EfModel.ModelSsdl", "Model.ssdl");

                otherFileNames.Add(this.RunTemplateWithExtension(ctx, "Implementation.ObjectClasses.AssociationSetAttributes", "AssociationSetAttributes", "cs"));

                return base.Generate_Other(ctx).Concat(otherFileNames);
            }
        }
    }
}
