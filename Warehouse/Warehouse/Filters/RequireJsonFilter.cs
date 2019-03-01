using System.Linq;
using Warehouse.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Warehouse.Filters
{
    public class RequireJsonFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HasApplicationJsonHeader(context) || HasUnrestrictedContentTypeFilter(context) || HasContentType(context))
            {
                CallBaseActionExecuting(context);
            }
            else
            {
                throw new UnsupportedMediaFormatException("Content-Type should be 'application/json'");
            }
        }
        
        public virtual void CallBaseActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        private static bool HasApplicationJsonHeader(ActionExecutingContext context)
        {           
            return context.HttpContext.Request.Headers.Any(h => h.Key.Equals("Content-Type") && h.Value.Any(s => s.Contains("application/json")));
        }
        
        private static bool HasUnrestrictedContentTypeFilter(ActionExecutingContext context)
        {
            return context?.ActionDescriptor?.FilterDescriptors?.Any(x => x.Filter.GetType() == typeof(UnrestrictedContentTypeFilter)) ?? false;
        }

        private static bool HasContentType(ActionExecutingContext context)
        {
            return context.HttpContext.Request.Method == "GET" || context.HttpContext.Request.Method == "DELETE";
        }
    }
}
