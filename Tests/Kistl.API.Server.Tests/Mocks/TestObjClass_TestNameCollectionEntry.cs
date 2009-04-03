using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kistl.API.Server.Mocks
{

    public class TestNameCollectionWrapper
        : CollectionBSideWrapper<TestObjClass, string, TestObjClass_TestNameCollectionEntry__Implementation__, List<TestObjClass_TestNameCollectionEntry__Implementation__>>
    {
        public TestNameCollectionWrapper(TestObjClass__Implementation__ parent, List<TestObjClass_TestNameCollectionEntry__Implementation__> baselist)
            : base(parent, baselist)
        {
        }

        protected override TestObjClass_TestNameCollectionEntry__Implementation__ CreateEntry(object item)
        {
            return new TestObjClass_TestNameCollectionEntry__Implementation__();
        }
    }

    public class TestObjClass_TestNameCollectionEntry__Implementation__ : BaseServerCollectionEntry, INewCollectionEntry<TestObjClass, string>
    {
        public override int RelationID { get { return -1; } }

        /// <summary>
        /// returns the most specific implemented data object interface
        /// </summary>
        /// <returns></returns>
        public override InterfaceType GetInterfaceType()
        {
            return new InterfaceType(typeof(INewCollectionEntry<TestObjClass, string>));
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

        #region INewCollectionEntry<TestObjClass,string> Members

        public TestObjClass A { get; set; }

        public string B { get; set; }

        #endregion

        public override void ToStream(System.IO.BinaryWriter sw)
        {
            base.ToStream(sw);
            BinarySerializer.ToStream(B, sw);
        }

        public override void FromStream(System.IO.BinaryReader sr)
        {
            base.FromStream(sr);
            string s;
            BinarySerializer.FromStream(out s, sr);
            B = s;
        }
    }
}
