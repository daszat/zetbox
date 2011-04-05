
namespace Kistl.API.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public interface TestCompoundObject : ICompoundObject
    {
        string TestProperty { get; set; }
    }

    public class TestCompoundObjectImpl : BaseCompoundObject, TestCompoundObject
    {
        public TestCompoundObjectImpl()
            : base(null)
        {
        }

        public override void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
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

        public override void ApplyChangesFrom(ICompoundObject other)
        {
            throw new NotImplementedException();
        }
    }
}
