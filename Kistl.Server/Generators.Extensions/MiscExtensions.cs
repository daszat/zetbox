using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.GeneratorsOld;
using Kistl.App.Base;
using Kistl.Server.Movables;

namespace Kistl.Server.Generators.Extensions
{
    public static class MiscExtensions
    {

        public static string ToNameSpace(this TaskEnum task)
        {
            if (task == TaskEnum.Interface)
            {
                return "Kistl.Objects";
            }
            else
            {
                return string.Format(@"Kistl.Objects.{0}", task);
            }
        }

        public static string ToCSharpTypeRef(this Type t)
        {
            if (t.IsGenericType)
            {
                return String.Format("{0}<{1}>",
                    t.FullName.Split('`')[0], // TODO: hack to get to class name
                    String.Join(", ", t.GetGenericArguments().Select(arg => arg.ToCSharpTypeRef()).ToArray())
                    );
            }
            else
            {
                return t.FullName;
            }
        }

        public static string ToDbType(this ValueTypeProperty prop)
        {
            if (prop is IntProperty)
                return "int";
            if (prop is StringProperty)
                return "nvarchar";
            if (prop is DoubleProperty)
                return "float";
            if (prop is BoolProperty)
                return "bit";
            if (prop is DateTimeProperty)
                return "datetime";

            throw new ArgumentOutOfRangeException("prop", "unknown ValueTypeProperty type: " + prop.GetType().FullName);
        }

        #region CollectionEntry naming standards

        public static string GetCollectionEntryClassName(this NewRelation rel)
        {
            return String.Format("{0}_{1}{2}CollectionEntry", rel.A.Type.ClassName, rel.A.Navigator.PropertyName, rel.ID);
        }

        public static string GetCollectionEntryFullName(this NewRelation rel)
        {
            return String.Format("{0}.{1}", rel.A.Type.Namespace, rel.GetCollectionEntryClassName());
        }

        public static string GetCollectionEntryClassName(this ValueTypeProperty prop)
        {
            return String.Format("{0}_{1}CollectionEntry", prop.ObjectClass.ClassName, prop.PropertyName);
        }

        public static string GetCollectionEntryFullName(this ValueTypeProperty prop)
        {
            return String.Format("{0}.{1}", prop.ObjectClass.Module.Namespace, prop.GetCollectionEntryClassName());
        }

        #endregion


        /// <summary>
        /// Calculates the preferred storage for a given NewRelation
        /// </summary>
        public static StorageHint GetPreferredStorage(this NewRelation rel)
        {
            if (rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() == 1)
            {
                // arbitrary 1:1 relations default 
                return StorageHint.MergeA;
            }
            else if (rel.A.Multiplicity.UpperBound() == 1 && rel.B.Multiplicity.UpperBound() > 1)
            {
                // if multiple Bs can exist, they get the fk
                return StorageHint.MergeB;
            }
            else if (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() == 1)
            {
                // if multiple As ca exist, they get the fk
                return StorageHint.MergeA;
            }
            else if (rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1)
            {
                // N:M needs "weak" entity
                return StorageHint.Separate;
            }

            // this means that UpperBound() < 1 for some end
            throw new NotImplementedException();
        }
    }
}
