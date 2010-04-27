
namespace Kistl.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Lifetime;
    using System.Text;

    public class ServerApplicationContext : ApplicationContext
    {
        public ServerApplicationContext()
            : base(HostType.Server)
        {
            BaseDataObjectType = typeof(BaseServerDataObject);
            BasePersistenceObjectType = typeof(BaseServerPersistenceObject);
            BaseCollectionEntryType = typeof(BaseServerCollectionEntry);
            BaseCompoundObjectType = typeof(BaseServerCompoundObject);
        }
    }
}
