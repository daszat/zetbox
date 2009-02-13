
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
    /// Metadefinition Object for Methods.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Method")]
    public class Method__Implementation__ : BaseFrozenDataObject, Method
    {


        /// <summary>
        /// 
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType ObjectClass
        {
            get
            {
                return _ObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_ObjectClass != value)
                {
                    NotifyPropertyChanging("ObjectClass");
                    _ObjectClass = value;
                    NotifyPropertyChanged("ObjectClass");;
                }
            }
        }
        private Kistl.App.Base.DataType _ObjectClass;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string MethodName
        {
            get
            {
                return _MethodName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MethodName != value)
                {
                    NotifyPropertyChanging("MethodName");
                    _MethodName = value;
                    NotifyPropertyChanged("MethodName");;
                }
            }
        }
        private string _MethodName;

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Module Module
        {
            get
            {
                return _Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Module != value)
                {
                    NotifyPropertyChanging("Module");
                    _Module = value;
                    NotifyPropertyChanged("Module");;
                }
            }
        }
        private Kistl.App.Base.Module _Module;

        /// <summary>
        /// Methodenaufrufe implementiert in dieser Objekt Klasse
        /// </summary>
        // object reference list property
        public virtual ICollection<Kistl.App.Base.MethodInvocation> MethodInvokations
        {
            get
            {
                if (_MethodInvokations == null)
                    _MethodInvokations = new List<Kistl.App.Base.MethodInvocation>();
                return _MethodInvokations;
            }
        }
        private ICollection<Kistl.App.Base.MethodInvocation> _MethodInvokations;

        /// <summary>
        /// Parameter der Methode
        /// </summary>
        // object reference list property
        public virtual IList<Kistl.App.Base.BaseParameter> Parameter
        {
            get
            {
                if (_Parameter == null)
                    _Parameter = new List<Kistl.App.Base.BaseParameter>();
                return _Parameter;
            }
        }
        private IList<Kistl.App.Base.BaseParameter> _Parameter;

        /// <summary>
        /// Shows this Method in th GUI
        /// </summary>
        // value type property
        public virtual bool IsDisplayable
        {
            get
            {
                return _IsDisplayable;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsDisplayable != value)
                {
                    NotifyPropertyChanging("IsDisplayable");
                    _IsDisplayable = value;
                    NotifyPropertyChanged("IsDisplayable");;
                }
            }
        }
        private bool _IsDisplayable;

        /// <summary>
        /// Description of this Method
        /// </summary>
        // value type property
        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Description != value)
                {
                    NotifyPropertyChanging("Description");
                    _Description = value;
                    NotifyPropertyChanged("Description");;
                }
            }
        }
        private string _Description;

        /// <summary>
        /// Returns the Return Parameter Meta Object of this Method Meta Object.
        /// </summary>

		public virtual Kistl.App.Base.BaseParameter GetReturnParameter() 
        {
            var e = new MethodReturnEventArgs<Kistl.App.Base.BaseParameter>();
            if (OnGetReturnParameter_Method != null)
            {
                OnGetReturnParameter_Method(this, e);
            };
            return e.Result;
        }
		public delegate void GetReturnParameter_Handler<T>(T obj, MethodReturnEventArgs<Kistl.App.Base.BaseParameter> ret);
		public event GetReturnParameter_Handler<Method> OnGetReturnParameter_Method;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Method != null)
            {
                OnToString_Method(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Method> OnToString_Method;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Method != null) OnPreSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPreSave_Method;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Method != null) OnPostSave_Method(this);
        }
        public event ObjectEventHandler<Method> OnPostSave_Method;


        internal Method__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }
		internal Dictionary<int, Method> DataStore = new Dictionary<int, Method>(0);
		static Method__Implementation__()
		{
		}

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