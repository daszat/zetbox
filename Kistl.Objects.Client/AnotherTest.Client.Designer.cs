
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

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AnotherTest")]
    public class AnotherTest__Implementation__ : BaseClientDataObject, AnotherTest
    {


		public override InterfaceType GetInterfaceType()
		{
			return new InterfaceType(typeof(AnotherTest));
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




#region Serializer


        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            base.ToStream(binStream);
        }

        public override void FromStream(System.IO.BinaryReader binStream)
        {
            base.FromStream(binStream);
        }

#endregion

    }


}