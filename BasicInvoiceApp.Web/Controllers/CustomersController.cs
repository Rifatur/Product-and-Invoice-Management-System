using Microsoft.AspNetCore.Mvc;

namespace BasicInvoiceApp.Web.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
