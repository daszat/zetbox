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

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;

    public class InterfaceGenerator
        : AbstractBaseGenerator
    {
        public InterfaceGenerator(IEnumerable<ISchemaProvider> schemaProviders)
            : base(schemaProviders)
        {
        }

        public override string ExtraSuffix { get { return String.Empty; } }
        public override string Description { get { return "Interfaces"; } }
        public override string TargetNameSpace { get { return "Zetbox.Objects"; } }
        public override string TemplateProviderNamespace { get { return "Zetbox.Generator.InterfaceTemplates"; } }
        public override string BaseName { get { return "Interface"; } }
        public override string ProjectGuid { get { return "{0C9E6E69-309F-46F7-A936-D5762229DEB9}"; } }
        public override int CompileOrder { get { return COMPILE_ORDER_Interface; } }

        protected override string Generate_ObjectClass(IZetboxContext ctx, ObjectClass objClass)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", objClass.Name, "Designer.cs", objClass);
        }

        protected override string Generate_CollectionEntries(IZetboxContext ctx)
        {
            return RunTemplateWithExtension(ctx, "CollectionEntries.CollectionEntries", "CollectionEntries", "Designer.cs");
        }

        protected override string Generate_Enumeration(IZetboxContext ctx, Enumeration e)
        {
            return RunTemplateWithExtension(ctx, "Enumerations.Template", e.Name, "Designer.cs", e);
        }

        protected override string Generate_CompoundObject(IZetboxContext ctx, CompoundObject s)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", s.Name, "Designer.cs", s);
        }

        protected override string Generate_Interface(IZetboxContext ctx, Zetbox.App.Base.Interface i)
        {
            return RunTemplateWithExtension(ctx, "Interfaces.Template", i.Name, "Designer.cs", i);
        }

        protected override IEnumerable<string> Generate_Other(IZetboxContext ctx)
        {
            var otherFileNames = new List<string>();

            otherFileNames.Add(RunTemplateWithExtension(ctx, "NamedObjects", "NamedObjects", "Designer.cs"));

            return base.Generate_Other(ctx).Concat(otherFileNames);
        }

        public override IEnumerable<string> RequiredNamespaces
        {
            get { return new List<string>(); }
        }
    }
}
