// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// The B-Side class for the N_to_M_relations Tests
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("N_to_M_relations_B")]
    public class N_to_M_relations_BMemoryImpl : Zetbox.DalProvider.Memory.DataObjectMemoryImpl, N_to_M_relations_B
    {
        private static readonly Guid _objectClassID = new Guid("c2af3719-d63a-4a94-9cc8-b4f94bb253ff");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public N_to_M_relations_BMemoryImpl()
            : base(null)
        {
        }

        public N_to_M_relations_BMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // collection entry list property
   		// Zetbox.Generator.Templates.Properties.CollectionEntryListProperty
		public ICollection<Zetbox.App.Test.N_to_M_relations_A> ASide
		{
			get
			{
				if (_ASide == null)
				{
					Context.FetchRelation<Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl>(new Guid("3555da6e-0e9b-4f7c-903e-a51f3cce7cd9"), RelationEndRole.B, this);
					_ASide 
						= new ObservableASideCollectionWrapper<Zetbox.App.Test.N_to_M_relations_A, Zetbox.App.Test.N_to_M_relations_B, Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl>>(
							this, 
							new RelationshipFilterBSideCollection<Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl>(this.Context, this));
				}
				return (ICollection<Zetbox.App.Test.N_to_M_relations_A>)_ASide;
			}
		}

		private ObservableASideCollectionWrapper<Zetbox.App.Test.N_to_M_relations_A, Zetbox.App.Test.N_to_M_relations_B, Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl, ICollection<Zetbox.App.Test.N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryMemoryImpl>> _ASide;

        public static event PropertyIsValidHandler<Zetbox.App.Test.N_to_M_relations_B> OnASide_IsValid;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        // BEGIN Zetbox.Generator.Templates.Properties.NotifyingDataProperty
        public string Name
        {
            get
            {
                // create local variable to create single point of return
                // for the benefit of down-stream templates
                var __result = _Name;
                if (OnName_Getter != null)
                {
                    var __e = new PropertyGetterEventArgs<string>(__result);
                    OnName_Getter(this, __e);
                    __result = _Name = __e.Result;
                }
                return __result;
            }
            set
            {
                if (this.IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    var __oldValue = _Name;
                    var __newValue = value;
                    if (OnName_PreSetter != null && IsAttached)
                    {
                        var __e = new PropertyPreSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PreSetter(this, __e);
                        __newValue = __e.Result;
                    }
                    NotifyPropertyChanging("Name", __oldValue, __newValue);
                    _Name = __newValue;
                    NotifyPropertyChanged("Name", __oldValue, __newValue);
                    UpdateChangedInfo = true;

                    if (OnName_PostSetter != null && IsAttached)
                    {
                        var __e = new PropertyPostSetterEventArgs<string>(__oldValue, __newValue);
                        OnName_PostSetter(this, __e);
                    }
                }
				else 
				{
					SetInitializedProperty("Name");
				}
            }
        }
        private string _Name;
        // END Zetbox.Generator.Templates.Properties.NotifyingDataProperty
		public static event PropertyGetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_Getter;
		public static event PropertyPreSetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_PreSetter;
		public static event PropertyPostSetterHandler<Zetbox.App.Test.N_to_M_relations_B, string> OnName_PostSetter;

        public static event PropertyIsValidHandler<Zetbox.App.Test.N_to_M_relations_B> OnName_IsValid;

        public override Type GetImplementedInterface()
        {
            return typeof(N_to_M_relations_B);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (N_to_M_relations_B)obj;
            var otherImpl = (N_to_M_relations_BMemoryImpl)obj;
            var me = (N_to_M_relations_B)this;

            me.Name = other.Name;
        }

        public override void AttachToContext(IZetboxContext ctx)
        {
            base.AttachToContext(ctx);
        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        protected override void OnPropertyChanged(string property, object oldValue, object newValue)
        {
            base.OnPropertyChanged(property, oldValue, newValue);

            // Do not audit calculated properties
            switch (property)
            {
                case "Name":
                    AuditPropertyChange(property, oldValue, newValue);
                    break;
            }
        }

        protected override bool ShouldSetModified(string property)
        {
            switch (property)
            {
                case "ASide":
                    return false;
                default:
                    return base.ShouldSetModified(property);
            }
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        private static readonly object _propertiesLock = new object();
        private static System.ComponentModel.PropertyDescriptor[] _properties;

        private void _InitializePropertyDescriptors(Func<IFrozenContext> lazyCtx)
        {
            if (_properties != null) return;
            lock (_propertiesLock)
            {
                // recheck for a lost race after aquiring the lock
                if (_properties != null) return;

                _properties = new System.ComponentModel.PropertyDescriptor[] {
                    // property.IsAssociation() && !property.IsObjectReferencePropertySingle()
                    new PropertyDescriptorMemoryImpl<N_to_M_relations_B, ICollection<Zetbox.App.Test.N_to_M_relations_A>>(
                        lazyCtx,
                        new Guid("a741d6bd-8a87-44c2-83b3-69225661f958"),
                        "ASide",
                        null,
                        obj => obj.ASide,
                        null, // lists are read-only properties
                        obj => OnASide_IsValid), 
                    // else
                    new PropertyDescriptorMemoryImpl<N_to_M_relations_B, string>(
                        lazyCtx,
                        new Guid("80ec9efe-c73b-4554-a145-064a32f225b8"),
                        "Name",
                        null,
                        obj => obj.Name,
                        (obj, val) => obj.Name = val,
						obj => OnName_IsValid), 
                    // position columns
                };
            }
        }

        protected override void CollectProperties(Func<IFrozenContext> lazyCtx, List<System.ComponentModel.PropertyDescriptor> props)
        {
            base.CollectProperties(lazyCtx, props);
            _InitializePropertyDescriptors(lazyCtx);
            props.AddRange(_properties);
        }
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_N_to_M_relations_B")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_N_to_M_relations_B != null)
            {
                OnToString_N_to_M_relations_B(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<N_to_M_relations_B> OnToString_N_to_M_relations_B;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_N_to_M_relations_B")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.IsValid = b.IsValid;
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_N_to_M_relations_B != null)
            {
                OnObjectIsValid_N_to_M_relations_B(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<N_to_M_relations_B> OnObjectIsValid_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyPreSave_N_to_M_relations_B")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_N_to_M_relations_B != null) OnNotifyPreSave_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyPreSave_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyPostSave_N_to_M_relations_B")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_N_to_M_relations_B != null) OnNotifyPostSave_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyPostSave_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyCreated_N_to_M_relations_B")]
        public override void NotifyCreated()
        {
            SetNotInitializedProperty("Name");
            base.NotifyCreated();
            if (OnNotifyCreated_N_to_M_relations_B != null) OnNotifyCreated_N_to_M_relations_B(this);
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyCreated_N_to_M_relations_B;

        [EventBasedMethod("OnNotifyDeleting_N_to_M_relations_B")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_N_to_M_relations_B != null) OnNotifyDeleting_N_to_M_relations_B(this);
            ASide.Clear();
        }
        public static event ObjectEventHandler<N_to_M_relations_B> OnNotifyDeleting_N_to_M_relations_B;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
            binStream.Write(this._Name);
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            this._Name = binStream.ReadString();
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        #endregion

    }
}