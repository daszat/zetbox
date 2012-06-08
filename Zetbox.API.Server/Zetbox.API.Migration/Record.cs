using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Kistl.API.Migration
{
    public class Record
    {
        private SortedDictionary<string, object> _values;

        public Record()
        {
            _values = new SortedDictionary<string, object>();
        }

        public Record(IDataReader rd)
        {
            if (rd == null) throw new ArgumentNullException("rd");
            _values = new SortedDictionary<string, object>();
            for (int i = 0; i < rd.FieldCount; i++)
            {
                _values[rd.GetName(i)] = rd.GetValue(i);
            }
        }

        public object GetField(string name)
        {
            return GetField(name, null);
        }

        public object GetField(string name, ITypeConverter converter)
        {
            if (!_values.ContainsKey(name)) throw new ArgumentOutOfRangeException("name", "Record does not contains field " + name);
            if (converter == null) return _values[name];
            return converter.Convert(_values[name]);
        }

        public object GetField(string name, object ifNull)
        {
            return GetField(name, ifNull, null);
        }
        
        public object GetField(string name, object ifNull, ITypeConverter converter)
        {
            var v = GetField(name, converter);
            if (v == null || v == DBNull.Value)
            {
                return ifNull;
            }
            else
            {
                return v;
            }
        }

        public void SetField(string name, object val)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            _values[name] = val;
        }

        public void SetField(string name, object val, ITypeConverter converter)
        {
            if (converter == null) throw new ArgumentNullException("converter");
            SetField(name, converter.Convert(val));
        }

        public IEnumerable<string> GetFieldNames()
        {
            return _values.Keys;
        }

        public IEnumerable GetFields()
        {
            return _values.Values;
        }
    }
}
