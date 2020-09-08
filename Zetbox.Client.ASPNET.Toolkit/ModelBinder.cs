﻿// This file is part of zetbox.
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
    using Autofac;
    using Zetbox.Client.Presentables;
    using System.ComponentModel;
    using Zetbox.API.Utils;
    using Zetbox.API;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using System.Threading.Tasks;

    #region ZetboxViewModelBinder
    public interface IZetboxViewModelBinder : IModelBinder
    {
    }

    public class ZetboxViewModelBinder : IZetboxViewModelBinder
    {
        IViewModelFactory _vmf;
        IMVCValidationManager _validation;
        ZetboxContextHttpScope _scope;

        public ZetboxViewModelBinder(IViewModelFactory vmf, IMVCValidationManager validation, ZetboxContextHttpScope scope)
        {
            _vmf = vmf;
            _validation = validation;
            _scope = scope;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));


            return Task.CompletedTask;
        }
    }

    public interface IZetboxViewModelBinderProvider : IModelBinderProvider
    {
    }

    public class ZetboxViewModelBinderProvider : IZetboxViewModelBinderProvider
    {
        public ZetboxViewModelBinderProvider()
        {
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (typeof(ViewModel).IsAssignableFrom(context.Metadata.ModelType))
            {
                return new BinderTypeModelBinder(typeof(ZetboxViewModelBinder));
            }

            return null;
        }
    }
    #endregion

    #region LookupDictionaryModelBinder
    public interface ILookupDictionaryModelBinder : IModelBinder
    {
    }

    public class LookupDictionaryModelBinder : ILookupDictionaryModelBinder
    {
        private IEnumerable<string> GetValueProviderKeys(ModelBindingContext context)
        {
            List<string> keys = new List<string>();
            keys.AddRange(context.HttpContext.Request.Form.Keys.Cast<string>());
            // keys.AddRange(((IDictionary<string, object>)context.RouteData.Values).Keys.Cast<string>());
            keys.AddRange(context.HttpContext.Request.Query.Keys.Cast<string>());
            // keys.AddRange(context.HttpContext.Request.Files.Keys.Cast<string>());
            return keys;
        }

        private object ConvertType(string stringValue, Type type)
        {
            try
            {
                return TypeDescriptor.GetConverter(type).ConvertFrom(stringValue);
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            object result = bindingContext.Model;
            if (result == null) return null; // Cannot bind on empty list

            throw new NotImplementedException();

            //Type[] ga = bindingContext.ModelType.GetGenericArguments();
            //var TKey = ga[0];
            //var TValue = ga[2];
            //IModelBinder valueBinder = Binders.GetBinder(TValue);

            //foreach (string key in GetValueProviderKeys(bindingContext))
            //{
            //    if (key.StartsWith(bindingContext.ModelName + "[", StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        int endbracket = key.IndexOf("]", bindingContext.ModelName.Length + 1);
            //        if (endbracket == -1) continue;

            //        var strDictKey = key.Substring(bindingContext.ModelName.Length + 1, endbracket - bindingContext.ModelName.Length - 1);
            //        var dictKey = ConvertType(strDictKey, TKey);
            //        if (dictKey == null) continue;

            //        var currentValue = bindingContext.ModelType.GetProperty("Item").GetValue(result, new object[] { dictKey });
            //        var modelName = key.Substring(0, endbracket + 1);

            //        ModelBindingContext innerBindingContext = new ModelBindingContext()
            //        {
            //            ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => currentValue, currentValue.GetType()),
            //            ModelName = modelName,
            //            ModelState = bindingContext.ModelState,
            //            PropertyFilter = bindingContext.PropertyFilter,
            //            ValueProvider = bindingContext.ValueProvider
            //        };

            //        valueBinder.BindModelAsync(bindingContext, innerBindingContext);
            //    }
            //}

            //return Task.CompletedTask;
        }
    }

    public interface ILookupDictionaryModelBinderProvider : IModelBinderProvider
    {
    }

    public class LookupDictionaryModelBinderProvider : ILookupDictionaryModelBinderProvider
    {
        private readonly Func<ILookupDictionaryModelBinder> _factory;

        public LookupDictionaryModelBinderProvider(Func<ILookupDictionaryModelBinder> factory)
        {
            _factory = factory;
        }
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsGenericType && context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(LookupDictionary<,,>))
            {
                return new BinderTypeModelBinder(typeof(ZetboxViewModelBinder));
            }

            return null;
        }
    }
    #endregion
}
