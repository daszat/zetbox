
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class ReloadOneReference
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, ObjectReferenceProperty prop)
        {
            if (prop == null) { throw new ArgumentNullException("prop"); }

            Relation rel = Kistl.App.Extensions.RelationExtensions.Lookup(ctx, prop);
            RelationEnd relEnd = rel.GetEnd(prop);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string referencedInterfaceUnused = null; // Mappings.ObjectClassHbm.GetInterfaceTypeReference(otherEnd.Type, _host.Settings);
            string referencedImplementation = Mappings.ObjectClassHbm.GetImplementationTypeReference(otherEnd.Type, _host.Settings);
            string name = prop.Name;
            string implNameUnused = null;
            string fkBackingName = "_fk_" + name;
            string fkGuidBackingName = "_fk_guid_" + name;
            bool isExportable = relEnd.Type.ImplementsIExportable() && otherEnd.Type.ImplementsIExportable();

            ReloadOneReference.Call(_host, ctx, referencedInterfaceUnused, referencedImplementation, name, implNameUnused, fkBackingName, fkGuidBackingName, isExportable);
        }
    }
}
