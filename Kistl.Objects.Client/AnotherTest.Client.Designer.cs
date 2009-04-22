
namespace Kistl.App.Test
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

    using Kistl.API.Client;
    using Kistl.DalProvider.ClientObjects;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AnotherTest")]
    public class AnotherTest__Implementation__ : BaseClientDataObject_ClientObjects, AnotherTest
    {
    
		public AnotherTest__Implementation__()
		{
            {
            }
        }


		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(AnotherTest));
		}

		public override void ApplyChangesFrom(IPersistenceObject obj)
		{
			base.ApplyChangesFrom(obj);
			var other = (AnotherTest)obj;
			var otherImpl = (AnotherTest__Implementation__)obj;
			var me = (AnotherTest)this;

		}

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
		}

        // tail template

        [System.Diagnostics.DebuggerHidden()]
        public override string ToString()
        {
            MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_AnotherTest != null)
            {
                OnToString_AnotherTest(this, e);
            }
            return e.Result;
        }
        public event ToStringHandler<AnotherTest> OnToString_AnotherTest;

        public override void NotifyPreSave()
        {
            base.NotifyPreSave();
            if (OnPreSave_AnotherTest != null) OnPreSave_AnotherTest(this);
        }
        public event ObjectEventHandler<AnotherTest> OnPreSave_AnotherTest;

        public override void NotifyPostSave()
        {
            base.NotifyPostSave();
            if (OnPostSave_AnotherTest != null) OnPostSave_AnotherTest(this);
        }
        public event ObjectEventHandler<AnotherTest> OnPostSave_AnotherTest;



		public override void UpdateParent(string propertyName, int? id)
		{
			switch(propertyName)
			{
				default:
					base.UpdateParent(propertyName, id);
					break;
			}
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