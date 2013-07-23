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
            }
        }

        private ZbTask<ReadOnlyObservableCollection<TagEntryViewModel>> _getPossibleValuesROTask;
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

        private void TriggerPossibleValuesROAsync()
        {
            if (_getPossibleValuesROTask == null)
            {
                var task = GetPossibleValuesAsync();
                _getPossibleValuesROTask = new ZbTask<ReadOnlyObservableCollection<TagEntryViewModel>>(task);
                _getPossibleValuesROTask.OnResult(t =>
                {
                    _possibleValues = new ObservableCollection<TagEntryViewModel>(task.Result);
                    _possibleValuesRO = new ReadOnlyObservableCollection<TagEntryViewModel>(_possibleValues);
                    EnsureValuePossible(Value);
                    OnPropertyChanged("PossibleValuesAsync");
                });
            }
        }

        protected virtual ZbTask<List<TagEntryViewModel>> GetPossibleValuesAsync()
        {
            var fetchTask = DataContext.GetQuery<TagCache>().ToListAsync();
            return new ZbTask<List<TagEntryViewModel>>(fetchTask)
                .OnResult(t =>
                {
                    t.Result = fetchTask
                        .Result
                        .Select(tag => new TagEntryViewModel(this, false, tag.Name, false)) // using the very basic & simple view model
                        .ToList();
                });
            //return new ZbTask<List<TagEntryViewModel>>(new List<TagEntryViewModel>()
            //{
            //    new TagEntryViewModel(this, false, "Hello", false),
            //    new TagEntryViewModel(this, false, "World", false),
            //    new TagEntryViewModel(this, false, "Summary", false),
            //    new TagEntryViewModel(this, false, "Main", false),
            //    new TagEntryViewModel(this, false, "Meta", false),
            //    new TagEntryViewModel(this, false, "Hidden", false),
            //});
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
                return;
            }

            var allItems = SplitValueItems(value);
            foreach (var item in allItems)
            {
                var lowerItem = item.ToLower().Trim();
                // Add if not found
                var tagItem = _possibleValues.FirstOrDefault(i => i.Text.ToLower() == lowerItem);
                if (tagItem == null)
                {
                    _possibleValues.Add(new TagEntryViewModel(this, true, item, true));
                }
                else
                {
                    tagItem.IsChecked = true;
                }
            }

            allItems = allItems.Select(i => i.ToLower()).ToArray();

            foreach (var toDelete in _possibleValues.Where(i => i.CurrentlyAdded == true && !allItems.Contains(i.Text.ToLower())).ToList())
            {
                _possibleValues.Remove(toDelete);
            }

            foreach (var toUncheck in _possibleValues.Where(i => !allItems.Contains(i.Text.ToLower())).ToList())
            {
                toUncheck.IsChecked = false;
            }
        }

        public void AddTag(string text)
        {
            var lowerText = text.ToLower();
            if (SplitValueItems(Value).Any(i => i.ToLower() == lowerText) == false)
            {
                FormattedValue += ", " + text;
            }
        }

        public void RemoveTag(string text)
        {
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
        public TagEntryViewModel(TagPropertyEditorViewModel parent, bool isChecked, string text, bool currentlyAdded)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            _parent = parent;
            _isChecked = isChecked;
            _text = text;

            CurrentlyAdded = currentlyAdded;
        }

        public bool CurrentlyAdded { get; private set; }

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
