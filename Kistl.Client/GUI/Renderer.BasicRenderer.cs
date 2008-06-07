using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.GUI.DB;
using Kistl.API.Client;

namespace Kistl.GUI.Renderer
{
    public interface IRenderer
    {
        /// <summary>
        /// Show a message to the user
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the given object to the user
        /// </summary>
        /// <param name="obj"></param>
        void ShowObject(IDataObject obj);

        /// <summary>
        /// Create a control corresponding to the visual for this object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        object CreateControl(IDataObject obj, Visual visual);

        /// <summary>
        /// The platform of this Renderer
        /// </summary>
        Toolkit Platform { get; }
    }

    public abstract class BasicRenderer<CONTROL, PROPERTY, CONTAINER> : IRenderer
        where PROPERTY : CONTROL
        where CONTAINER : CONTROL
    {
        public abstract void ShowMessage(string msg);
        public abstract void ShowObject(IDataObject obj);

        /// <summary>
        /// Create a Control for the given visual, showing the object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        public object CreateControl(IDataObject obj, Visual visual)
        {
            var cInfo = KistlGUIContext.FindControlInfo(Platform, visual);
            var pInfo = KistlGUIContext.FindPresenterInfo(visual, visual.Property.GetType());

            var widget = KistlGUIContext.CreateControl(cInfo);
            
            // TODO: call presenter.Dispose() when window is closed
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

        public abstract Toolkit Platform { get; }
    }
}
