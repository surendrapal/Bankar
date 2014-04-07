using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace System.Web.Mvc
{
    public static class ViewExtensions
    {
        //public static string CheckBoxFor(this HtmlHelper helper, string target, string text)
        //{
        //    return String.Format("<label for='{0}'>{1}</label>", target, text);

        //}
        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, string value)
        {
            var builder = new TagBuilder("input");
            builder.Attributes["type"] = "checkbox";
            builder.Attributes["name"] = "hello";
            builder.Attributes["value"] = value;
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString CheckBoxFor<TModel, TProperty>
        (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var name = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            return CheckBox(htmlHelper, name, metadata.Model as string);
        }

        public static string MyValidationSummary(this HtmlHelper helper, string validationMessage = "")
        {
            StringBuilder retVal = new StringBuilder();
            if (helper.ViewData.ModelState.IsValid)
                return "";
            else
            {
                retVal.Append("<div class='alert alert-danger' >");
                retVal.Append("<button data-dismiss='alert' class='close' type='button'>");
                retVal.Append("<i class='icon-remove'></i></button>");
                if (!String.IsNullOrEmpty(validationMessage))
                {
                    retVal.Append("<strong>");
                    retVal.Append(helper.Encode(validationMessage));
                    retVal.Append("</strong>");
                }
                foreach (var key in helper.ViewData.ModelState.Keys)
                {
                    foreach (var err in helper.ViewData.ModelState[key].Errors)
                        retVal.Append("<p><strong>" + helper.Encode(err.ErrorMessage) + "</strong></p>");
                }
                retVal.Append("</div>");
            }
            return retVal.ToString();
        }
    }
}