
namespace Kistl.API.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.App.Test;

    public class BaseClientDataObjectMock__Implementation__ 
        : BaseClientDataObject, ANewObjectClass
    {
        public BaseClientDataObjectMock__Implementation__() : base(null) { }
    
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

        public override bool IsValid()
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
    }
}
