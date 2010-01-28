using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

using NUnit.Framework;

namespace Kistl.API.AbstractConsumerTests.one_to_N_relations
{

    public abstract class should_obey_order
    {
        protected abstract IKistlContext GetContext();
        
        private static List<BaseParameter> CanonicalOrdering(IEnumerable<BaseParameter> input)
        {
            return input.OrderBy(p => p.IsReturnParameter)
                    .ThenBy(p => p.ParameterName).ToList();
        }

        [Test]
        public void when_ordering_items()
        {
            int methodID = Helper.INVALIDID;

            using (IKistlContext ctx = GetContext())
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

                Assert.That(ctx.SubmitChanges(), Is.GreaterThan(0));
            }

            using (IKistlContext ctx = GetContext())
            {
                var method = ctx.Find<Method>(methodID);

                var parameters = method.Parameter.ToList();
                Assert.That(parameters, Is.EquivalentTo(CanonicalOrdering(parameters)), "mismatch in retrieved parameters");
            }
        }

    }
}
