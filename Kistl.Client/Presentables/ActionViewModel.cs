
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public class ActionViewModel
        : ViewModel
    {
        public new delegate ActionViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj, Method m);

        public ActionViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, 
            IDataObject obj, Method m)
            : base(appCtx, dataCtx, parent)
        {
            Object = obj;
            Method = m;

            Method.PropertyChanged += MethodPropertyChanged;
        }


        #region Public Interface

        // TODO: proxying implementations might block on that
        public string Label { get { return Method.GetLabel(); } }
        // TODO: proxying implementations might block on that
        public string ToolTip { get { return Method.Description; } }

        public string MethodName { get { return Method.Name; } }

        public override string Name
        {
            get { return Label; }
        }

        private ICommandViewModel _ExecuteCommand = null;
        public ICommandViewModel ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, 
                        Parent,
                        Method.GetLabel(), 
                        Method.Description, 
                        Execute, 
                        null
                    );
                }
                return _ExecuteCommand;
            }
        }

        /// <summary>
        /// Execute the modelled Method. The callback will be called 
        /// back on the UI thread after the execution has finished.
        /// </summary>
        public void Execute()
        {
            var parameter = Method.Parameter.Where(i => !i.IsReturnParameter).ToArray();
            MethodInfo info = Object.GetType().FindMethod(Method.Name, parameter.Select(i => i.GetParameterType()).ToArray());
            if (info == null) throw new InvalidOperationException(string.Format(ActionViewModelResources.MethodNotFoundException, Method.Name));

            if (parameter.Length > 0)
            {
                var pitMdl = ViewModelFactory.CreateViewModel<ParameterInputTaskViewModel.Factory>().Invoke(DataContext, this, Method, 
                    (p) => {
                        var result = info.Invoke(Object, p);
                        HandleResult(result);
                    });
                ViewModelFactory.ShowModel(pitMdl, true);
            }
            else
            {
                var result = info.Invoke(Object, new object[] { });
                HandleResult(result);
            }                        
        }

        private void HandleResult(object result)
        {
            IDataObject obj = result as IDataObject;
            if (obj != null && obj.Context == DataContext)
            {
                this.ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(this.ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj), true);
            }
            else 
            {
                ViewModelFactory.ShowMessage(result != null ? result.ToString() : "<<null>>", "Result");
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
