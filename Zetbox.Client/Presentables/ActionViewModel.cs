
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;

    public class ActionViewModel
        : CommandViewModel
    {
        public new delegate ActionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IDataObject obj, Method m);

        public ActionViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, 
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, parent, string.Empty, string.Empty)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (m == null) throw new ArgumentNullException("m");

            Object = obj;
            Method = m;

            Method.PropertyChanged += MethodPropertyChanged;
        }

        public override string Label
        {
            get
            {
                return Method.GetLabel();
            }
            protected set
            {
                base.Label = value;
            }
        }

        public override string ToolTip
        {
            get
            {
                return string.IsNullOrEmpty(Reason) ? Method.Description : Reason;
            }
            protected set
            {
                base.ToolTip = value;
            }
        }

        public override App.GUI.Icon Icon
        {
            get
            {
                return Method.Icon ?? base.Icon;
            }
            set
            {
                base.Icon = value;
            }
        }

        #region Public Interface

        public string MethodName { get { return Method.Name; } }

        public override string Name
        {
            get { return Label; }
        }

        public override bool CanExecute(object data)
        {
            var result = Object.GetPropertyValue<bool>(Method.Name + "CanExec");
            if (result == false)
            {
                base.Reason = Object.GetPropertyValue<string>(Method.Name + "CanExecReason");
            }
            else
            {
                base.Reason = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Execute the modelled Method.
        /// </summary>
        public void Execute()
        {
            base.Execute(null);
        }

        /// <summary>
        /// Execute the modelled Method. The callback will be called 
        /// back on the UI thread after the execution has finished.
        /// </summary>
        /// <param name="callback">A callback or null</param>
        public void Execute(Action callback)
        {
            base.Execute(callback);
        }

        protected override void DoExecute(object data)
        {
            var parameter = Method.Parameter.Where(i => !i.IsReturnParameter).ToArray();
            MethodInfo info = Object.GetType().FindMethod(Method.Name, parameter.Select(i => i.GetParameterType()).ToArray());
            if (info == null) throw new InvalidOperationException(string.Format(ActionViewModelResources.MethodNotFoundException, Method.Name));

            if (parameter.Length > 0)
            {
                var pitMdl = ViewModelFactory.CreateViewModel<ParameterInputTaskViewModel.Factory>().Invoke(DataContext, this, Method,
                    (p) =>
                    {
                        var result = info.Invoke(Object, p);
                        HandleResult(result, data);
                    });
                ViewModelFactory.ShowDialog(pitMdl);
            }
            else
            {
                var result = info.Invoke(Object, new object[] { });
                HandleResult(result, data);
            }
        }

        private void HandleResult(object result, object callback)
        {
            IDataObject obj = result as IDataObject;
            if (obj != null && obj.Context == DataContext)
            {
                this.ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(this.ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj), true);
            }
            else if(result != null)
            {
                ViewModelFactory.ShowMessage(result.ToString(), "Result");
            }

            if (callback is Action)
            {
                ((Action)callback)();
            }
        }

        #endregion

        #region Utilities and UI callbacks

        #endregion

        #region PropertyChanged event handlers

        private void MethodPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name": OnPropertyChanged("Label"); break;
                case "Description": OnPropertyChanged("ToolTip"); break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }
    }
}
