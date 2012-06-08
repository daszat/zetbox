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
