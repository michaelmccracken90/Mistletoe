﻿// <copyright file="AjaxValidateAntiForgeryTokenAttribute.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Helpers
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    /// <summary>
    ///     Anti Forgery attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        ///     On Authorization handling
        /// </summary>
        /// <param name="filterContext">Filter context</param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    this.ValidateRequestHeader(filterContext.HttpContext.Request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException exception)
            {
                // Invalid token
                throw new HttpAntiForgeryException("Anti forgery token not found:" + exception.Message);
            }
        }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = string.Empty;
            string formToken = string.Empty;
            string tokenValue = request.Headers["VerificationToken"]; // read the header key and validate the tokens.
            if (!string.IsNullOrEmpty(tokenValue))
            {
                string[] tokens = tokenValue.Split(',');
                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken); // this validates the request token.
        }
    }
}