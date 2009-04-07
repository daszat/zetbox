//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections;
//using Kistl.API.Utils;

//namespace Kistl.API.Client.LinqToKistl
//{
//    /// <summary>
//    /// An immutable key to lookup cache results of FetchRelation
//    /// </summary>
//    internal struct RelationContentKey
//    {
//        internal RelationContentKey(int id, RelationEndRole role, int containerId)
//            : this()
//        {
//            this.ID = id;
//            this.Role = role;
//            this.ContainerId = containerId;
//        }

//        internal int ID { get; private set; }
//        internal RelationEndRole Role { get; private set; }
//        internal int ContainerId { get; private set; }

//        public override int GetHashCode()
//        {
//            return ID.GetHashCode() ^ Role.GetHashCode() ^ ContainerId.GetHashCode();
//        }

//        public override bool Equals(object obj)
//        {
//            if (obj is RelationContentKey)
//            {
//                var other = (RelationContentKey)obj;
//                return this.ID == other.ID
//                    && this.Role == other.Role
//                    && this.ContainerId == other.ContainerId;
//            }
//            else
//            {
//                return false;
//            }
//        }
//    }

//    internal class RelationshipManager
//    {
//        private Dictionary<RelationContentKey, object> _fetchRelationCache = new Dictionary<RelationContentKey, object>();
//        public Dictionary<RelationContentKey, object> FetchRelationCache
//        {
//            get
//            {
//                return _fetchRelationCache;
//            }
//        }

//        private KistlContextImpl _ctx;
//        public RelationshipManager(KistlContextImpl ctx)
//        {
//            _ctx = ctx;
//        }

//        public void ManageRelationsip(IRelationCollectionEntry obj)
//        {
//            obj.PropertyChangingWithValue += new PropertyChangeWithValueEventHandler(obj_PropertyChangingWithValue);
//            obj.PropertyChangedWithValue += new PropertyChangeWithValueEventHandler(obj_PropertyChangedWithValue);
//        }

//        void obj_PropertyChangedWithValue(object sender, PropertyChangeWithValueEventArgs args)
//        {
//            IRelationCollectionEntry e = (IRelationCollectionEntry)sender;
//            if (!(args.NewValue is IDataObject)) return;

//            if (args.PropertyName == "A")
//            {
//                IDataObject a = (IDataObject)args.NewValue;
//                IDataObject b = e.GetPropertyValue<object>("B") as IDataObject;
//                if (b == null) return; // initialization or value collection TODO: Seperate Relation from CollectionEntry

//                var key = new RelationContentKey(e.RelationID, RelationEndRole.A, a.ID);
//                IList<IRelationCollectionEntry> aList = null;
//                if (_fetchRelationCache.ContainsKey(key))
//                {
//                    aList = MagicCollectionFactory.WrapAsList<IRelationCollectionEntry>(_fetchRelationCache[key]);
//                }
//                else
//                {
//                    aList = _ctx.FetchRelation(new ImplementationType(e.GetType()), e.RelationID, RelationEndRole.A, a);
//                }

//                if (!aList.Contains(e))
//                {
//                    aList.Add(e);
//                }
//            }
//            else if (args.PropertyName == "B")
//            {
//                IDataObject a = e.GetPropertyValue<IDataObject>("A");
//                IDataObject b = (IDataObject)args.NewValue;
//                if (a == null) return; // initialization

//                var key = new RelationContentKey(e.RelationID, RelationEndRole.B, b.ID);
//                IList<IRelationCollectionEntry> bList = null;
//                if (_fetchRelationCache.ContainsKey(key))
//                {
//                    bList = MagicCollectionFactory.WrapAsList<IRelationCollectionEntry>(_fetchRelationCache[key]);
//                }
//                else
//                {
//                    bList = _ctx.FetchRelation(new ImplementationType(e.GetType()), e.RelationID, RelationEndRole.B, b);
//                }

//                if (!bList.Contains(e))
//                {
//                    bList.Add(e);
//                }
//            }

//        }

//        void obj_PropertyChangingWithValue(object sender, PropertyChangeWithValueEventArgs args)
//        {
//            // System.Diagnostics.Debug.WriteLine(string.Format("PropertyChanging: {0}.{1} = {2} -> {3}", sender, args.PropertyName, args.OldValue, args.NewValue));
//        }
//    }
//}
