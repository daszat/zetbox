using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables.Assemblies
{
    public class AssemblyModel
        : DataObjectModel
    {
        private Assembly _assembly;

        public AssemblyModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            Assembly assembly)
            : base(appCtx, dataCtx, assembly)
        {
            _assembly = assembly;
        }

        #region Public Interface

        private ImmutableAsyncList<Type, SystemTypeModel> _typeList;
        public ReadOnlyCollection<SystemTypeModel> Types
        {
            get
            {
                UI.Verify();

                if (_typeList == null)
                {
                    _typeList = AsyncListFactory.UiCreateImmutable(
                        AppContext, DataContext, this,
                        () => System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.AssemblyName).GetTypes(),
                        t => Factory.CreateSpecificModel<SystemTypeModel>(DataContext, t));
                }
                return _typeList.GetUiView();
            }
        }

        //private ObservableCollection<SystemTypeModel> _typesCache;
        //private ReadOnlyObservableCollection<SystemTypeModel> _typesView;
        //public ReadOnlyObservableCollection<SystemTypeModel> Types
        //{
        //    get
        //    {
        //        UI.Verify();
        //        if (_typesView == null)
        //        {
        //            _typesCache = new ObservableCollection<SystemTypeModel>();
        //            _typesView = new ReadOnlyObservableCollection<SystemTypeModel>(_typesCache);
        //            State = ModelState.Loading;
        //            Async.Queue(DataContext, () =>
        //            {
        //                AsyncFetchSystemTypes();
        //                UI.Queue(UI, () => this.State = ModelState.Active);
        //            });
        //        }
        //        return _typesView;
        //    }
        //}

        #endregion

        #region Async calls

        //private void AsyncFetchSystemTypes()
        //{
        //    Async.Verify();

        //    var self = System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.AssemblyName);
        //    foreach (var t in self.GetTypes())
        //    {
        //        UI.Queue(UI, () =>
        //        {
        //            _typesCache.Add(Factory.CreateSpecificModel<SystemTypeModel>(DataContext, t));
        //        });
        //    }
        //}

        #endregion

    }
}
