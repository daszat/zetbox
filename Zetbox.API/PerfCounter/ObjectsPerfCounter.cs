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

namespace Zetbox.API.PerfCounter
{
    public sealed class ObjectMemoryCounters
    {
        public readonly string Name;
        public ObjectMemoryCounters(string name)
        {
            this.Name = name;
        }

        public readonly MethodMemoryCounters FetchRelation = new MethodMemoryCounters("FetchRelation");
        public readonly MethodMemoryCounters GetList = new MethodMemoryCounters("GetList");
        public readonly MethodMemoryCounters GetListOf = new MethodMemoryCounters("GetListOf");
        public readonly MethodMemoryCounters Queries = new MethodMemoryCounters("Queries");

        public void Reset()
        {
            this.FetchRelation.Reset();
            this.GetList.Reset();
            this.GetListOf.Reset();
            this.Queries.Reset();
        }

        public void FormatTo(Dictionary<string, string> values)
        {
            FetchRelation.FormatTo(values);
            GetList.FormatTo(values);
            GetListOf.FormatTo(values);
            Queries.FormatTo(values);
        }
    }
}
