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
    /// A special editor for relations
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="RelationEditorKind")]
    [System.Diagnostics.DebuggerDisplay("RelationEditorKind")]
    public class RelationEditorKind__Implementation__ : Kistl.App.GUI.DataObjectKind__Implementation__, RelationEditorKind
    {
    
		public RelationEditorKind__Implementation__()
		{
        }


		public override Type GetImplementedInterface()
		{
			return typeof(RelationEditorKind);
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (RelationEditorKind)obj;
			var otherImpl = (RelationEditorKind__Implementation__)obj;
			var me = (RelationEditorKind)this;

		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_RelationEditorKind")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_RelationEditorKind != null)
            {
                OnToString_RelationEditorKind(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<RelationEditorKind> OnToString_RelationEditorKind;

        [EventBasedMethod("OnPreSave_RelationEditorKind")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_RelationEditorKind != null) OnPreSave_RelationEditorKind(this);
        }
        public static event ObjectEventHandler<RelationEditorKind> OnPreSave_RelationEditorKind;

        [EventBasedMethod("OnPostSave_RelationEditorKind")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_RelationEditorKind != null) OnPostSave_RelationEditorKind(this);
        }
        public static event ObjectEventHandler<RelationEditorKind> OnPostSave_RelationEditorKind;

        [EventBasedMethod("OnCreated_RelationEditorKind")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_RelationEditorKind != null) OnCreated_RelationEditorKind(this);
        }
        public static event ObjectEventHandler<RelationEditorKind> OnCreated_RelationEditorKind;

        [EventBasedMethod("OnDeleting_RelationEditorKind")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_RelationEditorKind != null) OnDeleting_RelationEditorKind(this);
        }
        public static event ObjectEventHandler<RelationEditorKind> OnDeleting_RelationEditorKind;


	

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