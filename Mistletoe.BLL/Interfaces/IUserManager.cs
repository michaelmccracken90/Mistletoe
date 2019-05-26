// <copyright file="IUserManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Mistletoe.Models;

    /// <summary>
    ///     User Manager Interface
    /// </summary>
    public interface IUserManager : IDisposable
    {
        /// <summary>
        ///     Is User Admin
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>True or False</returns>
        bool IsUserAdmin(string userID);

        /// <summary>
        ///     User exists
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>True or False</returns>
        bool UserExists(string userID);

        /// <summary>
        ///     Change user status
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="newStatus">New Status</param>
        /// <returns>True or False</returns>
        bool ChangeUserStatus(string userID, bool newStatus);

        /// <summary>
        ///     Get active users
        /// </summary>
        /// <returns>List of active users</returns>
        IEnumerable<UserModel> GetActiveUserModels();

        /// <summary>
        ///     Get all users
        /// </summary>
        /// <returns>List of all users</returns>
        List<UserModel> GetAllUserModels();

        /// <summary>
        ///     Add user
        /// </summary>
        /// <param name="newUser">New user</param>
        /// <returns>True or False</returns>
        bool AddUserModel(UserModel newUser);

        /// <summary>
        ///     Update user
        /// </summary>
        /// <param name="userToUpdate">User to update</param>
        /// <returns>True or False</returns>
        bool UpdateUserModel(UserModel userToUpdate);

        /// <summary>
        ///     Delete user
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>True or False</returns>
        bool DeleteUserModel(string userID);
    }
}
