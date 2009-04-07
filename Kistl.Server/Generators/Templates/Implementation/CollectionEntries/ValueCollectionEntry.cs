using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class ValueCollectionEntry
        : Template
    {
        protected ValueTypeProperty prop { get; private set; }

        public ValueCollectionEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ValueTypeProperty prop)
            : base(_host, ctx)
        {
            this.prop = prop;
        }
        
        protected override void ApplyRelationIdPropertyTemplate()
        {
        }

        protected override void ApplyObjectGetterTemplate()
        {
            this.WriteLine("        public IDataObject ParentObject {{ get {{ return Parent; }} set {{ Parent = ({0})value; }} }}", prop.ObjectClass.ClassName);
            this.WriteLine("        public object ValueObject {{ get {{ return Value; }} set {{ Value = ({0})value; }} }}", prop.ReferencedTypeAsCSharp());
        }

        protected override string GetCeClassName()
        {
            return prop.GetCollectionEntryClassName() + Kistl.API.Helper.ImplementationSuffix;
        }

        protected override string GetCeInterface()
        {
            return prop.GetCollectionEntryClassName();
        }

        protected override bool IsOrdered()
        {
            return prop.IsIndexed;
        }

        protected override void ApplyChangesFromBody()
        {
            if (IsOrdered())
            {
                this.WriteLine("            me.AIndex = other.AIndex;");
            }
            this.WriteLine("            me.B = other.B;");
        }
    }
}
