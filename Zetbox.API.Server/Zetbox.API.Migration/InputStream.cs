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
