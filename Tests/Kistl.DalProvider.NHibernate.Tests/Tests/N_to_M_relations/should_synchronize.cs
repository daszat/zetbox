
namespace Kistl.DalProvider.NHibernate.Tests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Test;
    using NUnit.Framework;
    using Base = Kistl.API.AbstractConsumerTests.N_to_M_relations;

    public class should_synchronize : Base.should_synchronize
    {
        // TODO: remove this after case 2115 is fixed
        [Test]
        public void when_deleting_items_without_workaround()
        {
            aSide1.BSide.Add(bSide1);
            SubmitAndReload();

            ctx.Delete(aSide1);
            ctx.Delete(bSide1);

            ctx.SubmitChanges();
        }
    }
}
