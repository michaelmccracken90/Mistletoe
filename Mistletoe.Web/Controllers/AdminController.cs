// <copyright file="AdminController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;
    using Mistletoe.Common;
    using Mistletoe.Models;
    using Mistletoe.Web.Helpers;
    using Newtonsoft.Json;
    using NLog;

    /// <summary>
    ///     Admin Controller
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        ///     Index View
        /// </summary>
        /// <returns>View model</returns>
        public ActionResult Index()
        {
            AdminPageModel pageModel = new AdminPageModel();
            bool userIsAdmin = false;

            // Check if logged-in user is an Admin
            userIsAdmin = MistletoeAPIHelper.IsUserAdmin(MvcApplication.UserID);

            if (userIsAdmin)
            {
                pageModel.UserHasAccess = true;

                var dbUsers = MistletoeAPIHelper.GetAllUserModels();

                dbUsers.ForEach(u =>
                {
                    // Only display non-admin users
                    if (!u.IsAdmin)
                    {
                        pageModel.UsersList.Add(u);
                    }
                });

                try
                {
                    pageModel.Footer = FileHelper.ReadFile(MvcApplication.GlobalEmailFooterFilePath);
                }
                catch (IOException ioEx)
                {
                    MvcApplication.GlobalLogger.Log(LogLevel.Error, ResourceHelper.ResourceValue("AdminIndexError") + ioEx.Message);
                }
                catch (Exception ex)
                {
                    MvcApplication.GlobalLogger.Log(LogLevel.Error, ResourceHelper.ResourceValue("AdminIndexError") + ex.Message);
                }
            }

            return this.View(pageModel);
        }

        /// <summary>
        ///     Change user status
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="isActive">Is Active</param>
        /// <returns>Success and Error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult ChangeUserStatus(string userID, string isActive)
        {
            bool success = false;
            string errorMsg = string.Empty;
            bool userIsAdmin = false;

            // Check if logged-in user is an Admin
            userIsAdmin = MistletoeAPIHelper.IsUserAdmin(MvcApplication.UserID);

            if (userIsAdmin)
            {
                bool is_Active = false;
                if (bool.TryParse(isActive, out is_Active))
                {
                    // If user is being deactivated
                    if (!is_Active)
                    {
                        // Deactivate all campaigns associated with this user
                        success = MistletoeAPIHelper.ChangeCampaignStatusByUser(userID, is_Active);
                    }
                    else
                    {
                        success = true;
                    }

                    // Now change user status
                    if (success)
                    {
                        success = MistletoeAPIHelper.ChangeUserStatus(userID, is_Active);                        
                    }
                    else
                    {
                        errorMsg = ResourceHelper.ResourceValue("AdminChangeUserStatusTryAgain");
                    }
                }
                else
                {
                    errorMsg = ResourceHelper.ResourceValue("AdminChangeUserStatusInvalidStatus");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
            }

            return this.Json(
                new
                {
                    Success = success,
                    ErrorMsg = errorMsg
                },
               JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Get active campaigns
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Success, Error message and campaign count</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult GetActiveCampaigns(string userID)
        {
            bool success = false;
            string errorMsg = string.Empty;
            string campaignCount = "0";
            bool userIsAdmin = false;

            // Check if logged-in user is an Admin
            userIsAdmin = MistletoeAPIHelper.IsUserAdmin(MvcApplication.UserID);

            if (userIsAdmin)
            {
                var campaignList = MistletoeAPIHelper.GetAllCampaignModelsByUser(MvcApplication.UserID);
                
                if (campaignList != null)
                {
                    campaignCount = campaignList.Count.ToString();
                    success = true;
                }
                else
                {
                    errorMsg = ResourceHelper.ResourceValue("AdminGetActiveCampaignsError");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
            }

            return this.Json(
                new
                {
                    Success = success,
                    ErrorMsg = errorMsg,
                    CampaignCount = campaignCount
                },
               JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Save footer
        /// </summary>
        /// <param name="newFooter">New Footer</param>
        /// <returns>Success and Error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveFooter(string newFooter)
        {
            bool success = false;
            string errorMsg = string.Empty;
            bool userIsAdmin = false;

            // Check if logged-in user is an Admin
            userIsAdmin = MistletoeAPIHelper.IsUserAdmin(MvcApplication.UserID);

            if (userIsAdmin)
            {
                try
                {
                    FileHelper.SaveFile(MvcApplication.GlobalEmailFooterFilePath, newFooter);
                    success = true;
                }
                catch (IOException ioEx)
                {
                    MvcApplication.GlobalLogger.Log(LogLevel.Error, ResourceHelper.ResourceValue("AdminSaveFooterError") + ioEx.Message);
                    errorMsg = ResourceHelper.ResourceValue("AdminSaveFooterWritingError");
                }
                catch (Exception ex)
                {
                    MvcApplication.GlobalLogger.Log(LogLevel.Error, ResourceHelper.ResourceValue("AdminSaveFooterError") + ex.Message);
                    errorMsg = ResourceHelper.ResourceValue("AdminSaveFooterWritingError");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
            }

            return this.Json(
                new
                {
                    Success = success,
                    ErrorMsg = errorMsg
                },
               JsonRequestBehavior.AllowGet);
        }
    }
}