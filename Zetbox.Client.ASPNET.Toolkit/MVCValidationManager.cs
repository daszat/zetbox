﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;

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

            foreach (var tags in _nameTags)
            {
                var error = tags.Value.ValidationError;
                if (error != null)
                {
                    var strError = string.Join(", ", error.Errors);
                    if (!string.IsNullOrWhiteSpace(strError))
                    {
                        if (tags.Value is DataObjectViewModel)
                        {
                            // Special case DatObject: remove any key
                            mdlState.AddModelError("", strError);
                        }
                        else
                        {
                            mdlState.AddModelError(tags.Key, strError);
                        }
                    }
                }
            }
        }
    }
}
