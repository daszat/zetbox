// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

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
