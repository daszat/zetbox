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
    using Kistl.DALProvider.EF;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// Select a boolean value with a checkbox
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="CheckboxKind")]
    [System.Diagnostics.DebuggerDisplay("CheckboxKind")]
    public class CheckboxKind__Implementation__ : Kistl.App.GUI.ControlKind__Implementation__, CheckboxKind
    {
    
		public CheckboxKind__Implementation__()
		{
        }


		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(CheckboxKind));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (CheckboxKind)obj;
			var otherImpl = (CheckboxKind__Implementation__)obj;
			var me = (CheckboxKind)this;

		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_CheckboxKind")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_CheckboxKind != null)
            {
                OnToString_CheckboxKind(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<CheckboxKind> OnToString_CheckboxKind;

        [EventBasedMethod("OnPreSave_CheckboxKind")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_CheckboxKind != null) OnPreSave_CheckboxKind(this);
        }
        public event ObjectEventHandler<CheckboxKind> OnPreSave_CheckboxKind;

        [EventBasedMethod("OnPostSave_CheckboxKind")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_CheckboxKind != null) OnPostSave_CheckboxKind(this);
        }
        public event ObjectEventHandler<CheckboxKind> OnPostSave_CheckboxKind;

        [EventBasedMethod("OnCreated_CheckboxKind")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_CheckboxKind != null) OnCreated_CheckboxKind(this);
        }
        public event ObjectEventHandler<CheckboxKind> OnCreated_CheckboxKind;

        [EventBasedMethod("OnDeleting_CheckboxKind")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_CheckboxKind != null) OnDeleting_CheckboxKind(this);
        }
        public event ObjectEventHandler<CheckboxKind> OnDeleting_CheckboxKind;



		public override void ReloadReferences()
		{
			// Do not reload references if the current object has been deleted.
			// TODO: enable when MemoryContext uses MemoryDataObjects
			//if (this.ObjectState == DataObjectState.Deleted) return;
			base.ReloadReferences();
			
			// fix direct object references
		}
#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects)
        {
            
            base.ToStream(binStream, auxObjects);
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