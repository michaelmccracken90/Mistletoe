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
    public class TemplateControllerTests
    {
        public TemplateControllerTests()
        {
            MvcApplication.UserID = "0";
        }

        [Fact()]
        public void EditTest()
        {
            // Arrange
            int CampaignID = 0;
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(cMock => cMock.CheckUserAccess(CampaignID, MvcApplication.UserID)).Returns(true);

            Mock<ITemplateManager> templateManagerMock = new Mock<ITemplateManager>();
            templateManagerMock.Setup(tMock => tMock.GetTemplateModel(CampaignID)).Returns(new TemplateModel());

            TemplateController templateController = new TemplateController(campaignManagerMock.Object, templateManagerMock.Object, null, null);

            // Act
            ViewResult result = templateController.Edit(CampaignID.ToString()) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("0", "-1", "New Template", "sender@mal.com", "receiver1@mal.com;receiver2@mal.com", "This is subject", "This is the body")]
        [InlineData("0", "0", "Edited Template", "sender@mal.com", "receiver1@mal.com;receiver2@mal.com", "This is edited subject", "This is the edited body")]
        [InlineData("0", "0", "Edited Template", "invalid email", "receiver1@mal.com", "This is edited subject", "This is the edited body")]
        [InlineData("0", "0", "Edited Template", "sender@mal.com", "invalid email", "This is edited subject", "This is the edited body")]
        public void SaveTemplateTest(string CampaignID, string TemplateID, string TemplateName, string Sender, string Receivers, string Subject, string Body)
        {
            // Arrange
            Mock<ICampaignManager> campaignManagerMock = new Mock<ICampaignManager>();
            campaignManagerMock.Setup(mock => mock.CheckUserAccess(Convert.ToInt32(CampaignID), MvcApplication.UserID)).Returns(true);

            Mock<ITemplateManager> templateManagerMock = new Mock<ITemplateManager>();

            TemplateModel template = new TemplateModel();
            template.CampaignID = CampaignID;
            template.TemplateID = TemplateID;
            template.TemplateName = TemplateName;
            template.Subject = Subject.Trim();
            template.Body = Body;

            templateManagerMock.Setup(mock => mock.CreateTemplateModel(template)).Returns(1);
            templateManagerMock.Setup(mock => mock.UpdateTemplateModel(template)).Returns(true);

            Mock<IEmailAddressManager> emailManagerMock = new Mock<IEmailAddressManager>();
            int senderEmailID = 1;
            // TODO
            //emailManagerMock.Setup(mock => mock.EmailExists(Sender, out senderEmailID)).Returns(true);

            Mock<ITemplateEmailAddressesManager> templateEmailAddressManagerMock = new Mock<ITemplateEmailAddressesManager>();
            if (TemplateID == "-1")
                templateEmailAddressManagerMock.Setup(mock => mock.UpdateTemplateAndEmailAddressReferences(0, senderEmailID, new List<int>() { 0, 0 })).Returns(true);
            else
                templateEmailAddressManagerMock.Setup(mock => mock.UpdateTemplateAndEmailAddressReferences(Convert.ToInt32(TemplateID), senderEmailID, new List<int>() { 0, 0 })).Returns(true);

            TemplateController templateController = new TemplateController(campaignManagerMock.Object, templateManagerMock.Object, emailManagerMock.Object, templateEmailAddressManagerMock.Object);

            // Act
            JsonResult jsonObject = templateController.SaveTemplate(CampaignID, TemplateID, TemplateName, Sender, Receivers, Subject, Body) as JsonResult;
            RouteValueDictionary tempDict = new RouteValueDictionary(jsonObject.Data);
            bool success = Convert.ToBoolean(tempDict["Success"]);

            // Assert
            if (Sender == "invalid email" || Receivers == "invalid email")
                Assert.False(success);
            else
                Assert.True(success);
        }
    }
}