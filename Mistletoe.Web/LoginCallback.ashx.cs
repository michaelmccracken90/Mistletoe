// <copyright file="LoginCallback.ashx.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.Web
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IdentityModel.Services;
    using System.Threading.Tasks;
    using System.Web;
    using Auth0.AspNet;
    using Auth0.AuthenticationApi;
    using Auth0.AuthenticationApi.Models;
    using Mistletoe.Models;
    using Mistletoe.Web.Helpers;

    /// <summary>
    ///     Login callback
    /// </summary>
    public class LoginCallback : HttpTaskAsyncHandler
    {
        /// <summary>
        ///     Gets a value indicating whether IsReusable
        /// </summary>
        public override bool IsReusable
        {
            get { return false; }
        }

        /// <summary>
        ///     Process request
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>void</returns>
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            AuthenticationApiClient client = new AuthenticationApiClient(
                new Uri(string.Format("https://{0}", ConfigurationManager.AppSettings["auth0:Domain"])));

            var token = await client.GetTokenAsync(new AuthorizationCodeTokenRequest
            {
                ClientId = ConfigurationManager.AppSettings["auth0:ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["auth0:ClientSecret"],
                Code = context.Request.QueryString["code"],
                RedirectUri = context.Request.Url.ToString()
            });

            var profile = await client.GetUserInfoAsync(token.AccessToken);

            var user = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("name", profile.FullName ?? profile.PreferredUsername ?? profile.Email),
                new KeyValuePair<string, object>("email", profile.Email),
                new KeyValuePair<string, object>("family_name", profile.LastName),
                new KeyValuePair<string, object>("given_name", profile.FirstName),
                new KeyValuePair<string, object>("nickname", profile.NickName),
                new KeyValuePair<string, object>("picture", profile.Picture),
                new KeyValuePair<string, object>("user_id", profile.UserId),
                new KeyValuePair<string, object>("id_token", token.IdToken),
                new KeyValuePair<string, object>("access_token", token.AccessToken),
                new KeyValuePair<string, object>("refresh_token", token.RefreshToken)
            };

            UserModel userModel = new UserModel();
            userModel.UserID = profile.UserId;
            userModel.UserName = string.IsNullOrEmpty(profile.NickName) ? string.Empty : profile.NickName;
            userModel.Email = string.IsNullOrEmpty(profile.Email) ? profile.FullName : profile.Email;
            MvcApplication.UserImage = string.IsNullOrEmpty(profile.Picture) ? string.Empty : profile.Picture;
            userModel.IsAdmin = MistletoeAPIHelper.IsUserAdmin(userModel.UserID);

            Action<UserModel> tempDelegate = this.StoreUserData;
            tempDelegate.BeginInvoke(userModel, null, null);

            // NOTE: Uncomment the following code in order to include claims from associated identities
            // profile.Identities.ToList().ForEach(i =>
            // {
            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".access_token", i.AccessToken));
            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".provider", i.Provider));
            //    user.Add(new KeyValuePair<string, object>(i.Connection + ".user_id", i.UserId));
            // });

            // NOTE: uncomment this if you send roles
            // user.Add(new KeyValuePair<string, object>(ClaimTypes.Role, profile.ExtraProperties["roles"]));

            // NOTE: this will set a cookie with all the user claims that will be converted
            //       to a ClaimsPrincipal for each request using the SessionAuthenticationModule HttpModule.
            //       You can choose your own mechanism to keep the user authenticated (FormsAuthentication, Session, etc.)
            FederatedAuthentication.SessionAuthenticationModule.CreateSessionCookie(user);

            var returnTo = "/";
            var state = context.Request.QueryString["state"];
            if (state != null)
            {
                var stateValues = HttpUtility.ParseQueryString(context.Request.QueryString["state"]);
                var redirectUrl = stateValues["ru"];

                // check for open redirection
                if (redirectUrl != null && this.IsLocalUrl(redirectUrl))
                {
                    returnTo = redirectUrl;
                }
            }

            context.Response.Redirect(returnTo);
        }

        private bool IsLocalUrl(string url)
        {
            return !string.IsNullOrEmpty(url)
                && url.StartsWith("/")
                && !url.StartsWith("//")
                && !url.StartsWith("/\\");
        }

        private void StoreUserData(UserModel user)
        {
            MvcApplication.UserID = user.UserID;
            MvcApplication.UserName = user.UserName;
            MvcApplication.IsAdmin = user.IsAdmin;

            if (MistletoeAPIHelper.UserExists(user.UserID))
            {
                MistletoeAPIHelper.UpdateUserModel(user);
            }
            else
            {
                MistletoeAPIHelper.AddUserModel(user);
            }
        }
    }
}