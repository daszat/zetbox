using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;

namespace Zetbox.Client
{
    public class ValidationError : INotifyPropertyChanged
    {
        public ValidationError(ViewModel source)
        {
            this.Source = source;

            this.Errors = new ObservableCollection<string>();
            this.Errors.CollectionChanged += (s, e) =>
            {
                var temp = PropertyChanged;
                if (temp != null)
                {
                    temp(this, new PropertyChangedEventArgs("HasErrors"));
                }
            };
            this.Children = new ObservableCollection<ValidationError>();
        }

        public ViewModel Source { get; private set; }
        public ObservableCollection<string> Errors { get; private set; }
        public ObservableCollection<ValidationError> Children { get; private set; }

        public bool HasErrors
        {
            get
            {
                return Errors.Count > 0;
            }
        }

        public void AddError(string error)
        {
            if (error == null) return;

            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            if (errors == null) return;

            foreach (var e in errors)
            {
                AddError(e);
            }
        }

        public void AddChild(ValidationError child)
        {
            if (child == null) return;

            Children.Add(child);
        }

        public void AddChildren(IEnumerable<ValidationError> children)
        {
            if (children == null) return;

            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        public string Message
        {
            get
            {
                return string.Join("\n", Errors.Where(i => !string.IsNullOrWhiteSpace(i)));
            }
        }

        public override string ToString()
        {
            return Message;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public interface IValidationManager
    {
        /// <summary>
        /// Register a ViewModel at the Validation Manager. This should be done in the constructor.
        /// </summary>
        /// <remarks>
        /// Only ViewModels, who realy have validation should register themselves.
        /// </remarks>
        /// <param name="vmdl"></param>
        void Register(ViewModel vmdl);

        /// <summary>
        /// Notifies the ValidationManager about the error state change of a ViewModel.
        /// </summary>
        /// <param name="vmdl"></param>
        void Notify(ViewModel vmdl);

        /// <summary>
        /// Validates all registrated ViewModels
        /// </summary>
        void Validate();

        /// <summary>
        /// Fired, when one or more ViewModels has changed their error state.
        /// </summary>
        event EventHandler Changed;

        /// <summary>
        /// Returns all current errors.
        /// </summary>
        IEnumerable<ValidationError> Errors { get; }

        bool IsValid { get; }
    }

    public class ValidationManager : IValidationManager
    {
        private List<ViewModel> _validationRequests = new List<ViewModel>();
        private List<ValidationError> _errors = null;
        private bool _isValidating = false;

        public event EventHandler Changed;

        public virtual void Register(ViewModel vmdl)
        {
            if (!_validationRequests.Contains(vmdl))
            {
                _validationRequests.Add(vmdl);
                _errors = null;
            }
        }

        public virtual void Notify(ViewModel vmdl)
        {
            if(!_isValidating)
            {
                OnNotifyChanged();
            }
        }

        protected virtual void OnNotifyChanged()
        {
            _errors = null;
            var temp = Changed;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public virtual void Validate()
        {
            _isValidating = true;
            try
            {
                // Clone the ValidationRequests list, as during the validation it may happen that new ViewModels are created. 
                // During creation they will register at the validation manager.
                foreach (var vmdl in _validationRequests.ToList())
                {
                    vmdl.Validate();
                }
            }
            finally
            {
                _isValidating = false;
                OnNotifyChanged();
            }
        }


        public IEnumerable<ValidationError> Errors
        {
            get 
            {
                if(_errors == null)
                {
                    _errors = _validationRequests.Where(i => i.ValidationError.HasErrors).Select(i => i.ValidationError).ToList();
                }
                return _errors;
            }
        }


        public bool IsValid
        {
            get 
            {
                return !Errors.Any();
            }
        }
    }
}
