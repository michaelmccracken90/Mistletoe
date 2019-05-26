// <copyright file="CampaignController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using Mistletoe.Common;
    using Mistletoe.Email;
    using Mistletoe.Models;
    using Mistletoe.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    /// <summary>
    ///     Campaign controller
    /// </summary>
    [Authorize]
    public class CampaignController : Controller
    {
        /// <summary>
        ///     Index view
        /// </summary>
        /// <returns>View with page model</returns>
        public ActionResult Index()
        {
            List<CampaignModel> campaignList = null;
            string campaignIDs = string.Empty;

            campaignList = MistletoeAPIHelper.GetAllCampaignModelsByUser(MvcApplication.UserID);

            if (campaignList != null)
            {
                campaignList.ForEach(c =>
                {
                    campaignIDs += c.CampaignID + ",";
                });
            }

            ManageCampaignsModel pageModel = new ManageCampaignsModel();
            pageModel.CampaignList = campaignList;
            pageModel.CampaignIDs = campaignIDs.TrimEnd(',');

            return this.View(pageModel);
        }

        /// <summary>
        ///     Create view
        /// </summary>
        /// <returns>View itself</returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        ///     Save campaign
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="campaignName">Campaign Name</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="frequency">Frequency</param>
        /// <param name="dataFilePath">Data file path</param>
        /// <returns>Action result with success and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveCampaign(string campaignID, string campaignName, string startDate, string endDate, string frequency, string dataFilePath)
        {
            bool success = false;
            string errorMsg = string.Empty;

            int campaignIDValue = 0;
            if (int.TryParse(campaignID, out campaignIDValue))
            {
                DateTime startDateValue, endDateValue;
                startDateValue = this.StringToDate(startDate);
                endDateValue = this.StringToDate(endDate);
                if (endDateValue >= startDateValue)
                {
                    CampaignModel tempModel = new CampaignModel();
                    tempModel.CampaignName = campaignName;
                    tempModel.StartDate = startDateValue.ToUniversalTime();
                    tempModel.EndDate = endDateValue.ToUniversalTime();
                    tempModel.Frequency = frequency;
                    tempModel.DataFilePath = dataFilePath;

                    if (campaignIDValue == -1)
                    {
                        // Create Campaign
                        tempModel.UserID = MvcApplication.UserID;
                        tempModel.IsActive = false;
                        tempModel.IsArchived = false;

                        campaignIDValue = MistletoeAPIHelper.CreateCampaignModel(tempModel);

                        if (campaignIDValue != -1)
                        {
                            // Now move the associated file to appropriate folder
                            if (string.IsNullOrEmpty(dataFilePath) == false)
                            {
                                string newFilePath = string.Empty;
                                this.MoveFile(campaignIDValue.ToString(), dataFilePath, out newFilePath);

                                // Update the path in db
                                bool updateSuccess = MistletoeAPIHelper.UpdateCampaignFilePath(campaignIDValue.ToString(), newFilePath);
                            }

                            success = true;
                        }
                    }
                    else
                    {
                        // Edit Campaign
                        tempModel.CampaignID = campaignIDValue.ToString();

                        success = MistletoeAPIHelper.UpdateCampaignModel(tempModel);
                    }
                }
                else
                {
                    errorMsg = ResourceHelper.ResourceValue("SaveCampaignValidationError");
                }
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
        ///     Edit campaign
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Action result with campaign data</returns>
        public ActionResult Edit(int campaignID)
        {
            var tempCampaign = MistletoeAPIHelper.GetCampaignModel(campaignID);

            if (tempCampaign != null)
            {
                if (tempCampaign.UserID == MvcApplication.UserID)
                {
                    this.ViewBag.UserHasAccess = true;
                }
                else
                {
                    this.ViewBag.UserHasAccess = false;
                }
            }
            else
            {
                tempCampaign = new CampaignModel();
                this.ViewBag.UserHasAccess = false;
            }

            return this.View(tempCampaign);
        }

        /// <summary>
        ///     Upload file
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Json result with success, file path and error message</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxValidateAntiForgeryToken]
        public JsonResult UploadFile(string campaignID)
        {
            bool success = false;
            string errorMsg = string.Empty;
            string filePath = string.Empty;

            try
            {
                foreach (string file in this.Request.Files)
                {
                    var fileContent = this.Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // Get a stream
                        var stream = fileContent.InputStream;
                        string path = this.Server.MapPath("~/App_Data/" + campaignID);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string fileName = Path.GetFileName(fileContent.FileName);
                        path = Path.Combine(this.Server.MapPath("~/App_Data/" + campaignID), fileName);
                        using (var fileStream = FileHelper.CreateFileStream(path))
                        {
                            stream.CopyTo(fileStream);
                            success = true;
                            filePath = path;
                            FileHelper.EncryptFile(filePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMsg = ex.Message + Environment.NewLine + ResourceHelper.ResourceValue("CampaignUploadFileError");
            }

            return this.Json(
                new
                {
                    Success = success,
                    FilePath = filePath,
                    ErrorMsg = errorMsg
                },
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Change campaign status
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="isActive">Is Active</param>
        /// <param name="scheduleCampaign">Schedule Campaign</param>
        /// <returns>Action result with success and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult ChangeCampaignStatus(string campaignID, string isActive, string scheduleCampaign)
        {
            bool success = false;
            string errorMsg = string.Empty;

            int campaign_ID = 0;
            if (int.TryParse(campaignID, out campaign_ID))
            {
                bool activateCampaign = false;
                if (bool.TryParse(isActive, out activateCampaign))
                {
                    bool scheduleCampaignValue = false;
                    bool.TryParse(scheduleCampaign, out scheduleCampaignValue);

                    bool userHasAccess = MistletoeAPIHelper.CheckUserAccess(campaign_ID, MvcApplication.UserID);

                    if (userHasAccess)
                    {
                        // Check if template exists before activating the campaign
                        bool templateExists = true;
                        if (activateCampaign)
                        {
                            templateExists = MistletoeAPIHelper.TemplateExists(campaign_ID);
                        }

                        if (templateExists)
                        {
                            // Schedule OR Unschedule the email job
                            if (scheduleCampaignValue)
                            {
                                if (activateCampaign)
                                {
                                    success = JobScheduler.ScheduleCampaign(campaign_ID);
                                }
                                else
                                {
                                    success = JobScheduler.UnscheduleCampaign(campaign_ID);
                                }
                            }
                            else
                            {
                                success = true;
                            }

                            // Now, activate OR deactivate the campaign
                            if (success)
                            {
                                bool campaignStatusChanged = MistletoeAPIHelper.ChangeCampaignStatus(campaign_ID, activateCampaign);
                            }
                            else
                            {
                                if (activateCampaign)
                                {
                                    errorMsg = ResourceHelper.ResourceValue("CampaignChangeCampaignStatusActivationFailed");
                                }
                                else
                                {
                                    errorMsg = ResourceHelper.ResourceValue("CampaignChangeCampaignStatusDeactivationFailed");
                                }
                            }
                        }
                        else
                        {
                            errorMsg = ResourceHelper.ResourceValue("CampaignChangeCampaignStatusAddTemplate");
                        }
                    }
                    else
                    {
                        errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
                    }
                }
                else
                {
                    errorMsg = ResourceHelper.ResourceValue("CampaignChangeCampaignStatusInvalidStatus");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CampaignCommonInvalidCampaign");
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
        ///     Delete campaign
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Action result with result and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult DeleteCampaign(string campaignID)
        {
            bool success = false;
            string errorMsg = string.Empty;

            int campaign_ID = 0;
            if (int.TryParse(campaignID, out campaign_ID))
            {
                bool userHasAccess = MistletoeAPIHelper.CheckUserAccess(campaign_ID, MvcApplication.UserID);

                if (userHasAccess)
                {
                    success = MistletoeAPIHelper.ArchiveCampaign(campaign_ID);
                }
                else
                {
                    errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CampaignCommonInvalidCampaign");
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
        ///     Preview email
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Action result with result, template and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult PreviewEmail(string campaignID)
        {
            bool success = false;
            string errorMsg = string.Empty;
            TemplateModel template = new TemplateModel();

            int campaign_ID = 0;
            if (int.TryParse(campaignID, out campaign_ID))
            {
                template = MistletoeAPIHelper.GetTemplateModel(campaign_ID);

                if (template != null && !string.IsNullOrEmpty(template.TemplateID))
                {
                    success = true;
                }
                else
                {
                    // Template doesn't exists yet
                    success = false;
                    errorMsg = ResourceHelper.ResourceValue("CampaignPreviewEmailNoTemplate");
                }
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CampaignCommonInvalidCampaign");
            }

            return this.Json(
                new
                {
                    Success = success,
                    Template = template,
                    ErrorMsg = errorMsg
                },
               JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Send test email
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Action result with result and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public async Task<ActionResult> SendTestEmail(string campaignID)
        {
            bool success = false;
            string errorMsg = string.Empty;

            int campaign_ID = 0;
            if (int.TryParse(campaignID, out campaign_ID))
            {
                // Get the Excel file path for the campaign
                string filePath = MistletoeAPIHelper.GetDataFilePath(campaign_ID);

                var template = MistletoeAPIHelper.GetTemplateModel(campaign_ID);

                var templateEmailAddressesModels = MistletoeAPIHelper.GetAllTemplateEmailAddressesItems();

                var emailAddressModels = MistletoeAPIHelper.GetAllEmailAddressItems();

                // Get Campaign Data Excel Sheet
                List<ExcelData> campaignData = ExcelHelper.GetCampaignData(filePath);

                // Create mail messages
                var mailMessages = EmailHelper.GetMailMessages(campaign_ID, campaignData, template, templateEmailAddressesModels, emailAddressModels, MvcApplication.GlobalEmailFooterFilePath);

                // Send mail
                await Mailer.SendEmailAsync(mailMessages[0]);

                success = true;
            }
            else
            {
                errorMsg = ResourceHelper.ResourceValue("CampaignCommonInvalidCampaign");
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
        ///     String to Date
        /// </summary>
        /// <param name="dateToConvert">Date to convert</param>
        /// <returns>DateTime object</returns>
        private DateTime StringToDate(string dateToConvert)
        {
            string[] tempArr = dateToConvert.Split('.');

            int day = Convert.ToInt32(tempArr[0]);
            int month = Convert.ToInt32(tempArr[1]);
            int year = Convert.ToInt32(tempArr[2]);

            return new DateTime(year, month, day);
        }

        /// <summary>
        ///     Move file
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="oldFilePath">Old file path</param>
        /// <param name="newFilePath">New file path</param>
        /// <returns>True or False</returns>
        private bool MoveFile(string campaignID, string oldFilePath, out string newFilePath)
        {
            bool success = false;
            newFilePath = string.Empty;

            string newPath = this.Server.MapPath("~/App_Data/" + campaignID);
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            string fileName = Path.GetFileName(oldFilePath);
            newPath = Path.Combine(this.Server.MapPath("~/App_Data/" + campaignID), fileName);

            // Ensure that the target does not exist
            FileHelper.DeleteFile(newPath);

            // Move the file
            FileHelper.MoveFile(oldFilePath, newPath);
            success = true;
            newFilePath = newPath;

            return success;
        }
    }
}