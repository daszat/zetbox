using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public class ClientApiContext : ApplicationContext
    {

        public static new ClientApiContext Current { get; private set; }

        public ClientApiContext(Configuration.KistlConfig config) :
            base(HostType.Client, config)
        {
            ClientApiContext.Current = this;

            BaseDataObjectType = typeof(BaseClientDataObject);
            BasePersistenceObjectType = typeof(BaseClientPersistenceObject);
            BaseCollectionEntryType = typeof(BaseClientCollectionEntry);
            BaseStructObjectType = typeof(BaseClientStructObject);

        }

    }
}
