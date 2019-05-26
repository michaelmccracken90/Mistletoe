// <copyright file="EmailController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    ///     Email controller
    /// </summary>
    public class EmailController : Controller
    {
        /// <summary>
        ///     Index view
        /// </summary>
        /// <returns>View result</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        ///     Edit view
        /// </summary>
        /// <returns>View result</returns>
        public ActionResult Edit()
        {
            return this.View();
        }
    }
}