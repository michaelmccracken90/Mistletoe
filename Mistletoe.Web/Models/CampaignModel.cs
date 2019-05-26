namespace Mistletoe.Web.Models
{
    public class CampaignModel
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Frequency { get; set; }
        public string DataFilePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchived { get; set; }

        public string StatusColor
        {
            get
            {
                if (IsActive)
                    return "success";
                else
                    return "danger";
            }
        }
    }
}