using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Common;

namespace Zetbox.API.Server.Mocks
{
    class MetaDataResolverMock : IMetaDataResolver
    {
        #region IMetaDataResolver Members

        public Zetbox.App.Base.ObjectClass GetObjectClass(InterfaceType ifType)
        {
            return new ObjectClassMock() { Name = ifType.Type.Name, ExportGuid = Guid.NewGuid(), TableName = ifType.Type.Name, IsFrozenObject = true };
        }

        #endregion
    }
}
