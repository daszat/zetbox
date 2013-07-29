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
    using Zetbox.App.Base;
    using Zetbox.API.Common;

    internal sealed class Listener : IZetboxContextEventListener
    {
        private readonly IQueue _queue;
        private readonly Common.Fulltext.DataObjectFormatter _formatter;
        private readonly IMetaDataResolver _resolver;

        internal Listener(IQueue queue, Common.Fulltext.DataObjectFormatter formatter, IMetaDataResolver resolver)
        {
            if (queue == null) throw new ArgumentNullException("queue");
            if (formatter == null) throw new ArgumentNullException("formatter");
            if (resolver == null) throw new ArgumentNullException("resolver");

            _queue = queue;
            _formatter = formatter;
            _resolver = resolver;
        }

        public void Created(IReadOnlyZetboxContext ctx)
        {
        }

        public void Submitted(IReadOnlyZetboxContext ctx, IEnumerable<IDataObject> added, IEnumerable<IDataObject> modified, IEnumerable<Tuple<InterfaceType, int>> deleted)
        {
            _queue.Enqueue(new IndexUpdate()
            {
                added = added.Select(obj => new Tuple<InterfaceType, int, IndexUpdate.Text>(
                    ctx.GetInterfaceType(obj), 
                    obj.ID, 
                    Rebuilder.ExtractText(obj, _formatter, _resolver)
                )).ToList(),
                modified = modified.Select(obj => new Tuple<InterfaceType, int, IndexUpdate.Text>(
                    ctx.GetInterfaceType(obj), 
                    obj.ID,
                    Rebuilder.ExtractText(obj, _formatter, _resolver)
                )).ToList(),
                deleted = deleted.ToList()
            });
        }

        public void Disposed(IReadOnlyZetboxContext ctx)
        {
        }        
    }
}
