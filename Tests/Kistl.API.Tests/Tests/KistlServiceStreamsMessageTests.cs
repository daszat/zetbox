using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using System.Reflection;
using System.IO;
using System.Linq.Expressions;
using System.Collections.ObjectModel;


namespace Kistl.API.Tests
{
    [TestFixture]
    public class KistlServiceStreamsMessageTests
    {
        [Test]
        public void Stream()
        {
            KistlServiceStreamsMessage msg = new KistlServiceStreamsMessage();
            msg.ID = 10;
            msg.Property = "TestProperty";
            msg.Type = new SerializableType(typeof(TestDataObject));
            msg.Filter = SerializableExpression.FromExpression(Expression.Constant(true));
            msg.OrderBy = SerializableExpression.FromExpression(Expression.Constant("Test"));

            MemoryStream ms = msg.ToStream();
            ms.Seek(0, SeekOrigin.Begin);
            KistlServiceStreamsMessage result = new KistlServiceStreamsMessage(ms);

            Assert.That(result.ID, Is.EqualTo(msg.ID));
            Assert.That(result.Property, Is.EqualTo(msg.Property));
            Assert.That(result.Type, Is.EqualTo(msg.Type));
        }
    }
}
