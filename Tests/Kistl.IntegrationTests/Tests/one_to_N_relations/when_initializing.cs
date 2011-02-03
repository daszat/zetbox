
namespace Kistl.IntegrationTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public class when_initializing
          : Kistl.API.AbstractConsumerTests.one_to_N_relations.when_initializing
    {
        public class after_reloading
            : when_initializing
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
