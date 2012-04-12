
namespace Kistl.API.Client.Mocks.OneNLists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.DalProvider.Base;

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
