
namespace Kistl.DalProvider.NHibernate.Tests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Test;
    using Kistl.DalProvider.Base.RelationWrappers;
    using NUnit.Framework;
    using Base = Kistl.API.AbstractConsumerTests.N_to_M_relations;

    public static class when_adding
    {
        public class on_A_side
            : Base.when_adding.on_A_side
        {
        }
        public class on_B_side
            : Base.when_adding.on_B_side
        {
        }
    }
}
