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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using Autofac;
    using NUnit.Framework;
    using Zetbox.API;
    using Zetbox.API.AbstractConsumerTests;
    using Zetbox.API.Common;
    using Zetbox.App.Base;
    using Zetbox.App.Test;
    using Zetbox.Client.Presentables;
    using System.IO;

    public abstract class when_cheating : AbstractSecurityTest
    {
        [Test]
        // TODO: fix Http/Wcf Exception mismatch
        //[ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to modify an Object", MatchType = MessageMatch.StartsWith)]
        public virtual void should_fail_when_modified()
        {
            child2.SetPrivateFieldValue<Zetbox.API.AccessRights>("_currentAccessRights", Zetbox.API.AccessRights.Full);
            child2.SetPrivateFieldValue<DataObjectState>("_ObjectState", DataObjectState.Modified);
            child2.Name = "cheating";
            Assert.That(() => ctx.SubmitChanges(), Throws.InstanceOf<FaultException>().Or.InstanceOf<IOException>());
            Reload();
        }

        [Test]
        // TODO: fix Http/Wcf Exception mismatch
        //[ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to delete this Object", MatchType = MessageMatch.StartsWith)]
        public virtual void should_fail_when_deleted()
        {
            child2.SetPrivateFieldValue<Zetbox.API.AccessRights>("_currentAccessRights", Zetbox.API.AccessRights.Full);
            child2.SetPrivateFieldValue<DataObjectState>("_ObjectState", DataObjectState.Deleted);
            Assert.That(() => ctx.SubmitChanges(), Throws.InstanceOf<FaultException>().Or.InstanceOf<IOException>());
            Reload();
        }

        public virtual void should_not_send_when_no_rights()
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
            // TODO: fix Http/Wcf Exception mismatch
            //[ExpectedException(typeof(FaultException), UserMessage = "The current identity has no rights to modify an Object", MatchType = MessageMatch.StartsWith)]
            public override void should_not_send_when_no_rights()
            {
                Assert.That(() => base.should_not_send_when_no_rights(), Throws.InstanceOf<FaultException>().Or.InstanceOf<IOException>());
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
            // TODO: fix Http/Wcf Exception mismatch
            //[ExpectedException(typeof(System.Security.SecurityException), UserMessage = "Inconsistent security/rights state detected", MatchType = MessageMatch.StartsWith)]
            public override void should_not_send_when_no_rights()
            {
                Assert.That(() => base.should_not_send_when_no_rights(), Throws.InstanceOf<System.Security.SecurityException>());
            }
        }
    }
}
