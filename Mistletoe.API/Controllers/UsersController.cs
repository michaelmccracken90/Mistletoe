// <copyright file="UsersController.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.API.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.BLL.Managers;
    using Mistletoe.Models;

    /// <summary>
    ///     Users controller
    /// </summary>
    public class UsersController : ApiController, IUserManager
    {
        private IUserManager userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController()
        {
            this.userManager = new UserManager();
        }

        /// <inheritdoc/>
        public bool AddUserModel(UserModel newUser)
        {
            return this.userManager.AddUserModel(newUser);
        }

        /// <inheritdoc/>
        public bool ChangeUserStatus(string userID, bool newStatus)
        {
            return this.userManager.ChangeUserStatus(userID, newStatus);
        }

        /// <inheritdoc/>
        public bool DeleteUserModel(string userID)
        {
            return this.userManager.DeleteUserModel(userID);
        }

        /// <inheritdoc/>
        public IEnumerable<UserModel> GetActiveUserModels()
        {
            return this.userManager.GetActiveUserModels();
        }

        /// <inheritdoc/>
        public List<UserModel> GetAllUserModels()
        {
            return this.userManager.GetAllUserModels();
        }

        /// <inheritdoc/>
        public bool IsUserAdmin(string userID)
        {
            return this.userManager.IsUserAdmin(userID);
        }

        /// <inheritdoc/>
        public bool UpdateUserModel(UserModel userToUpdate)
        {
            return this.userManager.UpdateUserModel(userToUpdate);
        }

        /// <inheritdoc/>
        public bool UserExists(string userID)
        {
            return this.userManager.UserExists(userID);
        }
    }
}
