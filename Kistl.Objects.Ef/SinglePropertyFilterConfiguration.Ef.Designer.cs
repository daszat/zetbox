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
    /// Filter configuration for filtering on a single value of a Property 
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="SinglePropertyFilterConfiguration")]
    [System.Diagnostics.DebuggerDisplay("SinglePropertyFilterConfiguration")]
    public class SinglePropertyFilterConfigurationEfImpl : Kistl.App.GUI.PropertyFilterConfigurationEfImpl, SinglePropertyFilterConfiguration
    {
        [Obsolete]
        public SinglePropertyFilterConfigurationEfImpl()
            : base(null)
        {
        }

        public SinglePropertyFilterConfigurationEfImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnCreateFilterModel_SinglePropertyFilterConfiguration")]
        public override Kistl.API.IFilterModel CreateFilterModel()
        {
            var e = new MethodReturnEventArgs<Kistl.API.IFilterModel>();
            if (OnCreateFilterModel_SinglePropertyFilterConfiguration != null)
            {
                OnCreateFilterModel_SinglePropertyFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.CreateFilterModel();
            }
            return e.Result;
        }
        public static event CreateFilterModel_Handler<SinglePropertyFilterConfiguration> OnCreateFilterModel_SinglePropertyFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        /// <summary>
        /// 
        /// </summary>
        // BEGIN Kistl.Generator.Templates.ObjectClasses.Method
        [EventBasedMethod("OnGetLabel_SinglePropertyFilterConfiguration")]
        public override string GetLabel()
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetLabel_SinglePropertyFilterConfiguration != null)
            {
                OnGetLabel_SinglePropertyFilterConfiguration(this, e);
            }
            else
            {
                e.Result = base.GetLabel();
            }
            return e.Result;
        }
        public static event GetLabel_Handler<SinglePropertyFilterConfiguration> OnGetLabel_SinglePropertyFilterConfiguration;
        // END Kistl.Generator.Templates.ObjectClasses.Method

        public override Type GetImplementedInterface()
        {
            return typeof(SinglePropertyFilterConfiguration);
        }

        public override void ApplyChangesFrom(IPersistenceObject obj)
        {
            base.ApplyChangesFrom(obj);
            var other = (SinglePropertyFilterConfiguration)obj;
            var otherImpl = (SinglePropertyFilterConfigurationEfImpl)obj;
            var me = (SinglePropertyFilterConfiguration)this;

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
        [EventBasedMethod("OnToString_SinglePropertyFilterConfiguration")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_SinglePropertyFilterConfiguration != null)
            {
                OnToString_SinglePropertyFilterConfiguration(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<SinglePropertyFilterConfiguration> OnToString_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnPreSave_SinglePropertyFilterConfiguration")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_SinglePropertyFilterConfiguration != null) OnPreSave_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnPreSave_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnPostSave_SinglePropertyFilterConfiguration")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_SinglePropertyFilterConfiguration != null) OnPostSave_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnPostSave_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnCreated_SinglePropertyFilterConfiguration")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_SinglePropertyFilterConfiguration != null) OnCreated_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnCreated_SinglePropertyFilterConfiguration;

        [EventBasedMethod("OnDeleting_SinglePropertyFilterConfiguration")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_SinglePropertyFilterConfiguration != null) OnDeleting_SinglePropertyFilterConfiguration(this);
        }
        public static event ObjectEventHandler<SinglePropertyFilterConfiguration> OnDeleting_SinglePropertyFilterConfiguration;

        #endregion // Kistl.Generator.Templates.ObjectClasses.DefaultMethods

        #region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(binStream, auxObjects, eagerLoadLists);
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader binStream)
        {
            var baseResult = base.FromStream(binStream);
            var result = new List<IPersistenceObject>();
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
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.Xml.XmlReader xml)
        {
            var baseResult = base.FromStream(xml);
            var result = new List<IPersistenceObject>();
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
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        public override void MergeImport(System.Xml.XmlReader xml)
        {
            base.MergeImport(xml);
            if (!CurrentAccessRights.HasReadRights()) return;
        }

        #endregion

    }
}