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
    using Autofac;
    using Zetbox.Client.Presentables;
    using System.ComponentModel;
    using Zetbox.API.Utils;
    using Zetbox.API;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    #region ZetboxViewModelBinder
    public interface IZetboxViewModelBinder : IModelBinder
    {
    }

    public class ZetboxViewModelBinder : ComplexTypeModelBinder, IZetboxViewModelBinder
    {
        IViewModelFactory _vmf;
        ZetboxContextHttpScope _scope;

        public ZetboxViewModelBinder(IViewModelFactory vmf, ZetboxContextHttpScope scope, 
            IDictionary<ModelMetadata, IModelBinder> propertyBinders, ILoggerFactory loggerFactory)
            : base(propertyBinders, loggerFactory, allowValidatingTopLevelNodes: true)
        {
            _vmf = vmf;
            _scope = scope;
        }

        protected override object CreateModel(ModelBindingContext bindingContext)
        {
            return _vmf.CreateViewModel<ViewModel.Factory>(bindingContext.ModelType).Invoke(_scope.Context, null);
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
                var propertyBinders = new Dictionary<ModelMetadata, IModelBinder>();
                for (var i = 0; i < context.Metadata.Properties.Count; i++)
                {
                    var property = context.Metadata.Properties[i];
                    propertyBinders.Add(property, context.CreateBinder(property));
                }

                return new ZetboxViewModelBinder(
                    context.Services.GetRequiredService<IViewModelFactory>(),
                    context.Services.GetRequiredService<ZetboxContextHttpScope>(),
                    propertyBinders,
                    context.Services.GetRequiredService<ILoggerFactory>());
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
        private readonly ModelBinderProviderContext binderProviderContext;
        public readonly Type TKey;
        public readonly Type TValue;

        public LookupDictionaryModelBinder(Type TKey, Type TValue, ModelBinderProviderContext binderProviderContext)
        {
            this.TKey = TKey;
            this.TValue = TValue;
            this.binderProviderContext = binderProviderContext;
        }

        private IEnumerable<string> GetValueProviderKeys(ModelBindingContext context)
        {
            List<string> keys = new List<string>();
            keys.AddRange(context.HttpContext.Request.Form.Keys.Cast<string>());
            keys.AddRange(context.HttpContext.Request.Query.Keys.Cast<string>());
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

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            object result = bindingContext.Model;
            if (result == null) return; // Cannot bind on empty list

            foreach (string key in GetValueProviderKeys(bindingContext))
            {
                if (key.StartsWith(bindingContext.ModelName + "[", StringComparison.InvariantCultureIgnoreCase))
                {
                    int endbracket = key.IndexOf("]", bindingContext.ModelName.Length + 1);
                    if (endbracket == -1) continue;

                    var strDictKey = key.Substring(bindingContext.ModelName.Length + 1, endbracket - bindingContext.ModelName.Length - 1);
                    var dictKey = ConvertType(strDictKey, TKey);
                    if (dictKey == null) continue;

                    var currentValue = bindingContext.ModelType.GetProperty("Item").GetValue(result, new object[] { dictKey });
                    var modelName = key.Substring(0, endbracket + 1);
                    var fieldName = key.Substring(endbracket + 2);

                    var services = bindingContext.HttpContext.RequestServices;
                    var provider = services.GetRequiredService<IModelMetadataProvider>();
                    var nestedMetaData = provider.GetMetadataForType(currentValue.GetType());

                    using (bindingContext.EnterNestedScope(
                        modelMetadata: nestedMetaData,
                        fieldName: fieldName,
                        modelName: modelName,
                        model: currentValue))
                    {
                        IModelBinder valueBinder = binderProviderContext.CreateBinder(nestedMetaData);
                        await valueBinder.BindModelAsync(bindingContext);
                    }
                }
            }
        }
    }

    public interface ILookupDictionaryModelBinderProvider : IModelBinderProvider
    {
    }

    public class LookupDictionaryModelBinderProvider : ILookupDictionaryModelBinderProvider
    {
        public LookupDictionaryModelBinderProvider()
        {
        }
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.IsGenericType && context.Metadata.ModelType.GetGenericTypeDefinition() == typeof(LookupDictionary<,,>))
            {
                Type[] ga = context.Metadata.ModelType.GetGenericArguments();
                var TKey = ga[0];
                var TValue = ga[2];

                return new LookupDictionaryModelBinder(TKey, TValue, context);
            }

            return null;
        }
    }
    #endregion
}
