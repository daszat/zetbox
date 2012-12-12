// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Server.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Common;

    class MetaDataResolverMock : IMetaDataResolver
    {
        #region IMetaDataResolver Members

        public Zetbox.App.Base.ObjectClass GetObjectClass(InterfaceType ifType)
        {
            return new ObjectClassMock() { Name = ifType.Type.Name, ExportGuid = Guid.NewGuid(), TableName = ifType.Type.Name, IsFrozenObject = true };
        }

        public App.Base.CompoundObject GetCompoundObject(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }

        public App.Base.DataType GetDataType(InterfaceType ifType)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
