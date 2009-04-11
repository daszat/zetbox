using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;
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

        private ReadOnlyProjectedList<Type, SystemTypeModel> _typeList;
        public IReadOnlyList<SystemTypeModel> Types
        {
            get
            {
                if (_typeList == null)
                {
                    _typeList = new ReadOnlyProjectedList<Type,SystemTypeModel>(
                        System.Reflection.Assembly.ReflectionOnlyLoad(_assembly.AssemblyName).GetTypes(),
                        t => Factory.CreateSpecificModel<SystemTypeModel>(DataContext, t),
                        mdl => mdl.Value);
                }
                return _typeList;
            }
        }

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
