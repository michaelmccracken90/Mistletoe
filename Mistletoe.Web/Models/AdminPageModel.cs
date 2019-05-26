using System.Collections.Generic;

namespace Mistletoe.Web.Models
{
    public class AdminPageModel
    {
        public List<UserModel> UsersList;
        public bool UserHasAccess;
        public string Footer;

        public AdminPageModel()
        {
            UserHasAccess = false;
            Footer = string.Empty;
            UsersList = new List<UserModel>();
        }
    }
}