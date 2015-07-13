using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET
{
    public interface IValidationManager
    {
        void RegisterForValidation(string key, ViewModel vmdl);
        void Validate(ModelStateDictionary mdlState);
    }

    public class ValidationManager : IValidationManager
    {
        private Dictionary<string, ViewModel> _validationRequests = new Dictionary<string, ViewModel>();

        public void RegisterForValidation(string key, ViewModel vmdl)
        {
            _validationRequests[key] = vmdl;
        }

        public void Validate(ModelStateDictionary mdlState)
        {
            foreach (var kv in _validationRequests)
            {
                var error = kv.Value as IDataErrorInfo;
                if (error != null)
                {
                    var strError = error.Error;
                    if (!string.IsNullOrWhiteSpace(strError))
                    {
                        mdlState.AddModelError(kv.Key, strError);
                    }
                }
            }
        }
    }
}
