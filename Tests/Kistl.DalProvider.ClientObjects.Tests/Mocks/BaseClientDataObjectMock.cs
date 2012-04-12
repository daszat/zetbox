
namespace Kistl.DalProvider.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Test;
    using Kistl.DalProvider.Base;

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
