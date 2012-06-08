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

namespace Zetbox.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Base;

    public class BaseClientDataObjectMockImpl
        : DataObjectBaseImpl, ANewObjectClass, IClientObject
    {
        public BaseClientDataObjectMockImpl(Func<IFrozenContext> lazyCtx) : base(lazyCtx) { }

        public override Type GetImplementedInterface()
        {
            return typeof(ANewObjectClass);
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        protected override ObjectIsValidResult ObjectIsValid()
        {
            throw new NotImplementedException();
        }

        protected override string GetPropertyError(string prop)
        {
            throw new NotImplementedException();
        }

        #region ANewObjectClass Members

        public string TestString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IClientObject Members

        void IClientObject.SetUnmodified() { base.SetUnmodified(); }
        void IClientObject.SetDeleted() { base.SetDeleted(); }

        BasePersistenceObject IClientObject.UnderlyingObject
        {
            get { return this; }
        }
        void IClientObject.MakeAccessDeniedProxy()
        {
        }
        #endregion

        public override void NotifyPreSave()
        {
            throw new NotImplementedException();
        }

        public override void NotifyPostSave()
        {
            throw new NotImplementedException();
        }

        public override void NotifyCreated()
        {
            throw new NotImplementedException();
        }

        public override void NotifyDeleting()
        {
            throw new NotImplementedException();
        }

        public override Guid ObjectClassID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
