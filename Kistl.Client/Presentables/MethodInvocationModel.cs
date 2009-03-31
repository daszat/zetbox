using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client.GUI.DB;

namespace Kistl.Client.Presentables
{
    public class MethodInvocationModel
        : DataObjectModel
    {
        public MethodInvocationModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            MethodInvocation mdl)
            : base(appCtx, dataCtx, mdl)
        {
            _invocation = mdl;
            _invocation.PropertyChanged += AsyncInvocationPropertyChanged;
        }

        #region Async handlers and UI callbacks

        private void AsyncUpdateMemberNamePossibilities()
        {
            Async.Verify();

            if (_memberNameProperty == null)
                _memberNameProperty = Object.GetObjectClass(MetaContext).Properties.Single(p => p.PropertyName == "MemberName");

            if (_memberNameModel == null)
                // fetches already generated model from cache
                _memberNameModel = (ChooseReferencePropertyModel<string>)Factory.CreateModel(
                    DataMocks.LookupDefaultPropertyModelDescriptor(_memberNameProperty),
                    DataContext,
                    new object[] { Object, _memberNameProperty });

            var possibleValues = _invocation
                .Implementor.AsType(true)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(mi => mi.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            UI.Queue(UI, () =>
            {
                var originalValue = _memberNameModel.Value;
                _memberNameModel.PossibleValues.Clear();
                possibleValues.ForEach(v => _memberNameModel.PossibleValues.Add(v));
                _memberNameModel.Value = originalValue;
            });
        }

        #endregion

        #region PropertyChanged event handlers

        private void AsyncInvocationPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            Async.Verify();
            switch (args.PropertyName)
            {
                case "Implementor": AsyncUpdateMemberNamePossibilities(); break;
            }
        }

        #endregion

        private MethodInvocation _invocation;
        private Property _memberNameProperty;
        private ChooseReferencePropertyModel<string> _memberNameModel;
    }
}
