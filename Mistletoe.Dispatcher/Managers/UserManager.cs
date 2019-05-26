namespace Mistletoe.Dispatcher.Managers
{
    using Mistletoe.DAL;
    using System.Linq;
    using System.Collections.Generic;

    internal class UserManager
    {
        internal static IEnumerable<User> GetActiveUsers()
        {
            var users = new List<User>();
            using (var context = new MistletoeDataEntities())
            {
                users = context.User.Where(x => x.Is_Active == true).ToList();
            }
            return users;
        }
    }
}