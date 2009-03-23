using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;

using NUnit.Framework;

namespace Kistl.IntegrationTests.one_to_N_relations
{
    public class should_obey_order
    {
        private static List<BaseParameter> CanonicalOrdering(IEnumerable<BaseParameter> input)
        {
            return input.OrderBy(p => p.IsReturnParameter)
                    .ThenBy(p => p.ParameterName).ToList();
        }

        [Test]
        public void when_ordering_items()
        {
            int methodID = Helper.INVALIDID;

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var method = ctx.GetQuery<Method>().ToList().Where(m => m.Module.ModuleName == "Projekte")
                    .OrderByDescending(m => m.Parameter.Count).First();

                // needs more than one Parameter to test ordering
                Assume.That(method.Parameter.Count, Is.GreaterThan(1));

                methodID = method.ID;

                var parameters = method.Parameter.ToList();
                method.Parameter.Clear();

                foreach (BaseParameter p in CanonicalOrdering(parameters))
                {
                    method.Parameter.Add(p);
                }

                ctx.SubmitChanges();
            }

            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var method = ctx.Find<Method>(methodID);

                var parameters = method.Parameter.ToList();
                Assert.That(parameters, Is.EquivalentTo(CanonicalOrdering(parameters)), "mismatch in retrieved parameters");
            }
        }

    }
}
