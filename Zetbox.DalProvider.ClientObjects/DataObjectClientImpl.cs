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

namespace Zetbox.DalProvider.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.DalProvider.Base;
    using System.IO;

    public abstract class DataObjectClientImpl
       : DataObjectBaseImpl, IClientObject
    {
        protected DataObjectClientImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        #region IClientObject Members

        void IClientObject.SetUnmodified() { base.SetUnmodified(); }
        void IClientObject.SetDeleted() { base.SetDeleted(); }

        BasePersistenceObject IClientObject.UnderlyingObject
        {
            get { return this; }
        }

        void IClientObject.MakeAccessDeniedProxy()
        {
            _currentAccessRights = AccessRights.None;
            SetUnmodified();
        }

        #endregion

        /// <summary>
        /// Reflects the current access rights by the current Identity. 
        /// Base implementations returnes always Full
        /// </summary>
        private Zetbox.API.AccessRights _currentAccessRights = Zetbox.API.AccessRights.Full;
        public override Zetbox.API.AccessRights CurrentAccessRights
        {
            get
            {
                return _currentAccessRights;
            }
        }

        protected override void AuditPropertyChange(string property, object oldValue, object newValue)
        {
            // client objects do not audit changes
        }

        protected override void ApplyRightsFromStream(AccessRights rights)
        {
            base.ApplyRightsFromStream(rights);
            _currentAccessRights = rights;
        }
    }
}
