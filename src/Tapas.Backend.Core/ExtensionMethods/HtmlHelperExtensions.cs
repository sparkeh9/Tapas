namespace Tapas.Backend.Core.ExtensionMethods
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

    public static class HtmlHelperExtensions
    {
        public static string AddValidationErrorClassFor<TModel, TResult>( this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, string className="has-error")
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof (expression));
            }

            string propertyName = ExpressionHelper.GetExpressionText( expression );
            return htmlHelper.ViewData.ModelState.ContainsKey( propertyName ) ? className : null;
        }
    }
}