
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
    public class DashboardModel
        : ViewModel
    {
        public new delegate DashboardModel Factory(IKistlContext dataCtx);

        /// <summary>
        /// Initializes a new instance of the DashboardModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public DashboardModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            // TODO: react to ctx.Create<ViewModelDescriptor>()/ctx.Delete<ViewModelDescriptor>()
            // TODO: Reactivate
            //ViewModelDescriptors = new ReadOnlyObservableCollection<DataObjectModel>(
            //    new ObservableCollection<DataObjectModel>(
            //        DataContext
            //            .GetQuery<ViewModelDescriptor>()
            //            .Select(desc => (DataObjectModel)Factory.CreateDefaultModel(DataContext, desc))
            //            .ToList()));

            //_selectedViewModelDescriptor = null;

            //DefaultViewDescriptors = new Dictionary<Toolkit, DataObjectModel>();
        }

        public ReadOnlyObservableCollection<DataObjectModel> ViewModelDescriptors { get; private set; }

        private DataObjectModel _selectedViewModelDescriptor;
        public DataObjectModel SelectedViewModelDescriptor
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
            //ApplicableViewDescriptors = new ReadOnlyObservableCollection<DataObjectModel>(
            //    new ObservableCollection<DataObjectModel>(
            //        ViewModelDescriptors
            //            .Select(dom => dom.Object)
            //            .OfType<ViewModelDescriptor>()
            //            .SelectMany(pmd => pmd.GetApplicableViewDescriptors())
            //            .Select(vd => (DataObjectModel)Factory.CreateDefaultModel(DataContext, vd))
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
                    DefaultViewDescriptors[tk] = ModelFactory.CreateViewModel<DataObjectModel.Factory>(defaultView).Invoke(DataContext, defaultView);
                }
            }
        }

        public Dictionary<Toolkit, DataObjectModel> DefaultViewDescriptors { get; private set; }

        public ReadOnlyObservableCollection<DataObjectModel> ApplicableViewDescriptors { get; private set; }

        public override string Name
        {
            get { return "GUI Dashboard"; }
        }
    }
}
