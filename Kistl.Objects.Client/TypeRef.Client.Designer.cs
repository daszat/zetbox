
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
    /// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("TypeRef")]
    public class TypeRef__Implementation__ : BaseClientDataObject, TypeRef
    {


        /// <summary>
        /// The assembly containing the referenced Type.
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly Assembly
        {
            get
            {
                if (fk_Assembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_Assembly.Value);
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
        public int? fk_Assembly
        {
            get
            {
                return _fk_Assembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Assembly != value)
                {
                    NotifyPropertyChanging("Assembly");
                    _fk_Assembly = value;
                    NotifyPropertyChanging("Assembly");
                }
            }
        }
        private int? _fk_Assembly;

        /// <summary>
        /// 
        /// </summary>
        // value type property
        public virtual string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_FullName != value)
                {
                    NotifyPropertyChanging("FullName");
                    _FullName = value;
                    NotifyPropertyChanged("FullName");
                }
            }
        }
        private string _FullName;

        /// <summary>
        /// list of type arguments
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public IList<Kistl.App.Base.TypeRef> GenericArguments
        {
            get
            {
                if (_GenericArgumentsWrapper == null)
                {
                    List<Kistl.App.Base.TypeRef> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.TypeRef>(this, "GenericArguments");
                    else
                        serverList = new List<Kistl.App.Base.TypeRef>();
                        
                    _GenericArgumentsWrapper = new BackReferenceCollection<Kistl.App.Base.TypeRef>(
                        "TypeRef",
                        this,
                        serverList);
                }
                return _GenericArgumentsWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.Base.TypeRef> _GenericArgumentsWrapper;

        /// <summary>
        /// get the referenced <see cref="System.Type"/>
        /// </summary>

		public virtual System.Type AsType(System.Boolean throwOnError) 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnAsType_TypeRef != null)
            {
                OnAsType_TypeRef(this, e, throwOnError);
            };
            return e.Result;
        }
		public delegate void AsType_Handler<T>(T obj, MethodReturnEventArgs<System.Type> ret, System.Boolean throwOnError);
		public event AsType_Handler<TypeRef> OnAsType_TypeRef;



		public override Type GetInterfaceType()
		{
			return typeof(TypeRef);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_TypeRef != null)
            {
                OnToString_TypeRef(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TypeRef> OnToString_TypeRef;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TypeRef != null) OnPreSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPreSave_TypeRef;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TypeRef != null) OnPostSave_TypeRef(this);
        }
        public event ObjectEventHandler<TypeRef> OnPostSave_TypeRef;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Assembly, binStream);
            BinarySerializer.ToStream(this._FullName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Assembly, binStream);
            BinarySerializer.FromStream(out this._FullName, binStream);
        }

#endregion

    }


}