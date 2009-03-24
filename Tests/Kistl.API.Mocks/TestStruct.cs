using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kistl.API.Mocks
{
    public interface TestStruct : IStruct
    {
        string TestProperty { get; set; }
    }

    public class TestStruct__Implementation__ : BaseStructObject, TestStruct
    {

        public void ToStream(BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream(TestProperty, sw);
        }

        public void FromStream(BinaryReader sr)
        {
            base.FromStream(sr);
            string _tmp;
            BinarySerializer.FromStream(out _tmp, sr);
            TestProperty = _tmp;
        }

        #region TestStruct Members

        public string TestProperty { get; set; }

        #endregion

        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(TestStruct));
        }
    }
}
