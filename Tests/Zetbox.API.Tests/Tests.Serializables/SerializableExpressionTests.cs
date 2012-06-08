
namespace Kistl.API.Tests.Serializables
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
