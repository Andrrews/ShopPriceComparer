using Microsoft.AspNetCore.Mvc;namespace ShopPriceComparer.WebUI.Controllers{
    /// <summary>
    /// This controller is responsible for handling the documentation related requests.
    /// </summary>
    /// <returns>
    /// It redirects the user to the Index.html page of the Help directory.
    /// </returns>
    public class DocumentationController : Controller    {
        /// <summary>
        /// This method is used to redirect the user to the Help Index page.
        /// </summary>
        /// <returns>
        /// An IActionResult that redirects to the Help Index page.
        /// </returns>
        public IActionResult Index()        {            return Redirect("~/Help/Index.html");        }    }}