using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mistletoe.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mistletoe.Web.Helpers.Tests
{
    [TestClass()]
    public class JobSchedulerTests
    {
        [TestMethod()]
        public void ScheduleCampaignTest()
        {
            JobScheduler.ScheduleCampaign(1);  
        }
    }
}