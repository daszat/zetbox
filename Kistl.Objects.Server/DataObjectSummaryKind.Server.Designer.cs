// <autogenerated/>


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

    using Kistl.API.Server;
    using Kistl.DalProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="DataObjectSummaryKind")]
    [System.Diagnostics.DebuggerDisplay("DataObjectSummaryKind")]
    public class DataObjectSummaryKind__Implementation__ : Kistl.App.GUI.DashboardKind__Implementation__, DataObjectSummaryKind
    {
    
		public DataObjectSummaryKind__Implementation__()
		{
        }


		public override Type GetImplementedInterface()
		{
			return typeof(DataObjectSummaryKind);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (DataObjectSummaryKind)obj;
			var otherImpl = (DataObjectSummaryKind__Implementation__)obj;
			var me = (DataObjectSummaryKind)this;

		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_DataObjectSummaryKind")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DataObjectSummaryKind != null)
            {
                OnToString_DataObjectSummaryKind(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DataObjectSummaryKind> OnToString_DataObjectSummaryKind;

        [EventBasedMethod("OnPreSave_DataObjectSummaryKind")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_DataObjectSummaryKind != null) OnPreSave_DataObjectSummaryKind(this);
        }
        public static event ObjectEventHandler<DataObjectSummaryKind> OnPreSave_DataObjectSummaryKind;

        [EventBasedMethod("OnPostSave_DataObjectSummaryKind")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_DataObjectSummaryKind != null) OnPostSave_DataObjectSummaryKind(this);
        }
        public static event ObjectEventHandler<DataObjectSummaryKind> OnPostSave_DataObjectSummaryKind;

        [EventBasedMethod("OnCreated_DataObjectSummaryKind")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_DataObjectSummaryKind != null) OnCreated_DataObjectSummaryKind(this);
        }
        public static event ObjectEventHandler<DataObjectSummaryKind> OnCreated_DataObjectSummaryKind;

        [EventBasedMethod("OnDeleting_DataObjectSummaryKind")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_DataObjectSummaryKind != null) OnDeleting_DataObjectSummaryKind(this);
        }
        public static event ObjectEventHandler<DataObjectSummaryKind> OnDeleting_DataObjectSummaryKind;


	

		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			base.ReloadReferences();
			
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            
            base.ToStream(binStream, auxObjects, eagerLoadLists);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            
            base.FromStream(binStream);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            
            base.ToStream(xml);
        }

        public override void FromStream(System.Xml.XmlReader xml)
        {
            
            base.FromStream(xml);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            
            base.Export(xml, modules);
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            
            base.MergeImport(xml);
        }

#endregion

    }


}