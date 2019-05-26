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
    public class CampaignControllerTests
    {
        int Campaign_ID = 1;

        public CampaignControllerTests()
        {
            MvcApplication.UserID = "1";
        }

        [Fact()]
        public void IndexTest()
        {
            // Arrange
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.GetAllCampaignModelsByUser(MvcApplication.UserID)).Returns(new List<CampaignModel>() { new CampaignModel() { CampaignID = Campaign_ID.ToString() } });

            CampaignController controller = new CampaignController(campaignManagerMock.Object, null, null, null);

            // Act
            ViewResult result = controller.Index() as ViewResult;
            ManageCampaignsModel pageModel = result.Model as ManageCampaignsModel;

            // Assert
            Assert.Equal(Campaign_ID.ToString(), pageModel.CampaignIDs);
        }

        [Theory]
        [InlineData("-1", "Test Campaign 1", "01.01.2018", "01.03.2018", "* * * * *", "")]
        [InlineData("1", "Test Campaign 1 Edited", "01.04.2018", "01.07.2018", "* * * * *", "")]
        public void SaveCampaignTest(string CampaignID, string CampaignName, string StartDate, string EndDate, string Frequency, string DataFilePath)
        {
            // Arrange
            int campaignID = Convert.ToInt32(CampaignID);
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            // TODO
            //campaignManagerMock.Setup(mock => mock.CreateCampaignModel(It.IsAny<CampaignModel>(), out campaignID)).Returns(true);
            campaignManagerMock.Setup(mock => mock.UpdateCampaignModel(It.IsAny<CampaignModel>())).Returns(true);

            CampaignController controller = new CampaignController(campaignManagerMock.Object, null, null, null);

            // Act
            JsonResult jsonObject = controller.SaveCampaign(CampaignID, CampaignName, StartDate, EndDate, Frequency, DataFilePath) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.True(success);
        }

        [Fact()]
        public void EditTest()
        {
            // Arrange
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.GetCampaignModel(It.IsAny<int>())).Returns(new CampaignModel() { CampaignID = Campaign_ID.ToString(), UserID = MvcApplication.UserID });

            CampaignController controller = new CampaignController(campaignManagerMock.Object, null, null, null);

            // Act
            ViewResult result = controller.Edit(Campaign_ID) as ViewResult;
            CampaignModel tempCampaign = result.Model as CampaignModel;

            // Assert
            Assert.Equal(Campaign_ID.ToString(), tempCampaign.CampaignID);
        }

        [Theory]
        [InlineData("1", "true", "false")]
        [InlineData("1", "false", "false")]
        [InlineData("test", "false", "false")]
        public void ChangeCampaignStatusTest(string CampaignID, string IsActive, string ScheduleCampaign)
        {
            // Arrange
            if (CampaignID == "1")
                CampaignID = Campaign_ID.ToString();

            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.CheckUserAccess(It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            campaignManagerMock.Setup(mock => mock.ChangeCampaignStatus(It.IsAny<int>(), It.IsAny<bool>())).Returns(true);

            Mock<ITemplateManager> templateManagerMock = new Mock<ITemplateManager>();
            templateManagerMock.Setup(mock => mock.TemplateExists(It.IsAny<int>())).Returns(true);

            CampaignController controller = new CampaignController(campaignManagerMock.Object, templateManagerMock.Object, null, null);

            // Act
            JsonResult jsonObject = controller.ChangeCampaignStatus(CampaignID, IsActive, ScheduleCampaign) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            if (CampaignID != "test")
                Assert.True(success);
            else
                Assert.False(success);
        }

        [Fact()]
        public void DeleteCampaignTest()
        {
            // Arrange
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.CheckUserAccess(It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            campaignManagerMock.Setup(mock => mock.ArchiveCampaign(It.IsAny<int>())).Returns(true);

            CampaignController controller = new CampaignController(campaignManagerMock.Object, null, null, null);

            // Act
            JsonResult jsonObject = controller.DeleteCampaign(Campaign_ID.ToString()) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            Assert.True(success);
        }

        ~CampaignControllerTests()
        {
            //MvcApplication.CampaignManager.DeleteCampaignModel(CampaignID);
            //MvcApplication.UserManager.DeleteUserModel(UserID);
        }
    }
}