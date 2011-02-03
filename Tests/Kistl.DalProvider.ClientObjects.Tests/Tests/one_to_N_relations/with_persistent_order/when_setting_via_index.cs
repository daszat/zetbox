
namespace Kistl.DalProvider.Client.Tests.one_to_N_relations.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public class when_setting_via_index
        : Kistl.API.AbstractConsumerTests.one_to_N_relations.with_persistent_order.when_setting_via_index
    {
        public class after_reloading
          : when_setting_via_index
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
