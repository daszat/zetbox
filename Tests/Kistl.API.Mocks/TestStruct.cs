using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public interface TestCompoundObject : ICompoundObject
    {
        string TestProperty { get; set; }
    }

    public class TestCompoundObject__Implementation__ : BaseCompoundObject, TestCompoundObject
    {

        public override void ToStream(BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream(TestProperty, sw);
        }

        public override void FromStream(BinaryReader sr)
        {
            base.FromStream(sr);
            string _tmp;
            BinarySerializer.FromStream(out _tmp, sr);
            TestProperty = _tmp;
        }

        #region TestCompoundObject Members

        public string TestProperty { get; set; }

        #endregion

        public override Type GetImplementedInterface()
        {
            return typeof(TestCompoundObject);
        }
    }
}
