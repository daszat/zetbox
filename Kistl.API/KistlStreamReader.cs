
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

    public class KistlStreamReader : IDisposable
    {
        public delegate KistlStreamReader Factory(BinaryReader source);

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

        public Stream BaseStream
        {
            get
            {
                return _source.BaseStream;
            }
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
        public bool ReadBoolean()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean();
            SerializerTrace("read bool {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out bool val)
        {
            val = ReadBoolean();
        }

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="consumer">An action consuming the bool.</param>
        public void ReadConverter(Action<bool> consumer)
        {
            if (consumer == null) { throw new ArgumentNullException("consumer"); }

            consumer(ReadBoolean());
        }

        /// <summary>
        /// Deserialize a nullable bool, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public bool? ReadNullableBoolean()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (bool?)_source.ReadBoolean() : null;
            SerializerTrace("read bool? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable bool, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out bool? val)
        {
            val = ReadNullableBoolean();
        }

        #endregion

        #region byte

        /// <summary>
        /// Deserialize a byte
        /// </summary>
        public byte ReadByte()
        {
            TraceCurrentPos();
            var result = _source.ReadByte();
            SerializerTrace("read byte {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a byte
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out byte val)
        {
            val = ReadByte();
        }

        /// <summary>
        /// Deserialize a nullable byte, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public byte? ReadNullableByte()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (byte?)_source.ReadByte() : null;
            SerializerTrace("read byte? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable byte, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out byte? val)
        {
            val = ReadNullableByte();
        }

        /// <summary>
        /// Deserialize an array of bytes
        /// </summary>
        /// <param name="bytes">data</param>
        public void Read(out byte[] bytes)
        {
            bytes = ReadByteArray();
        }

        /// <summary>
        /// Deserialize a byte[]
        /// </summary>
        public byte[] ReadByteArray()
        {
            TraceCurrentPos();
            var len = _source.ReadInt32();
            var result = _source.ReadBytes(len);
            SerializerTrace("read {0} bytes", len);
            return result;
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        public DateTime ReadDateTime()
        {
            TraceCurrentPos();
            var result = DateTime.FromBinary(_source.ReadInt64());
            SerializerTrace("read DateTime {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out DateTime val)
        {
            val = ReadDateTime();
        }

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        public DateTime? ReadNullableDateTime()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (DateTime?)DateTime.FromBinary(_source.ReadInt64()) : null;
            SerializerTrace("read DateTime {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out DateTime? val)
        {
            val = ReadNullableDateTime();
        }

        #endregion

        #region decimal

        /// <summary>
        /// Deserialize a decimal
        /// </summary>
        public decimal ReadDecimal()
        {
            TraceCurrentPos();
            var result = _source.ReadDecimal();
            SerializerTrace("read int {0} (4 bytes)", result);
            return result;
        }

        /// <summary>
        /// Deserialize a decimal
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out decimal val)
        {
            val = ReadDecimal();
        }

        /// <summary>
        /// Deserialize a nullable decimal, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public decimal? ReadNullableDecimal()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (decimal?)_source.ReadDecimal() : null;
            SerializerTrace("read int? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable decimal, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out decimal? val)
        {
            val = ReadNullableDecimal();
        }

        #endregion

        #region double

        /// <summary>
        /// Deserialize a double
        /// </summary>
        public double ReadDouble()
        {
            TraceCurrentPos();
            var result = _source.ReadDouble();
            SerializerTrace("read double {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a double
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out double val)
        {
            val = ReadDouble();
        }

        /// <summary>
        /// Deserialize a double
        /// </summary>
        public double? ReadNullableDouble()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (double?)_source.ReadDouble() : null;
            SerializerTrace("read double? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable double, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out double? val)
        {
            val = ReadNullableDouble();
        }

        #endregion

        #region float

        /// <summary>
        /// Deserialize a float
        /// </summary>
        public float ReadFloat()
        {
            TraceCurrentPos();
            var result = _source.ReadSingle();
            SerializerTrace("read float {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a float
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out float val)
        {
            val = ReadFloat();
        }

        /// <summary>
        /// Deserialize a nullable float, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public float? ReadNullableFloat()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (float?)_source.ReadSingle() : null;
            SerializerTrace("read float? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable float, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out float? val)
        {
            val = ReadNullableFloat();
        }

        #endregion

        #region Guid

        /// <summary>
        /// Deserialize a Guid
        /// </summary>
        public Guid ReadGuid()
        {
            TraceCurrentPos();
            var data = _source.ReadBytes(16);
            if (data.Length != 16)
                Log.Error("Failed reading 16 bytes for a GUID");
            var result = new Guid(data);
            SerializerTrace("read Guid {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a Guid
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out Guid val)
        {
            val = ReadGuid();
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public Guid? ReadNullableGuid()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (Guid?)ReadGuid() : null;
            SerializerTrace("read Guid? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out Guid? val)
        {
            val = ReadNullableGuid();
        }

        #endregion

        #region int

        /// <summary>
        /// Deserialize a int
        /// </summary>
        public int ReadInt32()
        {
            TraceCurrentPos();
            var result = _source.ReadInt32();
            SerializerTrace("read int {0} (4 bytes)", result);
            return result;
        }

        /// <summary>
        /// Deserialize a int
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out int val)
        {
            val = ReadInt32();
        }

        /// <summary>
        /// Deserialize a int?
        /// </summary>
        public int? ReadNullableInt32()
        {
            TraceCurrentPos();
            var result = _source.ReadBoolean() ? (int?)_source.ReadInt32() : null;
            SerializerTrace("read int? {0}", result);
            return result;
        }

        /// <summary>
        /// Deserialize a nullable int, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out int? val)
        {
            val = ReadNullableInt32();
        }

        /// <summary>
        /// Deserialize an int and call a converter action on it
        /// </summary>
        /// <param name="converter"></param>
        public void ReadConverter(Action<int> converter)
        {
            if (converter == null) { throw new ArgumentNullException("converter"); }
            converter(ReadInt32());
        }

        #endregion

        #region ICompoundObject

        /// <summary>
        /// Deserialize a CompoundObject, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public T ReadCompoundObject<T>()
            where T : class, ICompoundObject, new()
        {
            return (T)ReadCompoundObject(typeof(T));
        }

        /// <summary>
        /// Deserialize a CompoundObject, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read<T>(out T val)
            where T : class, ICompoundObject, new()
        {
            val = ReadCompoundObject<T>();
        }

        /// <summary>
        /// Deserialize a CompoundObject, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public ICompoundObject ReadCompoundObject(Type t)
        {
            ICompoundObject result;
            TraceCurrentPos();
            if (_source.ReadBoolean())
            {
                result = (ICompoundObject)Activator.CreateInstance(t);
                result.FromStream(this);
                // CompoundObjects cannot have lists
                SerializerTrace("read {0} value: [{1}]", t, result);
            }
            else
            {
                result = null;
                SerializerTrace("read {0} value: [null]", t);
            }
            return result;
        }

        #endregion

        #region SerializableExpression and SerializableExpression[]

        /// <summary>
        /// Deserialize a Linq Expression Tree.
        /// </summary>
        /// <param name="iftFactory">InterfaceType.Factory to pass on the the read SerializableExpressions</param>
        public SerializableExpression ReadSerializableExpression(InterfaceType.Factory iftFactory)
        {
            TraceCurrentPos();
            if (_source.ReadBoolean())
            {
                return SerializableExpression.FromStream(this, iftFactory);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deserialize a Linq Expression Tree.
        /// </summary>
        /// <param name="e">Expression Tree.</param>
        /// <param name="iftFactory">InterfaceType.Factory to pass on the the read SerializableExpressions</param>
        public void Read(out SerializableExpression e, InterfaceType.Factory iftFactory)
        {
            e = ReadSerializableExpression(iftFactory);
        }

        public void Read(out SerializableExpression[] expressions, InterfaceType.Factory iftFactory)
        {
            expressions = TraceArray(() => ReadSerializableExpression(iftFactory));
        }

        #endregion

        #region SerializableType

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        public SerializableType ReadSerializableType()
        {
            TraceCurrentPos();
            SerializableType result;

            long beginPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;

            Guid guid = ReadGuid();
            if (guid != Guid.Empty)
            {
                result = _typeMap.Map[guid];
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                result = (SerializableType)bf.Deserialize(_source.BaseStream);
            }

            long endPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            SerializerTrace("read SerializableType {0} ({1} bytes)", result, endPos - beginPos);

            return result;
        }

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        /// <param name="type">Destination Value.</param>
        public void Read(out SerializableType type)
        {
            type = ReadSerializableType();
        }

        /// <summary>
        /// Deserialize a SerializableType[].
        /// </summary>
        public SerializableType[] ReadSerializableTypeArray()
        {
            return TraceArray(ReadSerializableType);
        }

        /// <summary>
        /// Deserialize an array of SerializableType.
        /// </summary>
        /// <param name="types">Destination Value.</param>
        public void Read(out SerializableType[] types)
        {
            types = ReadSerializableTypeArray();
        }

        #endregion

        #region string

        /// <summary>
        /// Deserialize a string, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        public string ReadString()
        {
            TraceCurrentPos();
            long beginPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;
            var result = _source.ReadBoolean() ? _source.ReadString() : null;
            long endPos = _source.BaseStream.CanSeek ? _source.BaseStream.Position : -1;

            if (result == null)
            {
                SerializerTrace("read null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                SerializerTrace("read string ({0} chars, {1} bytes)", result.Length, endPos - beginPos);
            }
            return result;
        }

        /// <summary>
        /// Deserialize a string, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        public void Read(out string val)
        {
            val = ReadString();
        }

        /// <summary>
        /// Deserialize a string and call a converter action on it
        /// </summary>
        /// <param name="converter"></param>
        public void ReadConverter(Action<string> converter)
        {
            if (converter == null)
                throw new ArgumentNullException("converter");

            converter(ReadString());
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
                obj.FromStream(this);
                val.Add(obj);
                SerializerTrace("read {0} value: {1}", typeof(T), val);
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
                value = ReadCompoundObject(type);
            }
            else
            {
                throw new NotSupportedException(string.Format("Can't deserialize Value of type '{0}'.", type));
            }
            return value;
        }

        #endregion

        public void Dispose()
        {
            ((IDisposable)_source).Dispose();
        }

    }
}
