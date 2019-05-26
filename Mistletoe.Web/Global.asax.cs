// <copyright file="Global.asax.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web
{
    using System;
    using System.Configuration;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Mistletoe.Common;
    using Mistletoe.Web.Controllers;
    using NLog;
    using RestSharp;

    /// <summary>
    ///     MVC Application
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        private static string userID;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets IsAdmin
        /// </summary>
        public static bool IsAdmin { get; set; }

        /// <summary>
        /// Gets or sets UserName
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// Gets or sets UserImage
        /// </summary>
        public static string UserImage { get; set; }

        /// <summary>
        /// Gets or sets UserID
        /// </summary>
        public static string UserID
        {
            get
            {
                if (string.IsNullOrEmpty(userID))
                {
                    return string.Empty;
                }
                else
                {
                    return userID;
                }
            }

            set
            {
                userID = value;
            }
        }

        /// <summary>
        ///     Gets or sets Logger instance
        /// </summary>
        public static Logger GlobalLogger { get; set; }

        /// <summary>
        ///     Mistletoe API Client
        /// </summary>
        public static RestClient MistletoeAPIClient { get; set; }

        /// <summary>
        ///     Gets or sets Global Email Footer Path
        /// </summary>
        internal static string GlobalEmailFooterFilePath { get; set; }

        /// <summary>
        ///     Initialize managers
        /// </summary>
        /// <param name="footerPath">Path to footer</param>
        public static void InitializeManagers(string footerPath = "")
        {
            AutoMapperConfig.RegisterMapping();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimsIdentity.DefaultNameClaimType;

            if (!string.IsNullOrEmpty(footerPath))
            {
                GlobalEmailFooterFilePath = footerPath;
            }
        }

        /// <summary>
        ///     Application start
        /// </summary>
        protected void Application_Start()
        {
            GlobalLogger = LogManager.GetCurrentClassLogger();
            GlobalLogger.Log(LogLevel.Debug, "Application Start");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeManagers();

            var mistletoeApiUrl = ConfigurationManager.AppSettings["MistletoeAPI_URL"];
            GlobalLogger.Log(LogLevel.Debug, "mistletoeApiUrl:" + mistletoeApiUrl);
            MistletoeAPIClient = new RestClient(mistletoeApiUrl);
            
            GlobalEmailFooterFilePath = HttpContext.Current.Server.MapPath(@"~/App_Data/Footer.html");
        }

        /// <summary>
        ///     Application end
        /// </summary>
        protected void Application_End()
        {
            GlobalLogger.Log(LogLevel.Debug, "Application End");
        }

        /// <summary>
        ///     Application error handling
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            GlobalLogger.Log(LogLevel.Error, this.Server.GetLastError(), "Application Error");

            var httpContext = ((MvcApplication)sender).Context;
            var currentController = " ";
            var currentAction = " ";
            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !string.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !string.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            var ex = this.Server.GetLastError();
            var routeData = new RouteData();
            var action = "GenericError";

            if (ex is HttpException)
            {
                var httpEx = ex as HttpException;

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                        // others if any
                }
            }

            httpContext.ClearError();
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            httpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;
            routeData.Values["exception"] = new HandleErrorInfo(ex, currentController, currentAction);

            IController errormanagerController = new ErrorController();
            HttpContextWrapper wrapper = new HttpContextWrapper(httpContext);
            var rc = new RequestContext(wrapper, routeData);
            errormanagerController.Execute(rc);
        }

    }
}
