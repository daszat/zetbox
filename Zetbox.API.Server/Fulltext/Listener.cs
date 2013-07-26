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

    internal sealed class Listener : IZetboxContextEventListener
    {
        private readonly Service _service;
        private readonly Common.Fulltext.DataObjectFormatter _formatter;

        internal Listener(Service service, Common.Fulltext.DataObjectFormatter formatter)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (formatter == null) throw new ArgumentNullException("formatter");

            _service = service;
            _formatter = formatter;
        }

        public void Created(IReadOnlyZetboxContext ctx)
        {
        }

        public void Submitted(IReadOnlyZetboxContext ctx, IEnumerable<IDataObject> added, IEnumerable<IDataObject> modified, IEnumerable<Tuple<InterfaceType, int>> deleted)
        {
            _service.Enqueue(new IndexUpdate()
            {
                added = added.Select(obj => new Tuple<InterfaceType, int, string>(ctx.GetInterfaceType(obj), obj.ID, ExtractText(obj))).ToList(),
                modified = modified.Select(obj => new Tuple<InterfaceType, int, string>(ctx.GetInterfaceType(obj), obj.ID, ExtractText(obj))).ToList(),
                deleted = deleted.ToList()
            });
        }

        public void Disposed(IReadOnlyZetboxContext ctx)
        {
        }

        private string ExtractText(IDataObject obj)
        {
            // TODO: query ICustomFulltextFormat
            return _formatter.Format(obj);
        }
    }
}
