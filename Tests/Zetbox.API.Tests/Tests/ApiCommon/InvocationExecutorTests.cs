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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Zetbox.API.Mocks;

using Autofac;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Zetbox.API.AbstractConsumerTests;
using Zetbox.App.Base;
using Zetbox.API.Common;
using System.Threading.Tasks;

namespace Zetbox.API.Tests.InvocationExecutorTests
{
    public class InvocationExecutorTestsMockImplementor
    {
        public static bool WasCalled { get; set; }

        public void SimpleMethod()
        {
            WasCalled = true;
        }

        public bool MethodWithRetParam()
        {
            WasCalled = true;
            return true;
        }

        public bool MethodWithParams(object obj, string str)
        {
            WasCalled = true;
            return true;
        }

        public bool MethodWithOverloadedParams(object obj, int integer)
        {
            WasCalled = true;
            return true;
        }

        public bool MethodWithOverloadedParams(object obj, double dbl)
        {
            WasCalled = true;
            return true;
        }
    }

    public class IInvocationMock : IInvocation
    {
        public IInvocationMock(string member)
        {
            this.MemberName = member;
        }

        public string ImplementorName
        {
            get
            {
                return "Zetbox.API.Tests.InvocationExecutorTests.InvocationExecutorTestsMockImplementor, Zetbox.API.Tests";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string MemberName
        {
            get; set;
        }

        public Task<string> GetCodeTemplate()
        {
            throw new NotImplementedException();
        }
    }

    public class InvocationExecutorTests
    {
        [TestFixture]
        public class when_calling_HasValidInvocation : AbstractTestFixture
        {
            protected IInvocationExecutor executor;

            public override void SetUp()
            {
                base.SetUp();

                executor = scope.Resolve<IInvocationExecutor>();
            }

            [Test]
            public void is_valid_on_ExistingMethod(
                [Values("SimpleMethod", "MethodWithRetParam", "MethodWithParams")]
                string method)
            {
                Assert.That(executor.HasValidInvocation(new IInvocationMock(method)), Is.True);
            }

            [Test]
            public void is_invalid_on_MissingMethod()
            {
                Assert.That(executor.HasValidInvocation(new IInvocationMock("MissingMethod")), Is.False);
            }

            [Test]
            public void fail_on_SimpleMethodWithOverloadedParams()
            {
                Assert.That(() => executor.HasValidInvocation(new IInvocationMock("MethodWithOverloadedParams")), Throws.TypeOf<AmbiguousMatchException>());
            }
        }

        [TestFixture]
        public class when_calling_CallInvocation : AbstractTestFixture
        {
            protected IInvocationExecutor executor;

            public override void SetUp()
            {
                base.SetUp();

                executor = scope.Resolve<IInvocationExecutor>();
                InvocationExecutorTestsMockImplementor.WasCalled = false;
            }

            private delegate System.Threading.Tasks.Task SimpleDelegate();
            [Test]
            public void is_valid_on_SimpleMethod()
            {
                executor.CallInvocation(new IInvocationMock("SimpleMethod"), typeof(SimpleDelegate));
                Assert.That(InvocationExecutorTestsMockImplementor.WasCalled, Is.True);
            }

            private delegate bool MethodWithRetParamDelegate();
            [Test]
            public void is_valid_on_MethodWithRetParam()
            {
                executor.CallInvocation(new IInvocationMock("MethodWithRetParam"), typeof(MethodWithRetParamDelegate));
                Assert.That(InvocationExecutorTestsMockImplementor.WasCalled, Is.True);
            }

            private delegate bool MethodWithParamsDelegate(object obj, string str);
            [Test]
            public void is_valid_on_MethodWithParams()
            {
                executor.CallInvocation(new IInvocationMock("MethodWithParams"), typeof(MethodWithParamsDelegate), new object(), "foo");
                Assert.That(InvocationExecutorTestsMockImplementor.WasCalled, Is.True);
            }

            private delegate bool MethodWithWrongParamsDelegate(object obj, int integer);
            [Test]
            public void is_valid_on_MethodWithWrongParams()
            {
                Assert.That(() => executor.CallInvocation(new IInvocationMock("MethodWithParams"), typeof(MethodWithWrongParamsDelegate)), Throws.InvalidOperationException);
            }
        }
    }
}
