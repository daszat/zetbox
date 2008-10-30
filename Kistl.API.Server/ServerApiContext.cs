using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server
{
    public class ServerApiContext : ApplicationContext
    {
        public static ServerApiContext Current { get; private set; }

        public ServerApiContext(string configFile) :
            base(HostType.Server, configFile)
        {
            ServerApiContext.Current = this;

            BaseDataObjectType = typeof(BaseServerDataObject);
            BasePersistenceObjectType = typeof(BaseServerPersistenceObject);
            BaseCollectionEntryType = typeof(BaseServerCollectionEntry);
            BaseStructObjectType = typeof(BaseServerStructObject);
        }

    }
}
