using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server.Tests;

namespace Kistl.API.Server.Mocks
{
    public class KistlContextMock : BaseKistlDataContext
    {

        public readonly static Dictionary<int, TestObjClass__Implementation__> TestObjClasses = new Dictionary<int, TestObjClass__Implementation__>();
        static KistlContextMock()
        {
            TestObjClasses[1] = new TestObjClass__Implementation__() { ID = 1 };
            TestObjClasses[3] = new TestObjClass__Implementation__() { ID = 3 };
            TestObjClasses[22] = new TestObjClass__Implementation__() { ID = 22 };

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

        public override IQueryable<IDataObject> GetQuery(Type type)
        {
            if (type != typeof(TestObjClass))
            {
                throw new ArgumentOutOfRangeException("type");
            }

            return TestObjClasses.Values.Cast<IDataObject>().AsQueryable();
        }

        public override IPersistenceObject ContainsObject(Type type, int ID)
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

        public override IDataObject Find(Type type, int ID)
        {
            throw new NotImplementedException();
        }

        public override T Find<T>(int ID)
        {
            throw new NotImplementedException();
        }

        public override ICollection<T> FetchRelation<A, B, T>(RelationEndRole role, IDataObject parent)
        {
            throw new NotImplementedException();
        }
    }
}
