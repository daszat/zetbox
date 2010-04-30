using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Mocks
{
    public class TestCompoundObject : BaseServerCompoundObject
    {
        public TestCompoundObject()
        {
            var obj = new TestObjClass__Implementation__();
            base.AttachToObject(obj, "Test");
        }

        public int TestInt { get; set; }
        public string TestString { get; set; }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream(TestInt, sw);
            BinarySerializer.ToStream(TestString, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            int _tmpi;
            string _tmps;
            BinarySerializer.FromStream(out _tmpi, sr);
            BinarySerializer.FromStream(out _tmps, sr);
            TestInt = _tmpi;
            TestString = _tmps;
        }

        public override Type GetImplementedInterface()
        {
            return typeof(ICompoundObject);
        }
    }
}
