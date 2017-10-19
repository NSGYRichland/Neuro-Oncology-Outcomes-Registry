using System.Web;
using System.Web.Mvc;

namespace TumorTaskforce_Research
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
