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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using NUnit.Framework;

using Autofac;

namespace Zetbox.API.Tests.Serializables
{

    [TestFixture]
    public class ConstantExpressions : AbstractApiTestFixture
    {

        public interface ISomething { }
        public class Something : ISomething { }

        [Test]
        public void roundtrip_Constant_string()
        {
            ConstantExpression expr = Expression.Constant("a");
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr, iftFactory));

            AssertExpressions.AreEqual(result, expr);
        }

        [Test]
        public void roundtrip_Constant_object()
        {
            ISomething value = new Something();
            ConstantExpression expr = Expression.Constant(value);
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr, iftFactory));

            AssertExpressions.AreEqual(result, expr);
        }

        [Test]
        public void roundtrip_Constant_interface()
        {
            ISomething value = new Something();
            ConstantExpression expr = Expression.Constant(value, typeof(ISomething));
            var result = (ConstantExpression)SerializableExpression.ToExpression(SerializableExpression.FromExpression(expr, iftFactory));

            AssertExpressions.AreEqual(result, expr);
            Assert.That(result.Type, Is.EqualTo(typeof(ISomething)));
        }
    }
}
