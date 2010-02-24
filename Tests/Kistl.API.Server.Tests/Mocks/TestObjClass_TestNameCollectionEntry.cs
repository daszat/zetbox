using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.API.Server.Mocks
{

    public class TestNameCollectionWrapper
        : ValueCollectionWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntry__Implementation__, List<TestObjClass_TestNameCollectionEntry__Implementation__>>
    {
        public TestNameCollectionWrapper(IKistlContext ctx, TestObjClass__Implementation__ parent, List<TestObjClass_TestNameCollectionEntry__Implementation__> baselist)
            : base(ctx, parent, null, baselist)
        {
        }

        protected override TestObjClass_TestNameCollectionEntry__Implementation__ CreateEntry()
        {
            return new TestObjClass_TestNameCollectionEntry__Implementation__();
        }
    }

    public interface TestObjClass_TestNameCollectionEntry
        : IValueCollectionEntry<TestObjClass, string>
    {
    }

    public class TestObjClass_TestNameCollectionEntry__Implementation__
        : BaseServerCollectionEntry, TestObjClass_TestNameCollectionEntry
    {
        /// <summary>
        /// returns the most specific implemented data object interface
        /// </summary>
        /// <returns></returns>
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(IValueCollectionEntry<TestObjClass, string>));
        }

        public override int ID { get; set; }

        protected override void SetModified()
        {

        }

        public override DataObjectState ObjectState
        {
            get { throw new NotImplementedException(); }
        }


        public override bool IsAttached { get { return _IsAttached; } }
        private bool _IsAttached = false;

        public override void AttachToContext(IKistlContext ctx)
        {
            base.AttachToContext(ctx);
            _IsAttached = true;
        }

        public override void DetachFromContext(IKistlContext ctx)
        {
            base.DetachFromContext(ctx);
            _IsAttached = false;
        }

        #region IValueCollectionEntry<TestObjClass,string> Members

        public TestObjClass Parent { get; set; }
        public IDataObject ParentObject { get; set; }

        public string Value { get; set; }
        public object ValueObject { get; set; }

        #endregion

        public override void ToStream(System.IO.BinaryWriter sw, HashSet<IStreamable> auxObjects)
        {
            base.ToStream(sw, auxObjects);
            BinarySerializer.ToStream(Value, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            string s;
            BinarySerializer.FromStream(out s, sr);
            Value = s;
        }
    }
}
