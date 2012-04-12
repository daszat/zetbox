using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Server.Mocks
{
    public class TestCompoundObject : BaseServerCompoundObject
    {
        public TestCompoundObject()
            : base(null)
        {
            var obj = new TestObjClassImpl();
            base.AttachToObject(obj, "Test");
        }

        public int TestInt { get; set; }
        public string TestString { get; set; }

        public override void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            BinarySerializer.ToStream(TestInt, sw);
            BinarySerializer.ToStream(TestString, sw);
        }

        public override IEnumerable<IPersistenceObject> FromStream(System.IO.BinaryReader sr)
        {
            var baseResult = base.FromStream(sr);
            int _tmpi;
            string _tmps;
            BinarySerializer.FromStream(out _tmpi, sr);
            BinarySerializer.FromStream(out _tmps, sr);
            TestInt = _tmpi;
            TestString = _tmps;
            return baseResult;
        }

        public override Type GetImplementedInterface()
        {
            return typeof(ICompoundObject);
        }

        public override Guid CompoundObjectID
        {
            get { throw new NotImplementedException(); }
        }
    }
}
