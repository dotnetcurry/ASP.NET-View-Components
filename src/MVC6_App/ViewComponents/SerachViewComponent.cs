using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewComponents;
using MVC6_App.Models;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace MVC6_App.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly AppDbContext ctx;
        public SearchViewComponent(AppDbContext c)
        {
            ctx = c;
        }

        public IViewComponentResult Invoke(object [] model, string property, string filter)
        {
            var t = (model[0]).GetType();

            PropertyInfo[] properties = t.GetProperties();

            IEnumerable Res = null;

            foreach (PropertyInfo p in properties)
            {
                if (p.Name == property)
                {
                    Res = from pP in model
                          where ((string)pP.GetType().GetProperty(p.Name).GetValue(pP, null)).StartsWith(filter)
                          select pP;

                    break;
                }
            }

            string html = "<h3>The Filtered Data</h3><ul>";
            foreach (var item in Res)
            {
              html +=  "<li>" + item.GetType().GetProperty(property).GetValue(item, null) + "</li>";
            }
            html += "</ul>"; 

            return new ContentViewComponentResult(new HtmlString(html));
        }
    }
}
