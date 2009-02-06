
namespace Kistl.App.GUI
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
    [System.Diagnostics.DebuggerDisplay("Template")]
    public class Template__Implementation__ : BaseClientDataObject, Template
    {


        /// <summary>
        /// The visual representation of this Template
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.GUI.Visual VisualTree
        {
            get
            {
                if (fk_VisualTree.HasValue)
                    return Context.Find<Kistl.App.GUI.Visual>(fk_VisualTree.Value);
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
        public int? fk_VisualTree
        {
            get
            {
                return _fk_VisualTree;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_VisualTree != value)
                {
                    NotifyPropertyChanging("VisualTree");
                    _fk_VisualTree = value;
                    NotifyPropertyChanging("VisualTree");
                }
            }
        }
        private int? _fk_VisualTree;

        /// <summary>
        /// Assembly of the Type that is displayed with this Template
        /// </summary>
        // object reference property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public Kistl.App.Base.Assembly DisplayedTypeAssembly
        {
            get
            {
                if (fk_DisplayedTypeAssembly.HasValue)
                    return Context.Find<Kistl.App.Base.Assembly>(fk_DisplayedTypeAssembly.Value);
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
        public int? fk_DisplayedTypeAssembly
        {
            get
            {
                return _fk_DisplayedTypeAssembly;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_fk_DisplayedTypeAssembly != value)
                {
                    NotifyPropertyChanging("DisplayedTypeAssembly");
                    _fk_DisplayedTypeAssembly = value;
                    NotifyPropertyChanging("DisplayedTypeAssembly");
                }
            }
        }
        private int? _fk_DisplayedTypeAssembly;

        /// <summary>
        /// The main menu for this Template
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.GUI.Visual> Menu
        {
            get
            {
                if (_MenuWrapper == null)
                {
                    List<Kistl.App.GUI.Visual> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.GUI.Visual>(this, "Menu");
                    else
                        serverList = new List<Kistl.App.GUI.Visual>();
                        
                    _MenuWrapper = new BackReferenceCollection<Kistl.App.GUI.Visual>(
                        "Template",
                        this,
                        serverList);
                }
                return _MenuWrapper;
            }
        }
        
        private BackReferenceCollection<Kistl.App.GUI.Visual> _MenuWrapper;

        /// <summary>
        /// a short name to identify this Template to the user
        /// </summary>
        // value type property
        public virtual string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayName != value)
                {
                    NotifyPropertyChanging("DisplayName");
                    _DisplayName = value;
                    NotifyPropertyChanged("DisplayName");;
                }
            }
        }
        private string _DisplayName;

        /// <summary>
        /// FullName of the Type that is displayed with this Template
        /// </summary>
        // value type property
        public virtual string DisplayedTypeFullName
        {
            get
            {
                return _DisplayedTypeFullName;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DisplayedTypeFullName != value)
                {
                    NotifyPropertyChanging("DisplayedTypeFullName");
                    _DisplayedTypeFullName = value;
                    NotifyPropertyChanged("DisplayedTypeFullName");;
                }
            }
        }
        private string _DisplayedTypeFullName;

        /// <summary>
        /// 
        /// </summary>

		public virtual void PrepareDefault(Kistl.App.Base.ObjectClass cls) 
		{
            // base.PrepareDefault();
            if (OnPrepareDefault_Template != null)
            {
				OnPrepareDefault_Template(this, cls);
			}
        }
		public delegate void PrepareDefault_Handler<T>(T obj, Kistl.App.Base.ObjectClass cls);
		public event PrepareDefault_Handler<Template> OnPrepareDefault_Template;



        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Template != null)
            {
                OnToString_Template(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Template> OnToString_Template;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Template != null) OnPreSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPreSave_Template;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Template != null) OnPostSave_Template(this);
        }
        public event ObjectEventHandler<Template> OnPostSave_Template;




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._fk_VisualTree, binStream);
            BinarySerializer.ToStream(this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.ToStream(this._DisplayName, binStream);
            BinarySerializer.ToStream(this._DisplayedTypeFullName, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._fk_VisualTree, binStream);
            BinarySerializer.FromStream(out this._fk_DisplayedTypeAssembly, binStream);
            BinarySerializer.FromStream(out this._DisplayName, binStream);
            BinarySerializer.FromStream(out this._DisplayedTypeFullName, binStream);
        }

#endregion

    }


}