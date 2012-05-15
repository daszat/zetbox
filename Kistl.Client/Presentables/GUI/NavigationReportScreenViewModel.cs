namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.GUI;
    using Kistl.App.GUI;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.FilterViewModels;
    using Kistl.Client.Presentables.DtoViewModels;

    [ViewModelDescriptor]
    public abstract class NavigationReportScreenViewModel : NavigationScreenViewModel
    {
        public new delegate NavigationReportScreenViewModel Factory(IKistlContext dataCtx, ViewModel parent, NavigationScreen screen);

        private readonly Func<IKistlContext> _ctxFactory;
        private readonly IViewModelDependencies _appCtx;
        private readonly IFileOpener _fileOpener;

        public NavigationReportScreenViewModel(IViewModelDependencies appCtx, Func<IKistlContext> ctxFactory,
            IKistlContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener)
            : base(appCtx, dataCtx, parent, screen)
        {
            _ctxFactory = ctxFactory;
            _appCtx = appCtx;
            _fileOpener = fileOpener;
        }

        private DateRangeFilterModel _rangeMdl;
        private DateRangeFilterViewModel _berichtszeitraumVM;
        public DateRangeFilterViewModel Range
        {
            get
            {
                if (_berichtszeitraumVM == null)
                {
                    _rangeMdl = DateRangeFilterModel.Create(FrozenContext, "Report range", null, null, true, false, false);
                    _berichtszeitraumVM = ViewModelFactory.CreateViewModel<DateRangeFilterViewModel.Factory>()
                        .Invoke(DataContext, this, _rangeMdl);
                    _rangeMdl.FilterChanged += (s, e) => Refresh();
                }

                return _berichtszeitraumVM;
            }
        }

        public void Refresh()
        {
            _statistic = null;
            OnPropertyChanged("Statistic");
        }

        private object _statistic;
        private DtoBaseViewModel _statisticModel;
        public DtoBaseViewModel Statistic
        {
            get
            {
                if (_statistic == null)
                {
                    ViewModelFactory.CreateDelayedTask(Displayer, () =>
                    {
                        if (_rangeMdl != null && _rangeMdl.From.Value.HasValue && _rangeMdl.To.Value.HasValue)
                        {
                            _statistic = LoadStatistic(_rangeMdl.From.Value.Value, _rangeMdl.To.Value.Value);
                            if (_statistic != null)
                            {
                                var tmp = DtoBuilder.BuildFrom(_statistic, _appCtx, DataContext, this, _fileOpener);

                                if (_statisticModel == null)
                                {
                                    _statisticModel = tmp;
                                }
                                else
                                {
                                    tmp = DtoBuilder.Combine(_statisticModel, tmp);
                                    if (tmp != _statisticModel)
                                    {
                                        _statisticModel = tmp;
                                    }
                                }
                            }
                        }
                        OnPropertyChanged("Statistic");
                    }).Trigger();
                }
                return _statisticModel;
            }
        }

        protected abstract object LoadStatistic(DateTime from, DateTime until);
    }
}
