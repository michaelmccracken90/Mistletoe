// <copyright file="UserManager.cs" company="Moss and Lichens">
// Copyright (c) Moss and Lichens. All rights reserved.
// </copyright>
namespace Mistletoe.BLL.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Mistletoe.BLL.Interfaces;
    using Mistletoe.Entities;
    using Mistletoe.Models;
    using NLog;

    /// <summary>
    ///     User Manager
    /// </summary>
    public class UserManager : IUserManager, IDisposable
    {
        private IEnumerable<User> userList;

        private bool disposedValue = false; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userModelListValue">List of users</param>
        public UserManager(IEnumerable<UserModel> userModelListValue = null)
        {
            if (userModelListValue != null)
            {
                this.UpdateUserList(Mapper.Map<IEnumerable<User>>(userModelListValue));
            }
            else
            {
                this.UpdateUserList();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<UserModel> GetActiveUserModels()
        {
            var users = new List<UserModel>();
            try
            {
                var userItems = this.userList.Where(x => x.IsActive == true).ToList();
                users = Mapper.Map<List<UserModel>>(userItems);
            }
            catch (Exception exception)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:GetActiveUserModels. Exception: " + Environment.NewLine + exception.Message);
            }

            return users;
        }

        /// <inheritdoc/>
        public List<UserModel> GetAllUserModels()
        {
            List<UserModel> users = null;
            try
            {
                var userItems = this.userList.ToList();
                users = Mapper.Map<List<UserModel>>(userItems);
            }
            catch (Exception exception)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:GetAllUserModels. Exception: " + Environment.NewLine + exception.Message);
            }

            return users;
        }

        /// <inheritdoc/>
        public bool IsUserAdmin(string userID)
        {
            bool userIsAdmin = false;
            try
            {
                if (!string.IsNullOrEmpty(userID))
                {
                    User dbUser = this.userList.Where(c => c.UserID == userID).FirstOrDefault();
                    if (dbUser != null)
                    {
                        userIsAdmin = dbUser.IsAdmin;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:IsUserAdmin. Exception: " + Environment.NewLine + ex.Message);
            }

            return userIsAdmin;
        }

        /// <inheritdoc/>
        public bool UserExists(string userID)
        {
            bool userExists = false;
            try
            {
                userExists = this.userList.Any(u => u.UserID == userID);
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:UserExists. Exception: " + Environment.NewLine + ex.Message);
            }

            return userExists;
        }

        /// <inheritdoc/>
        public bool AddUserModel(UserModel newUser)
        {
            bool success = false;
            try
            {
                if (newUser != null)
                {
                    var userItem = Mapper.Map<User>(newUser);
                    if (Helper.UnitOfWorkInstance.UserRepository.GetByID(newUser.UserID) == null)
                    {
                        Helper.UnitOfWorkInstance.UserRepository.Insert(userItem);
                        Helper.UnitOfWorkInstance.Save();
                        this.UpdateUserList();
                        success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:AddUserModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool UpdateUserModel(UserModel userToUpdate)
        {
            bool success = false;

            try
            {
                User dbUser = this.userList.Where(c => c.UserID == userToUpdate.UserID).FirstOrDefault();
                if (dbUser != null)
                {
                    dbUser.UserName = userToUpdate.UserName;
                    dbUser.Email = userToUpdate.Email;
                    dbUser.IsActive = userToUpdate.IsActive;

                    Helper.UnitOfWorkInstance.Save();
                    this.UpdateUserList();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:UpdateUserModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool ChangeUserStatus(string userID, bool newStatus)
        {
            bool success = false;

            try
            {
                User dbUser = this.userList.Where(u => u.UserID == userID).FirstOrDefault();
                dbUser.IsActive = newStatus;

                Helper.UnitOfWorkInstance.Save();
                this.UpdateUserList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:ChangeUserStatus. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <inheritdoc/>
        public bool DeleteUserModel(string userID)
        {
            bool success = false;

            try
            {
                User user = Helper.UnitOfWorkInstance.UserRepository.GetByID(userID);
                Helper.UnitOfWorkInstance.UserRepository.Delete(user);
                Helper.UnitOfWorkInstance.Save();
                this.UpdateUserList();
                success = true;
            }
            catch (Exception ex)
            {
                Helper.GlobalLogger.Log(LogLevel.Error, "UserManager:DeleteUserModel. Exception: " + Environment.NewLine + ex.Message);
            }

            return success;
        }

        /// <summary>
        ///     Dispose managed and unmanaged objects
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        ///     Dispose managed and unmanaged objects
        /// </summary>
        /// <param name="disposing">Disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.userList = null;
                }

                this.disposedValue = true;
            }
        }

        private void UpdateUserList(IEnumerable<User> userListValue = null)
        {
            if (userListValue != null)
            {
                this.userList = userListValue;
            }
            else
            {
                this.userList = Helper.UnitOfWorkInstance.UserRepository.Get();
            }
        }
    }
}