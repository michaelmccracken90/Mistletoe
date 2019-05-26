// <copyright file="HomeController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    ///     Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///     Index view
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        ///     About view
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        /// <summary>
        ///     Contact view
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        /// <summary>
        ///     Quick start view
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult QuickStart()
        {
            this.ViewBag.Message = "Quick Start";

            return this.View();
        }

        /// <summary>
        ///     FAQ View
        /// </summary>
        /// <returns>Action result</returns>
        public ActionResult FAQ()
        {
            this.ViewBag.Message = "FAQ";

            return this.View();
        }
    }
}