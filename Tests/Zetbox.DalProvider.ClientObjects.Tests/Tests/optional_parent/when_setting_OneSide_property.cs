// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.DalProvider.Client.Tests.optional_parent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Test;

    using NUnit.Framework;

    public class when_setting_OneSide_property
        : Zetbox.API.AbstractConsumerTests.optional_parent.when_setting_OneSide_property
    {
        public class after_reloading
            : when_setting_OneSide_property
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
        public class and_reloading
            : when_setting_OneSide_property
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
