using Mistletoe.BLL.Interfaces;
using Mistletoe.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;

namespace Mistletoe.Web.Controllers.Tests
{
    [TestCaseOrderer("Mistletoe.Web.Tests.Helpers.AlphabeticalOrderer", "Mistletoe.Web.Tests")]
    public class AdminControllerTests
    {
        private string AdminUserID = Guid.NewGuid().ToString();
        private string User_ID = Guid.NewGuid().ToString();
        private static bool initialized = false;

        public AdminControllerTests()
        {
            MvcApplication.UserID = "1";
        }

        [Fact()]
        public void IndexTest_Success()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);
            userManagerMock.Setup(mock => mock.GetAllUserModels()).Returns(new List<UserModel>() { new UserModel() { IsAdmin = false } });

            AdminController controller = new AdminController(userManagerMock.Object, null);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            AdminPageModel pageModel = result.Model as AdminPageModel;

            // Assert
            Assert.True(pageModel.UserHasAccess);
            Assert.Single(pageModel.UsersList);
        }

        [Theory]
        [InlineData("0", "true")]
        [InlineData("0", "false")]
        [InlineData("0", "test")]
        public void ChangeUserStatusTest_Success(string UserID, string IsActive)
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);
            userManagerMock.Setup(mock => mock.ChangeUserStatus(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.ChangeCampaignStatusByUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(true);

            AdminController controller = new AdminController(userManagerMock.Object, campaignManagerMock.Object);

            // Act
            JsonResult jsonObject = controller.ChangeUserStatus(UserID, IsActive) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            if (IsActive != "test")
                Assert.True(success);
            else
                Assert.False(success);
        }

        [Fact]
        public void ChangeUserStatusTest_Fail_1()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(false);

            AdminController controller = new AdminController(userManagerMock.Object, null);

            // Act
            JsonResult jsonObject = controller.ChangeUserStatus("1", "false") as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.False(success);
        }

        [Fact]
        public void ChangeUserStatusTest_Fail_2()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);

            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.ChangeCampaignStatusByUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);

            AdminController controller = new AdminController(userManagerMock.Object, campaignManagerMock.Object);

            // Act
            JsonResult jsonObject = controller.ChangeUserStatus("1", "false") as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.False(success);
        }

        [Fact()]
        public void GetActiveCampaignsTest_Success()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);

            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.GetAllCampaignModelsByUser(It.IsAny<string>())).Returns(new List<CampaignModel>() { new CampaignModel() });

            AdminController controller = new AdminController(userManagerMock.Object, campaignManagerMock.Object);

            // Act
            JsonResult jsonObject = controller.GetActiveCampaigns(User_ID) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.True(success);
        }

        [Fact()]
        public void GetActiveCampaignsTest_Fail_1()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(false);

            AdminController controller = new AdminController(userManagerMock.Object, null);

            // Act
            JsonResult jsonObject = controller.GetActiveCampaigns(User_ID) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.False(success);
        }

        [Fact()]
        public void GetActiveCampaignsTest_Fail_2()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);

            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            List<CampaignModel> tempList = null;
            campaignManagerMock.Setup(mock => mock.GetAllCampaignModelsByUser(It.IsAny<string>())).Returns(tempList);

            AdminController controller = new AdminController(userManagerMock.Object, campaignManagerMock.Object);

            // Act
            JsonResult jsonObject = controller.GetActiveCampaigns(User_ID) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.False(success);
        }

        [Fact()]
        public void SaveFooterTest_Success()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(true);

            AdminController controller = new AdminController(userManagerMock.Object, null);

            // Act
            JsonResult jsonObject = controller.SaveFooter("This is test footer.") as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.True(success);
        }

        [Fact()]
        public void SaveFooterTest_Fail_1()
        {
            // Arrange
            Mock<IUserManager> userManagerMock = new Mock<IUserManager>();
            userManagerMock.Setup(mock => mock.IsUserAdmin(It.IsAny<string>())).Returns(false);

            AdminController controller = new AdminController(userManagerMock.Object, null);

            // Act
            JsonResult jsonObject = controller.SaveFooter("This is test footer.") as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.False(success);
        }
    }
}