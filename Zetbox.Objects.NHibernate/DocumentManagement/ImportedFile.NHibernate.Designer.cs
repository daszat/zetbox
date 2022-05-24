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

    using Zetbox.API.Utils;
    using Zetbox.DalProvider.Base;
    using Zetbox.DalProvider.NHibernate;

    /// <summary>
    /// Files beeing imported by the import service.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("ImportedFile")]
    public class ImportedFileNHibernateImpl : at.dasz.DocumentManagement.FileNHibernateImpl, ImportedFile
    {
        private static readonly Guid _objectClassID = new Guid("523a75bb-29c4-421c-9343-93d8658bb5f8");
        public override Guid ObjectClassID { get { return _objectClassID; } }

        public ImportedFileNHibernateImpl()
            : this(null)
        {
        }

        /// <summary>Create a new unattached instance</summary>
        public ImportedFileNHibernateImpl(Func<IFrozenContext> lazyCtx)
            : this(lazyCtx, new ImportedFileProxy())
        {
        }

        /// <summary>Create a instance, wrapping the specified proxy</summary>
        public ImportedFileNHibernateImpl(Func<IFrozenContext> lazyCtx, ImportedFileProxy proxy)
            : base(lazyCtx, proxy) // pass proxy to parent
        {
            this.Proxy = proxy;
        }

        /// <summary>the NHibernate proxy of the represented entity</summary>
        internal new readonly ImportedFileProxy Proxy;

        /// <summary>
        /// Creates an excerpt from the current file
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnExtractText_ImportedFile")]
        public override async System.Threading.Tasks.Task ExtractText()
        {
            // base.ExtractText();
            if (OnExtractText_ImportedFile != null)
            {
                await OnExtractText_ImportedFile(this);
            }
            else
            {
                await base.ExtractText();
            }
        }
        public static event ExtractText_Handler<ImportedFile> OnExtractText_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnExtractText_ImportedFile_CanExec;

        [EventBasedMethod("OnExtractText_ImportedFile_CanExec")]
        public override bool ExtractTextCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnExtractText_ImportedFile_CanExec != null)
				{
					OnExtractText_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.ExtractTextCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnExtractText_ImportedFile_CanExecReason;

        [EventBasedMethod("OnExtractText_ImportedFile_CanExecReason")]
        public override string ExtractTextCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnExtractText_ImportedFile_CanExecReason != null)
				{
					OnExtractText_ImportedFile_CanExecReason(this, e);
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
        [EventBasedMethod("OnHandleBlobChange_ImportedFile")]
        public override async System.Threading.Tasks.Task<Zetbox.App.Base.Blob> HandleBlobChange(Zetbox.App.Base.Blob oldBlob, Zetbox.App.Base.Blob newBlob)
        {
            var e = new MethodReturnEventArgs<Zetbox.App.Base.Blob>();
            if (OnHandleBlobChange_ImportedFile != null)
            {
                await OnHandleBlobChange_ImportedFile(this, e, oldBlob, newBlob);
            }
            else
            {
                e.Result = await base.HandleBlobChange(oldBlob, newBlob);
            }
            return e.Result;
        }
        public static event HandleBlobChange_Handler<ImportedFile> OnHandleBlobChange_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnHandleBlobChange_ImportedFile_CanExec;

        [EventBasedMethod("OnHandleBlobChange_ImportedFile_CanExec")]
        public override bool HandleBlobChangeCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnHandleBlobChange_ImportedFile_CanExec != null)
				{
					OnHandleBlobChange_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.HandleBlobChangeCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnHandleBlobChange_ImportedFile_CanExecReason;

        [EventBasedMethod("OnHandleBlobChange_ImportedFile_CanExecReason")]
        public override string HandleBlobChangeCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnHandleBlobChange_ImportedFile_CanExecReason != null)
				{
					OnHandleBlobChange_ImportedFile_CanExecReason(this, e);
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
        /// Converts the imported file to a document
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnMakeFile_ImportedFile")]
        public virtual async System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> MakeFile()
        {
            var e = new MethodReturnEventArgs<at.dasz.DocumentManagement.File>();
            if (OnMakeFile_ImportedFile != null)
            {
                await OnMakeFile_ImportedFile(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ImportedFile.MakeFile");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task MakeFile_Handler<T>(T obj, MethodReturnEventArgs<at.dasz.DocumentManagement.File> ret);
        public static event MakeFile_Handler<ImportedFile> OnMakeFile_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnMakeFile_ImportedFile_CanExec;

        [EventBasedMethod("OnMakeFile_ImportedFile_CanExec")]
        public virtual bool MakeFileCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnMakeFile_ImportedFile_CanExec != null)
				{
					OnMakeFile_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnMakeFile_ImportedFile_CanExecReason;

        [EventBasedMethod("OnMakeFile_ImportedFile_CanExecReason")]
        public virtual string MakeFileCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnMakeFile_ImportedFile_CanExecReason != null)
				{
					OnMakeFile_ImportedFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Converts the imported file to a readonly file
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnMakeReadonlyFile_ImportedFile")]
        public virtual async System.Threading.Tasks.Task<at.dasz.DocumentManagement.File> MakeReadonlyFile()
        {
            var e = new MethodReturnEventArgs<at.dasz.DocumentManagement.File>();
            if (OnMakeReadonlyFile_ImportedFile != null)
            {
                await OnMakeReadonlyFile_ImportedFile(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ImportedFile.MakeReadonlyFile");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task MakeReadonlyFile_Handler<T>(T obj, MethodReturnEventArgs<at.dasz.DocumentManagement.File> ret);
        public static event MakeReadonlyFile_Handler<ImportedFile> OnMakeReadonlyFile_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnMakeReadonlyFile_ImportedFile_CanExec;

        [EventBasedMethod("OnMakeReadonlyFile_ImportedFile_CanExec")]
        public virtual bool MakeReadonlyFileCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnMakeReadonlyFile_ImportedFile_CanExec != null)
				{
					OnMakeReadonlyFile_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnMakeReadonlyFile_ImportedFile_CanExecReason;

        [EventBasedMethod("OnMakeReadonlyFile_ImportedFile_CanExecReason")]
        public virtual string MakeReadonlyFileCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnMakeReadonlyFile_ImportedFile_CanExecReason != null)
				{
					OnMakeReadonlyFile_ImportedFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Deprecated, use MakeReadonlyFile instead
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnMakeStaticFile_ImportedFile")]
        public virtual async System.Threading.Tasks.Task<at.dasz.DocumentManagement.StaticFile> MakeStaticFile()
        {
            var e = new MethodReturnEventArgs<at.dasz.DocumentManagement.StaticFile>();
            if (OnMakeStaticFile_ImportedFile != null)
            {
                await OnMakeStaticFile_ImportedFile(this, e);
            }
            else
            {
                throw new NotImplementedException("No handler registered on ImportedFile.MakeStaticFile");
            }
            return e.Result;
        }
        public delegate System.Threading.Tasks.Task MakeStaticFile_Handler<T>(T obj, MethodReturnEventArgs<at.dasz.DocumentManagement.StaticFile> ret);
        public static event MakeStaticFile_Handler<ImportedFile> OnMakeStaticFile_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnMakeStaticFile_ImportedFile_CanExec;

        [EventBasedMethod("OnMakeStaticFile_ImportedFile_CanExec")]
        public virtual bool MakeStaticFileCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnMakeStaticFile_ImportedFile_CanExec != null)
				{
					OnMakeStaticFile_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = true;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnMakeStaticFile_ImportedFile_CanExecReason;

        [EventBasedMethod("OnMakeStaticFile_ImportedFile_CanExecReason")]
        public virtual string MakeStaticFileCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnMakeStaticFile_ImportedFile_CanExecReason != null)
				{
					OnMakeStaticFile_ImportedFile_CanExecReason(this, e);
				}
				else
				{
					e.Result = string.Empty;
				}
				return e.Result;
			}
        }
        // END Zetbox.Generator.Templates.ObjectClasses.MethodCanExec

        /// <summary>
        /// Öffnet das Dokument schreibgeschützt
        /// </summary>
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnOpen_ImportedFile")]
        public override async System.Threading.Tasks.Task Open()
        {
            // base.Open();
            if (OnOpen_ImportedFile != null)
            {
                await OnOpen_ImportedFile(this);
            }
            else
            {
                await base.Open();
            }
        }
        public static event Open_Handler<ImportedFile> OnOpen_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnOpen_ImportedFile_CanExec;

        [EventBasedMethod("OnOpen_ImportedFile_CanExec")]
        public override bool OpenCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnOpen_ImportedFile_CanExec != null)
				{
					OnOpen_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.OpenCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnOpen_ImportedFile_CanExecReason;

        [EventBasedMethod("OnOpen_ImportedFile_CanExecReason")]
        public override string OpenCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnOpen_ImportedFile_CanExecReason != null)
				{
					OnOpen_ImportedFile_CanExecReason(this, e);
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
        [EventBasedMethod("OnUpload_ImportedFile")]
        public override async System.Threading.Tasks.Task Upload()
        {
            // base.Upload();
            if (OnUpload_ImportedFile != null)
            {
                await OnUpload_ImportedFile(this);
            }
            else
            {
                await base.Upload();
            }
        }
        public static event Upload_Handler<ImportedFile> OnUpload_ImportedFile;
        // BEGIN Zetbox.Generator.Templates.ObjectClasses.MethodCanExec
		// CanExec
		public static event CanExecMethodEventHandler<ImportedFile> OnUpload_ImportedFile_CanExec;

        [EventBasedMethod("OnUpload_ImportedFile_CanExec")]
        public override bool UploadCanExec
        {
			get 
			{
				var e = new MethodReturnEventArgs<bool>();
				if (OnUpload_ImportedFile_CanExec != null)
				{
					OnUpload_ImportedFile_CanExec(this, e);
				}
				else
				{
					e.Result = base.UploadCanExec;
				}
				return e.Result;
			}
        }

		// CanExecReason
		public static event CanExecReasonMethodEventHandler<ImportedFile> OnUpload_ImportedFile_CanExecReason;

        [EventBasedMethod("OnUpload_ImportedFile_CanExecReason")]
        public override string UploadCanExecReason
        {
			get 
			{
				var e = new MethodReturnEventArgs<string>();
				if (OnUpload_ImportedFile_CanExecReason != null)
				{
					OnUpload_ImportedFile_CanExecReason(this, e);
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
            return typeof(ImportedFile);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (ImportedFile)obj;
            var otherImpl = (ImportedFileNHibernateImpl)obj;
            var me = (ImportedFile)this;

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

        public override async System.Threading.Tasks.Task ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            await base.ReloadReferences();

            // fix direct object references
        }
        #region Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Zetbox.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_ImportedFile")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_ImportedFile != null)
            {
                OnToString_ImportedFile(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<ImportedFile> OnToString_ImportedFile;

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnObjectIsValid_ImportedFile")]
        protected override ObjectIsValidResult ObjectIsValid()
        {
            ObjectIsValidEventArgs e = new ObjectIsValidEventArgs();
            var b = base.ObjectIsValid();
            e.Errors.AddRange(b.Errors);
            if (OnObjectIsValid_ImportedFile != null)
            {
                OnObjectIsValid_ImportedFile(this, e);
            }
            return new ObjectIsValidResult(e.IsValid, e.Errors);
        }
        public static event ObjectIsValidHandler<ImportedFile> OnObjectIsValid_ImportedFile;

        [EventBasedMethod("OnNotifyPreSave_ImportedFile")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnNotifyPreSave_ImportedFile != null) OnNotifyPreSave_ImportedFile(this);
        }
        public static event ObjectEventHandler<ImportedFile> OnNotifyPreSave_ImportedFile;

        [EventBasedMethod("OnNotifyPostSave_ImportedFile")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnNotifyPostSave_ImportedFile != null) OnNotifyPostSave_ImportedFile(this);
        }
        public static event ObjectEventHandler<ImportedFile> OnNotifyPostSave_ImportedFile;

        [EventBasedMethod("OnNotifyCreated_ImportedFile")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnNotifyCreated_ImportedFile != null) OnNotifyCreated_ImportedFile(this);
        }
        public static event ObjectEventHandler<ImportedFile> OnNotifyCreated_ImportedFile;

        [EventBasedMethod("OnNotifyDeleting_ImportedFile")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnNotifyDeleting_ImportedFile != null) OnNotifyDeleting_ImportedFile(this);


        }
        public static event ObjectEventHandler<ImportedFile> OnNotifyDeleting_ImportedFile;

        #endregion // Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses.DefaultMethods

        public class ImportedFileProxy
            : at.dasz.DocumentManagement.FileNHibernateImpl.FileProxy
        {
            public ImportedFileProxy()
            {
            }

            public override Type ZetboxWrapper { get { return typeof(ImportedFileNHibernateImpl); } }

            public override Type ZetboxProxy { get { return typeof(ImportedFileProxy); } }

        }

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