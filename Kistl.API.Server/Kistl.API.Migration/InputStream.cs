using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Kistl.API.Migration
{
    public class InputStream : IDisposable, IEnumerable<Record>
    {
        public class InputStreamEnumerator : IEnumerator<Record>
        {
            private readonly IDataReader _rd;
            private Record _current = null;

            public InputStreamEnumerator(IDataReader rd)
            {
                if (rd == null) throw new ArgumentNullException("rd");
                _rd = rd;
            }

            #region IEnumerator<Record> Members

            public Record Current
            {
                get { return _current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                if (_rd != null) _rd.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return _current; }
            }

            public bool MoveNext()
            {
                var result = _rd.Read();
                _current = result ? new Record(_rd) : null;
                return result;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            #endregion
        }

        private readonly IDataReader _rd;

        public InputStream(IDataReader rd)
        {
            if (rd == null) throw new ArgumentNullException("rd");
            _rd = rd;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_rd != null) _rd.Dispose();
        }

        #endregion

        #region IEnumerable<Record> Members

        public IEnumerator<Record> GetEnumerator()
        {
            return new InputStreamEnumerator(_rd);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new InputStreamEnumerator(_rd);
        }

        #endregion
    }
}
