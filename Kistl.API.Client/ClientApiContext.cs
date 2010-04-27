
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ClientApplicationContext : ApplicationContext
    {
        public ClientApplicationContext() :
            base(HostType.Client)
        {
            BaseDataObjectType = typeof(BaseClientDataObject);
            BasePersistenceObjectType = typeof(BaseClientPersistenceObject);
            BaseCollectionEntryType = typeof(BaseClientCollectionEntry);
            BaseCompoundObjectType = typeof(BaseClientCompoundObject);
        }
    }
}
