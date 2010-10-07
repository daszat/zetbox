
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;

    /// <summary>
    /// A dashboard for the GUI Module.
    /// </summary>
    [Obsolete("replaced by applications")]
    public class DashboardModel
        : ViewModel
    {
        public new delegate DashboardModel Factory(IKistlContext dataCtx);

        /// <summary>
        /// Initializes a new instance of the DashboardModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public DashboardModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            // TODO: react to ctx.Create<ViewModelDescriptor>()/ctx.Delete<ViewModelDescriptor>()
            // TODO: Reactivate
            //ViewModelDescriptors = new ReadOnlyObservableCollection<DataObjectViewModel>(
            //    new ObservableCollection<DataObjectViewModel>(
            //        DataContext
            //            .GetQuery<ViewModelDescriptor>()
            //            .Select(desc => (DataObjectViewModel)Factory.CreateDefaultModel(DataContext, desc))
            //            .ToList()));

            //_selectedViewModelDescriptor = null;

            //DefaultViewDescriptors = new Dictionary<Toolkit, DataObjectViewModel>();
        }

        public ReadOnlyObservableCollection<DataObjectViewModel> ViewModelDescriptors { get; private set; }

        private DataObjectViewModel _selectedViewModelDescriptor;
        public DataObjectViewModel SelectedViewModelDescriptor
        {
            get { return _selectedViewModelDescriptor; }
            set
            {
                if (value != _selectedViewModelDescriptor)
                {
                    _selectedViewModelDescriptor = value;
                    OnSelectedViewModelDescriptorChanged();
                }
            }
        }

        protected virtual void OnSelectedViewModelDescriptorChanged()
        {
            UpdateDefaultViewDescriptors();
            UpdateApplicableViewDescriptors();

            OnPropertyChanged("SelectedViewModelDescriptor");
        }

        private void UpdateApplicableViewDescriptors()
        {
            //ApplicableViewDescriptors = new ReadOnlyObservableCollection<DataObjectViewModel>(
            //    new ObservableCollection<DataObjectViewModel>(
            //        ViewModelDescriptors
            //            .Select(dom => dom.Object)
            //            .OfType<ViewModelDescriptor>()
            //            .SelectMany(pmd => pmd.GetApplicableViewDescriptors())
            //            .Select(vd => (DataObjectViewModel)Factory.CreateDefaultModel(DataContext, vd))
            //            .ToList()));
        }

        private void UpdateDefaultViewDescriptors()
        {
            DefaultViewDescriptors.Clear();
            foreach (Toolkit tk in Enum.GetValues(typeof(Toolkit)))
            {
                var defaultView = (SelectedViewModelDescriptor.Object as ViewModelDescriptor).GetViewDescriptor(tk);
                if (defaultView != null)
                {
                    DefaultViewDescriptors[tk] = ModelFactory.CreateViewModel<DataObjectViewModel.Factory>(defaultView).Invoke(DataContext, defaultView);
                }
            }
        }

        public Dictionary<Toolkit, DataObjectViewModel> DefaultViewDescriptors { get; private set; }

        public ReadOnlyObservableCollection<DataObjectViewModel> ApplicableViewDescriptors { get; private set; }

        public override string Name
        {
            get { return "GUI Dashboard"; }
        }
    }
}
