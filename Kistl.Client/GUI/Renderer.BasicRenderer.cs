using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.GUI.DB;
using Kistl.API.Client;

namespace Kistl.GUI.Renderer
{
    public abstract class BasicRenderer<CONTROL, PROPERTY, CONTAINER>
        where PROPERTY : CONTROL
        where CONTAINER : CONTROL
    {
        /// <summary>
        /// Show the specified object to the User
        /// </summary>
        /// <param name="obj"></param>
        public abstract void ShowObject(Kistl.API.IDataObject obj);

        /// <summary>
        /// Create a Control for the given visual, showing the object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        public CONTROL CreateControl(Kistl.API.IDataObject obj, Visual visual)
        {
            var cInfo = KistlGUIContext.FindControlInfo(Toolkit.WPF, visual);
            var pInfo = KistlGUIContext.FindPresenterInfo(visual, cInfo);

            var widget = KistlGUIContext.CreateControl(cInfo);
            var presenter = KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);

            if (cInfo.Container)
            {
                var childWidgets = from c in visual.Children select (CONTROL)CreateControl(obj, c);
                return Setup((CONTAINER)widget, childWidgets.ToList());
            }
            else
            {
                // TODO: Assert(visual.Children == null || visual.Children.Count == 0);
                return Setup((PROPERTY)widget);
            }
        }

        protected abstract CONTROL Setup(PROPERTY control);
        protected abstract CONTAINER Setup(CONTAINER widget, IList<CONTROL> list);
    }
}
