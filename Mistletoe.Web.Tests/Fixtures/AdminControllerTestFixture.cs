namespace Mistletoe.Web.Tests.Fixtures
{
    using Mistletoe.BLL.Models;
    using System;

    public class AdminControllerTestFixture : IDisposable
    {
        public string AdminUserID = Guid.NewGuid().ToString();
        public string User_ID = Guid.NewGuid().ToString();

        public AdminControllerTestFixture()
        {
            MvcApplication.UserManager.AddUserModel(new UserModel() { UserID = AdminUserID, Email = "admin@mal.com", IsActive = true, IsAdmin = true });
            MvcApplication.UserManager.AddUserModel(new UserModel() { UserID = User_ID, Email = "test@mal.com", IsActive = true, IsAdmin = false });
            MvcApplication.UserID = AdminUserID;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AdminControllerTestFixture() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
