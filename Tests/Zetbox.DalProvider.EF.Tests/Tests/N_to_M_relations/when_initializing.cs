
namespace Kistl.DalProvider.Ef.Tests.N_to_M_relations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Test;
    using Kistl.DalProvider.Base.RelationWrappers;
    using NUnit.Framework;
    using Base = Kistl.API.AbstractConsumerTests.N_to_M_relations;

    public class when_initializing : Base.when_initializing
    {
        [Test]
        public void should_be_of_proper_type()
        {
            Assert.That(aSide1.BSide, Is.TypeOf<BSideCollectionWrapper<N_to_M_relations_A, N_to_M_relations_B, N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl, EntityCollection<N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl>>>());
            Assert.That(bSide1.ASide, Is.TypeOf<ASideCollectionWrapper<N_to_M_relations_A, N_to_M_relations_B, N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl, EntityCollection<N_to_M_relations_A_connectsTo_N_to_M_relations_B_RelationEntryEfImpl>>>());
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
