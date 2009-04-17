
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
    /// Metadefinition Object for Enumerations.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Enumeration")]
    public class Enumeration__Implementation__ : Kistl.App.Base.DataType__Implementation__, Enumeration
    {
    
		public Enumeration__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Einträge der Enumeration
        /// </summary>
        // object list property
        // implement the user-visible interface
        [XmlIgnore()]
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        public ICollection<Kistl.App.Base.EnumerationEntry> EnumerationEntries
        {
            get
            {
                if (_EnumerationEntriesWrapper == null)
                {
                    List<Kistl.App.Base.EnumerationEntry> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<Kistl.App.Base.EnumerationEntry>(this, "EnumerationEntries");
                    else
                        serverList = new List<Kistl.App.Base.EnumerationEntry>();
                        
                    _EnumerationEntriesWrapper = new OneNRelationCollection<Kistl.App.Base.EnumerationEntry>(
                        "Enumeration",
                        this,
                        serverList);
                }
                return _EnumerationEntriesWrapper;
            }
        }
        
        private OneNRelationCollection<Kistl.App.Base.EnumerationEntry> _EnumerationEntriesWrapper;

        /// <summary>
        /// Returns the resulting Type of this Datatype Meta Object.
        /// </summary>

		public override System.Type GetDataType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetDataType_Enumeration != null)
            {
                OnGetDataType_Enumeration(this, e);
            }
            else
            {
                e.Result = base.GetDataType();
            }
            return e.Result;
        }
		public event GetDataType_Handler<Enumeration> OnGetDataType_Enumeration;



        /// <summary>
        /// Returns the String representation of this Datatype Meta Object.
        /// </summary>

		public override string GetDataTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetDataTypeString_Enumeration != null)
            {
                OnGetDataTypeString_Enumeration(this, e);
            }
            else
            {
                e.Result = base.GetDataTypeString();
            }
            return e.Result;
        }
		public event GetDataTypeString_Handler<Enumeration> OnGetDataTypeString_Enumeration;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(Enumeration));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (Enumeration)obj;
			var otherImpl = (Enumeration__Implementation__)obj;
			var me = (Enumeration)this;

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
            if (OnToString_Enumeration != null)
            {
                OnToString_Enumeration(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<Enumeration> OnToString_Enumeration;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_Enumeration != null) OnPreSave_Enumeration(this);
        }
        public event ObjectEventHandler<Enumeration> OnPreSave_Enumeration;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_Enumeration != null) OnPostSave_Enumeration(this);
        }
        public event ObjectEventHandler<Enumeration> OnPostSave_Enumeration;



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
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml, string[] modules)
        {
            base.ToStream(xml, modules);
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            base.FromStream(xml);
        }

#endregion

    }


}