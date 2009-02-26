
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

    using Kistl.API.Server;
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Metadefinition Object for Enumerations.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="Enumeration")]
    [System.Diagnostics.DebuggerDisplay("Enumeration")]
    public class Enumeration__Implementation__ : Kistl.App.Base.DataType__Implementation__, Enumeration
    {


        /// <summary>
        /// Einträge der Enumeration
        /// </summary>
    /*
    NewRelation: FK_Enumeration_EnumerationEntry_Enumeration_27 
    A: One Enumeration as Enumeration (site: A, from relation ID = 15)
    B: ZeroOrMore EnumerationEntry as EnumerationEntries (site: B, from relation ID = 15)
    Preferred Storage: MergeB
    */
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
                    _EnumerationEntriesWrapper = new EntityCollectionWrapper<Kistl.App.Base.EnumerationEntry, Kistl.App.Base.EnumerationEntry__Implementation__>(
                            this.Context, EnumerationEntries__Implementation__);
                }
                return _EnumerationEntriesWrapper;
            }
        }
        
        [EdmRelationshipNavigationProperty("Model", "FK_Enumeration_EnumerationEntry_Enumeration_27", "EnumerationEntries")]
        public EntityCollection<Kistl.App.Base.EnumerationEntry__Implementation__> EnumerationEntries__Implementation__
        {
            get
            {
                var c = ((IEntityWithRelationships)(this)).RelationshipManager
                    .GetRelatedCollection<Kistl.App.Base.EnumerationEntry__Implementation__>(
                        "Model.FK_Enumeration_EnumerationEntry_Enumeration_27",
                        "EnumerationEntries");
                if (this.EntityState.In(System.Data.EntityState.Modified, System.Data.EntityState.Unchanged)
                    && !c.IsLoaded)
                {
                    c.Load();
                }
                return c;
            }
        }
        private EntityCollectionWrapper<Kistl.App.Base.EnumerationEntry, Kistl.App.Base.EnumerationEntry__Implementation__> _EnumerationEntriesWrapper;



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



		public override Type GetInterfaceType()
		{
			return typeof(Enumeration);
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}