using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Website.Controllers
{
    public class AjaxContentController : Controller
    {
        public IActionResult StoreGalleryIndex(int nailStoreId)
        {
            return ViewComponent("StoreGallery", new { nailStoreId = nailStoreId });
        }
        public IActionResult StoreDescriptionIndex(int nailStoreId)
        {
            return ViewComponent("StoreGallery", new { nailStoreId = nailStoreId, isGetDescription=true });
        }
        
    }
}