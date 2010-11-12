
namespace Kistl.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Test;

    public class BaseClientDataObjectMockImpl
        : BaseClientDataObject, ANewObjectClass, IClientObject
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

        #region IClientObject Members

        void IClientObject.SetUnmodified() { base.SetUnmodified(); }
        void IClientObject.SetDeleted() { base.SetDeleted(); }

        BasePersistenceObject IClientObject.UnderlyingObject
        {
            get { return this; }
        }

        #endregion
    }
}
