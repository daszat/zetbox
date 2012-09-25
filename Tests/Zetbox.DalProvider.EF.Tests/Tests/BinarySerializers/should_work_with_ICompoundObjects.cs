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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

using Zetbox.API;
using Zetbox.API.Server;
using Zetbox.App.Test;
using Zetbox.DalProvider.Ef;

using NUnit.Framework;

namespace Zetbox.DalProvider.Ef.Tests.BinarySerializers
{
    public interface CompoundObjectMock : ICompoundObject
    {
    }

    public class CompoundObjectMockImpl : BaseServerCompoundObject_EntityFramework
    {
        public CompoundObjectMockImpl() : base(null) { }
        public override Type GetImplementedInterface()
        {
            return typeof(CompoundObjectMock);
        }

        public override Guid CompoundObjectID
        {
            get { throw new NotImplementedException(); }
        }
    }

    [TestFixture(typeof(CompoundObjectMockImpl))]
    [TestFixture(typeof(TestPhoneCompoundObjectEfImpl))]
    public class should_work_with_EFCompoundObjects<T>
        : Zetbox.API.AbstractConsumerTests.BinarySerializers.should_work_with_ICompoundObjects<T>
        where T : class, ICompoundObject, new()
    {
    }
}
