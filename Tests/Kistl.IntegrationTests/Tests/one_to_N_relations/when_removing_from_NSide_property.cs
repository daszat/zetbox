
namespace Kistl.IntegrationTests.one_to_N_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Test;

    using NUnit.Framework;

    public class when_removing_from_NSide_property
        : Kistl.API.AbstractConsumerTests.one_to_N_relations.when_removing_from_NSide_property
    {
        public class after_reloading
            : when_removing_from_NSide_property
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
        public class and_reloading
            : when_removing_from_NSide_property
        {
            protected override void DoModification()
            {
                base.DoModification();
                SubmitAndReload();
            }
            protected override DataObjectState GetExpectedModifiedState()
            {
                return DataObjectState.Unmodified; // after submit changes, all objects should be unmodified
            }
        }
    }
}
