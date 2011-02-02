
namespace Kistl.DalProvider.Client.Tests.N_to_M_relations
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

    public class when_initializing : Base.when_initializing
    {
        public class and_reloading : when_initializing
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
