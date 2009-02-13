
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

    using Kistl.DalProvider.Frozen;

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("AnotherTest")]
    public class AnotherTest__Implementation__ : BaseFrozenDataObject, AnotherTest
    {


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


        internal AnotherTest__Implementation__(FrozenContext ctx, int id)
            : base(ctx, id)
        { }


#region Serializer

        public override void ToStream(System.IO.BinaryWriter binStream)
        {
            throw new NotImplementedException();
        }
        public override void FromStream(System.IO.BinaryReader binStream)
        {
            throw new NotImplementedException();
        }

#endregion

    }


}