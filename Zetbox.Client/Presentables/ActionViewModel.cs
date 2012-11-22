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
    using System.Linq.Expressions;

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

        public override System.Drawing.Image Icon
        {
            get
            {
                return IconConverter.ToImage(Method.Icon) ?? base.Icon;
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

        private Func<bool> _canExec;
        private Func<string> _canExecReason;
        public override bool CanExecute(object data)
        {
            if (_canExec == null)
            {
                var lambda = Expression.Lambda<Func<bool>>(Expression.MakeMemberAccess(Expression.Constant(Object), Object.GetType().FindProperty(Method.Name + "CanExec").Single()));
                _canExec = lambda.Compile();
            }

            var result = _canExec();
            if (result == false)
            {
                if (_canExecReason == null)
                {
                    var lambda = Expression.Lambda<Func<string>>(Expression.MakeMemberAccess(Expression.Constant(Object), Object.GetType().FindProperty(Method.Name + "CanExecReason").Single()));
                    _canExecReason = lambda.Compile();
                }
                base.Reason = _canExecReason();
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
