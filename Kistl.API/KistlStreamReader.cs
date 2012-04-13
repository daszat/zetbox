
namespace Kistl.API
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Kistl.API.Utils;

    public class KistlStreamReader
    {
        public delegate TypeMap Factory(BinaryReader source);

        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Serialization");
        
        private readonly TypeMap _typeMap;
        
        private readonly BinaryReader _source;

        [Conditional("DEBUG_SERIALIZATION")]
        private static void SerializerTrace(string fmt, params object[] args)
        {
            Log.InfoFormat(fmt, args);
        }

        [Conditional("DEBUG_SERIALIZATION")]
        private void TraceCurrentPos()
        {
            SerializerTrace("CurrentPos: {0}", _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1);
        }

        private T Trace<T>(Func<T> deserializer)
        {
            bool canSeek = _source.BaseStream.CanSeek;
            long beginPos = canSeek ? _source.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            var result = deserializer();

            long endPos = canSeek ? _source.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Read {0} bytes", endPos - beginPos);

            return result;
        }

        private T[] TraceArray<T>(Func<T> deserializer)
        {
            bool canSeek = _source.BaseStream.CanSeek;
            long beginPos = canSeek ? _source.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            T[] result;

            if (_source.ReadBoolean())
            {
                var length = _source.ReadInt32();
                SerializerTrace("Reading array of {0} {1}", length, typeof(T).Name);

                result = new T[length];
                for (int i = 0; i < length; i++)
                {
                    result[i] = deserializer();
                }
            }
            else
            {
                result = null;
            }
            long endPos = canSeek ? _source.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Read {0} bytes", endPos - beginPos);

            return result;
        }

        public KistlStreamReader(TypeMap map, BinaryReader source)
        {
            if (map == null) throw new ArgumentNullException("map");
            if (source == null) throw new ArgumentNullException("source");

            _typeMap = map;
            _source = source;
        }

        #region bool

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out bool val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean();
            SerializerTrace("read bool {0}", val);
        }

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="consumer">An action consuming the bool.</param>
        public void ReadConverter(Action<bool> consumer)
        {
            if (consumer == null) { throw new ArgumentNullException("consumer"); }

            TraceCurrentPos();
            var val = _source.ReadBoolean();
            consumer(val);
            SerializerTrace("read bool {0}", val);
        }

        /// <summary>
        /// Deserialize a nullable bool, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out bool? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (bool?)_source.ReadBoolean() : null;
            SerializerTrace("read bool? {0}", val);
        }

        #endregion

        #region DateTime
        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out DateTime val)
        {
            TraceCurrentPos();
            val = DateTime.FromBinary(_source.ReadInt64());
            SerializerTrace("read DateTime {0}", val);
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out DateTime? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (DateTime?)DateTime.FromBinary(_source.ReadInt64()) : null;
            SerializerTrace("read DateTime? {0}", val);
        }

        #endregion

        #region double

        /// <summary>
        /// Deserialize a double
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out double val)
        {
            TraceCurrentPos();
            val = _source.ReadDouble();
            SerializerTrace("read double {0}", val);
        }

        /// <summary>
        /// Deserialize a nullable double, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out double? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (double?)_source.ReadDouble() : null;
            SerializerTrace("read double? {0}", val);
        }

        #endregion

        #region float

        /// <summary>
        /// Deserialize a float
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out float val)
        {
            TraceCurrentPos();
            val = _source.ReadSingle();
            SerializerTrace("read float {0}", val);
        }

        /// <summary>
        /// Deserialize a nullable float, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out float? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (float?)_source.ReadSingle() : null;
            SerializerTrace("read float? {0}", val);
        }

        #endregion

        #region Guid

        /// <summary>
        /// Deserialize a Guid
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out Guid val)
        {
            TraceCurrentPos();
            val = new Guid(_source.ReadBytes(16));
            SerializerTrace("read Guid {0}", val);
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out Guid? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (Guid?)new Guid(_source.ReadString()) : null;
            SerializerTrace("read Guid? {0}", val);
        }

        #endregion

        #region int

        /// <summary>
        /// Deserialize a int
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out int val)
        {
            TraceCurrentPos();
            val = _source.ReadInt32();
            SerializerTrace("read int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Deserialize a nullable int, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out int? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (int?)_source.ReadInt32() : null;
            SerializerTrace("read int? {0}", val);
        }

        /// <summary>
        /// Deserialize an int and call a converter action on it
        /// </summary>
        /// <param name="conv"></param>
        public void ReadConverter(Action<int> conv)
        {
            if (conv == null) { throw new ArgumentNullException("conv"); }
            TraceCurrentPos();
            int val = _source.ReadInt32();
            conv(val);
            SerializerTrace("read and converted int {0} (4 bytes)", val);
        }

        #endregion

        #region decimal

        /// <summary>
        /// Deserialize a decimal
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out decimal val)
        {
            TraceCurrentPos();
            val = _source.ReadDecimal();
            SerializerTrace("read int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Deserialize a nullable decimal, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out decimal? val)
        {
            TraceCurrentPos();
            val = _source.ReadBoolean() ? (decimal?)_source.ReadDecimal() : null;
            SerializerTrace("read int? {0}", val);
        }

        #endregion

        #region ICompoundObject

        /// <summary>
        /// Deserialize a CompoundObject, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read<T>(out T val)
            where T : class, ICompoundObject, new()
        {
            ICompoundObject value;
            Read(out value, typeof(T));
            val = (T)value;
        }

        public void Read(out ICompoundObject val, Type type)
        {
            TraceCurrentPos();
            val = null;
            if (_source.ReadBoolean())
            {
                val = (ICompoundObject)Activator.CreateInstance(type);
                val.FromStream(_source);
                // CompoundObjects cannot have lists
            }

            SerializerTrace("read {0} value: {1}", type, val);
        }

        #endregion

        #region SerializableExpression and SerializableExpression[]

        /// <summary>
        /// Deserialize a Linq Expression Tree.
        /// </summary>
        /// <param name="e">Expression Tree.</param>
        /// <param name="iftFactory">InterfaceType.Factory to pass on the the read SerializableExpressions</param>
        public void Read(out SerializableExpression e, InterfaceType.Factory iftFactory)
        {
            TraceCurrentPos();
            e = null;
            if (_source.ReadBoolean())
            {
                e = SerializableExpression.FromStream(_source, iftFactory);
            }
        }

        public void Read(out SerializableExpression[] expressions, InterfaceType.Factory iftFactory)
        {
            expressions = TraceArray(() =>
            {
                SerializableExpression result;
                Read(out result, iftFactory);
                return result;
            });
        }

        #endregion

        #region SerializableType

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        /// <param name="type">SerializableType</param>
        public void Read(out SerializableType type)
        {
            TraceCurrentPos();

            long beginPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;

            Guid guid;
            Read(out guid);
            if (guid != Guid.Empty)
            {
                type = _typeMap.Map[guid];
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                type = (SerializableType)bf.Deserialize(_source.BaseStream);
            }

            long endPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            SerializerTrace("read SerializableType {0} ({1} bytes)", type, endPos - beginPos);
        }

        public void Read(out SerializableType[] types)
        {
            types = TraceArray(() =>
            {
                SerializableType result;
                Read(out result);
                return result;
            });
        }

        #endregion

        #region string

        /// <summary>
        /// Deserialize a string, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out string val)
        {
            TraceCurrentPos();
            long beginPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            val = _source.ReadBoolean() ? _source.ReadString() : null;
            long endPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            if (val == null)
            {
                SerializerTrace("read null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                SerializerTrace("read string ({0} chars, {1} bytes)", val.Length, endPos - beginPos);
            }
        }

        /// <summary>
        /// Deserialize a string and call a converter action on it
        /// </summary>
        /// <param name="conv"></param>
        public void ReadConverter(Action<string> conv)
        {
            if (conv == null)
                throw new ArgumentNullException("conv");
            TraceCurrentPos();
            bool hasValue = _source.ReadBoolean();
            if (hasValue)
            {
                string val = _source.ReadString();
                conv(val);
                SerializerTrace("read and converted string \"{0}\"", val);
            }
            else
            {
                conv(null);
                SerializerTrace("read and converted null string");
            }
        }

        #endregion

        #region Collection Entries

        /// <summary>
        /// Deserialize a IValueCollectionEntry Collection, expected format: CONTINUE (true/false), IDataObject (if Object is present).
        /// </summary>
        /// <param name="parent">the parent container holding this collection.</param>
        /// <param name="val">Destination Value.</param>
        public void ReadCollectionEntries<T>(IDataObject parent, ICollection<T> val)
            where T : IStreamable, new()
        {
            if (val == null)
                throw new ArgumentNullException("val");

            TraceCurrentPos();
            while (_source.ReadBoolean())
            {
                TraceCurrentPos();

                // Read type
                SerializableType t;
                Read(out t);

                T obj = new T();
                obj.FromStream(_source);
                val.Add(obj);
                SerializerTrace("read {0} value: {1}", typeof(T), val);
            }
        }

        #endregion

        #region byte[]

        /// <summary>
        /// Deserialize an array of bytes
        /// </summary>
        /// <param name="bytes">data</param>
        public void Read(out byte[] bytes)
        {
            int len;
            Read(out len);
            if (len == -1)
            {
                bytes = null;
            }
            else
            {
                bytes = _source.ReadBytes(len);
            }
        }

        #endregion

        #region ObjectNotificationRequest and ObjectNotificationRequest[]

        public void Read(out ObjectNotificationRequest notificationRequest)
        {
            TraceCurrentPos();

            long beginPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;

            SerializableType type;
            Read(out type);
            int length = _source.ReadInt32();
            var ids = new int[length];
            for (int i = 0; i < length; i++)
            {
                ids[i] = _source.ReadInt32();
            }

            notificationRequest = new ObjectNotificationRequest()
            {
                Type = type,
                IDs = ids
            };

            long endPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            SerializerTrace("read ObjectNotificationRequest ({0} IDs, {1} bytes)", ids.Length, endPos - beginPos);
        }

        public void Read(out ObjectNotificationRequest[] notificationRequests)
        {
            notificationRequests = TraceArray(() =>
            {
                ObjectNotificationRequest result;
                Read(out result);
                return result;
            });
        }

        #endregion

        #region OrderByContract and OrderByContract[]

        public void Read(out OrderByContract orderBy, InterfaceType.Factory iftFactory)
        {
            orderBy = Trace(() =>
            {
                int type;
                Read(out type);

                SerializableExpression expression;
                Read(out expression, iftFactory);

                return new OrderByContract()
                {
                    Type = (OrderByType)type,
                    Expression = expression
                };
            });
        }

        public void Read(out OrderByContract[] orderBys, InterfaceType.Factory iftFactory)
        {
            orderBys = TraceArray(() =>
            {
                OrderByContract result;
                Read(out result, iftFactory);
                return result;
            });
        }

        #endregion

        #region object

        public void Read(out object value, Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            var isNull = _source.ReadBoolean();
            if (isNull)
            {
                value = null;
            }
            else
            {
                // Plain arrays
                if (type.IsArray)
                {
                    var elementType = type.GetElementType();
                    var array = new ArrayList();
                    ReadInternalArray(array, elementType);
                    value = Activator.CreateInstance(type);
                    array.CopyTo((Array)value);
                }
                // only IEnumerable<> -> use List<>
                else if (type.IsGenericType && type.GetGenericArguments().Length == 1 && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var elementType = type.FindElementTypes().Single(t => t != typeof(object));
                    var array = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
                    ReadInternalArray(array, elementType);
                    value = array;
                }
                // Try plain types
                else
                {
                    value = ReadInternal(type);
                }
            }
        }

        private void ReadInternalArray(IList array, Type elementType)
        {
            while (true)
            {
                var hasValue = _source.ReadBoolean();
                if (!hasValue) break;

                var elementValue = ReadInternal(elementType);
                array.Add(elementValue);
            }
        }

        private object ReadInternal(Type type)
        {
            object value;
            // Deserialize only basic types
            if (type == typeof(int) || type == typeof(int?) || type.IsEnum || type.IsNullableEnum())
            {
                value = _source.ReadInt32();
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                value = _source.ReadBoolean();
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                value = _source.ReadDouble();
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                value = _source.ReadSingle();
            }
            else if (type == typeof(string))
            {
                value = _source.ReadString();
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                value = _source.ReadDecimal();
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                DateTime val;
                Read(out val);
                value = val;
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                Guid val;
                Read(out val);
                value = val;
            }
            else if (type.IsICompoundObject())
            {
                ICompoundObject val;
                Read(out val, type);
                value = val;
            }
            else
            {
                throw new NotSupportedException(string.Format("Can't deserialize Value of type '{0}'.", type));
            }
            return value;
        }

        #endregion
    }
}
