
namespace Zetbox.DalProvider.Client.Tests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Base.RelationWrappers;
    using NUnit.Framework;
    using Base = Zetbox.API.AbstractConsumerTests.N_to_M_relations;

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
