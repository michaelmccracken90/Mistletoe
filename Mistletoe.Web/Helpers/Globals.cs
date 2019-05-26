namespace Mistletoe.Web.Helpers
{
    internal static class Globals
    {
        private static string user_ID;
        internal static string UserID
        {
            get
            {
                if (string.IsNullOrEmpty(user_ID))
                    return string.Empty;
                else
                    return user_ID;
            }
            set
            {
                user_ID = value;
            }
        }
    }
}