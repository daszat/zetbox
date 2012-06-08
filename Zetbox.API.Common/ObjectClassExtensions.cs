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
using System;
using System.Collections.Generic;
using System.Linq;

using Zetbox.API;
using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    /// <summary>
    /// Temp. Kist Objects Extensions
    /// </summary>
    public static partial class ObjectClassExtensions
    {
        public static ObjectClass GetObjectClass(this IDataObject obj, IReadOnlyZetboxContext ctx)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            return ctx.FindPersistenceObject<ObjectClass>(obj.ObjectClassID);
        }

        public static ObjectClass GetObjectClass(this Type type, IReadOnlyZetboxContext ctx)
        {
            if (type == null) { throw new ArgumentNullException("type"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }

            ObjectClass result = ctx.TransientState("__ObjectClassExtensions__GetObjectClass__", type.FullName, () => ctx.GetQuery<ObjectClass>().First(o => o.Module.Namespace == type.Namespace && o.Name == type.Name));
            return result;
        }

        public static ICollection<ObjectClass> GetObjectHierarchie(this ObjectClass cls)
        {
            List<ObjectClass> result = new List<ObjectClass>();
            while (cls != null)
            {
                result.Add(cls);
                cls = cls.BaseObjectClass;
            }

            result.Reverse();
            return result;
        }

        public static ObjectClass GetRootClass(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls.BaseObjectClass != null)
            {
                cls = cls.BaseObjectClass;
            }
            return cls;
        }

        public static void CollectChildClasses(this ObjectClass cls, IReadOnlyZetboxContext ctx, List<ObjectClass> children, bool includeAbstract)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (children == null) throw new ArgumentNullException("children");

            var nextChildren = ctx
                .GetQuery<ObjectClass>()
                .Where(oc => oc.BaseObjectClass != null && oc.BaseObjectClass.ID == cls.ID)
                .ToList();

            if (nextChildren.Count() > 0)
            {
                foreach (ObjectClass oc in nextChildren)
                {
                    if (includeAbstract || !oc.IsAbstract) children.Add(oc);
                    CollectChildClasses(oc, ctx, children, includeAbstract);
                };
            }
        }

        public static Property GetProperty(this ObjectClass cls, string property)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                Property prop = cls.Properties.SingleOrDefault(p => p.Name == property);
                if (prop != null)
                {
                    return prop;
                }
                cls = cls.BaseObjectClass;
            }

            return null;
        }

        public static bool IsFrozen(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                if (cls.IsFrozenObject)
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        /// <summary>
        /// Checks if a ObjectClass has an AccessControl list. This Method is used to append security filter
        /// </summary>
        /// <param name="cls">ObjectClass to test</param>
        /// <returns>true if a ACL is defined</returns>
        public static bool HasAccessControlList(this ObjectClass cls)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            while (cls != null)
            {
                if (cls.AccessControlList.Count > 0)
                    return true;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        /// <summary>
        /// Checks if this ObjectClass needs a Rights Table. Lookup is done only in current class, not in base classes.
        /// Only RoleMembership ACLs would need a Rights Table. GroupMembership ACLs are resolved directly at the server.
        /// </summary>
        /// <param name="cls">ObjectClass to test</param>
        /// <returns>true if the class needs a Rights Table</returns>
        public static bool NeedsRightsTable(this ObjectClass cls)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            return cls.AccessControlList.OfType<RoleMembership>().Count() > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cls"></param>
        /// <param name="id"></param>
        /// <returns>Returns null if not mentioned in any group membership</returns>
        public static Zetbox.API.AccessRights? GetGroupAccessRights(this ObjectClass cls, Identity id)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (id == null) throw new ArgumentNullException("id");
            cls = cls.GetRootClass();
            var groups = id.Groups.ToLookup(i => i.ExportGuid);

            Zetbox.App.Base.AccessRights? result = null;

            foreach (var gm in cls.AccessControlList.OfType<GroupMembership>())
            {
                if (groups.Contains(gm.Group.ExportGuid))
                {
                    if (result == null) result = 0;
                    result = result.Value | (gm.Rights ?? 0);
                }
            }

            return (Zetbox.API.AccessRights?)result;
        }

        public static InterfaceType GetDescribedInterfaceType(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            return cls.ReadOnlyContext.GetInterfaceType(GetDescribedInterfaceTypeName(cls));
        }

        public static string GetDescribedInterfaceTypeName(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }
            return cls.Module.Namespace + "." + cls.Name;
        }

        public static bool ImplementsIExportable(this ObjectClass cls)
        {
            return ImplementsIExportable(cls, true);
        }

        public static bool ImplementsIExportable(this ObjectClass cls, bool lookupInBase)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                // TODO: use named objects
                if (cls.ImplementsInterfaces.Count(o => o.Name == "IExportable" && o.Module.Name == "ZetboxBase") == 1)
                    return true;
                if (!lookupInBase) return false;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static bool ImplementsIModuleMember(this ObjectClass cls)
        {
            return ImplementsIModuleMember(cls, true);
        }

        public static bool ImplementsIModuleMember(this ObjectClass cls, bool lookupInBase)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                // TODO: use named objects
                if (cls.ImplementsInterfaces.Count(o => o.Name == "IModuleMember" && o.Module.Name == "ZetboxBase") == 1)
                    return true;
                if (!lookupInBase) return false;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static bool ImplementsIChangedBy(this ObjectClass cls)
        {
            return ImplementsIChangedBy(cls, true);
        }

        public static bool ImplementsIChangedBy(this ObjectClass cls, bool lookupInBase)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            while (cls != null)
            {
                // TODO: use named objects
                if (cls.ImplementsInterfaces.Count(o => o.Name == "IChangedBy" && o.Module.Name == "ZetboxBase") == 1)
                    return true;
                if (!lookupInBase) return false;
                cls = cls.BaseObjectClass;
            }
            return false;
        }

        public static IList<Property> GetAllProperties(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            return cls.GetInheritedProperties().Concat(cls.Properties).ToList();
        }

        public static IList<Property> GetInheritedProperties(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            var result = new List<Property>().AsEnumerable();
            while (cls.BaseObjectClass != null)
            {
                result = cls.BaseObjectClass.Properties.Concat(result);
                cls = cls.BaseObjectClass;
            }
            return result.ToList();
        }

        public static IList<Method> GetAllMethods(this ObjectClass cls)
        {
            if (cls == null) { throw new ArgumentNullException("cls"); }

            return cls.GetInheritedMethods().Concat(cls.Methods).ToList();
        }

        public static bool IsAssignableFrom(this DataType self, DataType other)
        {
            // if one or both parameters are null, it never can be assignable
            // also, this is a nice stop condition for the recursion for ObjectClasses
            if (self == null || other == null)
                return false;

            if (self == other)
                return true;

            if (!(self is ObjectClass && other is ObjectClass))
                return false;

            // self might be an ancestor of other, check here
            return IsAssignableFrom(self, (other as ObjectClass).BaseObjectClass);
        }

        private const string clsRelationsTransientCacheKey = "__ClassRelationsExtensionsCache__";
        public static List<Relation> GetRelations(this ObjectClass cls)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            var ctx = cls.Context;
            if (ctx == null) throw new InvalidOperationException("Passed an object without an Context");
            return ctx.TransientState(clsRelationsTransientCacheKey, cls, () => cls.Context.GetQuery<Relation>().Where(r => r.A.Type == cls || r.B.Type == cls).ToList());
        }
    }
}
