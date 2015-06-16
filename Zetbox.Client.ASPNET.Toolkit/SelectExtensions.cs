using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;


namespace Zetbox.Client.ASPNET
{
    /// <summary>
    /// http://stackoverflow.com/a/11340660/178517
    /// </summary>
    public static class ZbSelectExtensions
    {
        public static IHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string selectedValue, string optionLabel, object htmlAttributes = null)
        {
            return DropDownListHelper(helper, ExpressionHelper.GetExpressionText(expression), selectList, selectedValue, optionLabel, htmlAttributes);
        }

        /// <summary>
        /// This is almost identical to the one in ASP.NET MVC 3 however it removes the default values stuff so that the Selected property of the SelectListItem class actually works
        /// </summary>
        private static IHtmlString DropDownListHelper(HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, string selectedValue, string optionLabel, object htmlAttributes)
        {
            name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            // Convert each ListItem to an option tag
            var listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered
            if (optionLabel != null)
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem() { Text = optionLabel, Value = String.Empty, Selected = false }, selectedValue));

            // Add the other options
            foreach (var item in selectList)
            {
                listItemBuilder.AppendLine(ListItemToOption(item, selectedValue));
            }

            // Now add the select tag
            var tag = new TagBuilder("select") { InnerHtml = listItemBuilder.ToString() };
            tag.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tag.MergeAttribute("name", name, true);
            tag.GenerateId(name);

            // If there are any errors for a named field, we add the css attribute
            ModelState modelState;

            if (helper.ViewData.ModelState.TryGetValue(name, out modelState))
            {
                if (modelState.Errors.Count > 0)
                    tag.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }

            // Add the unobtrusive validation attributes
            tag.MergeAttributes(helper.GetUnobtrusiveValidationAttributes(name));

            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
        }

        private static string ListItemToOption(SelectListItem item, string selectedValue)
        {
            var tag = new TagBuilder("option") { InnerHtml = HttpUtility.HtmlEncode(item.Text) };

            if (item.Value != null)
                tag.Attributes["value"] = item.Value;

            if ((!string.IsNullOrEmpty(selectedValue) && item.Value == selectedValue) || item.Selected)
                tag.Attributes["selected"] = "selected";

            return tag.ToString(TagRenderMode.Normal);
        }
    }
}
