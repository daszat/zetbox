
namespace Kistl.Client.Presentables.DtoViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.Client.Presentables;

    public class DtoGroupedViewModel : DtoBaseViewModel
    {
        public DtoGroupedViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ViewModel parent, IFileOpener fileOpener, ITempFileService tmpService, object debugInfo)
            : base(dependencies, dataCtx, parent, fileOpener, tmpService, debugInfo)
        {
            Items = new ObservableCollection<DtoBaseViewModel>();
        }

        private DtoBaseViewModel _selectedItem;
        public DtoBaseViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        public ObservableCollection<DtoBaseViewModel> Items { get; private set; }

        private string _alternateBackground = String.Empty;
        public string AlternateBackground
        {
            get
            {
                return _alternateBackground;
            }
            set
            {
                if (_alternateBackground != value)
                {
                    _alternateBackground = value;
                    OnPropertyChanged("AlternateBackground");
                }
            }
        }

        protected internal override void ApplyChangesFrom(DtoBaseViewModel otherBase)
        {
            base.ApplyChangesFrom(otherBase);
            var other = otherBase as DtoGroupedViewModel;

            this.AlternateBackground = other.AlternateBackground;
            var selectedIdx = other.Items.IndexOf(other.SelectedItem);
            DtoBuilder.Merge(this.Items, other.Items);

            if (selectedIdx >= 0 && selectedIdx < this.Items.Count)
            {
                this.SelectedItem = this.Items[selectedIdx];
            }
            else
            {
                this.SelectedItem = this.Items.FirstOrDefault();
            }
        }
    }
}
