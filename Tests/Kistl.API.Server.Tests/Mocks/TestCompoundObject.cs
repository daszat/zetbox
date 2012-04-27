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

        public override void ToStream(KistlStreamWriter sw, HashSet<IStreamable> auxObjects, bool eagerLoadLists)
        {
            base.ToStream(sw, auxObjects, eagerLoadLists);
            sw.Write(TestInt);
            sw.Write(TestString);
        }

        public override IEnumerable<IPersistenceObject> FromStream(KistlStreamReader sr)
        {
            var baseResult = base.FromStream(sr);
            TestInt = sr.ReadInt32();
            TestString = sr.ReadString();
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
