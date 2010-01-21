
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class ClientApiContext : ApplicationContext
    {
        public static new ClientApiContext Current { get; private set; }

        public ClientApiContext(Configuration.KistlConfig config) :
            base(HostType.Client, config)
        {
            ClientApiContext.Current = this;

            BaseDataObjectType = typeof(BaseClientDataObject);
            BasePersistenceObjectType = typeof(BaseClientPersistenceObject);
            BaseCollectionEntryType = typeof(BaseClientCollectionEntry);
            BaseCompoundObjectType = typeof(BaseClientCompoundObject);
        }
    }
}
