namespace Mistletoe.Web.Models
{
    public class UserModel
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string StatusForDisplay
        {
            get
            {
                if (IsActive)
                    return "Active";
                else
                    return "Inactive";
            }
        }
        public string StatusColor
        {
            get
            {
                if (IsActive)
                    return "success";
                else
                    return "danger";
            }
        }
    }
}