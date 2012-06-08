using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Zetbox.Client.Mocks;
using Zetbox.Client.Presentables;

using NUnit.Framework;

namespace Zetbox.Client.Tests
{
    //[TestFixture]
    //public class PresenterModelTests : MockeryTestFixture
    //{
        //internal class ViewModelInspector : ViewModel
        //{
        //    internal ViewModelInspector(ThreadManagerMock uiThreadMock, ThreadManagerMock backgroundThreadMock)
        //        : base(uiThreadMock, backgroundThreadMock, null, null, null)
        //    {
        //    }

        //    internal IThreadManager GetUitm() { return UI; }
        //    internal IThreadManager GetBgtm() { return Async; }

        //    internal void SetState(ModelState s) { State = s; }
        //}

        //[Test]
        //public void CreatePresenterModel()
        //{
        //    ThreadManagerMock uiThreadMock = new ThreadManagerMock("UI Thread");
        //    ThreadManagerMock backgroundThreadMock = new ThreadManagerMock("Background Thread");
        //    ThreadManagerMock.SetDefaultThread(uiThreadMock);

        //    ViewModelInspector pm = new ViewModelInspector(uiThreadMock, backgroundThreadMock);

        //    Assert.AreSame(uiThreadMock, pm.GetUitm());
        //    Assert.AreSame(backgroundThreadMock, pm.GetBgtm());

        //    pm.SetState(ModelState.Active);

        //    int stateChangeCount = 0;
        //    PropertyChangedEventHandler expectStateChanged = delegate(object sender, PropertyChangedEventArgs args)
        //    {
        //        Assert.AreEqual("State", args.Name);
        //        stateChangeCount += 1;
        //    };

        //    pm.PropertyChanged += expectStateChanged;
        //    pm.SetState(ModelState.Loading);
        //    pm.SetState(ModelState.Loading);
        //    pm.SetState(ModelState.Invalid);
        //    pm.SetState(ModelState.Invalid);
        //    pm.SetState(ModelState.Active);
        //    pm.SetState(ModelState.Active);
        //    // test whether removing works too
        //    pm.PropertyChanged -= expectStateChanged;
        //    pm.SetState(ModelState.Loading);
        //    pm.SetState(ModelState.Invalid);

        //    Assert.AreEqual(3, stateChangeCount);
        //    Assert.AreEqual(ModelState.Invalid, pm.State);
        //}

    //}
}
