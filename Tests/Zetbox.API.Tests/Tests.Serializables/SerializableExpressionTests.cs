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

namespace Zetbox.API.Tests.Serializables
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Text;

	using NUnit.Framework;
	using NUnit.Framework.Constraints;
    using Autofac;

	[TestFixture]
    public class SerializableExpressionTests : AbstractApiTestFixture
	{
		[Test]
		public void FromExpression_null_fails()
		{
            Assert.That(() => SerializableExpression.FromExpression(null, iftFactory), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void ToExpression_null_fails()
		{
            Assert.That(() => SerializableExpression.ToExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}
	}
}
