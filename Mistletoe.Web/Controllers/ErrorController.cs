// <copyright file="ErrorController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    ///     Error controller
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        ///     Index view
        /// </summary>
        /// <returns>Action result as redirect</returns>
        public ActionResult Index()
        {
            return this.RedirectToAction("GenericError", new HandleErrorInfo(new HttpException(403, "Generic Error"), "ErrorController", "Index"));
        }

        /// <summary>
        ///     Generic Error view
        /// </summary>
        /// <param name="exception">Error exception</param>
        /// <returns>View result with exception</returns>
        public ViewResult GenericError(HandleErrorInfo exception)
        {
            return this.View("Error", exception);
        }

        /// <summary>
        ///     Not Found View
        /// </summary>
        /// <param name="exception">Error exception</param>
        /// <returns>View result with exception</returns>
        public ViewResult NotFound(HandleErrorInfo exception)
        {
            this.ViewBag.Title = "Page Not Found";
            return this.View("Error", exception);
        }
    }
}