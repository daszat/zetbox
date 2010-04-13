using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.DalProvider.Memory
{
    public abstract class BaseMemoryPersistenceObject : BasePersistenceObject
    {
        protected BaseMemoryPersistenceObject()
        {
            _objectState = DataObjectState.New;
        }

        public override int ID
        {
            get;
            set;
        }

        public override bool IsAttached
        {
            get { return Context != null; }
        }

        private DataObjectState _objectState;
        public override DataObjectState ObjectState
        {
            get { return _objectState; }
        }

        protected override void SetModified()
        {
            _objectState = DataObjectState.Modified;
        }
    }
}
