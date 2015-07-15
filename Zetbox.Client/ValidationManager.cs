using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;

namespace Zetbox.Client
{
    public class ValidationError
    {
        public ValidationError(ViewModel source, params string[] errors)
            : this(source, source, errors)
        {
        }

        public ValidationError(ViewModel source, ViewModel originalSource, params string[] errors)
        {
            this.Source = source;
            this.OriginalSource = originalSource;

            this.Errors = new List<string>(errors);
            this.Children = new List<ValidationError>();
        }

        public ViewModel Source { get; set; }
        public ViewModel OriginalSource { get; set; }
        public List<string> Errors { get; private set; }

        public List<ValidationError> Children { get; private set; }

        public override string ToString()
        {
            return string.Join("\n", Errors.Where(i => !string.IsNullOrWhiteSpace(i)));
        }
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
    }

    public class ValidationManager : IValidationManager
    {
        private List<ViewModel> _validationRequests = new List<ViewModel>();
        private bool _isValidating = false;

        public event EventHandler Changed;

        public virtual void Register(ViewModel vmdl)
        {
            if (!_validationRequests.Contains(vmdl))
            {
                _validationRequests.Add(vmdl);
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
                foreach (var vmdl in _validationRequests)
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
                return _validationRequests.Where(i => i.ValidationError != null).Select(i => i.ValidationError);
            }
        }
    }
}
