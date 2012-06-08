
namespace Zetbox.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator;
    using Zetbox.Generator.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IZetboxContext ctx,
            Templates.Serialization.SerializationMembersList serializationList,
            ObjectReferenceProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!prop.IsList()) { throw new ArgumentOutOfRangeException("prop", "prop must be a List-valued property"); }

            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);

            string name = prop.Name;
            
            // whether or not the collection will be eagerly loaded
            bool eagerLoading = relEnd.Navigator != null && relEnd.Navigator.EagerLoading;
            
            string wrapperName = "_" + name;
            string wrapperClass = "OneNRelationList";

            var exposedListType = otherEnd.HasPersistentOrder ? "IList" : "ICollection";
            // the name of the position property
            var positionPropertyName = rel.NeedsPositionStorage(otherEnd.GetRole()) ? Construct.ListPositionPropertyName(otherEnd) : String.Empty;
            var otherName = otherEnd.Navigator == null ? relEnd.RoleName : otherEnd.Navigator.Name;
            var referencedInterface = otherEnd.Type.GetDataTypeString();
            var referencedProxy = Mappings.ObjectClassHbm.GetProxyTypeReference(otherEnd.Type, host.Settings);

            Call(host, ctx, serializationList, name, eagerLoading, wrapperName, wrapperClass, exposedListType, positionPropertyName, otherName, referencedInterface, referencedProxy);
        }

        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Serialization.EagerLoadingSerialization", Templates.Serialization.SerializerType.Binary, null, null, memberName, true, false, null);
            }
        }
    }
}
