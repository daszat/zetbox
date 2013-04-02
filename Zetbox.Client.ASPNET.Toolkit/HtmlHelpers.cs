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
    using System.Web.Mvc;
    using System.IO;
    using System.Web.Mvc.Html;
    using System.Linq.Expressions;
    using Zetbox.Client.Presentables;
    using System.Reflection;
    using Zetbox.Client.Presentables.ValueViewModels;

    public static class HtmlHelpers
    {
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

        public static MvcHtmlString ZbLabel(this HtmlHelper html, ILabeledViewModel vmdl)
        {
            return LabelExtensions.Label(html, vmdl.Label);
        }

        public static MvcHtmlString ZbLabelFor<T>(this HtmlHelper<T> html, Expression<Func<T, ILabeledViewModel>> expression)
        {
            var result = (ILabeledViewModel)System.Web.Mvc.ModelMetadata.FromLambdaExpression<T, ILabeledViewModel>(expression, html.ViewData).Model;
            return LabelExtensions.Label(html, result.Label);
        }

        public static MvcHtmlString ZbEditorFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string templateName = null, string htmlFieldName = null, object additionalViewData = null)
            where TValue : BaseValueViewModel
        {
            var body = expression.Body;
            var formattedValueExpression = Expression.MakeMemberAccess(body, typeof(BaseValueViewModel).GetMember("FormattedValue").Single());
            var newLambda = Expression.Lambda<Func<TModel, string>>(formattedValueExpression, expression.Parameters.ToArray());

            return EditorExtensions.EditorFor<TModel, string>(html, newLambda, templateName, htmlFieldName, additionalViewData);
        }

        public static MvcHtmlString ZbTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
            where TValue : BaseValueViewModel
        {
            var body = expression.Body;
            var formattedValueExpression = Expression.MakeMemberAccess(body, typeof(BaseValueViewModel).GetMember("FormattedValue").Single());
            var newLambda = Expression.Lambda<Func<TModel, string>>(formattedValueExpression, expression.Parameters.ToArray());

            return InputExtensions.TextBoxFor<TModel, string>(html, newLambda, htmlAttributes);
        }
    }
}
