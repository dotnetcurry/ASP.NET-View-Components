using Microsoft.AspNet.Mvc;
using MVC6_App.Models;
using System.Linq;

namespace MVC6_App.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly AppDbContext ctx;

        public ProductListViewComponent(AppDbContext c)
        {
            ctx = c;
        }

        public IViewComponentResult Invoke(string filter)
        {
            var res = (from p in ctx.Product.ToList()
                       where p.ProductName.StartsWith(filter)
                       select p).ToList();


            return View(res);
        }
    }
}
