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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Zetbox.API.Utils;

    public sealed class ZetboxStreamWriter : IDisposable
    {
        /// <summary>
        /// Small helper to avoid off-thread autofac accesses.
        /// </summary>
        /// <remarks>Since the readers and writers are used in the thread pool, they may deadlock on autofac when a query is run from within an autofac resolution on the main thread.</remarks>
        public sealed class Factory
        {
            private readonly TypeMap _map;
            public Factory(TypeMap map)
            {
                if (map == null) throw new ArgumentNullException("map");
                _map = map;
            }

            public ZetboxStreamWriter Invoke(BinaryWriter destination)
            {
                return new ZetboxStreamWriter(_map, destination);
            }
        }
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ZetboxStreamWriter));

        private readonly TypeMap _typeMap;

        private readonly BinaryWriter _dest;

        [Conditional("DEBUG_SERIALIZATION")]
        private static void SerializerTrace(string fmt, params object[] args)
        {
            Log.InfoFormat(fmt, args);
        }

        [Conditional("DEBUG_SERIALIZATION")]
        private void TraceCurrentPos()
        {
            if (_dest.BaseStream.CanSeek)
                SerializerTrace("CurrentPos: {0}", _dest.BaseStream.Position);
        }

        private void Trace(Action serializer)
        {
            bool canSeek = _dest.BaseStream.CanSeek;
            long beginPos = canSeek ? _dest.BaseStream.Position : -1;
            TraceCurrentPos();

            serializer();

            long endPos = canSeek ? _dest.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        private void TraceArray<T>(T[] data, Action<T> serializer)
        {
            bool canSeek = _dest.BaseStream.CanSeek;
            long beginPos = canSeek ? _dest.BaseStream.Position : -1;
            TraceCurrentPos();

            if (data == null)
            {
                Write(false);
            }
            else
            {
                Write(true);

                var length = data.Length;
                SerializerTrace("Writing array of {0} {1}", length, typeof(T).Name);

                Write(length);
                for (int i = 0; i < length; i++)
                {
                    serializer(data[i]);
                }
            }
            long endPos = canSeek ? _dest.BaseStream.Position : -1;
            if (canSeek)
                SerializerTrace("Wrote {0} bytes", endPos - beginPos);
        }

        public Stream BaseStream
        {
            get
            {
                return _dest.BaseStream;
            }
        }

        public ZetboxStreamWriter(TypeMap map, BinaryWriter destination)
        {
            if (map == null) throw new ArgumentNullException("map");
            if (destination == null) throw new ArgumentNullException("destination");

            _typeMap = map;
            _dest = destination;
        }

        #region bool

        /// <summary>
        /// Serialize a bool
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(bool val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing bool {0} (1 byte)", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable bool. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(bool? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing bool? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region byte

        /// <summary>
        /// Serialize a byte
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(byte val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing byte {0} (1 byte)", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable byte. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(byte? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing byte? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region DateTime

        /// <summary>
        /// Serialize a DateTime
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(DateTime val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing DateTime {0} (8 bytes)", val);
            _dest.Write(val.ToBinary());
        }

        /// <summary>
        /// Serialize a nullable DateTime. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(DateTime? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing DateTime? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value.ToBinary());
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region double

        /// <summary>
        /// Serialize a double
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(double val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing double {0}", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable double. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(double? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing double? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region float

        /// <summary>
        /// Serialize a float
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(float val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing float {0}", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable float. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(float? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing float? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Serialize a Guid
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(Guid val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing Guid {0}", val);
            _dest.Write(val.ToByteArray());
        }

        /// <summary>
        /// Serialize a nullable Guid. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(Guid? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing Guid? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value.ToByteArray());
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region int

        /// <summary>
        /// Serialize a int
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(int val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing int {0} (four bytes)", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable int. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(int? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing int? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region decimal

        /// <summary>
        /// Serialize a decimal
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(decimal val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing int {0} (four bytes)", val);
            _dest.Write(val);
        }

        /// <summary>
        /// Serialize a nullable decimal. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(decimal? val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing int? {0}", val);
            if (val.HasValue)
            {
                _dest.Write(true);
                _dest.Write(val.Value);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region ICompoundObject

        /// <summary>
        /// Serialize a CompoundObject. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(ICompoundObject val)
        {
            TraceCurrentPos();
            if (val == null)
                Log.Warn("Writing null compoundobject");
            SerializerTrace("Writing ICompoundObject {0}", val);
            if (val != null)
            {
                _dest.Write(true);
                val.ToStream(this, null, false);
            }
            else
            {
                _dest.Write(false);
            }
        }

        #endregion

        #region IDataObject

        /// <summary>
        /// Serialize a IDataObject. Format is: ID, type if not null.
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(IDataObject val)
        {
            TraceCurrentPos();
            SerializerTrace("Writing IDataObject {0}", val);
            if (val != null)
            {
                _dest.Write(val.ID);
                Write(val.Context.GetInterfaceType(val).ToSerializableType());
            }
            else
            {
                _dest.Write(Helper.INVALIDID);
            }
        }

        #endregion

        #region SerializableExpression and SerializableExpression[]

        /// <summary>
        /// Serialize a SerializableExpression
        /// </summary>
        /// <param name="e">SerializableExpression to serialize.</param>
        public void Write(SerializableExpression e)
        {
            TraceCurrentPos();
            if (e != null)
            {
                _dest.Write(true);
                e.ToStream(this);
            }
            else
            {
                _dest.Write(false);
            }
        }

        public void Write(SerializableExpression[] expressions)
        {
            TraceArray(expressions, Write);
        }

        #endregion

        #region SerializableType

        /// <summary>
        /// Serialize a SerializableType
        /// </summary>
        /// <param name="type">SerializableType to serialize.</param>
        public void Write(SerializableType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            TraceCurrentPos();
            SerializerTrace("Writing SerializableType {0}", type);

            long beginPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;

            if (_typeMap.GuidMap.ContainsKey(type))
            {
                var guid = _typeMap.GuidMap[type];
                if (guid == Guid.Empty)
                    throw new InvalidOperationException(string.Format("Type [{0}] has invalid DefinitionGuid: [{1}]", type, guid));
                Write(guid);
            }
            else
            {
                Write(Guid.Empty);
                Write(type.TypeName);
                Write(type.AssemblyQualifiedName);
                Write(type.GenericTypeParameter);
            }

            long endPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;
            SerializerTrace("({0} bytes)", endPos - beginPos);
        }
        public void Write(SerializableType[] types)
        {
            TraceArray(types, Write);
        }

        #endregion

        #region string

        /// <summary>
        /// Serialize a string. Format is: NULL (true/false), Value (if not null).
        /// </summary>
        /// <param name="val">Value to serialize</param>
        public void Write(string val)
        {
            TraceCurrentPos();
            long beginPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;

            if (val != null)
            {
                _dest.Write(true);
                _dest.Write(val);
            }
            else
            {
                _dest.Write(false);
            }

            long endPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;
            if (val == null)
            {
                SerializerTrace("Wrote null string ({0} bytes)", endPos - beginPos);
            }
            else
            {
                SerializerTrace("Wrote string ({0} chars, {1} bytes)", val.Length, endPos - beginPos);
            }
        }

        #endregion

        #region Collection Entries

        /// <summary>
        /// Serialize a ICollectionEntry Collection. Format is: CONTINUE (true/false), ICollectionEntry (if Object is present).
        /// </summary>
        /// <param name="val">Collection to serialize. Assumed empty if null.</param>
        public void WriteCollectionEntries<T>(IEnumerable<T> val)
            where T : IStreamable
        {
            if (val == null)
            {
                TraceCurrentPos();
                SerializerTrace("writing null collection as empty");
            }
            else
            {
                TraceCurrentPos();
                foreach (IStreamable obj in val)
                {
                    Write(true);
                    TraceCurrentPos();
                    SerializerTrace("Writing CollectionEntry {0}", val.ToString());
                    obj.ToStream(this, null, false);
                }
            }

            Write(false);
        }

        #endregion

        #region byte[]

        /// <summary>
        /// Serialize an array of bytes
        /// </summary>
        /// <param name="bytes">data to serialize.</param>
        public void Write(byte[] bytes)
        {
            if (bytes == null)
            {
                _dest.Write(-1);
            }
            else
            {
                _dest.Write(bytes.Length);
                if (bytes.Length > 0)
                    _dest.Write(bytes);
            }
        }

        /// <summary>
        /// Write an array of bytes without any encoding.
        /// </summary>
        /// <param name="bytes">data to serialize.</param>
        public void WriteRaw(byte[] bytes)
        {
            _dest.Write(bytes);
        }

        #endregion

        #region ObjectNotificationRequest and ObjectNotificationRequest[]

        public void Write(ObjectNotificationRequest notificationRequest)
        {
            if (notificationRequest == null)
                throw new ArgumentNullException("notificationRequest");
            TraceCurrentPos();
            SerializerTrace("Writing ObjectNotificationRequest for {0} with {1} IDs", notificationRequest.Type, notificationRequest.IDs.Length);

            long beginPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;

            Write(notificationRequest.Type);
            _dest.Write(notificationRequest.IDs.Length);
            foreach (var id in notificationRequest.IDs)
            {
                _dest.Write(id);
            }

            long endPos = _dest.BaseStream.CanSeek ? _dest.BaseStream.Position : -1;
            SerializerTrace("({0} bytes)", endPos - beginPos);
        }

        public void Write(ObjectNotificationRequest[] notificationRequests)
        {
            TraceArray(notificationRequests, Write);
        }

        #endregion

        #region OrderByContract and OrderByContract[]

        public void Write(OrderByContract orderBy)
        {
            if (orderBy == null)
                throw new ArgumentNullException("orderBy");

            Trace(() =>
            {
                Write((int)orderBy.Type);
                Write(orderBy.Expression);
            });
        }

        public void Write(OrderByContract[] orderBys)
        {
            TraceArray(orderBys, Write);
        }

        #endregion

        #region object

        public void Write(object value)
        {
            if (value == null)
            {
                // IsNull
                _dest.Write(true);
            }
            else
            {
                var type = value.GetType();
                // IsNull
                _dest.Write(false);

                if (type.IsArray)
                {
                    var elementType = type.GetElementType();
                    WriteArrayInternal(value, elementType);
                }
                else if (typeof(System.Collections.IEnumerable).IsAssignableFrom(type) && type != typeof(string))
                {
                    var elementType = type.FindElementTypes().Single(t => t != typeof(object));
                    WriteArrayInternal(value, elementType);
                }
                else
                {
                    WriteInternal(value, type);
                }
            }
        }

        private void WriteArrayInternal(object value, Type elementType)
        {
            foreach (var element in (System.Collections.IEnumerable)value)
            {
                _dest.Write(true);
                WriteInternal(element, elementType);
            }
            _dest.Write(false);
        }

        // Serialize only basic types
        private void WriteInternal(object value, Type type)
        {
            if (type == typeof(int) || type == typeof(int?) || type.IsEnum || type.IsNullableEnum())
            {
                _dest.Write((int)value);
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                _dest.Write((bool)value);
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                _dest.Write((double)value);
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                _dest.Write((float)value);
            }
            else if (type == typeof(string))
            {
                _dest.Write((string)value);
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                _dest.Write((decimal)value);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                Write((DateTime)value);
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                Write((Guid)value);
            }
            else if (type.IsICompoundObject())
            {
                Write((ICompoundObject)value);
            }
            else if (type.IsIDataObject())
            {
                Write((IDataObject)value);
            }
            else
            {
                throw new NotSupportedException(string.Format("Can't serialize Value '{0}' of type '{1}'.", value, type));
            }
        }

        #endregion

        public void Dispose()
        {
            ((IDisposable)_dest).Dispose();
        }

        public void Flush()
        {
            _dest.Flush();
        }
    }
}
