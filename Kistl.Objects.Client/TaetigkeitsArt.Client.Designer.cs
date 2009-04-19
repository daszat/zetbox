
namespace Kistl.App.Zeiterfassung
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
    [System.Diagnostics.DebuggerDisplay("TaetigkeitsArt")]
    public class TaetigkeitsArt__Implementation__ : BaseClientDataObject, TaetigkeitsArt
    {
    
		public TaetigkeitsArt__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Name der Tätigkeitsart
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
					var __oldValue = _Name;
                    NotifyPropertyChanging("Name", __oldValue, value);
                    _Name = value;
                    NotifyPropertyChanged("Name", __oldValue, value);
                }
            }
        }
        private string _Name;

		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(TaetigkeitsArt));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (TaetigkeitsArt)obj;
			var otherImpl = (TaetigkeitsArt__Implementation__)obj;
			var me = (TaetigkeitsArt)this;

			me.Name = other.Name;
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
            if (OnToString_TaetigkeitsArt != null)
            {
                OnToString_TaetigkeitsArt(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<TaetigkeitsArt> OnToString_TaetigkeitsArt;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_TaetigkeitsArt != null) OnPreSave_TaetigkeitsArt(this);
        }
        public event ObjectEventHandler<TaetigkeitsArt> OnPreSave_TaetigkeitsArt;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_TaetigkeitsArt != null) OnPostSave_TaetigkeitsArt(this);
        }
        public event ObjectEventHandler<TaetigkeitsArt> OnPostSave_TaetigkeitsArt;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
		}

#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
            BinarySerializer.ToStream(this._Name, binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
            BinarySerializer.FromStream(out this._Name, binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
			// TODO: Use Propertyname
            XmlStreamer.ToStream(this._Name, xml, "Name", "http://dasz.at/Kistl");
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
			// TODO: Use Propertyname
            XmlStreamer.FromStream(ref this._Name, xml, "Name", "http://dasz.at/Kistl");
        }

#endregion

    }


}