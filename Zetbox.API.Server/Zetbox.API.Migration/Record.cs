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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace Zetbox.API.Migration
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
