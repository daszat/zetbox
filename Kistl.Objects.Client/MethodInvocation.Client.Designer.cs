
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
    /// Metadefinition Object for a MethodInvocation on a Method of a DataType.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("MethodInvocation")]
    public class MethodInvocation__Implementation__ : BaseClientDataObject, MethodInvocation
    {


        /// <summary>
        /// The Type implementing this invocation
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.TypeRef Implementor
        {
            get
            {
                if (fk_Implementor.HasValue)
                    return Context.Find<Kistl.App.Base.TypeRef>(fk_Implementor.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Implementor
        {
            get
            {
                return _fk_Implementor;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Implementor != value)
                {
                    NotifyPropertyChanging("Implementor");
                    _fk_Implementor = value;
                    NotifyPropertyChanging("Implementor");
                }
            }
        }
        private int? _fk_Implementor;

        /// <summary>
        /// In dieser Objektklasse implementieren
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.DataType InvokeOnObjectClass
        {
            get
            {
                if (fk_InvokeOnObjectClass.HasValue)
                    return Context.Find<Kistl.App.Base.DataType>(fk_InvokeOnObjectClass.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = InvokeOnObjectClass;
                if (value != null && value.ID != fk_InvokeOnObjectClass)
                {
                    oldValue.MethodInvocations.Remove(this);
                    fk_InvokeOnObjectClass = value.ID;
                    value.MethodInvocations.Add(this);
                }
                else
                {
                    oldValue.MethodInvocations.Remove(this);
                    fk_InvokeOnObjectClass = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_InvokeOnObjectClass
        {
            get
            {
                return _fk_InvokeOnObjectClass;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_InvokeOnObjectClass != value)
                {
                    NotifyPropertyChanging("InvokeOnObjectClass");
                    _fk_InvokeOnObjectClass = value;
                    NotifyPropertyChanging("InvokeOnObjectClass");
                }
            }
        }
        private int? _fk_InvokeOnObjectClass;

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
        /// Methode, die Aufgerufen wird
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Method Method
        {
            get
            {
                if (fk_Method.HasValue)
                    return Context.Find<Kistl.App.Base.Method>(fk_Method.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
                // fix up inverse reference
                var oldValue = Method;
                if (value != null && value.ID != fk_Method)
                {
                    oldValue.MethodInvokations.Remove(this);
                    fk_Method = value.ID;
                    value.MethodInvokations.Add(this);
                }
                else
                {
                    oldValue.MethodInvokations.Remove(this);
                    fk_Method = null;
                }
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Method
        {
            get
            {
                return _fk_Method;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Method != value)
                {
                    NotifyPropertyChanging("Method");
                    _fk_Method = value;
                    NotifyPropertyChanging("Method");
                }
            }
        }
        private int? _fk_Method;

        /// <summary>
        /// Zugehörig zum Modul
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Module Module
        {
            get
            {
                if (fk_Module.HasValue)
                    return Context.Find<Kistl.App.Base.Module>(fk_Module.Value);
                else
                    return null;
            }
            set
            {
                // TODO: only accept objects from same Context
                if (IsReadonly) throw new ReadOnlyObjectException();
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                return _fk_Module;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Module != value)
                {
                    NotifyPropertyChanging("Module");
                    _fk_Module = value;
                    NotifyPropertyChanging("Module");
                }
            }
        }
        private int? _fk_Module;

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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Implementor, binStream);
            BinarySerializer.ToStream(this._fk_InvokeOnObjectClass, binStream);
            BinarySerializer.ToStream(this._MemberName, binStream);
            BinarySerializer.ToStream(this._fk_Method, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Implementor, binStream);
            BinarySerializer.FromStream(out this._fk_InvokeOnObjectClass, binStream);
            BinarySerializer.FromStream(out this._MemberName, binStream);
            BinarySerializer.FromStream(out this._fk_Method, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
        }

#endregion

    }


}