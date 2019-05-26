// <copyright file="TemplateController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Mistletoe.Common;
    using Mistletoe.Models;
    using Mistletoe.Web.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    ///     Template controller
    /// </summary>
    [Authorize]
    public class TemplateController : Controller
    {
        /// <summary>
        ///     Edit view
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <returns>Action result</returns>
        public ActionResult Edit(string campaignID)
        {
            TemplateModel template = new TemplateModel();
            int campaign_ID = 0;
            if (int.TryParse(campaignID, out campaign_ID))
            {
                bool userHasAccess = MistletoeAPIHelper.CheckUserAccess(campaign_ID, MvcApplication.UserID);

                if (userHasAccess)
                {
                    this.ViewBag.UserHasAccess = true;

                    template = MistletoeAPIHelper.GetTemplateModel(campaign_ID);

                    if (string.IsNullOrEmpty(template.TemplateID))
                    {
                        // Template doesn't exists yet
                        template.CampaignID = campaignID;
                        template.TemplateID = "-1";
                    }
                }
                else
                {
                    this.ViewBag.UserHasAccess = false;
                }
            }

            return this.View(template);
        }

        /// <summary>
        ///     Save template
        /// </summary>
        /// <param name="campaignID">Campaign ID</param>
        /// <param name="templateID">Template ID</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="sender">Sender email</param>
        /// <param name="receivers">Receiver emails</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <returns>Action result with success and error message</returns>
        [AjaxValidateAntiForgeryToken]
        public ActionResult SaveTemplate(string campaignID, string templateID, string templateName, string sender, string receivers, string subject, string body)
        {
            bool success = false;
            string errorMsg = string.Empty;

            int campaign_ID = 0;
            int template_ID = 0;

            if (int.TryParse(campaignID, out campaign_ID))
            {
                if (int.TryParse(templateID, out template_ID))
                {
                    bool userHasAccess = MistletoeAPIHelper.CheckUserAccess(campaign_ID, MvcApplication.UserID);

                    if (userHasAccess)
                    {
                        // Check all the provided email addresses
                        string tempSender = sender.Trim();
                        List<string> tempReceiversList = new List<string>();
                        if (EmailHelper.CheckEmailAddresses(tempSender, receivers, ref tempReceiversList))
                        {
                            TemplateModel template = new TemplateModel();
                            template.CampaignID = campaign_ID.ToString();
                            template.TemplateID = template_ID.ToString();
                            template.TemplateName = templateName;
                            template.Subject = subject.Trim();
                            template.Body = HttpUtility.UrlDecode(body);

                            if (template_ID == -1)
                            {
                                // Template doesn't exists yet, create it
                                template_ID = MistletoeAPIHelper.CreateTemplateModel(template);
                            }
                            else
                            {
                                // Update the associated template
                                bool updateSuccess = MistletoeAPIHelper.UpdateTemplateModel(template);
                            }

                            if (template_ID != -1)
                            {
                                // Check and enter the sender
                                int senderEmailID = -1;
                                bool senderEmailExists = MistletoeAPIHelper.EmailExists(tempSender);

                                if (senderEmailExists)
                                {
                                    // Get the Email address id
                                    senderEmailID = MistletoeAPIHelper.GetEmailId(tempSender);
                                }
                                else
                                {
                                    // New Email address, enter it
                                    senderEmailID = MistletoeAPIHelper.AddEmail(tempSender);
                                }

                                // Check and enter the receivers
                                string receiversEmailIDs = string.Empty;
                                foreach (string rc in tempReceiversList)
                                {
                                    int receiverEmailID = -1;
                                    bool receiverEmailExists = MistletoeAPIHelper.EmailExists(rc);

                                    if (receiverEmailExists)
                                    {
                                        // Get the Email address id
                                        receiverEmailID = MistletoeAPIHelper.GetEmailId(rc);
                                    }
                                    else
                                    {
                                        // New Email address, enter it
                                        receiverEmailID = MistletoeAPIHelper.AddEmail(rc);
                                    }

                                    receiversEmailIDs += receiverEmailID + ",";
                                }

                                receiversEmailIDs = receiversEmailIDs.TrimEnd(',');

                                // Now, associate the template with sender and receivers
                                success = MistletoeAPIHelper.UpdateTemplateAndEmailAddressReferences(template_ID, senderEmailID, receiversEmailIDs);

                                if (!success)
                                {
                                    errorMsg = ResourceHelper.ResourceValue("TemplateSaveTemplateTryAgain");
                                }
                            }
                            else
                            {
                                errorMsg = ResourceHelper.ResourceValue("TemplateSaveTemplateTryAgain");
                            }
                        }
                        else
                        {
                            errorMsg = ResourceHelper.ResourceValue("TemplateSaveTemplateCheckEmail");
                        }
                    }
                    else
                    {
                        errorMsg = ResourceHelper.ResourceValue("CommonAccessDenied");
                    }
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
    }
}