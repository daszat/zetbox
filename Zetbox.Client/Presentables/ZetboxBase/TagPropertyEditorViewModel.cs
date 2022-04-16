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


namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Async;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class TagPropertyEditorViewModel
        : StringValueViewModel
    {
        public new delegate TagPropertyEditorViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);

        public TagPropertyEditorViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, IValueModel mdl)
            : base(dependencies, dataCtx, parent, mdl)
        {
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Value")
            {
                EnsureValuePossible(Value);
                _filter = SplitValueItems(Value);
                OnPropertyChanged("FilteredPossibleValuesAsync");
            }
        }

        private System.Threading.Tasks.Task<ReadOnlyObservableCollection<TagEntryViewModel>> _getPossibleValuesROTask;
        private ReadOnlyObservableCollection<TagEntryViewModel> _possibleValuesRO;
        private ObservableCollection<TagEntryViewModel> _possibleValues;
        public ReadOnlyObservableCollection<TagEntryViewModel> PossibleValues
        {
            get
            {
                TriggerPossibleValuesROAsync();
                _getPossibleValuesROTask.Wait();
                return _possibleValuesRO;
            }
        }

        public ReadOnlyObservableCollection<TagEntryViewModel> PossibleValuesAsync
        {
            get
            {
                TriggerPossibleValuesROAsync();
                return _possibleValuesRO;
            }
        }

        private string[] _filter = null;
        public IEnumerable<TagEntryViewModel> FilteredPossibleValuesAsync
        {
            get
            {
                TriggerPossibleValuesROAsync();
                if (_possibleValuesRO == null) return null;
                if (_filter != null)
                {
                    var predicate = LinqExtensions.False<TagEntryViewModel>();
                    foreach (var str in _filter)
                    {
                        var localStr = str.ToLower();
                        predicate = predicate.OrElse<TagEntryViewModel>(i => i.Text.ToLower().Contains(localStr));
                    }
                    return _possibleValuesRO.AsQueryable().Where(predicate);
                }
                return _possibleValuesRO;
            }
        }

        private void TriggerPossibleValuesROAsync()
        {
            if (_getPossibleValuesROTask == null)
            {
                _getPossibleValuesROTask = Task.Run(async () =>
                {
                    var task = await GetPossibleValuesAsync();

                    _possibleValues = new ObservableCollection<TagEntryViewModel>(task);
                    _possibleValuesRO = new ReadOnlyObservableCollection<TagEntryViewModel>(_possibleValues);
                    EnsureValuePossible(Value);
                    OnPropertyChanged("PossibleValuesAsync");
                    OnPropertyChanged("FilteredPossibleValuesAsync");

                    return _possibleValuesRO;
                });
            }
        }

        protected virtual System.Threading.Tasks.Task<List<TagEntryViewModel>> GetPossibleValuesAsync()
        {
            return Task.Run(async () =>
            {
                var fetchTask = await DataContext
                                    .GetQuery<TagCache>()
                                    .OrderBy(t => t.Name)
                                    .ToListAsync();
                return fetchTask
                    .Select(tag => new TagEntryViewModel(tag, this, false)) // using the very basic & simple view model
                    .ToList();
            });
        }

        public void ResetPossibleValues()
        {
            _possibleValues = null;
            _possibleValuesRO = null;
            _getPossibleValuesROTask = null;
            OnPropertyChanged("PossibleValues");
            OnPropertyChanged("PossibleValuesAsync");
        }

        private static readonly string[] empty = new string[] { };
        public static string[] SplitValueItems(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return empty;
            return value.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void EnsureValuePossible(string value)
        {
            if (_possibleValues == null) return;

            if (string.IsNullOrWhiteSpace(value))
            {
                foreach (var toUncheck in _possibleValues)
                {
                    toUncheck.IsChecked = false;
                }
            }
            else
            {
                var allItems = SplitValueItems(value);
                foreach (var item in allItems)
                {
                    var lowerItem = item.ToLower().Trim();
                    var tagItem = _possibleValues.FirstOrDefault(i => i.Text.ToLower() == lowerItem);
                    if (tagItem == null && !DataContext.IsReadonly)
                    {
                        // Add if not found
                        var newTag = DataContext.Create<TagCache>();
                        newTag.Name = item;
                        _possibleValues.Add(new TagEntryViewModel(newTag, this, true));
                    }
                    else if (tagItem != null)
                    {
                        tagItem.IsChecked = true;
                    }
                }

                allItems = allItems.Select(i => i.ToLower()).ToArray();

                foreach (var toDelete in _possibleValues.Where(i => i.CurrentlyAdded == true && !allItems.Contains(i.Text.ToLower())).ToList())
                {
                    if (!DataContext.IsReadonly)
                    {
                        DataContext.Delete(toDelete.Tag);
                    }
                    _possibleValues.Remove(toDelete);
                }

                foreach (var toUncheck in _possibleValues.Where(i => !allItems.Contains(i.Text.ToLower())).ToList())
                {
                    toUncheck.IsChecked = false;
                }
            }
        }

        public void AddTag(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            var lowerText = text.ToLower();
            if (SplitValueItems(Value).Any(i => i.ToLower() == lowerText) == false)
            {
                FormattedValue += ", " + text;
            }
        }

        public void RemoveTag(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            var lowerText = text.ToLower();
            if (SplitValueItems(Value).Any(i => i.ToLower() == lowerText) == true)
            {
                FormattedValue = string.Join(", ", SplitValueItems(Value).Where(i => i.ToLower() != lowerText));
            }
        }
    }

    public sealed class TagEntryViewModel : INotifyPropertyChanged
    {
        private readonly TagPropertyEditorViewModel _parent;
        public TagEntryViewModel(TagCache tag, TagPropertyEditorViewModel parent, bool isChecked)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (tag == null) throw new ArgumentNullException("tag");

            _parent = parent;
            _isChecked = isChecked;
            _text = tag.Name;

            this.Tag = tag;
            this.CurrentlyAdded = tag.ObjectState == DataObjectState.New;
        }

        public bool CurrentlyAdded { get; private set; }
        public TagCache Tag { get; private set; }

        private bool _isChecked = false;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");
                    if (value == true)
                    {
                        _parent.AddTag(Text);
                    }
                    else
                    {
                        _parent.RemoveTag(Text);
                    }
                }
            }
        }

        private string _text = string.Empty;
        public string Text
        {
            get
            {
                return _text;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            var temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
