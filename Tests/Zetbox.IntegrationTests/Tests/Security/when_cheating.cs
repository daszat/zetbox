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

namespace Zetbox.IntegrationTests.Security
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.App.Base;
    using Zetbox.API;
    using Zetbox.App.Test;
    using NUnit.Framework;
    using Zetbox.API.Common;
    using Zetbox.Client.Presentables;
    using System.ServiceModel;

    public abstract class when_cheating : AbstractSecurityTest
    {
        [Test]
        [ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to modify an Object", MatchType = MessageMatch.StartsWith)]
        public virtual void should_fail_when_modified()
        {
            child2.SetPrivateFieldValue<Zetbox.API.AccessRights>("_currentAccessRights", Zetbox.API.AccessRights.Full);
            child2.SetPrivateFieldValue<DataObjectState>("_ObjectState", DataObjectState.Modified);
            child2.Name = "cheating";
            ctx.SubmitChanges();
            Reload();
        }

        [Test]
        [ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to delete this Object", MatchType = MessageMatch.StartsWith)]
        public virtual void should_fail_when_deleted()
        {
            child2.SetPrivateFieldValue<Zetbox.API.AccessRights>("_currentAccessRights", Zetbox.API.AccessRights.Full);
            child2.SetPrivateFieldValue<DataObjectState>("_ObjectState", DataObjectState.Deleted);
            ctx.SubmitChanges();
            Reload();
        }

        protected void base_should_not_send_when_no_rights()
        {
            child2.SetPrivateFieldValue<DataObjectState>("_ObjectState", DataObjectState.Modified);
            child2.SetPrivateFieldValue<string>("_Name", "cheating");
            ctx.SubmitChanges();
            Reload();
        }

        public class in_same_context : when_cheating
        {
            /// <summary>
            /// Rights are cached on the client. Changes on the server will not reflect after submit.
            /// </summary>
            [Test]
            [ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to modify an Object", MatchType = MessageMatch.StartsWith)]
            public void should_not_send_when_no_rights()
            {
                base.base_should_not_send_when_no_rights();
            }
        }

        public class when_reloading : when_cheating
        {
            public override void SetUp()
            {
                base.SetUp();
                base.Reload();
            }

            [Test]
            [ExpectedException(typeof(System.Security.SecurityException), UserMessage = "Inconsistent security/rights state detected", MatchType = MessageMatch.StartsWith)]
            public void should_not_send_when_no_rights()
            {
                base.base_should_not_send_when_no_rights();
            }
        }
    }
}
