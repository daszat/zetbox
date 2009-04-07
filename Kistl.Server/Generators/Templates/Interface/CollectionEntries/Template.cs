using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.CollectionEntries
{
    public partial class ObjectCollectionEntry
        : Template
    {
        protected Relation rel { get; private set; }

        public ObjectCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx, (rel.A.Navigator ?? rel.B.Navigator).Module)
        {
            this.rel = rel;
        }

        protected override string GetCeClassName()
        {
            return rel.GetCollectionEntryClassName();
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "INewListEntry" : "INewCollectionEntry",
                rel.A.Type.ClassName,
                rel.B.Type.ClassName);
        }

        protected override bool IsOrdered()
        {
            return rel.NeedsPositionStorage(RelationEndRole.A) || rel.NeedsPositionStorage(RelationEndRole.B);
        }


        protected override string GetDescription()
        {
            return String.Format("ObjectCollectionEntry for {0}", rel.Description);
        }

        protected override IEnumerable<string> GetAdditionalImports()
        {
            var additionalImports = new HashSet<string>();

            // don't forget to import referenced referenced objectclasses' namespaces
            additionalImports.Add(rel.A.Type.Module.Namespace);
            additionalImports.Add(rel.B.Type.Module.Namespace);

            return base.GetAdditionalImports().Concat(additionalImports);
        }
    }

    public partial class ValueCollectionEntry
        : Template
    {
        protected ValueTypeProperty prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx, prop.Module)
        {
            this.prop = prop;
        }

        protected override string GetCeClassName()
        {
            return prop.GetCollectionEntryClassName();
        }

        protected override string GetCeInterface()
        {
            return String.Format("{0}<{1}, {2}>",
                IsOrdered() ? "IValueListEntry" : "IValueCollectionEntry",
                this.prop.ObjectClass.ClassName,
                this.prop.GetPropertyTypeString());
        }

        protected override bool IsOrdered()
        {
            return prop.IsIndexed;
        }

        protected override string GetDescription()
        {
            return String.Format("ValueCollectionEntry for {0}", prop.Description);
        }
    }

    public abstract partial class Template
    {
        protected abstract string GetCeClassName();

        /// <returns>which CollectionEntry interface to implement.</returns>
        protected abstract string GetCeInterface();

        /// <returns>true, if any side is ordered</returns>
        protected abstract bool IsOrdered();

        /// <returns>an one-line description of this interface</returns>
        protected abstract string GetDescription();

        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return new string[] { };
        }
    }
}
