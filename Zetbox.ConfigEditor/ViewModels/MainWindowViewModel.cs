﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Zetbox.ConfigEditor.ViewModels
{
    public class MainWindowViewModel : WindowViewModel
    {
        #region Properties
        private ConfigViewModel _config;
        public ConfigViewModel Config
        {
            get
            {
                return _config;
            }
            private set
            {
                if (_config != value)
                {
                    _config = value;
                    OnPropertyChanged("Config");
                }
            }
        }
        #endregion

        #region Commands
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
            var dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            if (dlg.ShowDialog() == true)
            {
                Open(dlg.FileName);
            }
        }

        public void Open(string path)
        {
            Config = new ConfigViewModel(Zetbox.API.Configuration.ZetboxConfig.FromFile(path, string.Empty));
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
        #endregion
    }
}