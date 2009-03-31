using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public class TestCollectionEntry : ICollectionEntry
    {
        private int _ID  = -1;
        public int ID { get { return _ID; } set { _ID = value; } }
        public bool IsReadonly { get; private set; }

        private string _TestName;
        public string TestName { get { return _TestName; } set { _TestName = value; } }

        public override bool Equals(object obj)
        {
            if (obj is TestCollectionEntry)
            {
                TestCollectionEntry x = (TestCollectionEntry)obj;
                return
                       this.ID == x.ID
                    && this.TestName == x.TestName;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public void CopyTo(ICollectionEntry obj)
        {
            ((TestCollectionEntry)obj).ID = this.ID;
            ((TestCollectionEntry)obj).TestName = this.TestName;
        }

        public void FromStream(System.IO.BinaryReader sr)
        {
            BinarySerializer.FromStream(out _ID, sr);
            BinarySerializer.FromStream(out _TestName, sr);
        }

        public void ToStream(System.IO.BinaryWriter sw)
        {
            BinarySerializer.ToStream(ID, sw);
            BinarySerializer.ToStream(TestName, sw);
        }

        public void ReloadReferences()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public void NotifyPropertyChanged(string property)
        {
            throw new NotImplementedException();
        }

        public void NotifyPropertyChanging(string property)
        {
            throw new NotImplementedException();
        }

        public void AttachToContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        public IKistlContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public void DetachFromContext(IKistlContext ctx)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Applies changes from another IPersistenceObject of the same interface type.
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ApplyChangesFrom(IPersistenceObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (obj.GetType().ToInterfaceType() != this.GetType().ToInterfaceType())
                throw new ArgumentOutOfRangeException("obj");

            this.ID = obj.ID;
        }


        public DataObjectState ObjectState
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

        #region IPersistenceObject Members


        public bool IsAttached
        {
            get { throw new NotImplementedException(); }
        }

        public InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(ICollectionEntry));
        }

        #endregion
    }
}
