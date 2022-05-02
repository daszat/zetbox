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
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.API.Common;
    using System.Threading.Tasks;

    [ViewModelDescriptor]
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
        }

        public string Description
        {
            get
            {
                if (Method.Module != null)
                    return Assets.GetString(Method.Module, ZetboxAssetKeys.ConstructBaseName(Method), ZetboxAssetKeys.ConstructDescriptionKey(Method), Method.Description);
                else
                    return Method.Description;
            }
        }

        public override string ToolTip
        {
            get
            {
                return string.IsNullOrEmpty(Reason) ? this.Description : Reason;
            }
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                Task.Run(async () => base.Icon = await IconConverter.ToImage(Method.Icon) ?? base.Icon);
                return null;
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

        private static Dictionary<Tuple<Type, string>, Func<object, bool>> _canExecCache = new Dictionary<Tuple<Type, string>, Func<object, bool>>();
        private static Dictionary<Tuple<Type, string>, Func<object, string>> _canExecReasonCache = new Dictionary<Tuple<Type, string>, Func<object, string>>();
        public override bool CanExecute(object data)
        {
            if (DataContext.IsDisposed) return false;

            Func<object, bool> canExec;
            var key = new Tuple<Type, string>(Object.GetType(), Method.Name);
            if (!_canExecCache.TryGetValue(key, out canExec))
            {
                var p = Expression.Parameter(typeof(object));
                var lambda = Expression.Lambda<Func<object, bool>>(Expression.MakeMemberAccess(Expression.Convert(p, Object.GetType()), Object.GetType().FindProperty(Method.Name + "CanExec").Single()), p);
                _canExecCache[key] = canExec = lambda.Compile();
            }

            var result = canExec(Object);
            if (result == false)
            {
                Func<object, string> canExecReason;
                if (!_canExecReasonCache.TryGetValue(key, out canExecReason))
                {
                    var p = Expression.Parameter(typeof(object));
                    var lambda = Expression.Lambda<Func<object, string>>(Expression.MakeMemberAccess(Expression.Convert(p, Object.GetType()), Object.GetType().FindProperty(Method.Name + "CanExecReason").Single()), p);
                    _canExecReasonCache[key] = canExecReason = lambda.Compile();
                }
                base.Reason = canExecReason(Object);
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
            var obj = result as IDataObject;
            var list = result as IEnumerable<IDataObject>;

            if (obj != null && obj.Context == DataContext)
            {
                this.ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(this.ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), obj), true);
            }
            else if (list != null && list.All(i => i.Context == DataContext))
            {
                var first = true;
                foreach (var o in list)
                {
                    this.ViewModelFactory.ShowModel(DataObjectViewModel.Fetch(this.ViewModelFactory, DataContext, ViewModelFactory.GetWorkspace(DataContext), o), first);
                    first = false;
                }
            }
            else if (result != null)
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
                case "Name": 
                    OnPropertyChanged("Label"); 
                    break;
                case "Description": 
                    OnPropertyChanged("Description"); 
                    OnPropertyChanged("ToolTip");
                    break;
            }
        }

        #endregion

        protected IDataObject Object { get; private set; }
        protected Method Method { get; private set; }
    }
}
