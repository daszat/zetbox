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

namespace Zetbox.DalProvider.Ef.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class ValueCollectionEntry
        : Templates.CollectionEntries.ValueCollectionEntry
    {
        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, Property prop)
            : base(_host, ctx, prop)
        {
        }

        protected override void ApplyConstructorTemplate()
        {
            // replace base constructors
            //base.ApplyConstructorTemplate();

            this.WriteObjects("[Obsolete]");
            this.WriteLine();
            this.WriteObjects("public ", GetCeClassName(), "()");
            this.WriteLine();
            this.WriteObjects(": base(null)");
            this.WriteLine();
            this.WriteObjects("{");
            this.WriteLine();

            if (this.prop is CompoundObjectProperty)
            {
                Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, prop.GetElementTypeString() + ImplementationSuffix, "Value", "_Value", "null");
            }

            this.WriteObjects("}");
            this.WriteLine();

            this.WriteObjects("public ", GetCeClassName() ,"(Func<IFrozenContext> lazyCtx)");
            this.WriteLine();
            this.WriteObjects("    : base(lazyCtx)");
            this.WriteLine();
            this.WriteObjects("{");
            this.WriteLine();
            
            if (this.prop is CompoundObjectProperty)
            {
                Templates.Properties.CompoundObjectPropertyInitialisation.Call(Host, ctx, prop.GetElementTypeString() + ImplementationSuffix, "Value", "_Value", "lazyCtx");
            }

            this.WriteObjects("}");
            this.WriteLine();

        }

        protected override void ApplyClassAttributeTemplate()
        {
            base.ApplyClassAttributeTemplate();
            this.WriteObjects(@"    [EdmEntityType(NamespaceName=""Model"", Name=""",
                prop.GetCollectionEntryClassName(), @""")]");
            this.WriteLine();
        }
    }
}
