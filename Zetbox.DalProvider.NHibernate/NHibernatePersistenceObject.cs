// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Server;

    public abstract class NHibernatePersistenceObject
       : BaseServerPersistenceObject
    {
        protected NHibernatePersistenceObject(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        public override bool IsAttached
        {
            get { return Context != null; }
        }

        internal void Delete()
        {
            SetDeleted();
        }

        private int _ID;
        public override int ID
        {
            get
            {
                var result = _ID;
                if (NHibernateProxy.ID != 0)
                    result = _ID = NHibernateProxy.ID;

                return result;
            }
            set
            {
                if (this.IsReadonly)
                    throw new ReadOnlyObjectException();
                if (_ID != value)
                {
                    var oldValue = _ID;
                    var newValue = value;

                    NotifyPropertyChanging("ID", oldValue, newValue);

                    _ID = newValue;
                    if (!this.ObjectState.In(DataObjectState.Detached, DataObjectState.New))
                        NHibernateProxy.ID = newValue;

                    NotifyPropertyChanged("ID", oldValue, newValue);
                }
            }
        }

        protected NHibernateContext OurContext { get { return (NHibernateContext)Context; } }
        public abstract IProxyObject NHibernateProxy { get; }

        public virtual void SaveOrUpdateTo(global::NHibernate.ISession session)
        {
            if (session == null) { throw new ArgumentNullException("session"); }

            switch (this.ObjectState)
            {
                case DataObjectState.New:
                    Zetbox.API.Utils.Logging.Log.DebugFormat("Save: {0}#{1}", this.GetType(), this.ID);
                    session.Save(this.NHibernateProxy);
                    break;
                case DataObjectState.Modified:
                case DataObjectState.Unmodified:
                    Zetbox.API.Utils.Logging.Log.DebugFormat("SaveOrUpdate: {0}#{1}", this.GetType(), this.ID);
                    // according to NH Documentation this is not needed
                    // session.SaveOrUpdate(this.NHibernateProxy);
                    break;
                case DataObjectState.Deleted:
                    throw new InvalidOperationException("object should be deleted, not saved");
                case DataObjectState.NotDeserialized:
                    throw new InvalidOperationException("object not deserialized");
                default:
                    throw new NotImplementedException(String.Format("unknown DataObjectState encountered: '{0}'", this.ObjectState));
            }
        }

        public readonly List<NHibernatePersistenceObject> ChildrenToDelete = new List<NHibernatePersistenceObject>();
        public readonly List<NHibernatePersistenceObject> ParentsToDelete = new List<NHibernatePersistenceObject>();

        public virtual List<NHibernatePersistenceObject> GetParentsToDelete() { return ParentsToDelete.Where(c => c.ObjectState == DataObjectState.Deleted).Distinct().ToList(); }
        public virtual List<NHibernatePersistenceObject> GetChildrenToDelete() { return ChildrenToDelete.Where(c => c.ObjectState == DataObjectState.Deleted).Distinct().ToList(); }
    }
}
