
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


        /// <summary>
        /// The property to be constrained
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.BaseProperty ConstrainedProperty
        {
            get
            {
                if (fk_ConstrainedProperty.HasValue)
                    return Context.Find<Kistl.App.Base.BaseProperty>(fk_ConstrainedProperty.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = ConstrainedProperty;
                if (value != null && value.ID != fk_ConstrainedProperty)
                {
                    oldValue.Constraints.Remove(this);
                    fk_ConstrainedProperty = value.ID;
                    value.Constraints.Add(this);
                }
                else
                {
                    oldValue.Constraints.Remove(this);
                    fk_ConstrainedProperty = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_ConstrainedProperty
        {
            get
            {
                return _fk_ConstrainedProperty;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_ConstrainedProperty != value)
                {
                    NotifyPropertyChanging("ConstrainedProperty");
                    _fk_ConstrainedProperty = value;
                    NotifyPropertyChanging("ConstrainedProperty");
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
                    NotifyPropertyChanging("Reason");
                    _Reason = value;
                    NotifyPropertyChanged("Reason");;
                }
            }
        }
        private string _Reason;

        /// <summary>
        /// 
        /// </summary>

		public virtual bool IsValid(System.Object constrainedValue, System.Object constrainedObj) 
        {
            var e = new MethodReturnEventArgs<bool>();
            if (OnIsValid_Constraint != null)
            {
                OnIsValid_Constraint(this, e, constrainedValue, constrainedObj);
            };
            return e.Result;
        }
		public delegate void IsValid_Handler<T>(T obj, MethodReturnEventArgs<bool> ret, System.Object constrainedValue, System.Object constrainedObj);
		public event IsValid_Handler<Constraint> OnIsValid_Constraint;



        /// <summary>
        /// 
        /// </summary>

		public virtual string GetErrorText(System.Object constrainedValue, System.Object constrainedObject) 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetErrorText_Constraint != null)
            {
                OnGetErrorText_Constraint(this, e, constrainedValue, constrainedObject);
            };
            return e.Result;
        }
		public delegate void GetErrorText_Handler<T>(T obj, MethodReturnEventArgs<string> ret, System.Object constrainedValue, System.Object constrainedObject);
		public event GetErrorText_Handler<Constraint> OnGetErrorText_Constraint;



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

#endregion

    }


}