
namespace Zetbox.API.Migration
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;

    public class InputStream : IDisposable, IEnumerable<Record>
    {
        private readonly IDataReader _rd;
        private bool _open = false;
        private long _readRecords = 0;

        public InputStream(IDataReader rd)
        {
            if (rd == null) throw new ArgumentNullException("rd");
            _rd = rd;
        }

        /// <summary>
        /// The number of records read from this InputStream.
        /// </summary>
        public long ReadRecords
        {
            get { return _readRecords; }
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
            if (_open)
            {
                throw new InvalidOperationException("InputStream enumeration is already in progress");
            }
            else
            {
                _open = true;
            }

            while (_rd.Read())
            {
                _readRecords += 1;
                yield return new Record(_rd);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
