
namespace Zetbox.DalProvider.Client.Tests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public class when_resetting_one_side
        : Zetbox.API.AbstractConsumerTests.optional_parent.when_resetting_one_side
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
