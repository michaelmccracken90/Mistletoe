using System.Collections.Generic;

namespace Mistletoe.Web.Models
{
    public class TemplateModel
    {
        public string CampaignID { get; set; }
        public string TemplateID { get; set; }
        public string TemplateName { get; set; }
        public string Sender { get; set; }
        public List<string> ReceiverList { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string Receivers
        {
            get
            {
                string tempStr = string.Empty;
                ReceiverList.ForEach(r => { tempStr += r + "; "; });
                return tempStr;
            }
        }

        public TemplateModel()
        {
            ReceiverList = new List<string>();
        }
    }
}