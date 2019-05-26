namespace Mistletoe.Entities.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IUserService : IDisposable
    {
        IEnumerable<User> GetActiveUsers();
        List<User> GetAllUsers(bool FetchRefreshedData);
        bool IsUserAdmin(string UserID);
        bool UserExists(string UserID);
        bool AddUser(User NewUser);
        bool UpdateUser(User UserToUpdate);
        bool ChangeUserStatus(string UserID, bool newStatus);
    }
}
