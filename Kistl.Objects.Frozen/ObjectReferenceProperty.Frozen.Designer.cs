
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// Metadefinition Object for ObjectReference Properties.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ObjectReferenceProperty")]
    public class ObjectReferenceProperty__Implementation__ : Kistl.App.Base.Property__Implementation__, ObjectReferenceProperty
    {


        /// <summary>
        /// Pointer zur Objektklasse
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.ObjectClass ReferenceObjectClass
        {
            get
            {
                return _ReferenceObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ReferenceObjectClass != value)
                {
                    NotifyPropertyChanging("ReferenceObjectClass");
                    _ReferenceObjectClass = value;
                    NotifyPropertyChanged("ReferenceObjectClass");;
                }
            }
        }
        private Kistl.App.Base.ObjectClass _ReferenceObjectClass;

        /// <summary>
        /// This Property is the right Part of the selected Relation.
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Relation RightOf
        {
            get
            {
                return _RightOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_RightOf != value)
                {
                    NotifyPropertyChanging("RightOf");
                    _RightOf = value;
                    NotifyPropertyChanged("RightOf");;
                }
            }
        }
        private Kistl.App.Base.Relation _RightOf;

        /// <summary>
        /// This Property is the left Part of the selected Relation.
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Relation LeftOf
        {
            get
            {
                return _LeftOf;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_LeftOf != value)
                {
                    NotifyPropertyChanging("LeftOf");
                    _LeftOf = value;
                    NotifyPropertyChanged("LeftOf");;
                }
            }
        }
        private Kistl.App.Base.Relation _LeftOf;

        /// <summary>
        /// Returns the String representation of this Property Meta Object.
        /// </summary>

		public override string GetPropertyTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetPropertyTypeString_ObjectReferenceProperty != null)
            {
                OnGetPropertyTypeString_ObjectReferenceProperty(this, e);
            };
            return e.Result;
        }
		public event GetPropertyTypeString_Handler<ObjectReferenceProperty> OnGetPropertyTypeString_ObjectReferenceProperty;



        /// <summary>
        /// 
        /// </summary>

		public override string GetGUIRepresentation() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetGUIRepresentation_ObjectReferenceProperty != null)
            {
                OnGetGUIRepresentation_ObjectReferenceProperty(this, e);
            };
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
            };
            return e.Result;
        }
		public event GetPropertyType_Handler<ObjectReferenceProperty> OnGetPropertyType_ObjectReferenceProperty;



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


        internal ObjectReferenceProperty__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}