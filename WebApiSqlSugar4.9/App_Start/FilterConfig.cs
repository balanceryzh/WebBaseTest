using System.Web;
using System.Web.Mvc;

namespace WebApiSqlSugar4._9
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
