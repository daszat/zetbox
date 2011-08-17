
namespace Kistl.IntegrationTests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public class when_initializing
          : Kistl.API.AbstractConsumerTests.optional_parent.when_initializing
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
