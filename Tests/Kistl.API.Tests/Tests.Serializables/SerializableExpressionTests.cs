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

namespace Kistl.API.Tests.Serializables
{
    [TestFixture]
    public class SerializableExpressionTests
    {

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromExpression_null_fails()
        {
            SerializableExpression.FromExpression(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToExpression_null_fails()
        {
            SerializableExpression.ToExpression(null);
        }


    }
}
