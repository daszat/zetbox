using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class ValueCollectionEntry
        : Template
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, ValueTypeProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.CollectionEntries.ValueCollectionEntry", ctx, prop);
        }

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
            return prop.HasPersistentOrder;
        }

        protected override void ApplyChangesFromBody()
        {
            if (IsOrdered())
            {
                this.WriteLine("            me.AIndex = other.AIndex;");
            }
            this.WriteLine("            me.B = other.B;");
        }

        protected override bool ImplementsIExportable()
        {
            if (prop.ObjectClass is ObjectClass)
            {
                return ((ObjectClass)prop.ObjectClass).ImplementsIExportable();
            }
            return false;
        }

        protected override string[] GetIExportableInterfaces()
        {
            return new string[] { "Kistl.API.IExportableCollectionEntryInternal" };
        }
    }
}
