
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.App.Base;
    using ZBox.App.SchemaMigration;

    public static class DbTypeMapper
    {
        private static readonly KeyValuePair<Type, DbType>[] _systemTypeMapping
            = {
                  new KeyValuePair<Type, DbType>(typeof(bool), DbType.Boolean),
                  new KeyValuePair<Type, DbType>(typeof(byte), DbType.Byte),
                  new KeyValuePair<Type, DbType>(typeof(ushort), DbType.UInt16),
                  new KeyValuePair<Type, DbType>(typeof(uint), DbType.UInt32),
                  new KeyValuePair<Type, DbType>(typeof(ulong), DbType.UInt64),
                  new KeyValuePair<Type, DbType>(typeof(short), DbType.Int16),
                  new KeyValuePair<Type, DbType>(typeof(int), DbType.Int32),
                  new KeyValuePair<Type, DbType>(typeof(decimal), DbType.Decimal),
                  new KeyValuePair<Type, DbType>(typeof(long), DbType.Int64),
                  new KeyValuePair<Type, DbType>(typeof(string), DbType.String),
                  new KeyValuePair<Type, DbType>(typeof(float), DbType.Double),
                  new KeyValuePair<Type, DbType>(typeof(double), DbType.Double),
                  new KeyValuePair<Type, DbType>(typeof(decimal), DbType.Decimal),
                  new KeyValuePair<Type, DbType>(typeof(DateTime), DbType.DateTime),
                  new KeyValuePair<Type, DbType>(typeof(Guid), DbType.Guid),
                  new KeyValuePair<Type, DbType>(typeof(byte[]), DbType.Binary),
              };

        private static readonly KeyValuePair<Type, DbType>[] _propertyMapping
            = {
                  new KeyValuePair<Type, DbType>(typeof(ObjectReferenceProperty), DbType.Int32),
                  new KeyValuePair<Type, DbType>(typeof(EnumerationProperty), DbType.Int32),
                  new KeyValuePair<Type, DbType>(typeof(IntProperty), DbType.Int32),
                  new KeyValuePair<Type, DbType>(typeof(DecimalProperty), DbType.Decimal),
                  new KeyValuePair<Type, DbType>(typeof(StringProperty), DbType.String),
                  new KeyValuePair<Type, DbType>(typeof(DoubleProperty), DbType.Double),
                  new KeyValuePair<Type, DbType>(typeof(BoolProperty), DbType.Boolean),
                  new KeyValuePair<Type, DbType>(typeof(DateTimeProperty), DbType.DateTime),
                  new KeyValuePair<Type, DbType>(typeof(GuidProperty), DbType.Guid),
              };

        private static DbType GetDbType(Type type, KeyValuePair<Type, DbType>[] mapping)
        {
            if (type == null) { throw new ArgumentNullException("type"); }
            if (mapping == null) { throw new ArgumentNullException("mapping"); }

            foreach (var pair in mapping)
            {
                if (pair.Key.IsAssignableFrom(type))
                {
                    return pair.Value;
                }
            }

            throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert system type '{0}' to an DbType", type));
        }

        private static Type GetSystemType(DbType type, KeyValuePair<Type, DbType>[] mapping)
        {
            if (mapping == null) { throw new ArgumentNullException("mapping"); }

            foreach (var pair in mapping)
            {
                if (pair.Value == type)
                {
                    return pair.Key;
                }
            }

            throw new ArgumentOutOfRangeException("type", string.Format("Unable to convert DbType '{0}' to a system type", type));
        }

        public static DbType GetDbType(Type type)
        {
            return GetDbType(type, _systemTypeMapping);
        }

        public static Type GetSystemType(DbType type)
        {
            return GetSystemType(type, _systemTypeMapping);
        }

        public static DbType GetDbTypeForProperty(Type type)
        {
            return GetDbType(type, _propertyMapping);
        }

        public static Type GetPropertyType(DbType type)
        {
            return GetSystemType(type, _propertyMapping);
        }

        public static DbType GetDbType(ColumnType colType)
        {
            return (DbType)colType;
        }

        // TODO: should be implemented on the Property
        public static DbType GetDbType(this Property prop)
        {
            if (prop == null) throw new ArgumentNullException("prop");

            return GetDbTypeForProperty(prop.GetType());
        }
    }
}
