
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public class DataTypeViewModel 
        : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate DataTypeViewModel Factory(IKistlContext dataCtx, DataType dt);
#else
        public new delegate DataTypeViewModel Factory(IKistlContext dataCtx, DataType dt);
#endif

        public DataTypeViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            DataType dt)
            : base(appCtx, dataCtx, dt)
        {
            _dataType = dt;
        }
        private DataType _dataType;

        private ReadOnlyProjectedList<Property, DescribedPropertyViewModel> _propertyModels;
        public IReadOnlyList<DescribedPropertyViewModel> DescribedPropertyModels
        {
            get
            {
                if (_propertyModels == null)
                {
                    _propertyModels = new ReadOnlyProjectedList<Property, DescribedPropertyViewModel>(
                        _dataType.Properties.OrderBy(p => p.Name).ToList(),
                        property => ViewModelFactory.CreateViewModel<DescribedPropertyViewModel.Factory>().Invoke(DataContext, property),
                        m => m.DescribedProperty);
                }
                return _propertyModels;
            }
        }

        private ReadOnlyProjectedList<Method, DescribedMethodViewModel> _methodModels;
        public IReadOnlyList<DescribedMethodViewModel> DescribedMethods
        {
            get
            {
                if (_methodModels == null)
                {
                    _methodModels = new ReadOnlyProjectedList<Method, DescribedMethodViewModel>(
                        _dataType.Methods.OrderBy(m => m.Name).ToList(),
                        m => ViewModelFactory.CreateViewModel<DescribedMethodViewModel.Factory>().Invoke(DataContext, m),
                        m => m.DescribedMethod);
                }
                return _methodModels;
            }
        }

        public IEnumerable<DescribedMethodViewModel> DescribedCustomMethods
        {
            get
            {
                return DescribedMethods.Where(m => !m.IsDefaultMethod);
            }
        }
    }
}
