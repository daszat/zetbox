// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Common;
using Zetbox.API.Configuration;
using Zetbox.API.Server.Tests;
using Zetbox.App.Base;

namespace Zetbox.API.Server.Mocks
{
    public class ZetboxContextMock : BaseZetboxDataContext, IFrozenContext
    {
        public Dictionary<int, TestObjClassImpl> TestObjClasses = new Dictionary<int, TestObjClassImpl>();
        public ZetboxContextMock(IMetaDataResolver metaDataResolver, Identity identity, ZetboxConfig config, Func<IFrozenContext> lazyCtx, InterfaceType.Factory iftFactory)
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
            TestObjClasses[1].TestEnumProp = Zetbox.API.Mocks.TestEnum.X;
            TestObjClasses[1].TestNames.Add("some name (ID=1,1)");

            TestObjClasses[3].BaseTestObjClass = TestObjClasses[1];
            TestObjClasses[3].StringProp = "some other value (ID=3)";
            TestObjClasses[3].TestEnumProp = Zetbox.API.Mocks.TestEnum.Y;
            TestObjClasses[3].TestNames.Add("some other name (ID=3,1)");
            TestObjClasses[3].TestNames.Add("some other name (ID=3,1)");

            TestObjClasses[22].BaseTestObjClass = TestObjClasses[1];
            TestObjClasses[22].StringProp = "some test value (ID=22)";
            TestObjClasses[22].TestEnumProp = Zetbox.API.Mocks.TestEnum.Y;
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
            return GetImplementationType(Type.GetType(t.Type.FullName + Zetbox.API.Helper.ImplementationSuffix + "," + typeof(ZetboxContextMock).Assembly.FullName));
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
            // Allways allowed
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
