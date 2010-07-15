
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;
    using System.Globalization;

    public sealed class Translator
        : IDataReader
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.API.Migration.Translator");

        private readonly SourceTable _tbl;
        private readonly IDataReader _source;
        private readonly SourceColumn[] _srcColumns;
        private readonly string[] _srcColumnNames;
        private readonly string[] _dstColumnNames;

        private object[] _resultValues;

        public Translator(SourceTable tbl, IDataReader source, IEnumerable<SourceColumn> srcColumns)
        {
            _tbl = tbl;
            _source = source;
            _srcColumns = srcColumns.ToArray();
            _srcColumnNames = srcColumns.Select(c => c.Name).ToArray();
            _dstColumnNames = srcColumns.Select(c => c.DestinationProperty.Name).ToArray();
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
            var result = _source.Read();
            if (result)
            {
                // calculate new row
                _resultValues = new object[_srcColumnNames.Length];
                _source.GetValues(_resultValues);

                for (int i = 0; i < _srcColumnNames.Length; i++)
                {
                    var val = _source.GetValue(i);
                    val = ConvertType(_srcColumns[i], val);
                    _resultValues[i] = val;
                }
            }
            else
            {
                _resultValues = null;
            }
            return result;
        }

        private object ConvertType(SourceColumn col, object val)
        {
            if (val == null || val == DBNull.Value) return val;

            var destType = DbTypeMapper.GetDbTypeForProperty(col.DestinationProperty.GetType());
            var srcType = DbTypeMapper.GetDbType(col.DbType);

            if (col.DestinationProperty is Kistl.App.Base.EnumerationProperty)
            {
                Log.InfoFormat("Convert [{0}] = '{1}' from [{2}] to enum", col.Name, val, srcType);
                var destVal = col.EnumEntries.FirstOrDefault(e => e.SourceValue == val.ToString());
                return destVal != null ? (object)destVal.DestinationValue.Value : DBNull.Value;
            }
            else if (srcType != destType
                && col.References == null)
            {
                Log.InfoFormat("Convert [{0}] = '{1}' from [{2}] to [{3}]", col.Name, val, srcType, destType);
                try
                {
                    switch (destType)
                    {
                        case DbType.AnsiString:
                        case DbType.AnsiStringFixedLength:
                        case DbType.String:
                        case DbType.StringFixedLength:
                            val = val.ToString();
                            break;

                        case DbType.Boolean:
                            // try string convertions
                            if (val is string)
                            {
                                var s = ((string)val).ToLower();
                                if (s.Contains("yes")) val = true;
                                else if (s.Contains("no")) val = false;
                                else if (s.Contains("ja")) val = true;
                                else if (s.Contains("nein")) val = false;
                                else if (s.Contains("-1")) val = true;
                                else if (s.Contains("1")) val = true;
                                else if (s.Contains("0")) val = false;
                                else val = DBNull.Value; // Error
                            }
                            else if (val is int)
                            {
                                var i = (int)val;
                                val = i != 0;
                            }
                            else
                            {
                                // try other
                                val = Convert.ToBoolean(val);
                            }
                            break;

                        case DbType.Date:
                        case DbType.DateTime:
                        case DbType.DateTime2:
                            val = Convert.ToDateTime(val);
                            break;

                        case DbType.Single:
                            val = Convert.ToSingle(val);
                            break;
                        case DbType.Double:
                            val = Convert.ToDouble(val);
                            break;

                        case DbType.Byte:
                            val = Convert.ToByte(val);
                            break;
                        case DbType.Int16:
                            val = Convert.ToInt16(val);
                            break;
                        case DbType.Int32:
                            val = Convert.ToInt32(val);
                            break;

                        case DbType.SByte:
                            val = Convert.ToSByte(val);
                            break;
                        case DbType.Int64:
                            val = Convert.ToInt64(val);
                            break;
                        case DbType.UInt16:
                            val = Convert.ToUInt16(val);
                            break;
                        case DbType.UInt32:
                            val = Convert.ToUInt32(val);
                            break;
                        case DbType.UInt64:
                            val = Convert.ToUInt64(val);
                            break;

                        case DbType.Guid:
                            val = new Guid(val.ToString());
                            break;

                        case DbType.Currency:
                        case DbType.Decimal:
                        case DbType.VarNumeric:
                            val = Convert.ToDecimal(val);
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
                    // error
                    val = DBNull.Value;
                }
            }
            Log.DebugFormat(" => '{0}'", val);
            return val;
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
            return col.DestinationProperty.GetPropertyType();
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
