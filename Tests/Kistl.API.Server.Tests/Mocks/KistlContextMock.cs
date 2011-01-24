using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server.Tests;
using Kistl.App.Base;
using Kistl.API.Configuration;

namespace Kistl.API.Server.Mocks
{
    public class KistlContextMock : BaseKistlDataContext, IFrozenContext
    {
        public Dictionary<int, TestObjClassImpl> TestObjClasses = new Dictionary<int, TestObjClassImpl>();
        public KistlContextMock(IMetaDataResolver metaDataResolver, Identity identity, KistlConfig config, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory)
            : base(metaDataResolver, identity, config, lazyCtx, iftFactory)
        {
            TestObjClasses[1] = new TestObjClassImpl() { ID = 1 };
            TestObjClasses[3] = new TestObjClassImpl() { ID = 3 };
            TestObjClasses[22] = new TestObjClassImpl() { ID = 22 };

            base.Attach(TestObjClasses[1]);
            base.Attach(TestObjClasses[3]);
            base.Attach(TestObjClasses[22]);

            TestObjClasses[1].BaseTestObjClass = null;
            TestObjClasses[1].StringProp = "some value (ID=1)";
            TestObjClasses[1].SubClasses.Add(TestObjClasses[3]);
            TestObjClasses[1].SubClasses.Add(TestObjClasses[22]);
            TestObjClasses[1].TestEnumProp = Kistl.API.Mocks.TestEnum.X;
            TestObjClasses[1].TestNames.Add("some name (ID=1,1)");

            TestObjClasses[3].BaseTestObjClass = TestObjClasses[1];
            TestObjClasses[3].StringProp = "some other value (ID=3)";
            TestObjClasses[3].TestEnumProp = Kistl.API.Mocks.TestEnum.Y;
            TestObjClasses[3].TestNames.Add("some other name (ID=3,1)");
            TestObjClasses[3].TestNames.Add("some other name (ID=3,1)");

            TestObjClasses[22].BaseTestObjClass = TestObjClasses[1];
            TestObjClasses[22].StringProp = "some test value (ID=22)";
            TestObjClasses[22].TestEnumProp = Kistl.API.Mocks.TestEnum.Y;
            TestObjClasses[22].TestNames.Add("some test name (ID=22,1)");
            TestObjClasses[22].TestNames.Add("some test name (ID=22,2)");

        }

        public override IQueryable<T> GetQuery<T>()
        {
            if (typeof(T) != typeof(TestObjClass))
            {
                throw new ArgumentOutOfRangeException("T");
            }

            return TestObjClasses.Values.Cast<T>().AsQueryable();
        }

        public override IQueryable<T> GetPersistenceObjectQuery<T>()
        {
            if (typeof(T) != typeof(TestObjClass))
            {
                throw new ArgumentOutOfRangeException("T");
            }

            return TestObjClasses.Values.Cast<T>().AsQueryable();
        }

        public override IPersistenceObject ContainsObject(InterfaceType type, int ID)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IPersistenceObject> AttachedObjects
        {
            get { throw new NotImplementedException(); }
        }

        public override int SubmitChanges()
        {
            throw new NotImplementedException();
        }

        public override int SubmitRestore()
        {
            throw new NotImplementedException();
        }

        public override IDataObject Find(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public override T Find<T>(int ID)
        {
            throw new NotImplementedException();
        }

        public override IList<T> FetchRelation<T>(Guid relationId, RelationEndRole role, IDataObject parent)
        {
            throw new NotImplementedException();
        }

        public override T FindPersistenceObject<T>(int ID)
        {
            throw new NotImplementedException();
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, int ID)
        {
            throw new NotImplementedException();
        }

        public override T FindPersistenceObject<T>(Guid exportGuid)
        {
            throw new NotImplementedException();
        }

        public override IPersistenceObject FindPersistenceObject(InterfaceType ifType, Guid exportGuid)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> FindPersistenceObjects<T>(IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IPersistenceObject> FindPersistenceObjects(InterfaceType ifType, IEnumerable<Guid> exportGuids)
        {
            throw new NotImplementedException();
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            if (ifType.Type == typeof(TestObjClass))
            {
                return new TestObjClassImpl();
            }
            else
            {
                return Activator.CreateInstance(ifType.Type);
            }
        }

        public override ImplementationType ToImplementationType(InterfaceType t)
        {
            return GetImplementationType(Type.GetType(t.Type.FullName + Kistl.API.Helper.ImplementationSuffix + "," + typeof(KistlContextMock).Assembly.FullName));
        }

        public override ImplementationType GetImplementationType(Type t)
        {
            return new ServerImplementationTypeMock(t, iftFactory);
        }

        public override void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public override void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public override void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        protected override int ExecGetSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        protected override int ExecGetContinuousSequenceNumber(Guid sequenceGuid)
        {
            throw new NotImplementedException();
        }

        protected override bool IsTransactionRunning
        {
            get { throw new NotImplementedException(); }
        }
    }
}
