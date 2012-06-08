
namespace Zetbox.DalProvider.Ef.Tests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public class when_initializing
          : Zetbox.API.AbstractConsumerTests.optional_parent.when_initializing
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
