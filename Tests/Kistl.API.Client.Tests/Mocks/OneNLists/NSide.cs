using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Mocks.OneNLists
{
    public class NSide : BaseClientDataObject, INSide
    {
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

        public string Description { get; set; }

        public override void UpdateParent(string propertyName, int? id)
        {
            if (propertyName == "OneSide")
            {
                _fk_OneSide = id;
            }
            else
            {
                base.UpdateParent(propertyName, id);
            }
        }

        public override InterfaceType GetInterfaceType()
        {
            throw new NotImplementedException();
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        protected override string GetPropertyError(string prop)
        {
            throw new NotImplementedException();
        }
    }
}
