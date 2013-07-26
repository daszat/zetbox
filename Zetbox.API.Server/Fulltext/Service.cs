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
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.Search;

    internal struct IndexUpdate
    {
        public List<Tuple<InterfaceType, int, string>> added;
        public List<Tuple<InterfaceType, int, string>> modified;
        public List<Tuple<InterfaceType, int>> deleted;

        public bool IsValid { get { return added != null && modified != null && deleted != null; } }
        public bool IsEmpty
        {
            get
            {
                return (added == null || added.Count == 0)
                    && (modified == null || modified.Count == 0)
                    && (deleted == null || deleted.Count == 0);
            }
        }
    }

    internal sealed class Service : ThreadedQueueService<IndexUpdate>
    {
        private readonly IndexWriter _indexWriter;

        internal Service(IndexWriter indexWriter)
        {
            if (indexWriter == null) throw new ArgumentNullException("indexWriter");

            _indexWriter = indexWriter;
        }

        #region IService Members

        protected override bool OnEnqueue(IndexUpdate item)
        {
            // ignore updates with no contents
            return item.IsValid && !item.IsEmpty;
        }

        public override string DisplayName
        {
            get { return "Lucene Indexwriter Service"; }
        }

        public override string Description
        {
            get { return "Listens to updates from contexts and keeps the Lucene.NET index up-to-date"; }
        }

        #endregion

        protected override void ProcessItem(IndexUpdate item)
        {
            foreach (var add in item.added.Concat(item.modified))
            {
                var clsId = string.Format(CultureInfo.InvariantCulture, "{0}#{1}", add.Item1.Type.FullName, add.Item2);
                var doc = new Document();
                doc.Add(new Field(Module.FIELD_CLASS, add.Item1.Type.FullName, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                doc.Add(new Field(Module.FIELD_CLASS_ID, clsId, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                doc.Add(new Field(Module.FIELD_ID, add.Item2.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                doc.Add(new Field(Module.FIELD_BODY, add.Item3, Field.Store.NO, Field.Index.ANALYZED));

                _indexWriter.UpdateDocument(new Term(Module.FIELD_CLASS_ID, clsId), doc);
            }

            foreach (var del in item.deleted)
            {
                var clsId = string.Format(CultureInfo.InvariantCulture, "{0}#{1}", del.Item1.Type.FullName, del.Item2);
                _indexWriter.DeleteDocuments(new Term(Module.FIELD_CLASS_ID, clsId));
            }

            _indexWriter.Commit();
        }
    }
}
