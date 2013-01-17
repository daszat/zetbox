using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class SelectModuleDlgViewModel : WindowViewModel
    {
        private IEnumerable<ModuleViewModel> _current;
        public SelectModuleDlgViewModel(IEnumerable<ModuleViewModel> current)
        {
            if (current == null) throw new ArgumentNullException("current");
            _current = current;
        }

        public IEnumerable<ModuleViewModel> List
        {
            get
            {
                return ModulesCache.Instance.All
                    .Where(i => i.IsFeature)
                    .Where(i => !_current.Any(c => c.TypeName == i.TypeName))
                    .Select(i => new ModuleViewModel(i));
            }
        }

        private List<ModuleViewModel> _selected = new List<ModuleViewModel>();
        public List<ModuleViewModel> Selected
        {
            get
            {
                return _selected;
            }
        }

        private ICommandViewModel _SelectCommand = null;
        public ICommandViewModel SelectCommand
        {
            get
            {
                if (_SelectCommand == null)
                {
                    _SelectCommand = new SimpleCommandViewModel("Select", "Select", Select);
                }
                return _SelectCommand;
            }
        }

        public void Select()
        {
            OnClose();
        }

        private ICommandViewModel _CancelCommand = null;
        public ICommandViewModel CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new SimpleCommandViewModel("Cancel", "Cancel", Cancel);
                }
                return _CancelCommand;
            }
        }

        public void Cancel()
        {
            OnClose();
        }
    }
}
