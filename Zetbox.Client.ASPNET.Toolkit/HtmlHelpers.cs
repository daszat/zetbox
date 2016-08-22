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
    using System.IO;
    using System.Web.Mvc.Html;
    using System.Linq.Expressions;
    using Zetbox.Client.Presentables;
    using System.Reflection;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;
    using System.Web;

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
        public static IDisposable Widget(this HtmlHelper html, string title)
        {
            return new WidgetContainer(html.ViewContext.Writer, title);
        }
        #endregion

        #region ZbHiddenID
        public static MvcHtmlString ZbHiddenID(this HtmlHelper helper, int ID)
        {
            return InputExtensions.Hidden(helper, "ID", ID > Helper.INVALIDID ? ID : Helper.INVALIDID);
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
        public static MvcHtmlString ZbLabelFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, ILabeledViewModel>> expression, object htmlAttributes = null, string name = null, bool asReadOnly = false)
        {
            var exprStr = name ?? ExpressionHelper.GetExpressionText(expression);
            var mdl = System.Web.Mvc.ModelMetadata.FromLambdaExpression<TModel, ILabeledViewModel>(expression, html.ViewData).IfNotNull(meta => meta.Model);
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

            var labelStr = string.Format("<label for=\"{0}\"{1}>{2}</label>",
                                                exprStr,
                                                string.Join("", HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Select(kv => string.Format(" {0}=\"{1}\"", kv.Key, kv.Value))),
                                                lbmdl.IfNotNull(v => v.Label).IfNullOrEmpty(exprStr));
            return MvcHtmlString.Create(labelStr);
        }
        #endregion

        #region Display
        public static MvcHtmlString ZbDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
            where TValue : BaseValueViewModel
        {
            var newExpression = AppendMember<TModel, TValue, string>(expression, "FormattedValue");
            var vmdl = (BaseValueViewModel)System.Web.Mvc.ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData).Model;
            return DisplayExtensions.DisplayFor<TModel, string>(html, newExpression, GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
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
        [Obsolete("Use MVC Editor Templates instead!")]
        public static MvcHtmlString ZbEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
            where TValue : BaseValueViewModel
        {
            var vmdl = (BaseValueViewModel)ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData).Model;
            var type = vmdl.GetType();
            if (vmdl.IsReadOnly)
            {
                return DisplayExtensions.DisplayFor<TModel, string>(html, AppendMember<TModel, TValue, string>(expression, "FormattedValue"), GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
            }
            else
            {
                if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
                {
                    var enumVmdl = (EnumerationValueViewModel)vmdl;
                    return SelectExtensions.DropDownList(
                        html,
                        ExpressionHelper.GetExpressionText(expression) + ".Value",
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
                    return EditorExtensions.EditorFor<TModel, string>(html, AppendMember<TModel, TValue, string>(expression, "FormattedValue"), GetTemplate(vmdl, templateName), htmlFieldName, additionalViewData);
                }
            }
        }
        #endregion

        #region ValidationMessages
        [Obsolete("Add them in MVC Editor Templates instead!")]
        public static MvcHtmlString ZbValidationMessageFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string validationMessage = null, object htmlAttributes = null)
             where TValue : BaseValueViewModel
        {
            var vmdl = (BaseValueViewModel)ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData).Model;
            var type = vmdl.GetType();
            if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
            {
                return ValidationExtensions.ValidationMessageFor<TModel, string>(html, AppendMember<TModel, TValue, string>(expression, "Value"), validationMessage, htmlAttributes);
            }
            else
            {
                return ValidationExtensions.ValidationMessageFor<TModel, string>(html, AppendMember<TModel, TValue, string>(expression, "FormattedValue"), validationMessage, htmlAttributes);
            }
        }

        public static MvcHtmlString StatusMessage(this HtmlHelper helper, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return MvcHtmlString.Empty;
            }

            return new MvcHtmlString(string.Format("<div class=\"alert alert-success\">{0}</div>", HttpUtility.HtmlEncode(msg)));
        }

        public static MvcHtmlString WarningMessage(this HtmlHelper helper, string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                return MvcHtmlString.Empty;
            }

            return new MvcHtmlString(string.Format("<div class=\"alert alert-warning\">{0}</div>", HttpUtility.HtmlEncode(msg)));
        }

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors = false)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            if (htmlHelper.ViewData.ModelState.IsValid || (excludePropertyErrors && (htmlHelper.ViewData.ModelState[""] == null || htmlHelper.ViewData.ModelState[""].Errors.Count() == 0)))
            {
                return MvcHtmlString.Empty;
            }

            return new MvcHtmlString(string.Format("<div class=\"alert alert-danger\">{0}</div>",
                htmlHelper.ValidationSummary(excludePropertyErrors).ToHtmlString()));
        }
        #endregion


        #region TextBox
        public static MvcHtmlString ZbTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
            where TValue : BaseValueViewModel
        {
            return InputExtensions.TextBoxFor<TModel, string>(html, AppendMember<TModel, TValue, string>(expression, "FormattedValue"), htmlAttributes);
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
                return templateName ?? "DateTime";
            }
            else if (typeof(NullableDecimalPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "Decimal";
            }
            else if (typeof(NullableIntPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "Integer";
            }
            else if (typeof(NullableDoublePropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "Double";
            }
            else if (typeof(NullableGuidPropertyViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "Guid";
            }
            else if (typeof(StringValueViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "String";
            }
            else if (typeof(EnumerationValueViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "Enumeration";
            }
            else if (typeof(ObjectReferenceViewModel).IsAssignableFrom(type))
            {
                return templateName ?? "ObjectReference";
            }
            else
            {
                return templateName;
            }
        }
        #endregion
    }
}
