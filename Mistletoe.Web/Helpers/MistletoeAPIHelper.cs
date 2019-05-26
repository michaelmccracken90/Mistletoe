namespace Mistletoe.Web.Helpers
{
    using Mistletoe.Models;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Generic;
    using System.Net;

    public class MistletoeAPIHelper
    {
        public static bool IsUserAdmin(string userId)
        {
            bool result = false;

            RestRequest request = new RestRequest("Users/IsUserAdmin", Method.POST);
            request.AddParameter("userID", userId, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool UserExists(string userId)
        {
            bool result = false;

            RestRequest request = new RestRequest("Users/UserExists", Method.POST);
            request.AddParameter("userID", userId, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool CheckUserAccess(int campaign_ID, string userID)
        {
            bool result = false;

            RestRequest request = new RestRequest("Campaigns/CheckUserAccess", Method.POST);
            request.AddParameter("campaignID", campaign_ID, ParameterType.QueryString);
            request.AddParameter("userId", userID, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static List<UserModel> GetAllUserModels()
        {
            List<UserModel> userModels = null;

            RestRequest request = new RestRequest("User/GetAllUserModels", Method.GET);

            var response = MvcApplication.MistletoeAPIClient.Execute<List<UserModel>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                userModels = response.Data;
            }

            return userModels;
        }

        internal static void UpdateUserModel(UserModel user)
        {
            RestRequest request = new RestRequest("Users/UpdateUserModel", Method.POST);
            request.AddObject(user);

            MvcApplication.MistletoeAPIClient.Execute(request);
        }

        internal static void AddUserModel(UserModel user)
        {
            RestRequest request = new RestRequest("Users/AddUserModel", Method.POST);
            request.AddObject(user);

            MvcApplication.MistletoeAPIClient.Execute(request);
        }

        internal static TemplateModel GetTemplateModel(int campaignId)
        {
            TemplateModel templateModel = null;

            RestRequest request = new RestRequest("Templates/GetTemplateModel", Method.GET);
            request.AddParameter("campaignId", campaignId);

            var response = MvcApplication.MistletoeAPIClient.Execute<TemplateModel>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                templateModel = response.Data;
            }

            return templateModel;
        }

        internal static IEnumerable<TemplateEmailAddressesModel> GetAllTemplateEmailAddressesItems()
        {
            IEnumerable<TemplateEmailAddressesModel> templateEmailAddressesModels = null;

            RestRequest request = new RestRequest("TemplateEmailAddresses/GetAllItems", Method.GET);

            var response = MvcApplication.MistletoeAPIClient.Execute<List<TemplateEmailAddressesModel>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                templateEmailAddressesModels = response.Data;
            }

            return templateEmailAddressesModels;
        }

        internal static int CreateCampaignModel(CampaignModel tempModel)
        {
            int campaignId = -1;
            RestRequest request = new RestRequest("Campaigns/CreateCampaignModel", Method.POST);
            request.AddJsonBody(tempModel);

            var response = MvcApplication.MistletoeAPIClient.Execute<int>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                campaignId = response.Data;
            }

            return campaignId;
        }

        internal static bool UpdateCampaignFilePath(string campaignIDValue, string newFilePath)
        {
            bool result = false;

            RestRequest request = new RestRequest("Campaigns/UpdateCampaignFilePath", Method.POST);
            request.AddParameter("campaignID", campaignIDValue, ParameterType.QueryString);
            request.AddParameter("newFilePath", newFilePath, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool UpdateCampaignModel(CampaignModel tempModel)
        {
            bool result = false;
            RestRequest request = new RestRequest("Campaigns/UpdateCampaignModel", Method.POST);
            request.AddObject(tempModel);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static CampaignModel GetCampaignModel(int campaignID)
        {
            CampaignModel campaignModel = null;

            RestRequest request = new RestRequest("Campaigns/GetCampaignModel", Method.GET);
            request.AddParameter("campaignId", campaignID);

            var response = MvcApplication.MistletoeAPIClient.Execute<CampaignModel>(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                campaignModel = response.Data;
            }

            return campaignModel;
        }

        internal static int CreateTemplateModel(TemplateModel template)
        {
            int templateId = -1;
            RestRequest request = new RestRequest("Templates/CreateTemplateModel", Method.POST);
            request.AddObject(template);

            var response = MvcApplication.MistletoeAPIClient.Execute<int>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                templateId = response.Data;
            }

            return templateId;
        }

        internal static bool EmailExists(string tempSender)
        {
            bool result = false;
            RestRequest request = new RestRequest("EmailAddress/EmailExists", Method.POST);
            request.AddParameter("emailAddress", tempSender, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static int AddEmail(string tempSender)
        {
            int emailId = -1;
            RestRequest request = new RestRequest("EmailAddress/AddEmail", Method.POST);
            request.AddParameter("emailAddress", tempSender, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<int>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                emailId = response.Data;
            }

            return emailId;
        }

        internal static int GetEmailId(string tempSender)
        {
            int emailId = -1;

            RestRequest request = new RestRequest("EmailAddress/GetEmailId", Method.GET);
            request.AddParameter("emailAddress", tempSender, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<int>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                emailId = response.Data;
            }

            return emailId;
        }

        internal static bool UpdateTemplateModel(TemplateModel template)
        {
            bool result = false;
            RestRequest request = new RestRequest("Templates/UpdateTemplateModel", Method.POST);
            request.AddObject(template);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool UpdateTemplateAndEmailAddressReferences(int template_ID, int senderEmailID, string receiversEmailIDs)
        {
            bool result = false;
            RestRequest request = new RestRequest("TemplateEmailAddresses/UpdateTemplateAndEmailAddressReferences", Method.POST);
            request.AddParameter("templateID", template_ID, ParameterType.QueryString);
            request.AddParameter("senderEmailID", senderEmailID, ParameterType.QueryString);
            request.AddParameter("receiverEmailIDs", receiversEmailIDs, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool TemplateExists(int campaign_ID)
        {
            bool result = false;

            RestRequest request = new RestRequest("Templates/TemplateExists", Method.POST);
            request.AddParameter("CampaignID", campaign_ID);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static IEnumerable<EmailAddressModel> GetAllEmailAddressItems()
        {
            IEnumerable<EmailAddressModel> emailAddressModels = null;

            RestRequest request = new RestRequest("EmailAddress/GetAllItems", Method.GET);

            var response = MvcApplication.MistletoeAPIClient.Execute<List<EmailAddressModel>>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                emailAddressModels = response.Data;
            }

            return emailAddressModels;
        }

        internal static bool ChangeCampaignStatus(int campaign_ID, bool activateCampaign)
        {
            bool result = false;

            RestRequest request = new RestRequest("Campaigns/ChangeCampaignStatus", Method.POST);
            request.AddParameter("campaignID", campaign_ID);
            request.AddParameter("newStatus", activateCampaign);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool ChangeCampaignStatusByUser(string userID, bool is_Active)
        {
            bool result = false;
            RestRequest request = new RestRequest("Campaigns/ChangeCampaignStatusByUser", Method.POST);
            request.AddParameter("UserID", userID, ParameterType.QueryString);
            request.AddParameter("newStatus", is_Active, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static bool ChangeUserStatus(string userID, bool is_Active)
        {
            bool result = false;
            RestRequest request = new RestRequest("Users/ChangeUserStatus", Method.POST);
            request.AddParameter("UserID", userID, ParameterType.QueryString);
            request.AddParameter("newStatus", is_Active, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static List<CampaignModel> GetAllCampaignModelsByUser(string userId)
        {
            List<CampaignModel> campaignModels = null;

            RestRequest request = new RestRequest("Campaigns/GetAllCampaignModelsByUser", Method.GET);
            request.AddParameter("userID", userId);

            var response = MvcApplication.MistletoeAPIClient.Execute<List<CampaignModel>>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                campaignModels = response.Data;
            }

            return campaignModels;
        }

        internal static bool ArchiveCampaign(int campaign_ID)
        {
            bool result = false;
            RestRequest request = new RestRequest("Campaigns/ArchiveCampaign", Method.POST);
            request.AddParameter("campaignID", campaign_ID, ParameterType.QueryString);

            var response = MvcApplication.MistletoeAPIClient.Execute<bool>(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                result = response.Data;
            }

            return result;
        }

        internal static string GetDataFilePath(int campaign_ID)
        {
            string dataFilePath = string.Empty;

            RestRequest request = new RestRequest("Campaigns/GetDataFilePath", Method.GET);
            request.AddParameter("campaignId", campaign_ID);
            IRestResponse response = MvcApplication.MistletoeAPIClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK && response.Content != "null")
            {
                dataFilePath = response.Content;
            }

            return dataFilePath;
        }
    }
}