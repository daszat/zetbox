// <autogenerated/>


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

    using Kistl.DalProvider.Memory;

    /// <summary>
    /// Metadefinition Object for Bool Parameter.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("BoolParameter")]
    public class BoolParameter__Implementation__Memory : Kistl.App.Base.BaseParameter__Implementation__Memory, BoolParameter
    {
        [Obsolete]
        public BoolParameter__Implementation__Memory()
            : base(null)
        {
            {
            }
        }

        public BoolParameter__Implementation__Memory(Func<IReadOnlyKistlContext> lazyCtx)
            : base(lazyCtx)
        {
            {
            }
        }


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>
		[EventBasedMethod("OnGetParameterType_BoolParameter")]
		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_BoolParameter != null)
            {
                OnGetParameterType_BoolParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public static event GetParameterType_Handler<BoolParameter> OnGetParameterType_BoolParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>
		[EventBasedMethod("OnGetParameterTypeString_BoolParameter")]
		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_BoolParameter != null)
            {
                OnGetParameterTypeString_BoolParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public static event GetParameterTypeString_Handler<BoolParameter> OnGetParameterTypeString_BoolParameter;



        public override Type GetImplementedInterface()
        {
            return typeof(BoolParameter);
        }

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (BoolParameter)obj;
			var otherImpl = (BoolParameter__Implementation__Memory)obj;
			var me = (BoolParameter)this;

		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template
   		// Kistl.Server.Generators.Templates.Implementation.ObjectClasses.Tail

        [System.Diagnostics.DebuggerHidden()]
        [EventBasedMethod("OnToString_BoolParameter")]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_BoolParameter != null)
            {
                OnToString_BoolParameter(this, e);
            }
            return e.Result;
        }
        public static event ToStringHandler<BoolParameter> OnToString_BoolParameter;

        [EventBasedMethod("OnPreSave_BoolParameter")]
        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_BoolParameter != null) OnPreSave_BoolParameter(this);
        }
        public static event ObjectEventHandler<BoolParameter> OnPreSave_BoolParameter;

        [EventBasedMethod("OnPostSave_BoolParameter")]
        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_BoolParameter != null) OnPostSave_BoolParameter(this);
        }
        public static event ObjectEventHandler<BoolParameter> OnPostSave_BoolParameter;

        [EventBasedMethod("OnCreated_BoolParameter")]
        public override void NotifyCreated()
        {
            base.NotifyCreated();
            if (OnCreated_BoolParameter != null) OnCreated_BoolParameter(this);
        }
        public static event ObjectEventHandler<BoolParameter> OnCreated_BoolParameter;

        [EventBasedMethod("OnDeleting_BoolParameter")]
        public override void NotifyDeleting()
        {
            base.NotifyDeleting();
            if (OnDeleting_BoolParameter != null) OnDeleting_BoolParameter(this);
        }
        public static event ObjectEventHandler<BoolParameter> OnDeleting_BoolParameter;


	


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