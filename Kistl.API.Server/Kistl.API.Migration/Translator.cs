
namespace Kistl.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Kistl.API.Server;
    using ZBox.App.SchemaMigration;

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
                    var srcType = DbTypeMapper.GetDbTypeForProperty(_srcColumns[i].DestinationProperty.GetType());
                    var destType = DbTypeMapper.GetDbType(_srcColumns[i].DbType);

                    if (val != null
                        && val != DBNull.Value
                        && srcType != destType)
                    {
                        Log.InfoFormat("Convert [{0}] from [{1}] to [{2}]", _srcColumns[i].Name, srcType, destType);
                        switch (destType)
                        {
                            case DbType.AnsiString:
                            case DbType.AnsiStringFixedLength:
                            case DbType.String:
                            case DbType.StringFixedLength:
                                val = val.ToString();
                                break;

                            case DbType.Boolean:
                                val = Convert.ToBoolean(val);
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
                    _resultValues[i] = val;
                }
            }
            else
            {
                _resultValues = null;
            }
            return result;
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
            return _source.GetBoolean(i);
        }

        public byte GetByte(int i)
        {
            return _source.GetByte(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _source.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return _source.GetChar(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _source.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        public string GetDataTypeName(int i)
        {
            return _source.GetDataTypeName(i);
        }

        public DateTime GetDateTime(int i)
        {
            return _source.GetDateTime(i);
        }

        public decimal GetDecimal(int i)
        {
            return _source.GetDecimal(i);
        }

        public double GetDouble(int i)
        {
            return _source.GetDouble(i);
        }

        public Type GetFieldType(int i)
        {
            return _source.GetFieldType(i);
        }

        public float GetFloat(int i)
        {
            return _source.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            if (i == 2000) return Guid.NewGuid();
            return _source.GetGuid(i);
        }

        public short GetInt16(int i)
        {
            return _source.GetInt16(i);
        }

        public int GetInt32(int i)
        {
            return _source.GetInt32(i);
        }

        public long GetInt64(int i)
        {
            return _source.GetInt64(i);
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
            return _source.GetString(i);
        }

        public object GetValue(int i)
        {
            if (i == 2000) return Guid.NewGuid();
            return _source.GetValue(i);
        }

        public int GetValues(object[] values)
        {
            return _source.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return _source.IsDBNull(i);
        }

        public object this[string name]
        {
            get { return _source[name]; }
        }

        public object this[int i]
        {
            get { return _source[i]; }
        }

        #endregion
    }
}
