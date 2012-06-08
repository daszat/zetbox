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
