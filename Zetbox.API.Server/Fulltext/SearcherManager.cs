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

namespace Zetbox.API.Server.Fulltext
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Lucene.Net.Index;
    using Lucene.Net.Search;
    using Lucene.Net.Store;

    /// <summary>simple empulation from SearcherManager API from 3.5 </summary>
    public sealed class SearcherManager
    {
        private readonly object _lock = new object();
        private readonly Directory _path;
        private readonly Dictionary<IndexReader, int> _threadCount = new Dictionary<IndexReader, int>();

        private IndexReader _currentReader;

        internal SearcherManager(Directory path)
        {
            _path = path;
        }

        public IndexSearcher Aquire()
        {
            lock (_lock)
            {
                if (_currentReader == null)
                {
                    _currentReader = DirectoryReader.Open(_path);
                    _threadCount[_currentReader] = 0;
                }
                _threadCount[_currentReader] += 1;
                return new IndexSearcher(_currentReader);
            }
        }

        public void Release(IndexSearcher searcher)
        {
            if (searcher == null) throw new ArgumentNullException("searcher");

            lock (_lock)
            {
                if (searcher.IndexReader != _currentReader && _threadCount[searcher.IndexReader] == 1)
                {
                    searcher.IndexReader.Dispose();
                    _threadCount.Remove(searcher.IndexReader);
                }
                else
                {
                    _threadCount[searcher.IndexReader] -= 1;
                }
            }
        }
    }
}
