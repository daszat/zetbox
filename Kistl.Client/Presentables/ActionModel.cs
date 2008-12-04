using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.Reflection;
using System.ComponentModel;

namespace Kistl.Client.Presentables
{
    public class ActionModel
        : PresentableModel
    {
        public ActionModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IDataObject obj, Method m)
            : base(appCtx, dataCtx)
        {
            Object = obj;
            Method = m;

            Method.PropertyChanged += AsyncMethodPropertyChanged;
        }


        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.MethodName; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        /// <summary>
        /// Execute the modelled Method. The callback will be called 
        /// back on the UI thread after the execution has finished.
        /// </summary>
        /// <param name="callback"></param>
        public void Execute(Action callback)
        {
            UI.Verify();
            State = ModelState.Loading;
            Async.Queue(DataContext, () =>
            {
                MethodInfo info = Object.GetType().FindMethod(Method.MethodName, new Type[] { });
                info.Invoke(Object, new object[] { });
                UI.Queue(UI, () =>
                {
                    try
                    {
                        if (callback != null)
                            callback();
                    }
                    finally
                    {
                        State = ModelState.Active;
                    }
                });
            });
        }

        #endregion

        #region Async handlers and UI callbacks

        #endregion

        #region PropertyChanged event handlers

        private void AsyncMethodPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            switch (e.PropertyName)
            {
                case "MethodName": AsyncOnPropertyChanged("Label"); break;
                case "Description": AsyncOnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }

    }
}
