
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using Kistl.App.Base;
    using ZBox.App.SchemaMigration;

    internal sealed class Translator
        : IDataReader
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration.Translator");

        private readonly SourceTable _tbl;
        private readonly IDataReader _source;
        private readonly SourceColumn[] _srcColumns;
        private readonly Converter[] _nullConverter;

        private readonly int _errorColIdx;
        private StringBuilder _currentError;

        private object[] _resultValues;
        private int _resultColumnCount;

        private Dictionary<string, int> _compoundObjectSourceColumns = new Dictionary<string,int>();

        private long _processedRows = 0;

        public Translator(SourceTable tbl, IDataReader source, IEnumerable<SourceColumn> srcColumns, Converter[] nullConverter)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (source == null) throw new ArgumentNullException("source");
            if (srcColumns == null) throw new ArgumentNullException("srcColumns");

            _tbl = tbl;
            _source = source;
            _srcColumns = srcColumns.ToArray();
            _nullConverter = nullConverter ?? new Converter[] { };
            _resultColumnCount = _srcColumns.Length;

            if (typeof(IMigrationInfo).IsAssignableFrom(tbl.DestinationObjectClass.GetDataType()))
            {
                // TODO: That's a bad hack!
                _errorColIdx = _resultColumnCount;
                _resultColumnCount++;
            }
            else
            {
                _errorColIdx = -1;
            }

            foreach(var comp in srcColumns
                .Where(c => c.DestinationProperty.First() is CompoundObjectProperty)
                .GroupBy(c => c.DestinationProperty.First()))
            {
                foreach (var col in comp)
                {
                    _compoundObjectSourceColumns[col.Name] = _resultColumnCount;
                }
                _resultColumnCount++;
            }
        }

        public long ProcessedRows
        {
            get { return _processedRows; }
        }

        private void AddError(string msg, object val)
        {
            if (_currentError == null) _currentError = new StringBuilder();
            _currentError.AppendFormat(msg, val);
            _currentError.AppendLine();
        }

        #region IDataReader Members

        public void Close()
        {
            _source.Close();
            _resultValues = null;
        }

        public int Depth
        {
            get { return _source.Depth; }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        public bool IsClosed
        {
            get { return _source.IsClosed; }
        }

        public bool NextResult()
        {
            // only the first result set is supported
            return false;
        }

        public bool Read()
        {
            _currentError = null;
            var result = _source.Read();
            if (result)
            {
                // allocate new row
                _resultValues = new object[_resultColumnCount];
                _source.GetValues(_resultValues);

                // calculate new row
                for (int i = 0; i < _srcColumns.Length; i++)
                {
                    var src_col = _srcColumns[i];
                    var src_val = _source.GetValue(i);
                    object val = null;

                    var fieldConv = _nullConverter.OfType<FieldConverter>().SingleOrDefault(c => c.Column.Name == src_col.Name);
                    if (fieldConv != null)
                    {
                        val = fieldConv.Converter(_source);
                    }
                    else
                    {
                        val = ConvertType(src_col, src_val);
                    }

                    val = HandleNullValue(src_col, val, src_val);
                    _resultValues[i] = val;

                    // Handle compound object null bit
                    if (src_col.DestinationProperty.First() is CompoundObjectProperty)
                    {
                        bool isNull = val == null || val == DBNull.Value;
                        var idx = _compoundObjectSourceColumns[src_col.Name];
                        if (_resultValues[idx] == null)
                        {
                            _resultValues[idx] = isNull;
                        }
                        else
                        {
                            _resultValues[idx] = (bool)_resultValues[idx] && isNull;
                        }
                    }
                }

                // append errors
                if (_errorColIdx != -1)
                {
                    _resultValues[_errorColIdx] = _currentError != null ? (object)_currentError.ToString() : DBNull.Value;
                }
                _processedRows += 1;
            }
            else
            {
                _resultValues = null;
            }
            return result;
        }

        private object HandleNullValue(SourceColumn sourceColumn, object val, object src_val)
        {
            var nullConv = _nullConverter.OfType<NullConverter>().SingleOrDefault(i => i.Column == sourceColumn);
            if (nullConv == null) return val;
            if (val == null || val == DBNull.Value)
            {
                AddError(nullConv.ErrorMsg, src_val != null ? src_val.ToString() : "(null)");
                val = nullConv.NullValue;
            }
            return val;
        }

        private static ITypeConverter _dateTimeConverter = new SqlServerDateTimeConverter();
        private static ITypeConverter _int32Converter = new Int32Converter();
        private static ITypeConverter _int16Converter = new Int16Converter();
        private static ITypeConverter _byteConverter = new ByteConverter();
        private static ITypeConverter _boolConverter = new BoolConverter();

        private object ConvertType(SourceColumn col, object src_val)
        {
            if (src_val == null || src_val == DBNull.Value) return src_val;

            var destType = DbTypeMapper.GetDbTypeForProperty(col.DestinationProperty.Last().GetType());
            var srcType = DbTypeMapper.GetDbType(col.DbType);
            object dest_val = src_val;

            if (col.DestinationProperty.Last() is Kistl.App.Base.EnumerationProperty)
            {
                Log.DebugFormat("Convert [{0}] = '{1}' from [{2}] to enum", col.Name, src_val, srcType);
                // Lookup mapping first
                var destEnum = col.EnumEntries.FirstOrDefault(e => e.SourceValue == src_val.ToString());
                if (destEnum != null)
                {
                    return destEnum.DestinationValue.Value;
                }
                else
                {
                    // Try to autoresolve
                    var enumProp = (Kistl.App.Base.EnumerationProperty)col.DestinationProperty.Last();
                    // Lookup by name
                    var destEnumEntry = enumProp.Enumeration.EnumerationEntries.FirstOrDefault(e => e.Name == src_val.ToString());
                    if (destEnumEntry != null)
                    {
                        return destEnumEntry.Value;
                    }
                    else
                    {
                        // Try by number
                        int int_val;
                        if (int.TryParse(src_val.ToString(), out int_val))
                        {
                            destEnumEntry = enumProp.Enumeration.EnumerationEntries.FirstOrDefault(e => e.Value == int_val);
                            if (destEnumEntry != null)
                            {
                                return destEnumEntry.Value;
                            }
                        }
                    }
                    // Nothing found -> return null
                    return DBNull.Value;
                }                
            }
            else if (srcType != destType
                && col.References == null)
            {
                Log.DebugFormat("Convert [{0}] = '{1}' from [{2}] to [{3}]", col.Name, src_val, srcType, destType);
                try
                {
                    switch (destType)
                    {
                        case DbType.AnsiString:
                        case DbType.AnsiStringFixedLength:
                        case DbType.String:
                        case DbType.StringFixedLength:
                            dest_val = src_val.ToString();
                            break;

                        case DbType.Boolean:
                            dest_val = _boolConverter.Convert(src_val);
                            break;

                        case DbType.Date:
                        case DbType.DateTime:
                        case DbType.DateTime2:
                            dest_val = _dateTimeConverter.Convert(src_val);
                            break;

                        case DbType.Single:
                            dest_val = Convert.ToSingle(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;
                        case DbType.Double:
                            dest_val = Convert.ToDouble(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;

                        case DbType.Byte:
                            dest_val = _byteConverter.Convert(src_val);
                            break;
                        case DbType.Int16:
                            dest_val = _int16Converter.Convert(src_val);
                            break;
                        case DbType.Int32:
                            dest_val = _int32Converter.Convert(src_val);
                            break;

                        case DbType.SByte:
                            dest_val = Convert.ToSByte(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;
                        case DbType.Int64:
                            dest_val = Convert.ToInt64(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;
                        case DbType.UInt16:
                            dest_val = Convert.ToUInt16(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;
                        case DbType.UInt32:
                            dest_val = Convert.ToUInt32(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;
                        case DbType.UInt64:
                            dest_val = Convert.ToUInt64(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;

                        case DbType.Guid:
                            dest_val = new Guid(src_val.ToString());
                            break;

                        case DbType.Currency:
                        case DbType.Decimal:
                        case DbType.VarNumeric:
                            dest_val = Convert.ToDecimal(src_val, CultureInfo.GetCultureInfo("de-AT"));
                            break;


                        case DbType.Binary:
                        case DbType.DateTimeOffset:
                        case DbType.Object:
                        case DbType.Time:
                        case DbType.Xml:
                        default:
                            throw new NotSupportedException("Unknown DbType " + destType);

                    }
                }
                catch
                {
                    dest_val = DBNull.Value;
                }

                if (dest_val == null || dest_val == DBNull.Value)
                {
                    AddError(string.Format("Unable to convert '{{0}}' to {0}", destType), src_val);
                }
            }
            Log.DebugFormat(" => '{0}'", dest_val);
            return dest_val;
        }

        public int RecordsAffected
        {
            get { return _source.RecordsAffected; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _source.Dispose();
        }

        #endregion

        #region IDataRecord Members

        public int FieldCount
        {
            get { return _source.FieldCount; }
        }

        public bool GetBoolean(int i)
        {
            return (bool)_resultValues[i];
        }

        public byte GetByte(int i)
        {
            return (byte)_resultValues[i];
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public char GetChar(int i)
        {
            return (char)_resultValues[i];
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        public string GetDataTypeName(int i)
        {
            return _srcColumns[i].DbType.ToString();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)_resultValues[i];
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)_resultValues[i];
        }

        public double GetDouble(int i)
        {
            return (double)_resultValues[i];
        }

        public Type GetFieldType(int i)
        {
            var col = _srcColumns[i];
            return col.DestinationProperty.SingleOrDefault().GetPropertyType();
        }

        public float GetFloat(int i)
        {
            return (float)_resultValues[i];
        }

        public Guid GetGuid(int i)
        {
            return (Guid)_resultValues[i];
        }

        public short GetInt16(int i)
        {
            return (short)_resultValues[i];
        }

        public int GetInt32(int i)
        {
            return (int)_resultValues[i];
        }

        public long GetInt64(int i)
        {
            return (long)_resultValues[i];
        }

        public string GetName(int i)
        {
            return _source.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return _source.GetOrdinal(name);
        }

        public string GetString(int i)
        {
            return (string)_resultValues[i];
        }

        public object GetValue(int i)
        {
            return _resultValues[i];
        }

        public int GetValues(object[] values)
        {
            _resultValues.CopyTo(values, 0);
            return _resultValues.Length;
        }

        public bool IsDBNull(int i)
        {
            return _resultValues[i] == null || _resultValues[i] == DBNull.Value;
        }

        public object this[string name]
        {
            get { return _resultValues[GetOrdinal(name)]; }
        }

        public object this[int i]
        {
            get { return _resultValues[i]; }
        }

        #endregion
    }
}
