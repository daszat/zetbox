
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
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceProperty")]
    public class ObjectReferenceProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, ObjectReferenceProperty
    {


        /// <summary>
        /// This Property is the left Part of the selected Relation.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation LeftOf
        {
            get
            {
                if (fk_LeftOf.HasValue)
                    return Context.Find<Kistl.App.Base.Relation>(fk_LeftOf.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = LeftOf;
                if (value != null && value.ID != fk_LeftOf)
                {
                    fk_LeftOf = value.ID;
                    value.LeftPart = this;
                }
                else
                {
                    fk_LeftOf = null;
                    value.LeftPart = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_LeftOf
        {
            get
            {
                return _fk_LeftOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_LeftOf != value)
                {
                    NotifyPropertyChanging("LeftOf");
                    _fk_LeftOf = value;
                    NotifyPropertyChanging("LeftOf");
                }
            }
        }
        private int? _fk_LeftOf;

        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.ObjectClass ReferenceObjectClass
        {
            get
            {
                if (fk_ReferenceObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.ObjectClass>(fk_ReferenceObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                fk_ReferenceObjectClass = value == null ? (int?)null : value.ID;
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ReferenceObjectClass
        {
            get
            {
                return _fk_ReferenceObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ReferenceObjectClass != value)
                {
                    NotifyPropertyChanging("ReferenceObjectClass");
                    _fk_ReferenceObjectClass = value;
                    NotifyPropertyChanging("ReferenceObjectClass");
                }
            }
        }
        private int? _fk_ReferenceObjectClass;

        /// <summary>
        /// This Property is the right Part of the selected Relation.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Relation RightOf
        {
            get
            {
                if (fk_RightOf.HasValue)
                    return Context.Find<Kistl.App.Base.Relation>(fk_RightOf.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = RightOf;
                if (value != null && value.ID != fk_RightOf)
                {
                    fk_RightOf = value.ID;
                    value.RightPart = this;
                }
                else
                {
                    fk_RightOf = null;
                    value.RightPart = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_RightOf
        {
            get
            {
                return _fk_RightOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_RightOf != value)
                {
                    NotifyPropertyChanging("RightOf");
                    _fk_RightOf = value;
                    NotifyPropertyChanging("RightOf");
                }
            }
        }
        private int? _fk_RightOf;

        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_ObjectReferenceProperty != null)
            {
                OnGetGUIRepresentation_ObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetGUIRepresentation();
            }
            return e.Result;
        }
		public event GetGUIRepresentation_Handler<ObjectReferenceProperty> OnGetGUIRepresentation_ObjectReferenceProperty;



        /// <summary>
        /// Returns the resulting Type of this Property Meta Object.
        /// </summary>

		public override System.Type GetPropertyType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetPropertyType_ObjectReferenceProperty != null)
            {
                OnGetPropertyType_ObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyType();
            }
            return e.Result;
        }
		public event GetPropertyType_Handler<ObjectReferenceProperty> OnGetPropertyType_ObjectReferenceProperty;



        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferenceProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferenceProperty(this, e);
            }
            else
            {
                e.Result = base.GetPropertyTypeString();
            }
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<ObjectReferenceProperty> OnGetPropertyTypeString_ObjectReferenceProperty;



		public override Type GetInterfaceType()
		{
			return typeof(ObjectReferenceProperty);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ObjectReferenceProperty != null)
            {
                OnToString_ObjectReferenceProperty(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<ObjectReferenceProperty> OnToString_ObjectReferenceProperty;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_ObjectReferenceProperty != null) OnPreSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPreSave_ObjectReferenceProperty;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_ObjectReferenceProperty != null) OnPostSave_ObjectReferenceProperty(this);
        }
        public event ObjectEventHandler<ObjectReferenceProperty> OnPostSave_ObjectReferenceProperty;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_LeftOf, binStream);
            BinarySerializer.ToStream(this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.ToStream(this._fk_RightOf, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_LeftOf, binStream);
            BinarySerializer.FromStream(out this._fk_ReferenceObjectClass, binStream);
            BinarySerializer.FromStream(out this._fk_RightOf, binStream);
        }

#endregion

    }


}