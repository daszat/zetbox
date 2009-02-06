
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
    [System.Diagnostics.DebuggerDisplay("Assembly")]
    public class Assembly__Implementation__ : BaseClientDataObject, Assembly
    {


        /// <summary>
        /// Module
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
                // fix up inverse reference
                var oldValue = Module;
                if (value != null && value.ID != fk_Module)
                {
                    oldValue.Assemblies.Remove(this);
                    fk_Module = value.ID;
                    value.Assemblies.Add(this);
                }
                else
                {
                    oldValue.Assemblies.Remove(this);
                    fk_Module = null;
                }
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

        /// <summary>
        /// Full Assemblyname eg. MyActions, Version=1.0.0.0
        /// </summary>
        // value type property
        public virtual string AssemblyName
        {
            get
            {
                return _AssemblyName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_AssemblyName != value)
                {
                    NotifyPropertyChanging("AssemblyName");
                    _AssemblyName = value;
                    NotifyPropertyChanged("AssemblyName");;
                }
            }
        }
        private string _AssemblyName;

        /// <summary>
        /// Legt fest, ob es sich um ein Client-Assembly handelt.
        /// </summary>
        // value type property
        public virtual bool IsClientAssembly
        {
            get
            {
                return _IsClientAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_IsClientAssembly != value)
                {
                    NotifyPropertyChanging("IsClientAssembly");
                    _IsClientAssembly = value;
                    NotifyPropertyChanged("IsClientAssembly");;
                }
            }
        }
        private bool _IsClientAssembly;

        /// <summary>
        /// Regenerates the stored list of TypeRefs from the loaded assembly
        /// </summary>

		public virtual void RegenerateTypeRefs() 
		{
            // base.RegenerateTypeRefs();
            if (OnRegenerateTypeRefs_Assembly != null)
            {
				OnRegenerateTypeRefs_Assembly(this);
			}
        }
		public delegate void RegenerateTypeRefs_Handler<T>(T obj);
		public event RegenerateTypeRefs_Handler<Assembly> OnRegenerateTypeRefs_Assembly;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Assembly != null)
            {
                OnToString_Assembly(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Assembly> OnToString_Assembly;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Assembly != null) OnPreSave_Assembly(this);
        }
        public event ObjectEventHandler<Assembly> OnPreSave_Assembly;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Assembly != null) OnPostSave_Assembly(this);
        }
        public event ObjectEventHandler<Assembly> OnPostSave_Assembly;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
            BinarySerializer.ToStream(this._AssemblyName, binStream);
            BinarySerializer.ToStream(this._IsClientAssembly, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
            BinarySerializer.FromStream(out this._AssemblyName, binStream);
            BinarySerializer.FromStream(out this._IsClientAssembly, binStream);
        }

#endregion

    }


}