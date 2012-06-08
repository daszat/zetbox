using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Zetbox.API.Client;

using NUnit.Framework;
using NUnit.Framework.Constraints;
using Zetbox.App.Base;

namespace Zetbox.IntegrationTests
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
