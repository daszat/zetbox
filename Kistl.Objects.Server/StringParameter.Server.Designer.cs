
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
    /// Metadefinition Object for String Parameter.
    /// </summary>
    [EdmEntityType(NamespaceName="Model", Name="StringParameter")]
    [System.Diagnostics.DebuggerDisplay("StringParameter")]
    public class StringParameter__Implementation__ : Kistl.App.Base.BaseParameter__Implementation__, StringParameter
    {
    
		public StringParameter__Implementation__()
		{
            {
            }
        }


        /// <summary>
        /// Returns the resulting Type of this Method-Parameter Meta Object.
        /// </summary>

		public override System.Type GetParameterType() 
        {
            var e = new MethodReturnEventArgs<System.Type>();
            if (OnGetParameterType_StringParameter != null)
            {
                OnGetParameterType_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterType();
            }
            return e.Result;
        }
		public event GetParameterType_Handler<StringParameter> OnGetParameterType_StringParameter;



        /// <summary>
        /// Returns the String representation of this Method-Parameter Meta Object.
        /// </summary>

		public override string GetParameterTypeString() 
        {
            var e = new MethodReturnEventArgs<string>();
            if (OnGetParameterTypeString_StringParameter != null)
            {
                OnGetParameterTypeString_StringParameter(this, e);
            }
            else
            {
                e.Result = base.GetParameterTypeString();
            }
            return e.Result;
        }
		public event GetParameterTypeString_Handler<StringParameter> OnGetParameterTypeString_StringParameter;



		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(StringParameter));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (StringParameter)obj;
			var otherImpl = (StringParameter__Implementation__)obj;
			var me = (StringParameter)this;

		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_StringParameter != null)
            {
                OnToString_StringParameter(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<StringParameter> OnToString_StringParameter;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_StringParameter != null) OnPreSave_StringParameter(this);
        }
        public event ObjectEventHandler<StringParameter> OnPreSave_StringParameter;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_StringParameter != null) OnPostSave_StringParameter(this);
        }
        public event ObjectEventHandler<StringParameter> OnPostSave_StringParameter;



		public override void ReloadReferences()
		{
			base.ReloadReferences();
			
			// fix direct object references
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