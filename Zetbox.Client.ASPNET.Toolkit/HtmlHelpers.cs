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

    public static class HtmlHelpers
    {
        #region Widget
        private class WidgetContainer : IDisposable
        {
            private readonly TextWriter _writer;
            public WidgetContainer(TextWriter writer, string title)
            {
                _writer = writer;

                _writer.Write("<div class=\"widget\"><div class=\"top\"><div class=\"bottom\"><div class=\"left\"><div class=\"right\"><div class=\"ro\"><div class=\"lo\"><div class=\"ru\"><div class=\"lu\">\n");
                _writer.Write("<h4 class=\"widget-title\">");
                _writer.Write(title);
                _writer.Write("</h4>\n");

                _writer.Write("<div class=\"widget-content\">\n");
            }

            public void Dispose()
            {
                _writer.Write("</div>\n");
                _writer.Write("</div></div></div></div></div></div></div></div></div>\n");
            }
        }
        public static IDisposable Widget(this IHtmlHelper html, string title)
        {
            return new WidgetContainer(html.ViewContext.Writer, title);
        }
        #endregion

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
            var newExpression = AppendMember<TModel, TValue, string>(expression, "FormattedValue");

            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var vmdl = modelExpressionProvider.CreateModelExpression(html.ViewData, expression)?.Model;

            return html.DisplayFor<string>(newExpression, GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
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
                return html.DisplayFor<string>(AppendMember<TModel, TValue, string>(expression, "FormattedValue"), GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
            }
            else
            {
                if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
                {
                    var enumVmdl = (EnumerationValueViewModel)vmdl;
                    return html.DropDownList(
                        modelExpressionProvider.GetExpressionText(expression) + ".Value",
                        enumVmdl
                            .PossibleValues
                            .Select(i => new SelectListItem()
                            {
                                Text = i.Value,
                                Value = i.Key != null ? i.Key.Value.ToString() : string.Empty,
                                Selected = i.Key == enumVmdl.Value
                            }));
                }
                else
                {
                    return html.EditorFor(expression, GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
                }
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

            return new HtmlString(string.Format("<div class=\"alert alert-danger\">{0}</div>",
                htmlHelper.ValidationSummary(excludePropertyErrors, "Error", null, null)));
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
