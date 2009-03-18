using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client.Mocks
{
    /// <summary>
    /// Empty interface for BaseClientDataObjectMock
    /// </summary>
    public interface BaseClientDataObjectMockInterface
        : IDataObject
    {
    }

    public class BaseClientDataObjectMock 
        : BaseClientDataObject, BaseClientDataObjectMockInterface
    {
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(BaseClientDataObjectMockInterface));
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
    }
}
