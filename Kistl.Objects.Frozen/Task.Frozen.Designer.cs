
namespace Kistl.App.Projekte
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
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Task")]
    public class Task__Implementation__ : BaseFrozenDataObject, Task
    {


        /// <summary>
        /// Taskname
        /// </summary>
        // value type property
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Name != value)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");;
                }
            }
        }
        private string _Name;

        /// <summary>
        /// Start Datum
        /// </summary>
        // value type property
        public virtual DateTime? DatumVon
        {
            get
            {
                return _DatumVon;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumVon != value)
                {
                    NotifyPropertyChanging("DatumVon");
                    _DatumVon = value;
                    NotifyPropertyChanged("DatumVon");;
                }
            }
        }
        private DateTime? _DatumVon;

        /// <summary>
        /// Enddatum
        /// </summary>
        // value type property
        public virtual DateTime? DatumBis
        {
            get
            {
                return _DatumBis;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_DatumBis != value)
                {
                    NotifyPropertyChanging("DatumBis");
                    _DatumBis = value;
                    NotifyPropertyChanged("DatumBis");;
                }
            }
        }
        private DateTime? _DatumBis;

        /// <summary>
        /// Aufwand in Stunden
        /// </summary>
        // value type property
        public virtual double? Aufwand
        {
            get
            {
                return _Aufwand;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Aufwand != value)
                {
                    NotifyPropertyChanging("Aufwand");
                    _Aufwand = value;
                    NotifyPropertyChanged("Aufwand");;
                }
            }
        }
        private double? _Aufwand;

        /// <summary>
        /// Verknüpfung zum Projekt
        /// </summary>
        // object reference property
        public virtual Kistl.App.Projekte.Projekt Projekt
        {
            get
            {
                return _Projekt;
            }
            set
            {
                if (IsReadonly) throw new ReadOnlyObjectException();
                if (_Projekt != value)
                {
                    NotifyPropertyChanging("Projekt");
                    _Projekt = value;
                    NotifyPropertyChanged("Projekt");;
                }
            }
        }
        private Kistl.App.Projekte.Projekt _Projekt;

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_Task != null)
            {
                OnToString_Task(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Task> OnToString_Task;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Task != null) OnPreSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPreSave_Task;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Task != null) OnPostSave_Task(this);
        }
        public event ObjectEventHandler<Task> OnPostSave_Task;


        internal Task__Implementation__(FrozenContext ctx, int id)
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