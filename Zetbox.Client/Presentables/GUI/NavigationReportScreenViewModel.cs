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
namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.DtoViewModels;
    using Zetbox.Client.Presentables.FilterViewModels;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public abstract class NavigationReportScreenViewModel : NavigationScreenViewModel, IRefreshCommandListener
    {
        public new delegate NavigationReportScreenViewModel Factory(IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen);

        private readonly IViewModelDependencies _appCtx;
        private readonly IFileOpener _fileOpener;
        private readonly ITempFileService _tmpService;

        public NavigationReportScreenViewModel(IViewModelDependencies appCtx,
            IZetboxContext dataCtx, ViewModel parent, NavigationScreen screen, IFileOpener fileOpener, ITempFileService tmpService)
            : base(appCtx, dataCtx, parent, screen)
        {
            _appCtx = appCtx;
            _fileOpener = fileOpener;
            _tmpService = tmpService;
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

        private RefreshCommand _RefreshCommand = null;
        public ICommandViewModel RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = ViewModelFactory.CreateViewModel<RefreshCommand.Factory>().Invoke(
                        DataContext,
                        this);
                }
                return _RefreshCommand;
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
                                var tmp = DtoBuilder.BuildFrom(_statistic, _appCtx, DataContext, this, _fileOpener, _tmpService);

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
