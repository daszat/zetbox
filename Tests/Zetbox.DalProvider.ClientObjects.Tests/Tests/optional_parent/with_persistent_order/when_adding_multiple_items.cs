
namespace Zetbox.DalProvider.Client.Tests.optional_parent.with_persistent_order
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    public class when_adding_multiple_items
        : Zetbox.API.AbstractConsumerTests.optional_parent.with_persistent_order.when_adding_multiple_items
    {
        public class after_reloading
            : when_adding_multiple_items
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
