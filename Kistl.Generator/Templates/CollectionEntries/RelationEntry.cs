
namespace Kistl.Generator.Templates.CollectionEntries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Generator.Extensions;

    /// <summary>
    /// Client-side template for Object-Object CollectionEntries. Since the 
    /// client uses lazily loaded OneNRelationList, this Template 
    /// generates no class definition.
    /// </summary>
    public partial class RelationEntry
        : CollectionEntryTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Relation rel)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("CollectionEntries.RelationEntry", ctx, rel);
        }

        protected Relation rel { get; private set; }

        public RelationEntry(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Relation rel)
            : base(_host, ctx)
        {
            this.rel = rel;
        }

        protected override string GetCeClassName()
        {
            return rel.GetRelationClassName() + ImplementationSuffix;
        }

        protected override string GetCeBaseClassName()
        {
            return String.Format("{0}.RelationEntry{1}<{2}, {2}{1}, {3}, {3}{1}>",
                ImplementationNamespace,
                ImplementationSuffix,
                rel.A.Type.Module.Name + "." + rel.A.Type.Name,
                rel.B.Type.Module.Name + "." + rel.B.Type.Name);
        }

        protected override bool IsExportable()
        {
            return rel.A.Type.ImplementsIExportable() && rel.B.Type.ImplementsIExportable();
        }

        protected override string[] GetIExportableInterfaces()
        {
            if (IsExportable())
            {
                return new string[] { "Kistl.API.IExportableInternal", "Kistl.App.Base.IExportable" };
            }
            else
            {
                return new string[0];
            }
        }

        protected override string GetCeInterface()
        {
            return rel.GetRelationClassName();
        }

        protected override void ApplyClassHeadTemplate()
        {
            base.ApplyClassHeadTemplate();
            this.WriteLine("        #region RelationEntry.ApplyClassHeadTemplate");
            this.WriteLine();

            // Implement RelationID by using a static backing store
            this.WriteLine("        private static readonly Guid _relationID = new Guid(\"{0}\");", rel.ExportGuid);
            this.WriteLine("        public override Guid RelationID { get { return _relationID; } }");
            this.WriteLine();

            // Implement untyped IRelationEntry
            this.WriteLine("        IDataObject IRelationEntry.AObject");
            this.WriteLine("        {");
            this.WriteLine("            get");
            this.WriteLine("            {");
            this.WriteLine("                return A;");
            this.WriteLine("            }");
            this.WriteLine("            set");
            this.WriteLine("            {");
            this.WriteLine("                // settor will do checking for us");
            this.WriteLine("                A = ({0})value;", rel.A.Type.GetDescribedInterfaceType().Type.FullName);
            this.WriteLine("            }");
            this.WriteLine("        }");
            this.WriteLine();
            this.WriteLine("        IDataObject IRelationEntry.BObject");
            this.WriteLine("        {");
            this.WriteLine("            get");
            this.WriteLine("            {");
            this.WriteLine("                return B;");
            this.WriteLine("            }");
            this.WriteLine("            set");
            this.WriteLine("            {");
            this.WriteLine("                // settor will do checking for us");
            this.WriteLine("                B = ({0})value;", rel.B.Type.GetDescribedInterfaceType().Type.FullName);
            this.WriteLine("            }");
            this.WriteLine("        }");
            this.WriteLine();
            this.WriteLine("        #endregion // RelationEntry.ApplyClassHeadTemplate");
            this.WriteLine();
        }

        protected override bool IsOrdered()
        {
            return rel.NeedsPositionStorage(RelationEndRole.A) || rel.NeedsPositionStorage(RelationEndRole.B);
        }

        protected override void ApplyChangesFromBody()
        {
            if (rel.NeedsPositionStorage(RelationEndRole.A))
            {
                this.WriteLine("            me.AIndex = other.AIndex;");
            }
            if (rel.NeedsPositionStorage(RelationEndRole.B))
            {
                this.WriteLine("            me.BIndex = other.BIndex;");
            }
        }

        protected override void ApplyReloadReferenceBody()
        {
            base.ApplyReloadReferenceBody();

            {
                string referencedInterface = rel.A.Type.Module.Namespace + "." + rel.A.Type.Name;
                string referencedImplementation = referencedInterface + ImplementationSuffix;
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "A", "A" + ImplementationPropertySuffix, "_fk_A", "_fk_guid_A", IsExportable());
            }
            {
                string referencedInterface = rel.B.Type.Module.Namespace + "." + rel.B.Type.Name;
                string referencedImplementation = referencedInterface + ImplementationSuffix;
                ObjectClasses.ReloadOneReference.Call(Host, ctx, referencedInterface, referencedImplementation, "B", "B" + ImplementationPropertySuffix, "_fk_B", "_fk_guid_B", IsExportable());
            }
        }
    }
}
