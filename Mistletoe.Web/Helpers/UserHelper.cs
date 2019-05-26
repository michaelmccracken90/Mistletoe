namespace Mistletoe.Web.Helpers
{
    using Mistletoe.Entities;
    using Mistletoe.BLL.Models;
    using Mistletoe.Web;

    internal class UserHelper
    {
        internal bool UserExists(string UserID)
        {
            return MvcApplication.UserManager.UserExists(UserID);
        }

        internal bool AddUser(UserModel User)
        {
            bool success = false;

            MvcApplication.UserManager.AddUserModel(User);
            success = true;

            return success;
        }

        internal bool UpdateUser(UserModel User)
        {
            bool success = false;

            MvcApplication.UserManager.UpdateUserModel(User);
            success = true;

            return success;
        }

        internal User ConvertToDBEntity(UserModel UserToConvert)
        {
            User tempModel = new User();
            tempModel.UserID = UserToConvert.UserID;
            tempModel.FirstName = UserToConvert.FirstName;
            tempModel.LastName = UserToConvert.LastName;
            tempModel.Email = UserToConvert.Email;
            tempModel.IsActive = UserToConvert.IsActive;

            return tempModel;
        }

        internal UserModel ConvertToModel(User UserToConvert)
        {
            UserModel tempModel = new UserModel();
            tempModel.UserID = UserToConvert.UserID;
            tempModel.FirstName = UserToConvert.FirstName;
            tempModel.LastName = UserToConvert.LastName;
            tempModel.Email = UserToConvert.Email;
            tempModel.IsActive = UserToConvert.IsActive;

            return tempModel;
        }
    }
}