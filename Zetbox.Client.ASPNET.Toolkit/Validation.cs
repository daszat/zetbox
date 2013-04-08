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

namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Zetbox.Client.Presentables;
    using System.ComponentModel;
    using Zetbox.API;
    using System.Reflection;

    public interface IViewModelValidatorProvider
    {
    }

    public class ViewModelValidatorProvider : ModelValidatorProvider, IViewModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelValidator[] { new ViewModelValidator(metadata, context) };
        }

        public class ViewModelValidator : ModelValidator
        {
            public ViewModelValidator(ModelMetadata metadata, ControllerContext controllerContext)
                : base(metadata, controllerContext)
            {
            }

            public override IEnumerable<ModelValidationResult> Validate(object container)
            {
                var vmdl = container as ViewModel;
                if (vmdl != null && vmdl is IDataErrorInfo)
                {
                    var error = (IDataErrorInfo)vmdl;
                    yield return new ModelValidationResult { Message = vmdl.Name + "; " + error[this.Metadata.PropertyName] };
                }

                //var field = Metadata.ContainerType.FindProperty(this.Metadata.PropertyName)
                //    .OfType<PropertyInfo>()
                //    .FirstOrDefault();
                //if (field != null)
                //{
                //    var vmdl = field.GetValue(container, null) as ViewModel;
                //    if (vmdl != null && vmdl is IDataErrorInfo)
                //    {
                //        var error = (IDataErrorInfo)vmdl;
                //        if (!string.IsNullOrWhiteSpace(error.Error))
                //        {
                //            yield return new ModelValidationResult { Message = error.Error };
                //        }
                //    }
                //}
            }
        }
    }
}
