
namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Kistl.API.Utils;

    /// <summary>
    /// Binary Serializer Helper.
    /// TODO: Rename to BinaryStreamer due to a naming conflict in XmlStreamer (Kistl.API.XmlSearializer vs. System.Xml.XmlSearializer)
    /// </summary>
    public static class BinarySerializer
    {
        [Conditional("DEBUG_SERIALIZATION")]
        private static void SerializerTrace(string fmt, params object[] args)
        {
            Logging.Log.InfoFormat(fmt, args);
        }

        private static void Trace(BinaryWriter sw, Action serializer)
        {
            long beginPos = sw.BaseStream.Position;
            SerializerTrace("CurrentPos: {0}", beginPos);

            serializer();

            long endPos = sw.BaseStream.Position;
            SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        private static T Trace<T>(BinaryReader sr, Func<T> deserializer)
        {
            long beginPos = sr.BaseStream.Position;
            SerializerTrace("CurrentPos: {0}", beginPos);

            var result = deserializer();

            long endPos = sr.BaseStream.Position;
            SerializerTrace("Read {0} bytes", endPos - beginPos);

            return result;
        }

        private static void TraceArray<T>(BinaryWriter sw, T[] data, Action<T> serializer)
        {
            long beginPos = sw.BaseStream.Position;
            SerializerTrace("CurrentPos: {0}", beginPos);

            var length = data.Length;
            SerializerTrace("Writing array of {0} {1}", length, typeof(T).Name);

            sw.Write(length);
            for (int i = 0; i < length; i++)
            {
                serializer(data[i]);
            }

            long endPos = sw.BaseStream.Position;
            SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        private static T[] TraceArray<T>(BinaryReader sr, Func<T> deserializer)
        {
            long beginPos = sr.BaseStream.Position;
            SerializerTrace("CurrentPos: {0}", beginPos);

            var length = sr.ReadInt32();
            SerializerTrace("Reading array of {0} {1}", length, typeof(T).Name);

            var result = new T[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = deserializer();
            }

            long endPos = sr.BaseStream.Position;
            SerializerTrace("Read {0} bytes", endPos - beginPos);

            return result;
        }

        #region bool

        /// <summary>
        /// Serialize a bool
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(bool val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing bool {0} (1 byte)", val);
            sw.Write(val);
        }


        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out bool val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean();
            SerializerTrace("read bool {0}", val);
        }

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="consumer">An action consuming the bool.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStreamConverter(Action<bool> consumer, BinaryReader sr)
        {
            if (consumer == null) { throw new ArgumentNullException("consumer"); }
            if (sr == null) { throw new ArgumentNullException("sr"); }

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            var val = sr.ReadBoolean();
            consumer(val);
            SerializerTrace("read bool {0}", val);
        }

        /// <summary>
        /// Serialize a nullable bool. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(bool? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing bool? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); }
            else
                sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable bool, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out bool? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (bool?)sr.ReadBoolean() : null;
            SerializerTrace("read bool? {0}", val);
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Serialize a DateTime
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(DateTime val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing DateTime {0} (8 bytes)", val);
            sw.Write(val.ToBinary());
        }

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out DateTime val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = DateTime.FromBinary(sr.ReadInt64());
            SerializerTrace("read DateTime {0}", val);
        }

        /// <summary>
        /// Serialize a nullable DateTime. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(DateTime? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing DateTime? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value.ToBinary()); }
            else
                sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out DateTime? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (DateTime?)DateTime.FromBinary(sr.ReadInt64()) : null;
            SerializerTrace("read DateTime? {0}", val);
        }

        #endregion

        #region Guid

        /// <summary>
        /// Serialize a DateTime
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(Guid val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing Guid {0}", val);
            sw.Write(val.ToString());
        }

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out Guid val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = new Guid(sr.ReadString());
            SerializerTrace("read Guid {0}", val);
        }

        /// <summary>
        /// Serialize a nullable DateTime. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(Guid? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing Guid? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value.ToString()); }
            else
                sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out Guid? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (Guid?)new Guid(sr.ReadString()) : null;
            SerializerTrace("read Guid? {0}", val);
        }

        #endregion

        #region double

        /// <summary>
        /// Serialize a double
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(double val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing double {0}", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a double
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out double val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadDouble();
            SerializerTrace("read double {0}", val);
        }

        /// <summary>
        /// Serialize a nullable double. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(double? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing double? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); }
            else
                sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable double, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out double? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (double?)sr.ReadDouble() : null;
            SerializerTrace("read double? {0}", val);
        }

        #endregion

        #region float

        /// <summary>
        /// Serialize a float
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(float val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing float {0}", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a float
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out float val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadSingle();
            SerializerTrace("read float {0}", val);
        }

        /// <summary>
        /// Serialize a nullable float. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(float? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing float? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); }
            else
                sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable float, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out float? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (float?)sr.ReadSingle() : null;
            SerializerTrace("read float? {0}", val);
        }

        #endregion

        #region int

        /// <summary>
        /// Serialize a int
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(int val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing int {0} (four bytes)", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a int
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out int val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadInt32();
            SerializerTrace("read int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Deserialize an int and call a converter action on it
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="sr"></param>
        public static void FromStreamConverter(Action<int> conv, BinaryReader sr)
        {
            if (conv == null) { throw new ArgumentNullException("conv"); }
            if (sr == null) { throw new ArgumentNullException("sr"); }
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            int val = sr.ReadInt32();
            conv(val);
            SerializerTrace("read and converted int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Serialize a nullable int. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(int? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing int? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); }
            else
                sw.Write(false);
        }
        /// <summary>
        /// Deserialize a nullable int, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out int? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (int?)sr.ReadInt32() : null;
            SerializerTrace("read int? {0}", val);
        }

        #endregion

        #region decimal

        /// <summary>
        /// Serialize a decimal
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(decimal val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing int {0} (four bytes)", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a decimal
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out decimal val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadDecimal();
            SerializerTrace("read int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Serialize a nullable decimal. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(decimal? val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing int? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); }
            else
                sw.Write(false);
        }
        /// <summary>
        /// Deserialize a nullable decimal, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out decimal? val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (decimal?)sr.ReadDecimal() : null;
            SerializerTrace("read int? {0}", val);
        }

        #endregion

        #region ICompoundObject

        /// <summary>
        /// Serialize a CompoundObject. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWriter to serialize to.</param>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "This API is only for ICompoundObject")]
        public static void ToStream(ICompoundObject val, BinaryWriter sw)
        {
            if (sw == null) { throw new ArgumentNullException("sw"); }
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing ICompoundObject {0}", val);
            if (val != null) { sw.Write(true); val.ToStream(sw, null, false); } else { sw.Write(false); }
        }

        /// <summary>
        /// Deserialize a CompoundObject, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream<T>(out T val, BinaryReader sr)
            where T : class, ICompoundObject, new()
        {
            if (sr == null) { throw new ArgumentNullException("sr"); }
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            val = null;
            if (sr.ReadBoolean())
            {
                val = new T();
                val.FromStream(sr);
            }

            SerializerTrace("read {0} value: {1}", typeof(T), val);
        }

        #endregion

        #region SerializableExpression and SerializableExpression[]

        /// <summary>
        /// Serialize a SerializableExpression
        /// </summary>
        /// <param name="e">SerializableExpression to serialize.</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(SerializableExpression e, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            if (e != null)
            {
                sw.Write(true);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(sw.BaseStream, e);
            }
            else
            {
                sw.Write(false);
            }
        }

        /// <summary>
        /// Deserialize a Linq Expression Tree.
        /// </summary>
        /// <param name="e">Expression Tree.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out SerializableExpression e, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            e = null;
            if (sr.ReadBoolean())
            {
                BinaryFormatter bf = new BinaryFormatter();
                e = (SerializableExpression)bf.Deserialize(sr.BaseStream);
            }
        }

        public static void ToStream(SerializableExpression[] expressions, BinaryWriter sw)
        {
            if (expressions == null)
                throw new ArgumentNullException("expressions");
            if (sw == null)
                throw new ArgumentNullException("sw");

            TraceArray(sw, expressions, expression =>
            {
                BinarySerializer.ToStream(expression, sw);
            });
        }

        public static void FromStream(out SerializableExpression[] expressions, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            expressions = TraceArray(sr, () =>
            {
                SerializableExpression result;
                BinarySerializer.FromStream(out result, sr);
                return result;
            });
        }

        #endregion

        #region SerializableType

        /// <summary>
        /// Serialize a SerializableType
        /// </summary>
        /// <param name="type">SerializableType to serialize.</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(SerializableType type, BinaryWriter sw)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing SerializableType {0}", type);

            long beginPos = sw.BaseStream.Position;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(sw.BaseStream, type);

            long endPos = sw.BaseStream.Position;
            SerializerTrace("({0} bytes)", endPos - beginPos);

        }

        public static void ToStream(SerializableType[] types, BinaryWriter sw)
        {
            if (types == null) throw new ArgumentNullException("types");
            if (sw == null) throw new ArgumentNullException("sw");

            TraceArray(sw, types, type =>
            {
                BinarySerializer.ToStream(type, sw);
            });
        }

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        /// <param name="type">SerializableType</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out SerializableType type, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);

            long beginPos = sr.BaseStream.Position;
            BinaryFormatter bf = new BinaryFormatter();
            type = (SerializableType)bf.Deserialize(sr.BaseStream);
            long endPos = sr.BaseStream.Position;
            SerializerTrace("read SerializableType {0} ({1} bytes)", type, endPos - beginPos);
        }

        public static void FromStream(out SerializableType[] types, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            types = TraceArray(sr, () =>
            {
                SerializableType result;
                BinarySerializer.FromStream(out result, sr);
                return result;
            });
        }

        #endregion

        #region string

        /// <summary>
        /// Serialize a string. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(string val, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            long beginPos = sw.BaseStream.Position;
            if (val != null) { sw.Write(true); sw.Write(val); }
            else
                sw.Write(false);
            long endPos = sw.BaseStream.Position;
            if (val == null)
            {
                SerializerTrace("Wrote null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                SerializerTrace("Wrote string ({0} chars, {1} bytes)", val.Length, endPos - beginPos);
            }
        }

        /// <summary>
        /// Deserialize a string, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out string val, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            long beginPos = sr.BaseStream.Position;
            val = sr.ReadBoolean() ? sr.ReadString() : null;
            long endPos = sr.BaseStream.Position;
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
        /// <param name="sr"></param>
        public static void FromStreamConverter(Action<string> conv, BinaryReader sr)
        {
            if (conv == null)
                throw new ArgumentNullException("conv");
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            bool hasValue = sr.ReadBoolean();
            if (hasValue)
            {
                string val = sr.ReadString();
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
        /// Serialize a ICollectionEntry Collection. Format is: CONTINUE (true/false), ICollectionEntry (if Object is present).
        /// </summary>
        /// <param name="val">Collection to serialize. Assumed empty if null.</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, BinaryWriter sw)
            where T : IStreamable
        {
            if (sw == null) { throw new ArgumentNullException("sw"); }

            if (val == null)
            {
                SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
                SerializerTrace("writing null collection as empty");
            }
            else
            {
                SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
                foreach (IStreamable obj in val)
                {
                    ToStream(true, sw);
                    SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
                    SerializerTrace("Writing CollectionEntry {0}", val.ToString());
                    obj.ToStream(sw, null, false);
                }
            }

            ToStream(false, sw);
        }

        /// <summary>
        /// Deserialize a ICollectionEntry Collection, expected format: CONTINUE (true/false), IDataObject (if Object is present).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStreamCollectionEntries<T>(ICollection<T> val, BinaryReader sr)
            where T : IStreamable, new()
        {
            if (val == null)
                throw new ArgumentNullException("val");
            if (sr == null)
                throw new ArgumentNullException("sr");

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);
            while (sr.ReadBoolean())
            {
                SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);

                // Read type
                SerializableType t;
                BinarySerializer.FromStream(out t, sr);

                T obj = new T();
                obj.FromStream(sr);
                val.Add(obj);
                SerializerTrace("read {0} value: {1}", typeof(T), val);
            }
        }

        #endregion

        #region byte[]

        /// <summary>
        /// Serialize a SerializableType
        /// </summary>
        /// <param name="bytes">data to serialize.</param>
        /// <param name="sw">BinaryWriter to serialize to.</param>
        public static void ToStream(byte[] bytes, BinaryWriter sw)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing byte[{0}]", bytes.Length);

            long beginPos = sw.BaseStream.Position;

            sw.Write(bytes.Length);
            sw.Write(bytes);

            long endPos = sw.BaseStream.Position;
            SerializerTrace("({0} bytes, including length)", endPos - beginPos);
        }

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        /// <param name="bytes">data</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out byte[] bytes, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);

            long beginPos = sr.BaseStream.Position;

            int length = sr.ReadInt32();
            bytes = sr.ReadBytes(length);

            long endPos = sr.BaseStream.Position;
            SerializerTrace("read byte[{0}] ({1} bytes, including length)", bytes.Length, endPos - beginPos);
        }

        #endregion

        #region ObjectNotificationRequest and ObjectNotificationRequest[]

        public static void ToStream(ObjectNotificationRequest notificationRequest, BinaryWriter sw)
        {
            if (notificationRequest == null)
                throw new ArgumentNullException("notificationRequest");
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.Position);
            SerializerTrace("Writing ObjectNotificationRequest for {0} with {1} IDs", notificationRequest.Type, notificationRequest.IDs.Length);

            long beginPos = sw.BaseStream.Position;

            BinarySerializer.ToStream(notificationRequest.Type, sw);
            sw.Write(notificationRequest.IDs.Length);
            foreach (var id in notificationRequest.IDs)
            {
                sw.Write(id);
            }

            long endPos = sw.BaseStream.Position;
            SerializerTrace("({0} bytes)", endPos - beginPos);
        }

        public static void FromStream(out ObjectNotificationRequest notificationRequest, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.Position);

            long beginPos = sr.BaseStream.Position;

            SerializableType type;
            BinarySerializer.FromStream(out type, sr);
            int length = sr.ReadInt32();
            var ids = new int[length];
            for (int i = 0; i < length; i++)
            {
                ids[i] = sr.ReadInt32();
            }

            notificationRequest = new ObjectNotificationRequest()
            {
                Type = type,
                IDs = ids
            };

            long endPos = sr.BaseStream.Position;
            SerializerTrace("read ObjectNotificationRequest ({0} IDs, {1} bytes)", ids.Length, endPos - beginPos);
        }

        public static void ToStream(ObjectNotificationRequest[] notificationRequests, BinaryWriter sw)
        {
            if (notificationRequests == null)
                throw new ArgumentNullException("notificationRequests");
            if (sw == null)
                throw new ArgumentNullException("sw");

            TraceArray(sw, notificationRequests, req =>
            {
                BinarySerializer.ToStream(req, sw);
            });
        }

        public static void FromStream(out ObjectNotificationRequest[] notificationRequests, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            notificationRequests = TraceArray(sr, () =>
            {
                ObjectNotificationRequest result;
                BinarySerializer.FromStream(out result, sr);
                return result;
            });
        }

        #endregion

        #region OrderByContract and OrderByContract[]

        public static void ToStream(OrderByContract orderBy, BinaryWriter sw)
        {
            if (orderBy == null)
                throw new ArgumentNullException("orderBy");
            if (sw == null)
                throw new ArgumentNullException("sw");

            Trace(sw, () =>
            {
                BinarySerializer.ToStream((int)orderBy.Type, sw);
                BinarySerializer.ToStream(orderBy.Expression, sw);
            });
        }

        public static void FromStream(out OrderByContract orderBy, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            orderBy = Trace(sr, () =>
            {
                int type;
                BinarySerializer.FromStream(out type, sr);

                SerializableExpression expression;
                BinarySerializer.FromStream(out expression, sr);

                return new OrderByContract()
                {
                    Type = (OrderByType)type,
                    Expression = expression
                };
            });
        }

        public static void ToStream(OrderByContract[] orderBys, BinaryWriter sw)
        {
            if (orderBys == null)
                throw new ArgumentNullException("orderBys");
            if (sw == null)
                throw new ArgumentNullException("sw");

            TraceArray(sw, orderBys, orderBy =>
            {
                BinarySerializer.ToStream(orderBy, sw);
            });
        }

        public static void FromStream(out OrderByContract[] orderBys, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            orderBys = TraceArray(sr, () =>
            {
                OrderByContract result;
                BinarySerializer.FromStream(out result, sr);
                return result;
            });
        }

        #endregion

    }
}
