// <autogenerated/>

namespace at.dasz.DocumentManagement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Zetbox.API;
    using Zetbox.DalProvider.Base.RelationWrappers;

    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.Memory;

    /// <summary>
    /// Depricated
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("DynamicFile")]
    public class DynamicFileMemoryImpl : at.dasz.DocumentManagement.FileMemoryImpl, DynamicFile
    {
        private static readonly Guid _objectClassID = new Guid("e1556c0b-cdb9-4174-a9e5-07efba58ef05");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        [Obsolete]
        public DynamicFileMemoryImpl()
            : base(null)
        {
        }

        public DynamicFileMemoryImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Creates an excerpt from the current file
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnExtractText_DynamicFile")]
        public override void ExtractText()
        {
            // base.ExtractText();
            if (OnExtractText_DynamicFile != null)
            {
                OnExtractText_DynamicFile(this);
            }
            else
            {
                base.ExtractText();
            }
        }
        public static event ExtractText_Handler<DynamicFile> OnExtractText_DynamicFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DynamicFile> OnExtractText_DynamicFile_CanExec;

        [EventBasedMethod("OnExtractText_DynamicFile_CanExec")]
        public override bool ExtractTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnExtractText_DynamicFile_CanExec != null)
				{
					OnExtractText_DynamicFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.ExtractTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DynamicFile> OnExtractText_DynamicFile_CanExecReason;

        [EventBasedMethod("OnExtractText_DynamicFile_CanExecReason")]
        public override string ExtractTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnExtractText_DynamicFile_CanExecReason != null)
				{
					OnExtractText_DynamicFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.ExtractTextCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Handles the change of the current blob
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnHandleBlobChange_DynamicFile")]
        public override Zetbox.App.Base.Blob HandleBlobChange(Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.Blob>();
            if (OnHandleBlobChange_DynamicFile != null)
            {
                OnHandleBlobChange_DynamicFile(this, e, oldBlob, newBlob);
            }
            else
            {
                e.Result = base.HandleBlobChange(oldBlob, newBlob);
            }
            return e.Result;
        }
        public static event HandleBlobChange_Handler<DynamicFile> OnHandleBlobChange_DynamicFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DynamicFile> OnHandleBlobChange_DynamicFile_CanExec;

        [EventBasedMethod("OnHandleBlobChange_DynamicFile_CanExec")]
        public override bool HandleBlobChangeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnHandleBlobChange_DynamicFile_CanExec != null)
				{
					OnHandleBlobChange_DynamicFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.HandleBlobChangeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DynamicFile> OnHandleBlobChange_DynamicFile_CanExecReason;

        [EventBasedMethod("OnHandleBlobChange_DynamicFile_CanExecReason")]
        public override string HandleBlobChangeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnHandleBlobChange_DynamicFile_CanExecReason != null)
				{
					OnHandleBlobChange_DynamicFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.HandleBlobChangeCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Öffnet das Dokument schreibgeschützt
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnOpen_DynamicFile")]
        public override void Open()
        {
            // base.Open();
            if (OnOpen_DynamicFile != null)
            {
                OnOpen_DynamicFile(this);
            }
            else
            {
                base.Open();
            }
        }
        public static event Open_Handler<DynamicFile> OnOpen_DynamicFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DynamicFile> OnOpen_DynamicFile_CanExec;

        [EventBasedMethod("OnOpen_DynamicFile_CanExec")]
        public override bool OpenCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnOpen_DynamicFile_CanExec != null)
				{
					OnOpen_DynamicFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.OpenCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DynamicFile> OnOpen_DynamicFile_CanExecReason;

        [EventBasedMethod("OnOpen_DynamicFile_CanExecReason")]
        public override string OpenCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnOpen_DynamicFile_CanExecReason != null)
				{
					OnOpen_DynamicFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.OpenCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Uploads a new Content
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnUpload_DynamicFile")]
        public override void Upload()
        {
            // base.Upload();
            if (OnUpload_DynamicFile != null)
            {
                OnUpload_DynamicFile(this);
            }
            else
            {
                base.Upload();
            }
        }
        public static event Upload_Handler<DynamicFile> OnUpload_DynamicFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<DynamicFile> OnUpload_DynamicFile_CanExec;

        [EventBasedMethod("OnUpload_DynamicFile_CanExec")]
        public override bool UploadCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnUpload_DynamicFile_CanExec != null)
				{
					OnUpload_DynamicFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.UploadCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<DynamicFile> OnUpload_DynamicFile_CanExecReason;

        [EventBasedMethod("OnUpload_DynamicFile_CanExecReason")]
        public override string UploadCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnUpload_DynamicFile_CanExecReason != null)
				{
					OnUpload_DynamicFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = base.UploadCanExecReason;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        public override Type GetImplementedInterface()
        {
            return typeof(DynamicFile);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (DynamicFile)obj;
            var otherImpl = (DynamicFileMemoryImpl)obj;
            var me = (DynamicFile)this;

        }
        public override void SetNew()
        {
            base.SetNew();
        }

        #region Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        #endregion // Zetbox.Generator.Templates.ObjectClasses.OnPropertyChange

        public override System.Threading.Tasks.Task TriggerFetch(string propName)
        {
            switch(propName)
            {
            default:
                return base.TriggerFetch(propName);
            }
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
            // fix cached lists references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_DynamicFile")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_DynamicFile != null)
            {
                OnToString_DynamicFile(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<DynamicFile> OnToString_DynamicFile;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_DynamicFile")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_DynamicFile != null)
            {
                OnObjectIsValid_DynamicFile(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<DynamicFile> OnObjectIsValid_DynamicFile;

        [EventBasedMethod("OnNotifyPreSave_DynamicFile")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_DynamicFile != null) OnNotifyPreSave_DynamicFile(this);
        }
        public static event ObjectEventHandler<DynamicFile> OnNotifyPreSave_DynamicFile;

        [EventBasedMethod("OnNotifyPostSave_DynamicFile")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_DynamicFile != null) OnNotifyPostSave_DynamicFile(this);
        }
        public static event ObjectEventHandler<DynamicFile> OnNotifyPostSave_DynamicFile;

        [EventBasedMethod("OnNotifyCreated_DynamicFile")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_DynamicFile != null) OnNotifyCreated_DynamicFile(this);
        }
        public static event ObjectEventHandler<DynamicFile> OnNotifyCreated_DynamicFile;

        [EventBasedMethod("OnNotifyDeleting_DynamicFile")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_DynamicFile != null) OnNotifyDeleting_DynamicFile(this);
        }
        public static event ObjectEventHandler<DynamicFile> OnNotifyDeleting_DynamicFile;

        #endregion // Zetbox.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(Zetbox.API.ZetboxStreamWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(Zetbox.API.ZetboxStreamReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Zetbox.API.AccessRights.None) {
            } // if (CurrentAccessRights != Zetbox.API.AccessRights.None)
            return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void Export(System.Xml.XmlWriter xml, string[] modules)
        {
            base.Export(xml, modules);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}