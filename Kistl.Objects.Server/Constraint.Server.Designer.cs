// <autogenerated/>


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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Constraint")]
    [System.Diagnostics.DebuggerDisplay("Constraint")]
    public class Constraint__Implementation__ : BaseServerDataObject_EntityFramework, Constraint
    {
    
		public Constraint__Implementation__()
		{
            {
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public override int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ID != value)
                {
					var __oldValue = _ID;
                    NotifyPropertyChanging("ID", __oldValue, value);
                    _ID = value;
                    NotifyPropertyChanged("ID", __oldValue, value);
                }
            }
        }
        private int _ID;

        /// <summary>
        /// The property to be constrained
        /// </summary>
    /*
    Relation: FK_Property_Constraint_ConstrainedProperty_62
    A: One Property as ConstrainedProperty
    B: ZeroOrMore Constraint as Constraints
    Preferred Storage: MergeIntoB
    */
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Property ConstrainedProperty
        {
            get
            {
                return ConstrainedProperty__Implementation__;
            }
            set
            {
                // TODO: NotifyPropertyChanged()
                // TODO: only accept EF objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                ConstrainedProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        private int? _fk_ConstrainedProperty;
        // EF sees only this property
        [EdmRelationshipNavigationProperty("Model", "FK_Property_Constraint_ConstrainedProperty_62", "ConstrainedProperty")]
        public Kistl.App.Base.Property__Implementation__ ConstrainedProperty__Implementation__
        {
            get
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_Constraint_ConstrainedProperty_62",
                        "ConstrainedProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                    if(r.Value != null) r.Value.AttachToContext(this.Context);
                }
                return r.Value;
            }
            set
            {
                EntityReference<Kistl.App.Base.Property__Implementation__> r
                    = ((IEntityWithRelationships)(this)).RelationshipManager.GetRelatedReference<Kistl.App.Base.Property__Implementation__>(
                        "Model.FK_Property_Constraint_ConstrainedProperty_62",
                        "ConstrainedProperty");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !r.IsLoaded)
                {
                    r.Load(); 
                }
                r.Value = (Kistl.App.Base.Property__Implementation__)value;
            }
        }
        
        

        /// <summary>
        /// The reason of this constraint
        /// </summary>
        // value type property
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        [EdmScalarProperty()]
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

		public virtual string GetErrorText(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.GetErrorText");
            }
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedObject, System.Object constrainedValue);
		public event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



        /// <summary>
        /// 
        /// </summary>

		public virtual bool IsValid(System.Object constrainedObject, System.Object constrainedValue) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedObject, constrainedValue);
            }
            else
            {
                throw new NotImplementedException("No handler registered on Constraint.IsValid");
            }
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedObject, System.Object constrainedValue);
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
			this._fk_ConstrainedProperty = otherImpl._fk_ConstrainedProperty;
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


		protected override string GetPropertyError(string propertyName) 
		{
			switch(propertyName)
			{
				case "ConstrainedProperty":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(171).Constraints
						.Where(c => !c.IsValid(this, this.ConstrainedProperty))
						.Select(c => c.GetErrorText(this, this.ConstrainedProperty))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				case "Reason":
				{
					var errors = Context.Find<Kistl.App.Base.Property>(167).Constraints
						.Where(c => !c.IsValid(this, this.Reason))
						.Select(c => c.GetErrorText(this, this.Reason))
						.ToArray();
					
					return String.Join("; ", errors);
				}
				default:
					return base.GetPropertyError(propertyName);
			}
		}

		public override void ReloadReferences()
		{
			// fix direct object references
			if (_fk_ConstrainedProperty.HasValue)
				ConstrainedProperty__Implementation__ = (Kistl.App.Base.Property__Implementation__)Context.Find<Kistl.App.Base.Property>(_fk_ConstrainedProperty.Value);
			else
				ConstrainedProperty__Implementation__ = null;
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(ConstrainedProperty != null ? ConstrainedProperty.ID : (int?)null, binStream);
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
            XmlStreamer.ToStream(this._Reason, xml, "Reason", "Kistl.App.Base");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._Reason, xml, "Reason", "Kistl.App.Base");
        }

#endregion

    }


}