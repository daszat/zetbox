
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Lifetime;
    using System.Text;

    public abstract class ServerApiContext : ApplicationContext
    {
        public static new ServerApiContext Current { get; private set; }

        public ServerApiContext(Configuration.KistlConfig config)
            : base(HostType.Server, config)
        {
            ServerApiContext.Current = this;

            BaseDataObjectType = typeof(BaseServerDataObject);
            BasePersistenceObjectType = typeof(BaseServerPersistenceObject);
            BaseCollectionEntryType = typeof(BaseServerCollectionEntry);
            BaseStructObjectType = typeof(BaseServerStructObject);
        }
    }
}
