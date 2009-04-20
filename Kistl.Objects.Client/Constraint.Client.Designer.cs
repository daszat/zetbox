
namespace Kistl.App.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Kistl.API;

    using Kistl.API.Client;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Constraint")]
    public class Constraint__Implementation__ : BaseClientDataObject, Constraint
    {
    
		public Constraint__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// The property to be constrained
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property ConstrainedProperty
        {
            get
            {
                if (fk_ConstrainedProperty.HasValue)
                    return Context.Find<Kistl.App.Base.Property>(fk_ConstrainedProperty.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                
                // shortcut noops
                if (value == null && _fk_ConstrainedProperty == null)
					return;
                else if (value != null && value.ID == _fk_ConstrainedProperty)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = ConstrainedProperty;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("ConstrainedProperty", oldValue, value);
                
				// next, set the local reference
                _fk_ConstrainedProperty = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Constraints as OneNRelationCollection<Kistl.App.Base.Constraint>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Constraints as OneNRelationCollection<Kistl.App.Base.Constraint>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("ConstrainedProperty", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ConstrainedProperty
        {
            get
            {
                return _fk_ConstrainedProperty;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ConstrainedProperty != value)
                {
					var __oldValue = _fk_ConstrainedProperty;
                    NotifyPropertyChanging("ConstrainedProperty", __oldValue, value);
                    _fk_ConstrainedProperty = value;
                    NotifyPropertyChanged("ConstrainedProperty", __oldValue, value);
                }
            }
        }
        private int? _fk_ConstrainedProperty;

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        public virtual string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Reason != value)
                {
					var __oldValue = _Reason;
                    NotifyPropertyChanging("Reason", __oldValue, value);
                    _Reason = value;
                    NotifyPropertyChanged("Reason", __oldValue, value);
                }
            }
        }
        private string _Reason;

        /// <summary>
        /// 
        /// </summary>

		public virtual string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedValue, constrainedObject);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.GetErrorText");
            }
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedValue, System.Object constrainedObject);
		public event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



        /// <summary>
        /// 
        /// </summary>

		public virtual bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedValue, constrainedObj);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.IsValid");
            }
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedValue, System.Object constrainedObj);
		public event IsValid_Handler<Constraint> OnIsValid_Constraint;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Constraint));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Constraint)obj;
			var otherImpl = (Constraint__Implementation__)obj;
			var me = (Constraint)this;

			me.Reason = other.Reason;
			this.fk_ConstrainedProperty = otherImpl.fk_ConstrainedProperty;
		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Constraint != null)
            {
                OnToString_Constraint(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Constraint> OnToString_Constraint;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Constraint != null) OnPreSave_Constraint(this);
        }
        public event ObjectEventHandler<Constraint> OnPreSave_Constraint;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Constraint != null) OnPostSave_Constraint(this);
        }
        public event ObjectEventHandler<Constraint> OnPostSave_Constraint;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "ConstrainedProperty":
                    fk_ConstrainedProperty = id;
                    break;
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_ConstrainedProperty, binStream);
            BinarySerializer.ToStream(this._Reason, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_ConstrainedProperty, binStream);
            BinarySerializer.FromStream(out this._Reason, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._fk_ConstrainedProperty, xml, "ConstrainedProperty", "http://dasz.at/Kistl");
            XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._fk_ConstrainedProperty, xml, "ConstrainedProperty", "http://dasz.at/Kistl");
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
        }

#endregion

    }


}