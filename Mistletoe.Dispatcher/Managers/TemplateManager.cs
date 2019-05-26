namespace Mistletoe.Dispatcher.Managers
{
    using Mistletoe.DAL;
    using System.Linq;
    using System.Collections.Generic;

    internal class TemplateManager
    {
        internal static IEnumerable<Template> GetTemplates(int campaignId)
        {
            var templates = new List<Template>();
            using (var context = new MistletoeDataEntities())
            {
                templates = context.Template.Where(t => t.Campaign_ID == campaignId).ToList();
            }
            return templates;
        }
    }
}