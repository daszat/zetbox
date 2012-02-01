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
    using Kistl.DalProvider.Base.RelationWrappers;

    using Kistl.API.Server;
    using Kistl.DalProvider.Ef;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;

    /// <summary>
    /// 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="NavigationAction")]
    [System.Diagnostics.DebuggerDisplay("NavigationAction")]
    public class NavigationActionEfImpl : Kistl.App.GUI.NavigationEntryEfImpl, NavigationAction
    {
        [Obsolete]
        public NavigationActionEfImpl()
            : base(null)
        {
        }

        public NavigationActionEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetDefaultViewModel_NavigationAction")]
        public override System.Object GetDefaultViewModel(Kistl.API.IKistlContext dataCtx, System.Object parent)
        {
            var e = new MethodReturnEventArgs<System.Object>();
            if (OnGetDefaultViewModel_NavigationAction != null)
            {
                OnGetDefaultViewModel_NavigationAction(this, e, dataCtx, parent);
            }
            else
            {
                e.Result = base.GetDefaultViewModel(dataCtx, parent);
            }
            return e.Result;
        }
        public static event GetDefaultViewModel_Handler<NavigationAction> OnGetDefaultViewModel_NavigationAction;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(NavigationAction);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (NavigationAction)obj;
            var otherImpl = (NavigationActionEfImpl)obj;
            var me = (NavigationAction)this;

        }

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
        }

        public override void ReloadReferences()
        {
            // Do not reload references if the current object has been deleted.
            // TODO: enable when MemoryContext uses MemoryDataObjects
            //if (this.ObjectState == DataObjectState.Deleted) return;
            base.ReloadReferences();

            // fix direct object references
        }
        #region Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #endregion // Kistl.Generator.Templates.ObjectClasses.CustomTypeDescriptor
        #region Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_NavigationAction")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_NavigationAction != null)
            {
                OnToString_NavigationAction(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<NavigationAction> OnToString_NavigationAction;

        [EventBasedMethod("OnPreSave_NavigationAction")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_NavigationAction != null) OnPreSave_NavigationAction(this);
        }
        public static event ObjectEventHandler<NavigationAction> OnPreSave_NavigationAction;

        [EventBasedMethod("OnPostSave_NavigationAction")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_NavigationAction != null) OnPostSave_NavigationAction(this);
        }
        public static event ObjectEventHandler<NavigationAction> OnPostSave_NavigationAction;

        [EventBasedMethod("OnCreated_NavigationAction")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_NavigationAction != null) OnCreated_NavigationAction(this);
        }
        public static event ObjectEventHandler<NavigationAction> OnCreated_NavigationAction;

        [EventBasedMethod("OnDeleting_NavigationAction")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_NavigationAction != null) OnDeleting_NavigationAction(this);
        }
        public static event ObjectEventHandler<NavigationAction> OnDeleting_NavigationAction;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
			return baseResult == null
                ? result.Count == 0
                    ? null
                    : result
                : baseResult.Concat(result);
        }

        public override void ToStream(System.Xml.XmlWriter xml)
        {
            base.ToStream(xml);
            // it may be only an empty shell to stand-in for unreadable data
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
            // it may be only an empty shell to stand-in for unreadable data
            if (CurrentAccessRights != Kistl.API.AccessRights.None) {
            } // if (CurrentAccessRights != Kistl.API.AccessRights.None)
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