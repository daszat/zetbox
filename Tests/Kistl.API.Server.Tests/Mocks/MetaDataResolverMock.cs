using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Mocks
{
    class MetaDataResolverMock : IMetaDataResolver
    {
        #region IMetaDataResolver Members

        public Kistl.App.Base.ObjectClass GetObjectClass(InterfaceType ifType)
        {
            return null;
        }

        #endregion
    }
}
