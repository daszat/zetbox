using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    public class ObjectsPerfCounter
    {
        public ObjectsPerfCounter(string clsName)
        {
            ClassName = clsName;
        }

        public string ClassName;
        public long QueriesTotal = 0;

        public long GetListTotal = 0;
        public long GetListObjectsTotal = 0;

        public long GetListOfTotal = 0;
        public long GetListOfObjectsTotal = 0;

        public long FetchRelationTotal = 0;
        public long FetchRelationObjectsTotal = 0;
    }
}
