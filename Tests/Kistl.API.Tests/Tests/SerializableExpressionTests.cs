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
using NUnit.Framework.SyntaxHelpers;


namespace Kistl.API.Tests
{
    [TestFixture]
    public class SerializableExpressionTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromExpression_Null_fails()
        {
            SerializableExpression.FromExpression(null);
        }

       
    }
}
