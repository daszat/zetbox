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

            Method.PropertyChanged += MethodPropertyChanged;
        }


        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.MethodName; } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        public override string Name
        {
            get { return Label; }
        }

        /// <summary>
        /// Execute the modelled Method. The callback will be called 
        /// back on the UI thread after the execution has finished.
        /// </summary>
        /// <param name="callback"></param>
        public void Execute(Action callback)
        {
            MethodInfo info = Object.GetType().FindMethod(Method.MethodName, new Type[] { });
            IDataObject result = info.Invoke(Object, new object[] { }) as IDataObject;
            if (result != null && result.Context == DataContext)
            {
                this.Factory.ShowModel(this.Factory.CreateDefaultModel(DataContext, (IDataObject)result), true);
            }
            if (callback != null)
                callback();
        }

        #endregion

        #region Utilities and UI callbacks

        #endregion

        #region PropertyChanged event handlers

        private void MethodPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "MethodName": OnPropertyChanged("Label"); break;
                case "Description": OnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }

    }
}
