
namespace Kistl.IntegrationTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public class when_resetting_one_side
        : Kistl.API.AbstractConsumerTests.one_to_N_relations.when_resetting_one_side
    {
        public class after_reloading
            : when_resetting_one_side
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
