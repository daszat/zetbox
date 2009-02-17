using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace Kistl.API
{
    /// <summary>
    /// Binary Serializer Helper.
    /// </summary>
    public static class BinarySerializer
    {

        #region bool

        /// <summary>
        /// Serialize a bool
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(bool val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing bool {0} (1 byte)", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a bool
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out bool val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean();
            Trace.TraceInformation("read bool {0}", val);
        }


        /// <summary>
        /// Serialize a nullable bool. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(bool? val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing bool? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable bool, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out bool? val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (bool?)sr.ReadBoolean() : null;
            Trace.TraceInformation("read bool? {0}", val);
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Serialize a DateTime
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(DateTime val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing DateTime {0} (8 bytes)", val);
            sw.Write(val.ToBinary());
        }

        /// <summary>
        /// Deserialize a DateTime
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out DateTime val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = DateTime.FromBinary(sr.ReadInt64());
            Trace.TraceInformation("read DateTime {0}", val);
        }

        /// <summary>
        /// Serialize a nullable DateTime. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(DateTime? val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing DateTime? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value.ToBinary()); } else sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable DateTime, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out DateTime? val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (DateTime?)DateTime.FromBinary(sr.ReadInt64()) : null;
            Trace.TraceInformation("read DateTime? {0}", val);
        }

        #endregion

        #region double

        /// <summary>
        /// Serialize a double
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(double val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing double {0}", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a double
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out double val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadDouble();
            Trace.TraceInformation("read double {0}", val);
        }

        /// <summary>
        /// Serialize a nullable double. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(double? val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing double? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable double, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out double? val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (double?)sr.ReadDouble() : null;
            Trace.TraceInformation("read double? {0}", val);
        }

        #endregion

        #region float

        /// <summary>
        /// Serialize a float
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(float val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing float {0}", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a float
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out float val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadSingle();
            Trace.TraceInformation("read float {0}", val);
        }

        /// <summary>
        /// Serialize a nullable float. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(float? val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing float? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }

        /// <summary>
        /// Deserialize a nullable float, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out float? val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (float?)sr.ReadSingle() : null;
            Trace.TraceInformation("read float? {0}", val);
        }

        #endregion

        #region int

        /// <summary>
        /// Serialize a int
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(int val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing int {0} (four bytes)", val);
            sw.Write(val);
        }

        /// <summary>
        /// Deserialize a int
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out int val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadInt32();
            Trace.TraceInformation("read int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Deserialize an int and call a converter action on it
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="sr"></param>
        public static void FromStreamConverter(Action<int> conv, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            int val = sr.ReadInt32();
            conv(val);
            Trace.TraceInformation("read and converted int {0} (4 bytes)", val);
        }

        /// <summary>
        /// Serialize a nullable int. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(int? val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing int? {0}", val);
            if (val.HasValue) { sw.Write(true); sw.Write(val.Value); } else sw.Write(false);
        }
        /// <summary>
        /// Deserialize a nullable int, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out int? val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = sr.ReadBoolean() ? (int?)sr.ReadInt32() : null;
            Trace.TraceInformation("read int? {0}", val);
        }

        #endregion

        #region IPersistenceObject

        /// <summary>
        /// Serializes a Reference to this obj
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sw"></param>
        /// TODO: See Case 805
        public static void ToStream(IPersistenceObject obj, System.IO.BinaryWriter sw)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (sw == null) throw new ArgumentNullException("sw");
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing IPersistenceObject #{0}", obj.ID);

            //if (obj == null)
            //{
            //    sw.Write(false);
            //}
            //else
            //{
            //    sw.Write(true);
            sw.Write(obj.ID);
            //}
        }

        #endregion

        #region IStruct

        /// <summary>
        /// Serialize a struct. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWriter to serialize to.</param>
        public static void ToStream(IStruct val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing IStruct {0}", val);
            if (val != null) { sw.Write(true); val.ToStream(sw); } else sw.Write(false);
        }

        /// <summary>
        /// Deserialize a struct, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream<T>(out T val, System.IO.BinaryReader sr)
            where T : class, IStruct, new()
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            val = null;
            if (sr.ReadBoolean())
            {
                val = new T();
                val.FromStream(sr);
            }
            Trace.TraceInformation("read {0} value: {1}", typeof(T), val);
        }

        #endregion

        #region SerializableExpression

        /// <summary>
        /// Serialize a SerializableExpression
        /// </summary>
        /// <param name="e">SerializableExpression to serialize.</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(SerializableExpression e, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
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
        public static void FromStream(out SerializableExpression e, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            e = null;
            if (sr.ReadBoolean())
            {
                BinaryFormatter bf = new BinaryFormatter();
                e = (SerializableExpression)bf.Deserialize(sr.BaseStream);
            }
        }

        #endregion

        #region SerializableType

        /// <summary>
        /// Serialize a SerializableType
        /// </summary>
        /// <param name="type">SerializableType to serialize.</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(SerializableType type, System.IO.BinaryWriter sw)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (sw == null) throw new ArgumentNullException("sw");
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            Trace.TraceInformation("Writing SerializableType {0}", type);

            long beginPos = sw.BaseStream.Position;

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(sw.BaseStream, type);

            long endPos = sw.BaseStream.Position;
            Trace.TraceInformation("({0} bytes)", endPos - beginPos);

        }

        /// <summary>
        /// Deserialize a SerializableType.
        /// </summary>
        /// <param name="type">SerializableType</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out SerializableType type, System.IO.BinaryReader sr)
        {
            if (sr == null) throw new ArgumentNullException("sr");
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);

            long beginPos = sr.BaseStream.Position;
            BinaryFormatter bf = new BinaryFormatter();
            type = (SerializableType)bf.Deserialize(sr.BaseStream);
            long endPos = sr.BaseStream.Position;
            Trace.TraceInformation("read SerializableType {0} ({1} bytes)", type, endPos - beginPos);
        }

        #endregion

        #region string

        /// <summary>
        /// Serialize a string. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStream(string val, System.IO.BinaryWriter sw)
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            long beginPos = sw.BaseStream.Position;
            if (val != null) { sw.Write(true); sw.Write(val); } else sw.Write(false);
            long endPos = sw.BaseStream.Position;
            if (val == null)
            {
                Trace.TraceInformation("Wrote null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                Trace.TraceInformation("Wrote string ({0} chars, {1} bytes)", val.Length, endPos - beginPos);
            }
        }

        /// <summary>
        /// Deserialize a string, expected format: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStream(out string val, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            long beginPos = sr.BaseStream.Position;
            val = sr.ReadBoolean() ? sr.ReadString() : null;
            long endPos = sr.BaseStream.Position;
            if (val == null)
            {
                Trace.TraceInformation("read null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                Trace.TraceInformation("read string ({0} chars, {1} bytes)", val.Length, endPos - beginPos);
            }
        }

        /// <summary>
        /// Deserialize a string and call a converter action on it
        /// </summary>
        /// <param name="conv"></param>
        /// <param name="sr"></param>
        public static void FromStreamConverter(Action<string> conv, System.IO.BinaryReader sr)
        {
            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            bool hasValue = sr.ReadBoolean();
            if (hasValue)
            {
                string val = sr.ReadString();
                conv(val);
                Trace.TraceInformation("read and converted string \"{0}\"", val);
            }
            else
            {
                conv(null);
                Trace.TraceInformation("read and converted null string");
            }
        }

        #endregion

        #region Collection Entries

        /// <summary>
        /// Serialize a ICollectionEntry Collection. Format is: CONTINUE (true/false), ICollectionEntry (if Object is present).
        /// </summary>
        /// <param name="val">Collection to serialize,</param>
        /// <param name="sw">BinaryWrite to serialize to.</param>
        public static void ToStreamCollectionEntries<T>(IEnumerable<T> val, System.IO.BinaryWriter sw)
            where T : ICollectionEntry
        {
            Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
            foreach (ICollectionEntry obj in val)
            {
                ToStream(true, sw);
                Trace.TraceInformation("CurrentPos: {0}", sw.BaseStream.Position);
                Trace.TraceInformation("Writing CollectionEntry {0}", val.ToString());
                obj.ToStream(sw);
            }

            ToStream(false, sw);
        }

        /// <summary>
        /// Deserialize a ICollectionEntry Collection, expected format: CONTINUE (true/false), IDataObject (if Object is present).
        /// </summary>
        /// <param name="val">Destination Value.</param>
        /// <param name="sr">BinaryReader to deserialize from.</param>
        public static void FromStreamCollectionEntries<T>(ICollection<T> val, System.IO.BinaryReader sr)
            where T : ICollectionEntry, new()
        {
            if (val == null) throw new ArgumentNullException("val");
            if (sr == null) throw new ArgumentNullException("sr");

            Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
            while (sr.ReadBoolean())
            {
                Trace.TraceInformation("CurrentPos: {0}", sr.BaseStream.Position);
                T obj = new T();
                obj.FromStream(sr);
                val.Add(obj);
                Trace.TraceInformation("read {0} value: {1}", typeof(T), val);
            }
        }

        #endregion

    }
}
