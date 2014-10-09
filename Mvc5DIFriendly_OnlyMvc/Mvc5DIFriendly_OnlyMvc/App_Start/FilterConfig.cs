using System.Web;
using System.Web.Mvc;

namespace Mvc5DIFriendly_OnlyMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
