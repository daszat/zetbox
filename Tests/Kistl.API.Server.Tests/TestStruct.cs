using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Tests
{
    public class TestStruct : BaseServerStructObject
    {
        public int TestInt { get; set; }
        public string TestString { get; set; }

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToBinary(TestInt, sw);
            BinarySerializer.ToBinary(TestString, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            int _tmpi;
            string _tmps;
            BinarySerializer.FromBinary(out _tmpi, sr);
            BinarySerializer.FromBinary(out _tmps, sr);
            TestInt = _tmpi;
            TestString = _tmps;
        }
    }
}
