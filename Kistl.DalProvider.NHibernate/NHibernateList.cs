
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NHibernateList<TA, TB, TCollectionEntry, TItem>
        : IList<TItem>, ICollection<TItem>, ICollection, IEnumerable
    {
        public NHibernateList(object muh, object blah)
        {
        }

        public void Add(TItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TItem item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public int IndexOf(TItem item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TItem this[int index]
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
    }

    public class NHibernateAList<TA, TB, TCollectionEntry>
        : NHibernateList<TA, TB, TCollectionEntry, TB>
    {
        public NHibernateAList(object muh, object blah)
            : base(muh, blah)
        {
        }
    }
    public class NHibernateBList<TA, TB, TCollectionEntry>
        : NHibernateList<TA, TB, TCollectionEntry, TA>
    {
        public NHibernateBList(object muh, object blah)
            : base(muh, blah)
        {
        }
    }
}
