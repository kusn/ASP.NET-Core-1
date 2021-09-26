using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    //[ViewComponent(Name = "BrandsView")]
    public class BrandsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

        /*public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }*/
    }
}
