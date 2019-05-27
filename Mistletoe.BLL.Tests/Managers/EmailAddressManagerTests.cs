namespace Mistletoe.BLL.Tests.Managers
{
    using Mistletoe.Common;
    using Mistletoe.DAL.Interfaces;
    using Mistletoe.Entities;
    using Moq;
    using Xunit;

    public class EmailAddressManagerTests
    {
        private Email_Address emailAddress1;
        private Email_Address emailAddress2;

        public EmailAddressManagerTests()
        {
            AutoMapperConfig.RegisterMapping();

            var mockEmailAddressRepository = new Mock<IRepository<Email_Address>>();
            
        }

        public void AddEmailSuccessTest()
        {
            // Arrange
            // Act
            // Assert
        }

        public void AddEmailFailureTest()
        {
            // Arrange
            // Act
            // Assert
        }

        public void EmailExistsSuccessTest()
        {
            // Arrange
            // Act
            // Assert
        }

        public void EmailExistsFailureTest()
        {
            // Arrange
            // Act
            // Assert
        }

        public void GetAllItemsTest()
        {
            // Arrange
            // Act
            // Assert
        }
    }
}
