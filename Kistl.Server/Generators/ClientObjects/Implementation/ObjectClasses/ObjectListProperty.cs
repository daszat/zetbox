using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class ObjectListProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            ObjectReferenceProperty prop)
        {
            Debug.Assert(prop.IsList);

            string name = prop.PropertyName;
            string wrapperClass = "BackReferenceCollection";
            string exposedListType = prop.IsIndexed ? "IList" : "ICollection";
            var rel = RelationExtensions.Lookup(ctx, prop);
            var endRole = (RelationEndRole)rel.GetEnd(prop).Role;

            Call(host, ctx, serializationList, name, wrapperClass, exposedListType, rel, endRole);
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
        /// <param name="relEnd"></param>
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            string name,
            string wrapperClass,
            string exposedListType,
            Relation rel,
            RelationEndRole endRole)
        {
            RelationEnd relEnd = rel.GetEnd(endRole);
            RelationEnd otherEnd = rel.GetOtherEnd(relEnd);

            string wrapperName = "_" + name + "Wrapper";

            var otherName = otherEnd.Navigator == null ? relEnd.RoleName : otherEnd.Navigator.PropertyName;

            string referencedInterface = otherEnd.Type.GetDataTypeString();

            Call(host, ctx, serializationList, name, wrapperName, wrapperClass, exposedListType, rel, endRole, otherName, referencedInterface);
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
        /// <param name="relEnd"></param>
        /// <param name="otherEnd"></param>
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
            string otherName,
            string referencedInterface)
        {
            host.CallTemplate("Implementation.ObjectClasses.ObjectListProperty",
                ctx, serializationList,
                name, wrapperName, wrapperClass, exposedListType,
                rel, endRole, otherName,
                referencedInterface);
        }
    }
}
