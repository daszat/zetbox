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

namespace Zetbox.API.Client.Mocks.OneNLists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.DalProvider.Base;

    public class NSide
        : DataObjectBaseImpl, INSide
    {
        public NSide() : base(null) { }

        private int? _fk_OneSide;
        private IOneSide _oneSide;
        public IOneSide OneSide
        {
            get
            {
                return _oneSide;
            }
            set
            {
                _oneSide = value;
                _fk_OneSide = ((OneSide)value).ID;
            }
        }

        private int? _OneSide_pos;
        public int? OneSide_pos
        {
            get
            {
                return _OneSide_pos;
            }
            set
            {
                _OneSide_pos = value;
            }
        }

        public int? LastParentId { get { return _fk_OneSide; } }

        public string Description { get; set; }

        public override void UpdateParent(string propertyName, IDataObject parentObj)
        {
            if (propertyName == "OneSide")
            {
                _fk_OneSide = parentObj == null ? (int?)null : parentObj.ID;
            }
            else
            {
                base.UpdateParent(propertyName, parentObj);
            }
        }

        public override Type GetImplementedInterface()
        {
            throw new NotImplementedException();
        }

        protected override ObjectIsValidResult ObjectIsValid()
        {
            throw new NotImplementedException();
        }

        protected override string GetPropertyError(string prop)
        {
            throw new NotImplementedException();
        }

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
