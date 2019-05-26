namespace Mistletoe.Web.Tests.Helpers
{
    using KellermanSoftware.CompareNetObjects;
    using Mistletoe.Common;
    using Mistletoe.Web.Helpers;
    using System.Collections.Generic;
    using Xunit;

    public class ExcelHelperTests
    {
        [Fact]
        public void GetCampaignDataTest()
        {
            var actualCampaignData = ExcelHelper.GetCampaignData("./Data/EmailTemplateData.xlsx");
            var expectedCampaignData = new List<ExcelData> {
                new ExcelData {
                    Receiver = "ranjith.venkatesh@mossandlichens.com",
                    Placeholders = new List<string>
                    {
                        "Test1", "Test2", "Test3", "Test4", "Test5", "Test6", "Test7", "Test8", "Test9", "Test10"
                    }
                }
            };

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(actualCampaignData, expectedCampaignData);
            Assert.True(result.AreEqual);
        }
    }
}
