using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class WindowViewModel : ViewModel
    {
        private ICommandViewModel _OpenCommand = null;
        public ICommandViewModel OpenCommand
        {
            get
            {
                if (_OpenCommand == null)
                {
                    _OpenCommand = new SimpleCommandViewModel("Open", "open a configuration", Open);
                }
                return _OpenCommand;
            }
        }

        public void Open()
        {
        }

        private ICommandViewModel _SaveCommand = null;
        public ICommandViewModel SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new SimpleCommandViewModel("Save", "Saves the configuration", Save);
                }
                return _SaveCommand;
            }
        }

        public void Save()
        {
        }

        private ICommandViewModel _SaveAsCommand = null;
        public ICommandViewModel SaveAsCommand
        {
            get
            {
                if (_SaveAsCommand == null)
                {
                    _SaveAsCommand = new SimpleCommandViewModel("SaveAs", "Save the configuration under a new name", SaveAs);
                }
                return _SaveAsCommand;
            }
        }

        public void SaveAs()
        {
        }
    }
}
