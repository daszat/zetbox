using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.API.Configuration;

namespace Kistl.Client.Presentables
{
    public class MethodInvocationModel
        : DataObjectModel
    {
        public new delegate MethodInvocationModel Factory(IKistlContext dataCtx, MethodInvocation mdl);

        public MethodInvocationModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx,
            MethodInvocation mdl)
            : base(appCtx, config, dataCtx, mdl)
        {
            _invocation = mdl;
            _invocation.PropertyChanged += InvocationPropertyChanged;
            UpdateMemberNamePossibilities();
        }

        #region Utilities and UI callbacks

        private void UpdateMemberNamePossibilities()
        {
            //if (_memberNameProperty == null)
            //    _memberNameProperty = Object.GetObjectClass(FrozenContext).Properties.Single(p => p.Name == "MemberName");

            //if (_memberNameModel == null)
            //    // fetches already generated model from cache
            //    _memberNameModel = ModelFactory.CreateViewModel<ChooseReferencePropertyModel<string>.Factory>(_memberNameProperty).Invoke(DataContext, Object, _memberNameProperty);

            //if (_invocation.Implementor == null) return;

            //if (_invocation.Implementor.AsType(false) == null) return;

            //var possibleValues = _invocation
            //    .Implementor.AsType(true)
            //    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            //    .Select(mi => mi.Name)
            //    .Distinct()
            //    .OrderBy(name => name)
            //    .ToList();

            //var originalValue = _memberNameModel.Value;
            //_memberNameModel.PossibleValues.Clear();
            //possibleValues.ForEach(v => _memberNameModel.PossibleValues.Add(v));
            //_memberNameModel.Value = originalValue;
        }

        #endregion

        #region PropertyChanged event handlers

        private void InvocationPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Implementor": UpdateMemberNamePossibilities(); break;
            }
        }

        #endregion

        private MethodInvocation _invocation;
        //private Property _memberNameProperty;
        //private PropertyModel<string> _memberNameModel;
    }
}
