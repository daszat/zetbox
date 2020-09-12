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
    using System.IO;
    using System.Linq.Expressions;
    using Zetbox.Client.Presentables;
    using System.Reflection;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using System.Web;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Text.Encodings.Web;
    using System.Data;
    using Zetbox.Client.Models;

    public static class HtmlHelpers
    {
        #region ZbHiddenID
        public static IHtmlContent ZbHiddenID(this IHtmlHelper helper, int ID)
        {
            return helper.Hidden("ID", ID > Helper.INVALIDID ? ID : Helper.INVALIDID, null);
        }
        #endregion

        #region Label
        /// <summary>
        /// Renders a label based on a given ILabeledViewModel
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression">Expression to use for determinating the target control name</param>
        /// <param name="htmlAttributes"></param>
        /// <param name="name">overrides expression</param>
        /// <param name="asReadOnly">Renders the for attribute always with FormattedValue</param>
        /// <returns></returns>
        public static IHtmlContent ZbLabelFor<TModel>(this IHtmlHelper<TModel> html, Expression<Func<TModel, ILabeledViewModel>> expression, object htmlAttributes = null, string name = null, bool asReadOnly = false)
        {
            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));

            var exprStr = name ?? modelExpressionProvider.GetExpressionText(expression);
            var mdl = modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;


            var lbmdl = mdl as ILabeledViewModel;
            var basemdl = mdl as BaseValueViewModel;

            if (asReadOnly || basemdl.IfNotNull(b => b.IsReadOnly))
            {
                // Readonly should be rendered with FormattedValue
                exprStr = exprStr + ".FormattedValue";
            }
            else if (lbmdl is EnumerationValueViewModel || lbmdl is NullableBoolPropertyViewModel || lbmdl is ObjectReferenceViewModel)
            {
                // The exceptions, rendered by Value
                exprStr = exprStr + ".Value";
            }
            else
            {
                exprStr = exprStr + ".FormattedValue";
            }

            var labelStr = string.Format("<label class=\"{4}\" for=\"{0}\"{1}>{2}{3}</label>",
                                                exprStr,
                                                string.Join("", HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Select(kv => string.Format(" {0}=\"{1}\"", kv.Key, kv.Value))),
                                                html.Encode(lbmdl.IfNotNull(v => v.Label).IfNullOrEmpty(exprStr)),
                                                !asReadOnly && lbmdl.Required ? " <span class=\"required\"></span>" : "",
                                                !asReadOnly && lbmdl.Required ? "required" : "");
            return new HtmlString(labelStr);
        }
        #endregion

        #region Display
        public static IHtmlContent ZbDisplayFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
            where TValue : BaseValueViewModel
        {
            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var vmdl = modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;

            return html.DisplayFor(expression, GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
        }
        #endregion

        #region Editor
        /// <summary>
        /// Select a default editor. Only usable for very primitiv types (int, string, DateTime). These types are rendered through EditorFor() and "FormattedValue" appended to the original expression. Enums are supported by rendering a dropdown.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="templateName"></param>
        /// <param name="htmlFieldName"></param>
        /// <param name="additionalViewData"></param>
        /// <returns></returns>
        public static IHtmlContent ZbEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
            where TValue : BaseValueViewModel
        {
            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var vmdl = (BaseValueViewModel)modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;

            var type = vmdl.GetType();
            if (vmdl.IsReadOnly)
            {
                return html.ZbDisplayFor(expression, null, htmlFieldName, additionalViewData);
            }
            else
            {
                return html.EditorFor(expression, GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
            }
        }

        public static IHtmlContent ZbEditorFor<TModel, TValue>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
            where TValue : DataObjectViewModel
        {
            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var vmdl = (DataObjectViewModel)modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;

            using (var writer = new System.IO.StringWriter())
            {
                foreach (var prop in vmdl.PropertyModels)
                {
                    if (prop is ObjectListViewModel || prop is ObjectCollectionViewModel) continue;

                    var propName = ((BasePropertyValueModel)prop.ValueModel).Property.Name;

                    Expression<Func<TModel, ILabeledViewModel>> lbExpr = Expression.Lambda<Func<TModel, ILabeledViewModel>>(
                            Expression.Call(Expression.Property(expression.Body, "PropertyModelsByName"), "get_Item", null, Expression.Constant(propName)),
                            expression.Parameters.ToArray());
                    Expression<Func<TModel, BaseValueViewModel>> propExpr = Expression.Lambda<Func<TModel, BaseValueViewModel>>(
                            Expression.Call(Expression.Property(expression.Body, "PropertyModelsByName"), "get_Item", null, Expression.Constant(propName)),
                            expression.Parameters.ToArray());

                    writer.WriteLine("<div class=\"form-group\">");
                    writer.WriteLine("<label class=\"col-md-3 control-label\">");
                    html.ZbLabelFor(lbExpr).WriteTo(writer, HtmlEncoder.Default);
                    writer.WriteLine("</label>");
                    writer.WriteLine("<div class=\"col-md-6\">");
                    html.ZbEditorFor(propExpr).WriteTo(writer, HtmlEncoder.Default);
                    writer.WriteLine("</div>");
                    writer.WriteLine("</div>");
                }

                return new HtmlString(writer.ToString());
            }
        }
        #endregion

        #region ValidationMessages
        [Obsolete("Add them in MVC Editor Templates instead!")]
        public static IHtmlContent ZbValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string validationMessage = null, object htmlAttributes = null)
         where TValue : BaseValueViewModel
        {
            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var vmdl = (BaseValueViewModel)modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;

            var type = vmdl.GetType();
            if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
            {
                return html.ValidationMessageFor<string>(AppendMember<TModel, TValue, string>(expression, "Value"), validationMessage, htmlAttributes, null);
            }
            else
            {
                return html.ValidationMessageFor<string>(AppendMember<TModel, TValue, string>(expression, "FormattedValue"), validationMessage, htmlAttributes, null);
            }
        }

        public static IHtmlContent StatusMessage(this IHtmlHelper helper, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return HtmlString.Empty;
            }

            return new HtmlString(string.Format("<div class=\"alert alert-success\">{0}</div>", HttpUtility.HtmlEncode(msg)));
        }

        public static IHtmlContent WarningMessage(this IHtmlHelper helper, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return HtmlString.Empty;
            }

            return new HtmlString(string.Format("<div class=\"alert alert-warning\">{0}</div>", HttpUtility.HtmlEncode(msg)));
        }

        public static IHtmlContent BootstrapValidationSummary(this IHtmlHelper htmlHelper, bool excludePropertyErrors = false)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            if (htmlHelper.ViewData.ModelState.IsValid || (excludePropertyErrors && (htmlHelper.ViewData.ModelState[""] == null || htmlHelper.ViewData.ModelState[""].Errors.Count() == 0)))
            {
                return HtmlString.Empty;
            }

            using (var writer = new System.IO.StringWriter())
            {
                htmlHelper.ValidationSummary(excludePropertyErrors, "Error", null, null).WriteTo(writer, HtmlEncoder.Default);
                return new HtmlString(string.Format("<div class=\"alert alert-danger\">{0}</div>", writer.ToString()));
            }
        }
        #endregion

        #region TextBox
        public static IHtmlContent ZbTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
            where TValue : BaseValueViewModel
        {
            return html.TextBoxFor<string>(AppendMember<TModel, TValue, string>(expression, "FormattedValue"), null, htmlAttributes);
        }
        #endregion

        #region Helper
        private static Expression<Func<TModel, TReturnValue>> AppendMember<TModel, TValue, TReturnValue>(Expression<Func<TModel, TValue>> expression, string member)
            where TValue : BaseValueViewModel
        {
            return Expression.Lambda<Func<TModel, TReturnValue>>(
                        Expression.Property(expression.Body, member),
                        expression.Parameters.ToArray());
        }

        /// <summary>
        /// Resolve only very basic kind of views. Don't use the ViewDescriptor infrastructure. In ASP.NET the HTML is fully controlled by the developer.
        /// Also, don't resole ObjectList/Collections -> this will render a table.
        /// Object references are also too complicated to be handled here.
        /// This method is used by EditorFor and DisplayFor. EditorFor would never resolve Enums - they will be rendered passing by value and possible values. All other by passing FormattedValue
        /// </summary>
        /// <param name="vmdl"></param>
        /// <param name="templateName"></param>
        /// <returns></returns>
        private static string GetTemplate(object vmdl, string templateName)
        {
            if (vmdl == null) return templateName;

            var type = vmdl.GetType();
            if (typeof(NullableDateTimePropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "NullableDateTimePropertyViewModel";
            }
            else if (typeof(NullableDecimalPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "NullableDecimalPropertyViewModel";
            }
            else if (typeof(NullableIntPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "NullableIntPropertyViewModel";
            }
            else if (typeof(NullableDoublePropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "NullableDoublePropertyViewModel";
            }
            else if (typeof(NullableGuidPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "NullableGuidPropertyViewModel";
            }
            else if (typeof(StringValueViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "StringValueViewModel";
            }
            else if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "EnumerationValueViewModel";
            }
            else if (typeof(ObjectReferenceViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "ObjectReferenceViewModel";
            }
            else
            {
                return templateName;
            }
        }
        #endregion
    }
}
