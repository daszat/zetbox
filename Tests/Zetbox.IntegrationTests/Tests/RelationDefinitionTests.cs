using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using Kistl.App.Base;

namespace Kistl.IntegrationTests
{
    [TestFixture]
    public class RelationDefinitionTests : AbstractIntegrationTestFixture
    {
        [Test]
        public void CalculateParent()
        {
            using (var ctx = GetContext())
            {
                foreach (var relEnd in ctx.GetQuery<RelationEnd>())
                {
                    Assert.That(relEnd.Parent, Is.SameAs(relEnd.AParent ?? relEnd.BParent));
                }
            }
        }
    }
}
