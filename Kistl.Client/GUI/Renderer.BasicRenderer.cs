using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.GUI.DB;

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
        /// Intructs the Renderer to let the User choose an IDataObject of a given type
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns>null if no Object was chosen or the chosen object</returns>
        IDataObject ChooseObject(IKistlContext ctx, Type objectType, string prompt);

        /// <summary>
        /// Intructs the Renderer to let the User choose an IDataObject of a given type
        /// </summary>
        /// <returns>null if no Object was chosen or the chosen object</returns>
        T ChooseObject<T>(IKistlContext ctx, string prompt) where T : IDataObject;

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

    /// <summary>
    /// Basic rendering functionality. The BasicRenderer walks the VisualTree and call the implementation to setup the controls.
    /// The Typeparameters are mostly for convinience of the implementation.
    /// </summary>
    /// <typeparam name="CONTROL">The Baseclass of all Controls of this Renderer</typeparam>
    /// <typeparam name="PROPERTY">The Baseclass of all Controls to display a Property of this Renderer</typeparam>
    /// <typeparam name="CONTAINER">The Baseclass of all Containers of this Renderer</typeparam>
    public abstract class BasicRenderer<CONTROL, PROPERTY, CONTAINER> : IRenderer
        where CONTROL : class
        where PROPERTY : CONTROL
        where CONTAINER : CONTROL
    {

        public abstract void ShowMessage(string msg);
        public abstract IDataObject ChooseObject(IKistlContext ctx, Type klass, string prompt);
        public T ChooseObject<T>(IKistlContext ctx, string prompt)
            where T : IDataObject
        {
            return (T)ChooseObject(ctx, typeof(T), prompt);
        }    
        
        public abstract Toolkit Platform { get; }

        /// <summary>
        /// Create a Control for the given visual, showing the object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="visual"></param>
        /// <returns></returns>
        // TODO: all this logic should probably go into the KistlGUIContext
        public object CreateControl(IDataObject obj, Visual visual)
        {
            var cInfo = KistlGUIContext.FindControlInfo(Platform, visual);
            PresenterInfo pInfo = null;

            // TODO: finding PresenterInfos has to be regularized,
            // this is currently too messy
            if (visual.Property != null)
            {
                switch (visual.ControlType)
                {
                    case VisualType.SimpleObjectList:
                        //if (visual.Property.GetInterfaceType() == typeof(BackReferenceProperty)
                        //    && visual.Property.GetPropertyType() == typeof(EnumerationEntry))
                        //    pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(IList<EnumerationEntry>));
                        //else
                            pInfo = KistlGUIContext.FindPresenterInfo(visual, visual.Property.GetInterfaceType());
                        break;
                    default:
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, visual.Property.GetInterfaceType());
                        break;
                }
            }
            else if (visual.Method != null)
            {
                switch (visual.ControlType)
                {
                    default:
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(Method));
                        break;
                }
            }
            else
            {
                switch (visual.ControlType)
                {
                    case VisualType.Object:
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(IDataObject));
                        break;
                    case VisualType.PropertyGroup:
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(Kistl.GUI.GroupPresenter));
                        break;
                    case VisualType.TemplateEditor:
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(IDataObject));
                        break;
                    // TODO: enable Menu
                    //case VisualType.Toolbar:
                    //    pInfo = KistlGUIContext.FindPresenterInfo(visual, typeof(Kistl.GUI.ToolBarPresenter));
                    //    break;
                    default:
                        // don't know how to handle this case => FAIL
                        pInfo = KistlGUIContext.FindPresenterInfo(visual, null);
                        break;
                }
            }


            var widget = KistlGUIContext.CreateControl(cInfo);

            // pass on the context of the object to the Controls for advanced usage
            widget.Context = obj.Context;

            // TODO: call presenter.Dispose() when window is closed
            var presenter = KistlGUIContext.CreatePresenter(pInfo, obj, visual, widget);

            // TODO: create Menu here
            List<CONTROL> menus = new List<CONTROL>();
            if (visual.ContextMenu != null)
            {
                var menuItems = from c in visual.ContextMenu select (CONTROL)CreateControl(obj, c);
                menus.AddRange(menuItems);
            }

            if (cInfo.IsContainer)
            {
                var childWidgets = from c in visual.Children select (CONTROL)CreateControl(obj, c);
                return Setup((CONTAINER)widget, childWidgets.ToList(), menus);
            }
            else
            {
                // TODO: Assert(visual.Children == null || visual.Children.Count == 0);
                return Setup((PROPERTY)widget, menus);
            }
        }

        public virtual void ShowObject(IDataObject obj)
        {
            var template = obj.FindTemplate(TemplateUsage.EditControl);
            var ctrl = (CONTAINER)CreateControl(obj, template.VisualTree);
            var menus = from m in template.Menu select (CONTROL)CreateControl(obj, m);
            ShowObject(obj, ctrl, menus.ToList());
        }

        /// <summary>
        /// Set the given property control up. Add the given menus.
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="menus"></param>
        /// <returns>A correctly configured control, not neccesarily the original control.</returns>
        protected abstract PROPERTY Setup(PROPERTY widget, IList<CONTROL> menus);
        /// <summary>
        /// Set the given container control up. Add the given children and menus.
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="list"></param>
        /// <param name="menus"></param>
        /// <returns>A correctly configured control, not neccesarily the original control.</returns>
        protected abstract CONTAINER Setup(CONTAINER widget, IList<CONTROL> list, IList<CONTROL> menus);

        /// <summary>
        /// Show the given object in this container.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ctrl"></param>
        protected abstract void ShowObject(IDataObject obj, CONTAINER ctrl, IList<CONTROL> menus);

    }
}
