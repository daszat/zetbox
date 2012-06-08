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

namespace Zetbox.API.AbstractConsumerTests.PersistenceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public abstract class ObjectLoadFixture : AbstractTestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CreateObject();
            ctx = GetContext();
            obj = GetObject();
        }

        public override void TearDown()
        {
            if (ctx != null)
                ctx.Dispose();
            DestroyObjects();
            base.TearDown();
        }

        protected virtual void CreateObject()
        {
            using (var ctx = GetContext())
            {
                var newObject = ctx.Create<TestCustomObject>();
                newObject.Birthday = new DateTime(1960, 12, 24);
                newObject.PersonName = "Testname";
                ctx.SubmitChanges();
            }
        }

        protected virtual void DestroyObjects()
        {
            using (var ctx = GetContext())
            {
                ctx.GetQuery<TestCustomObject>().ForEach(obj => ctx.Delete(obj));
                ctx.SubmitChanges();
            }
        }

        protected virtual TestCustomObject GetObject()
        {
            return ctx.GetQuery<TestCustomObject>().First();
        }

        protected IZetboxContext ctx { get; private set; }
        protected TestCustomObject obj { get; private set; }
    }
}
