
namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Server.Generators.Extensions;

    public partial class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ObjectReferenceProperty prop)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }
            if (!prop.IsList()) { throw new ArgumentNullException("prop", "prop must be a List-valued property"); }

            string name = prop.Name;
            string wrapperClass = "OneNRelationList";
            var rel = RelationExtensions.Lookup(ctx, prop);
            var relEnd = rel.GetEnd(prop);
            var otherEnd = rel.GetOtherEnd(relEnd);
            var exposedListType = rel.NeedsPositionStorage(otherEnd.GetRole()) ? "IList" : "ICollection";
            // the name of the position property
            var positionPropertyName = rel.NeedsPositionStorage(otherEnd.GetRole()) ? Construct.ListPositionPropertyName(otherEnd) : String.Empty;

            Call(host, ctx, serializationList, name, wrapperClass, exposedListType, rel, relEnd.GetRole(), positionPropertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="ctx"></param>
        /// <param name="serializationList"></param>
        /// <param name="name">the name of the property to create</param>
        /// <param name="wrapperClass">the name of the wrapper class for wrapping the EntityCollection</param>
        /// <param name="exposedListType">which generic interface to use for the collection (IList or ICollection)</param>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        /// <param name="positionPropertyName">the name of the position property for this list</param>
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string wrapperClass,
            string exposedListType,
            Relation rel,
            RelationEndRole endRole,
            string positionPropertyName)
        {
            if (rel == null) { throw new ArgumentNullException("rel"); }

            RelationEnd relEnd = rel.GetEndFromRole(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string wrapperName = "_" + name + "Wrapper";

            var otherName = otherEnd.Navigator == null ? relEnd.RoleName : otherEnd.Navigator.Name;

            string referencedInterface = otherEnd.Type.GetDataTypeString();

            Call(host, ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, positionPropertyName, otherName, referencedInterface);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="ctx"></param>
        /// <param name="serializationList"></param>
        /// <param name="name">the name of the property to create</param>
        /// <param name="wrapperName">the name of the private backing store for the conversion wrapper list</param>
        /// <param name="wrapperClass">the name of the wrapper class for wrapping the EntityCollection</param>
        /// <param name="exposedListType">which generic interface to use for the collection (IList or ICollection)</param>
        /// <param name="rel"></param>
        /// <param name="endRole"></param>
        /// <param name="positionPropertyName">the name of the position property for this list</param>
        /// <param name="otherName"></param>
        /// <param name="referencedInterface">which Kistl interface this list contains</param>
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string wrapperName,
            string wrapperClass,
            string exposedListType,
            Relation rel,
            RelationEndRole endRole,
            string positionPropertyName,
            string otherName,
            string referencedInterface)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (name == null) { throw new ArgumentNullException("name"); }
            if (String.IsNullOrEmpty(wrapperName)) { throw new ArgumentNullException("wrapperName"); }
            if (String.IsNullOrEmpty(wrapperClass)) { throw new ArgumentNullException("wrapperClass"); }
            if (rel == null) { throw new ArgumentNullException("rel"); }
            if (String.IsNullOrEmpty(otherName)) { throw new ArgumentNullException("otherName"); }
            if (String.IsNullOrEmpty(referencedInterface)) { throw new ArgumentNullException("referencedInterface"); }

            host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty",
                ctx, serializationList,
                name, wrapperName, wrapperClass, exposedListType,
                rel, endRole, positionPropertyName, otherName,
                referencedInterface);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Implementation.ObjectClasses.EagerLoadingSerialization", Templates.Implementation.SerializerType.Binary, null, null, memberName, true);
            }
        }
    }
}
