using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Client.Mocks;
using Kistl.GUI;
using Kistl.GUI.DB;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Kistl.Client.Tests
{
    public abstract class PresenterTest<CONTROL, PRESENTER> 
        where CONTROL: IBasicControl
        where PRESENTER: Presenter
    {
        protected TestObject obj { get; set; }
        protected Visual visual { get; set; }
        protected ControlInfo cInfo { get; set; }
        protected PresenterInfo pInfo { get; set; }
        protected CONTROL widget { get; set; }
        protected PRESENTER presenter { get; set; }


        protected void Init(ControlInfo ci, BaseProperty bp)
        {
            visual = new Visual() { Name = ci.Control, Property = bp };
            cInfo = KistlGUIContext.FindControlInfo(Toolkit.TEST, visual);
            pInfo = KistlGUIContext.FindPresenterInfo(visual);
            widget = (CONTROL)KistlGUIContext.CreateControl(cInfo);
            presenter = (PRESENTER)KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);
        }

        [SetUp]
        public void SetUp()
        {
            obj = new TestObject();
        }

    }
}
