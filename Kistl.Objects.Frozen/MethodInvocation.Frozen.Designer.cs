
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
    /// Metadefinition Object for a MethodInvocation on a Method of a DataType.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodInvocation")]
    public class MethodInvocation__Implementation__ : BaseFrozenDataObject, MethodInvocation
    {


        /// <summary>
        /// Methode, die Aufgerufen wird
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.Method Method
        {
            get
            {
                return _Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Method != value)
                {
                    NotifyPropertyChanging("Method");
                    _Method = value;
                    NotifyPropertyChanged("Method");;
                }
            }
        }
        private Kistl.App.Base.Method _Method;

        /// <summary>
        /// Name des implementierenden Members
        /// </summary>
        // value type property
        public virtual string MemberName
        {
            get
            {
                return _MemberName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_MemberName != value)
                {
                    NotifyPropertyChanging("MemberName");
                    _MemberName = value;
                    NotifyPropertyChanged("MemberName");;
                }
            }
        }
        private string _MemberName;

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
        /// In dieser Objektklasse implementieren
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                return _InvokeOnObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_InvokeOnObjectClass != value)
                {
                    NotifyPropertyChanging("InvokeOnObjectClass");
                    _InvokeOnObjectClass = value;
                    NotifyPropertyChanged("InvokeOnObjectClass");;
                }
            }
        }
        private Kistl.App.Base.DataType _InvokeOnObjectClass;

        /// <summary>
        /// The Type implementing this invocation
        /// </summary>
        // object reference property
        public virtual Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                return _Implementor;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Implementor != value)
                {
                    NotifyPropertyChanging("Implementor");
                    _Implementor = value;
                    NotifyPropertyChanged("Implementor");;
                }
            }
        }
        private Kistl.App.Base.TypeRef _Implementor;

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_MethodInvocation != null)
            {
                OnToString_MethodInvocation(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<MethodInvocation> OnToString_MethodInvocation;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_MethodInvocation != null) OnPreSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPreSave_MethodInvocation;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_MethodInvocation != null) OnPostSave_MethodInvocation(this);
        }
        public event ObjectEventHandler<MethodInvocation> OnPostSave_MethodInvocation;


        internal MethodInvocation__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }
		internal Dictionary<int, MethodInvocation> DataStore = new Dictionary<int, MethodInvocation>(0);
		static MethodInvocation__Implementation__()
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