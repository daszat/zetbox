// TODO: cannot reference Kistl.Objects.Client, but must!
//namespace Kistl.DalProvider.Client.Tests
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using Kistl.API;
//    using Kistl.API.Client;
//    using Kistl.API.Tests;
//    using Kistl.App.Projekte;
//    using NUnit.Framework;

//    [TestFixture(0)]
//    [TestFixture(1)]
//    [TestFixture(10)]
//    [TestFixture(50)]
//    public class NMRelationBSideTests
//        : BasicListTests<ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl>, Mitarbeiter>
//    {
//        private ProjektImpl parent;

//        public NMRelationBSideTests(int items)
//            : base(items) { }


//        protected override Mitarbeiter NewItem()
//        {
//            var result = ctx.Create<Mitarbeiter>();
//            result.Name = "item#" + result.ID;
//            return result;
//        }

//        protected override ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl> CreateCollection(List<Mitarbeiter> items)
//        {
//            parent = (ProjektImpl)ctx.Create<Projekt>();
//            parent.Name = "proj#" + parent.ID;
//            foreach (var item in items)
//            {
//                parent.Mitarbeiter.Add(item);
//            }
//            return (ClientRelationBSideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl>)parent.Mitarbeiter;
//        }
//    }

//    // TODO: Create Testclass with A-side collection
//    //[TestFixture(0)]
//    //[TestFixture(1)]
//    //[TestFixture(10)]
//    //[TestFixture(50)]
//    //public class NMRelationASideTests
//    //    : BasicListTests<ClientRelationASideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl>, Mitarbeiter>
//    //{
//    //    private ProjektImpl parent;

//    //    public NMRelationASideTests(int items)
//    //        : base(items) { }


//    //    protected override Mitarbeiter NewItem()
//    //    {
//    //        var result = ctx.Create<Mitarbeiter>();
//    //        result.Name = "item#" + result.ID;
//    //        return result;
//    //    }

//    //    protected override ClientRelationASideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl> CreateCollection(List<Mitarbeiter> items)
//    //    {
//    //        parent = (ProjektImpl)ctx.Create<Projekt>();
//    //        parent.Name = "proj#" + parent.ID;
//    //        foreach (var item in items)
//    //        {
//    //            parent.Mitarbeiter.Add(item);
//    //        }
//    //        return (ClientRelationASideListWrapper<Projekt, Mitarbeiter, Projekt_haben_Mitarbeiter_RelationEntryImpl>)parent.Mitarbeiter;
//    //    }
//    //}
//}
