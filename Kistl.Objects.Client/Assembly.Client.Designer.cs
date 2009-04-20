
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
    
		public Assembly__Implementation__()
		{
            {
            }
        }


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
					var __oldValue = _AssemblyName;
                    NotifyPropertyChanging("AssemblyName", __oldValue, value);
                    _AssemblyName = value;
                    NotifyPropertyChanged("AssemblyName", __oldValue, value);
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
					var __oldValue = _IsClientAssembly;
                    NotifyPropertyChanging("IsClientAssembly", __oldValue, value);
                    _IsClientAssembly = value;
                    NotifyPropertyChanged("IsClientAssembly", __oldValue, value);
                }
            }
        }
        private bool _IsClientAssembly;

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
                
                // shortcut noops
                if (value == null && _fk_Module == null)
					return;
                else if (value != null && value.ID == _fk_Module)
					return;
			           
	            // cache old value to remove inverse references later
                var oldValue = Module;

				// Changing Event fires before anything is touched
				NotifyPropertyChanging("Module", oldValue, value);
                
				// next, set the local reference
                _fk_Module = value == null ? (int?)null : value.ID;
				
				// now fixup redundant, inverse references
				// The inverse navigator will also fire events when changed, so should 
				// only be touched after setting the local value above. 
				// TODO: for complete correctness, the "other" Changing event should also fire 
				//       before the local value is changed
				if (oldValue != null)
				{
					// remove from old list
					(oldValue.Assemblies as OneNRelationCollection<Kistl.App.Base.Assembly>).RemoveWithoutClearParent(this);
				}

                if (value != null)
                {
					// add to new list
					(value.Assemblies as OneNRelationCollection<Kistl.App.Base.Assembly>).AddWithoutSetParent(this);
                }
				// everything is done. fire the Changed event
				NotifyPropertyChanged("Module", oldValue, value);
            }
        }
        
        // provide a way to directly access the foreign key int
        public int? fk_Module
        {
            get
            {
                return _fk_Module;
            }
            private set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_Module != value)
                {
					var __oldValue = _fk_Module;
                    NotifyPropertyChanging("Module", __oldValue, value);
                    _fk_Module = value;
                    NotifyPropertyChanged("Module", __oldValue, value);
                }
            }
        }
        private int? _fk_Module;

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
			else
			{
                throw new NotImplementedException("No handler registered on Assembly.RegenerateTypeRefs");
			}
        }
		public delegate void RegenerateTypeRefs_Handler<T>(T obj);
		public event RegenerateTypeRefs_Handler<Assembly> OnRegenerateTypeRefs_Assembly;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Assembly));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Assembly)obj;
			var otherImpl = (Assembly__Implementation__)obj;
			var me = (Assembly)this;

			me.AssemblyName = other.AssemblyName;
			me.IsClientAssembly = other.IsClientAssembly;
			this.fk_Module = otherImpl.fk_Module;
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



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
                case "Module":
                    fk_Module = id;
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
            BinarySerializer.ToStream(this._AssemblyName, binStream);
            BinarySerializer.ToStream(this._IsClientAssembly, binStream);
            BinarySerializer.ToStream(this._fk_Module, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._AssemblyName, binStream);
            BinarySerializer.FromStream(out this._IsClientAssembly, binStream);
            BinarySerializer.FromStream(out this._fk_Module, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
            XmlStreamer.ToStream(this._AssemblyName, xml, "AssemblyName", "Kistl.App.Base");
            XmlStreamer.ToStream(this._IsClientAssembly, xml, "IsClientAssembly", "Kistl.App.Base");
            XmlStreamer.ToStream(this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
            XmlStreamer.FromStream(ref this._AssemblyName, xml, "AssemblyName", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._IsClientAssembly, xml, "IsClientAssembly", "Kistl.App.Base");
            XmlStreamer.FromStream(ref this._fk_Module, xml, "Module", "http://dasz.at/Kistl");
        }

#endregion

    }


}