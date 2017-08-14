using System.Web;
using System.Web.Mvc;

namespace APISwaggerNeo4j
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
