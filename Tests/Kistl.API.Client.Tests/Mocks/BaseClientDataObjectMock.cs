using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Mocks
{
    /// <summary>
    /// Empty interface for BaseClientDataObjectMock__Implementation__
    /// </summary>
    public interface BaseClientDataObjectMock
        : IDataObject
    {
    }

    public class BaseClientDataObjectMock__Implementation__ 
        : BaseClientDataObject, BaseClientDataObjectMock
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(BaseClientDataObjectMock));
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
