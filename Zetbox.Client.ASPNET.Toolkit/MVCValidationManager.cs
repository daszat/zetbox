using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET
{
    public interface IMVCValidationManager : IValidationManager
    {
        void RegisterNameTagForValidation(string nameTag, ViewModel vmdl);
        void Validate(ModelStateDictionary mdlState);
    }

    public class MVCValidationManager : ValidationManager, IMVCValidationManager
    {
        private Dictionary<string, ViewModel> _nameTags = new Dictionary<string, ViewModel>();

        public void RegisterNameTagForValidation(string nameTag, ViewModel vmdl)
        {
            _nameTags[nameTag] = vmdl;
        }

        public void Validate(ModelStateDictionary mdlState)
        {
            base.Validate();

            foreach (var kv in _nameTags)
            {
                var error = kv.Value.ValidationError;
                if (error != null)
                {
                    var strError = string.Join("\n", error.Errors);
                    if (!string.IsNullOrWhiteSpace(strError))
                    {
                        mdlState.AddModelError(kv.Key, strError);
                    }
                }
            }
        }
    }
}
