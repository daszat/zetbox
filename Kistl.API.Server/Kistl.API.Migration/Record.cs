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
            if (!_values.ContainsKey(name)) throw new ArgumentOutOfRangeException("name", "Record does not contains field " + name);
            return _values[name];
        }

        public void SetField(string name, object val)
        {
            _values[name] = val;
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
