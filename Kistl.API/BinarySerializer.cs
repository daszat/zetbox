
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
    using System.Collections;

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
            bool canSeek = sw.BaseStream.CanSeek;
            long beginPos = canSeek ? sw.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            serializer();

            long endPos = canSeek ? sw.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        private static T Trace<T>(BinaryReader sr, Func<T> deserializer)
        {
            bool canSeek = sr.BaseStream.CanSeek;
            long beginPos = canSeek ? sr.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            var result = deserializer();

            long endPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Read {0} bytes", endPos - beginPos);

            return result;
        }

        private static void TraceArray<T>(BinaryWriter sw, T[] data, Action<T> serializer)
        {
            bool canSeek = sw.BaseStream.CanSeek;
            long beginPos = canSeek ? sw.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            if (data == null)
            {
                BinarySerializer.ToStream(false, sw);
            }
            else
            {
                BinarySerializer.ToStream(true, sw);

                var length = data.Length;
                SerializerTrace("Writing array of {0} {1}", length, typeof(T).Name);

                sw.Write(length);
                for (int i = 0; i < length; i++)
                {
                    serializer(data[i]);
                }
            }
            long endPos = canSeek ? sw.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        private static T[] TraceArray<T>(BinaryReader sr, Func<T> deserializer)
        {
            bool canSeek = sr.BaseStream.CanSeek;
            long beginPos = canSeek ? sr.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("CurrentPos: {0}", beginPos);

            T[] result;

            if (sr.ReadBoolean())
            {
                var length = sr.ReadInt32();
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
            long endPos = canSeek ? sr.BaseStream.Position : -1;
            if (canSeek)
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
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
            ICompoundObject value;
            FromStream(out value, typeof(T), sr);
            val = (T)value;
        }

        public static void FromStream(out ICompoundObject val, Type type, BinaryReader sr)
        {
            if (sr == null) { throw new ArgumentNullException("sr"); }
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
            val = null;
            if (sr.ReadBoolean())
            {
                val = (ICompoundObject)Activator.CreateInstance(type);
                val.FromStream(sr);
                // CompoundObjects cannot have lists
            }

            SerializerTrace("read {0} value: {1}", type, val);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
            if (e != null)
            {
                sw.Write(true);
                e.ToStream(sw);
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
        /// <param name="iftFactory">InterfaceType.Factory to pass on the the read SerializableExpressions</param>
        public static void FromStream(out SerializableExpression e, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
            e = null;
            if (sr.ReadBoolean())
            {
                e = SerializableExpression.FromStream(sr, iftFactory);
            }
        }

        public static void ToStream(SerializableExpression[] expressions, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");

            TraceArray(sw, expressions, expression =>
            {
                BinarySerializer.ToStream(expression, sw);
            });
        }

        public static void FromStream(out SerializableExpression[] expressions, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            expressions = TraceArray(sr, () =>
            {
                SerializableExpression result;
                BinarySerializer.FromStream(out result, sr, iftFactory);
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
            SerializerTrace("Writing SerializableType {0}", type);

            long beginPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(sw.BaseStream, type);

            long endPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);

            long beginPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
            BinaryFormatter bf = new BinaryFormatter();
            type = (SerializableType)bf.Deserialize(sr.BaseStream);
            long endPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
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
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
            long beginPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;
            if (val != null) { sw.Write(true); sw.Write(val); }
            else
                sw.Write(false);
            long endPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
            long beginPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
            val = sr.ReadBoolean() ? sr.ReadString() : null;
            long endPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
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
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
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
                SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
                SerializerTrace("writing null collection as empty");
            }
            else
            {
                SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
                foreach (IStreamable obj in val)
                {
                    ToStream(true, sw);
                    SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
                    SerializerTrace("Writing CollectionEntry {0}", val.ToString());
                    obj.ToStream(sw, null, false);
                }
            }

            ToStream(false, sw);
        }

        /// <summary>
        /// Deserialize a IValueCollectionEntry Collection, expected format: CONTINUE (true/false), IDataObject (if Object is present).
        /// </summary>
        /// <param name="parent">the parent container holding this collection.</param>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStreamCollectionEntries<T>(IDataObject parent, ICollection<T> val, BinaryReader sr)
            where T : IStreamable, new()
        {
            if (val == null)
                throw new ArgumentNullException("val");
            if (sr == null)
                throw new ArgumentNullException("sr");

            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);
            while (sr.ReadBoolean())
            {
                SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);

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

            TraceArray(sw, bytes, b => sw.Write(b));
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

            bytes = TraceArray(sr, () => sr.ReadByte());
        }

        #endregion

        #region ObjectNotificationRequest and ObjectNotificationRequest[]

        public static void ToStream(ObjectNotificationRequest notificationRequest, BinaryWriter sw)
        {
            if (notificationRequest == null)
                throw new ArgumentNullException("notificationRequest");
            if (sw == null)
                throw new ArgumentNullException("sw");
            SerializerTrace("CurrentPos: {0}", sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1);
            SerializerTrace("Writing ObjectNotificationRequest for {0} with {1} IDs", notificationRequest.Type, notificationRequest.IDs.Length);

            long beginPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;

            BinarySerializer.ToStream(notificationRequest.Type, sw);
            sw.Write(notificationRequest.IDs.Length);
            foreach (var id in notificationRequest.IDs)
            {
                sw.Write(id);
            }

            long endPos = sw.BaseStream.CanSeek ? sw.BaseStream.Position : -1;
            SerializerTrace("({0} bytes)", endPos - beginPos);
        }

        public static void FromStream(out ObjectNotificationRequest notificationRequest, BinaryReader sr)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");
            SerializerTrace("CurrentPos: {0}", sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1);

            long beginPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;

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

            long endPos = sr.BaseStream.CanSeek ? sr.BaseStream.Position : -1;
            SerializerTrace("read ObjectNotificationRequest ({0} IDs, {1} bytes)", ids.Length, endPos - beginPos);
        }

        public static void ToStream(ObjectNotificationRequest[] notificationRequests, BinaryWriter sw)
        {
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

        public static void FromStream(out OrderByContract orderBy, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            orderBy = Trace(sr, () =>
            {
                int type;
                BinarySerializer.FromStream(out type, sr);

                SerializableExpression expression;
                BinarySerializer.FromStream(out expression, sr, iftFactory);

                return new OrderByContract()
                {
                    Type = (OrderByType)type,
                    Expression = expression
                };
            });
        }

        public static void ToStream(OrderByContract[] orderBys, BinaryWriter sw)
        {
            if (sw == null)
                throw new ArgumentNullException("sw");

            TraceArray(sw, orderBys, orderBy =>
            {
                BinarySerializer.ToStream(orderBy, sw);
            });
        }

        public static void FromStream(out OrderByContract[] orderBys, BinaryReader sr, InterfaceType.Factory iftFactory)
        {
            if (sr == null)
                throw new ArgumentNullException("sr");

            orderBys = TraceArray(sr, () =>
            {
                OrderByContract result;
                BinarySerializer.FromStream(out result, sr, iftFactory);
                return result;
            });
        }

        #endregion

        #region object
        public static void ToStream(object value, BinaryWriter sw)
        {
            if (sw == null) throw new ArgumentNullException("sw");
            if (value == null)
            {
                // IsNull
                sw.Write(true);
            }
            else
            {
                var type = value.GetType();
                // IsNull
                sw.Write(false);

                if (type.IsArray)
                {
                    var elementType = type.GetElementType();
                    foreach (var element in (IEnumerable)value)
                    {
                        sw.Write(true);
                        ToStreamInternal(element, elementType, sw);
                    }
                    sw.Write(false);
                }
                else
                {
                    ToStreamInternal(value, type, sw);
                }
            }
        }

        // Serialize only basic types
        private static void ToStreamInternal(object value, Type type, BinaryWriter sw)
        {
            if (type == typeof(int) || type == typeof(int?) || type.IsEnum || type.IsNullableEnum())
            {
                sw.Write((int)value);
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                sw.Write((bool)value);
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                sw.Write((double)value);
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                sw.Write((float)value);
            }
            else if (type == typeof(string))
            {
                sw.Write((string)value);
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                sw.Write((decimal)value);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                BinarySerializer.ToStream((DateTime)value, sw);
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                BinarySerializer.ToStream((Guid)value, sw);
            }
            else if (type.IsICompoundObject())
            {
                BinarySerializer.ToStream((ICompoundObject)value, sw);
            }
            else
            {
                throw new NotSupportedException(string.Format("Can't serialize Value '{0}' of type '{1}'.", value, type));
            }
        }

        public static void FromStream(out object value, Type type, BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            if (type == null) throw new ArgumentNullException("type");
            var isNull = sr.ReadBoolean();
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
                    ArrayList array = new ArrayList();
                    FromStreamInternalArray(array, sr, elementType);
                    value = Activator.CreateInstance(type);
                    array.CopyTo((Array)value);
                }
                // only IEnumerable<> -> use List<>
                else if (type.IsGenericType && type.GetGenericArguments().Length == 1 && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    var elementType = type.FindElementTypes().First();
                    IList array = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
                    FromStreamInternalArray(array, sr, elementType);
                    value = array;
                }
                // Try plain types
                else
                {
                    value = FromStreamInternal(type, sr);
                }
            }
        }

        private static void FromStreamInternalArray(IList array, BinaryReader sr, Type elementType)
        {
            while (true)
            {
                var hasValue = sr.ReadBoolean();
                if (!hasValue) break;

                var elementValue = FromStreamInternal(elementType, sr);
                array.Add(elementValue);
            }
        }

        private static object FromStreamInternal(Type type, BinaryReader sr)
        {
            object value;
            // Deserialize only basic types
            if (type == typeof(int) || type == typeof(int?) || type.IsEnum || type.IsNullableEnum())
            {
                value = sr.ReadInt32();
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                value = sr.ReadBoolean();
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                value = sr.ReadDouble();
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                value = sr.ReadSingle();
            }
            else if (type == typeof(string))
            {
                value = sr.ReadString();
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                value = sr.ReadDecimal();
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                DateTime val;
                BinarySerializer.FromStream(out val, sr);
                value = val;
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                Guid val;
                BinarySerializer.FromStream(out val, sr);
                value = val;
            }
            else if (type.IsICompoundObject())
            {
                ICompoundObject val;
                BinarySerializer.FromStream(out val, type, sr);
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
