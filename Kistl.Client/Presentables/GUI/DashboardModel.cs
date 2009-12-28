
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
        : PresentableModel
    {
        /// <summary>
        /// Initializes a new instance of the DashboardModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        public DashboardModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            // TODO: react to ctx.Create<PresentableModelDescriptor>()/ctx.Delete<PresentableModelDescriptor>()
            PresentableModelDescriptors = new ReadOnlyObservableCollection<DataObjectModel>(
                new ObservableCollection<DataObjectModel>(
                    DataContext
                        .GetQuery<PresentableModelDescriptor>()
                        .Select(desc => (DataObjectModel)Factory.CreateDefaultModel(DataContext, desc))
                        .ToList()));

            _selectedPresentableModelDescriptor = null;

            DefaultViewDescriptors = new Dictionary<Toolkit, DataObjectModel>();
        }

        public ReadOnlyObservableCollection<DataObjectModel> PresentableModelDescriptors { get; private set; }

        private DataObjectModel _selectedPresentableModelDescriptor;
        public DataObjectModel SelectedPresentableModelDescriptor
        {
            get { return _selectedPresentableModelDescriptor; }
            set
            {
                if (value != _selectedPresentableModelDescriptor)
                {
                    _selectedPresentableModelDescriptor = value;
                    OnSelectedPresentableModelDescriptorChanged();
                }
            }
        }

        protected virtual void OnSelectedPresentableModelDescriptorChanged()
        {
            UpdateDefaultViewDescriptors();
            UpdateApplicableViewDescriptors();

            OnPropertyChanged("SelectedPresentableModelDescriptor");
        }

        private void UpdateApplicableViewDescriptors()
        {
            //ApplicableViewDescriptors = new ReadOnlyObservableCollection<DataObjectModel>(
            //    new ObservableCollection<DataObjectModel>(
            //        PresentableModelDescriptors
            //            .Select(dom => dom.Object)
            //            .OfType<PresentableModelDescriptor>()
            //            .SelectMany(pmd => pmd.GetApplicableViewDescriptors())
            //            .Select(vd => (DataObjectModel)Factory.CreateDefaultModel(DataContext, vd))
            //            .ToList()));
        }

        private void UpdateDefaultViewDescriptors()
        {
            DefaultViewDescriptors.Clear();
            foreach (Toolkit tk in Enum.GetValues(typeof(Toolkit)))
            {
                var defaultView = (SelectedPresentableModelDescriptor.Object as PresentableModelDescriptor).GetDefaultViewDescriptor(tk);
                if (defaultView != null)
                {
                    DefaultViewDescriptors[tk] = (DataObjectModel)Factory.CreateDefaultModel(DataContext, defaultView);
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
