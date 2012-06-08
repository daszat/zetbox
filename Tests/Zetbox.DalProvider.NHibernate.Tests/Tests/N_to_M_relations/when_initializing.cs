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

namespace Zetbox.DalProvider.NHibernate.Tests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Test;
    using Zetbox.DalProvider.Base.RelationWrappers;
    using Zetbox.DalProvider.NHibernate;
    using NUnit.Framework;
    using Base = Zetbox.API.AbstractConsumerTests.N_to_M_relations;

    public class when_initializing : Base.when_initializing
    {
        [Test]
        public void should_be_of_proper_type()
        {
            Assert.That(aSide1.BSide, Is.TypeOf<NHibernateBSideCollectionWrapper<N_to_M_relations_A, N_to_M_relations_B, N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl>>());
            Assert.That(bSide1.ASide, Is.TypeOf<NHibernateASideCollectionWrapper<N_to_M_relations_A, N_to_M_relations_B, N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryNHibernateImpl>>());
        }

        public class and_reloading : when_initializing
        {
            public override void InitTestObjects()
            {
                base.InitTestObjects();
                SubmitAndReload();
            }
        }
    }
}
