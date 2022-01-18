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

namespace Zetbox.Server.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Zetbox.API;
    using Zetbox.API.Server;
    using Zetbox.App.Base;

    [TestFixture]
    public class SequenceTests : AbstractServerTestFixture
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SequenceTests));

        private IZetboxContext ctx;

        public override void SetUp()
        {
            base.SetUp();
            ctx = GetContext();
        }

        [Test]
        public void should_GetNextNumber()
        {
            int num = ctx.GetSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestSequence.Guid);
            Assert.That(num, Is.GreaterThan(0));
        }
        [Test]
        public void should_GetContinuousSequenceNumber()
        {
            ctx.BeginTransaction();
            int num = ctx.GetContinuousSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestContinuousSequence.Guid);
            Assert.That(num, Is.GreaterThan(0));
            ctx.CommitTransaction();
        }

        [Test]
        public void should_GetNextNumber_twice_with_Submit()
        {
            int num = ctx.GetSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestSequence.Guid);
            Assert.That(num, Is.GreaterThan(0));
            ctx.SubmitChanges();
            int num2 = ctx.GetSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestSequence.Guid);
            Assert.That(num2, Is.GreaterThan(num));
        }
        [Test]
        public void should_GetNextNumber_twice_without_Submit()
        {
            int num = ctx.GetSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestSequence.Guid);
            Assert.That(num, Is.GreaterThan(0));

            int num2 = ctx.GetSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestSequence.Guid);
            Assert.That(num2, Is.GreaterThan(num));
        }

        [Test]
        public void should_fail_ContinuousSequenceNumber_without_transaction()
        {
            Assert.That(() => ctx.GetContinuousSequenceNumber(NamedObjects.Base.Sequences.TestModule.TestContinuousSequence.Guid), Throws.InvalidOperationException);
        }
    }
}
