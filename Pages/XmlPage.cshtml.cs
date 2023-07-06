using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;


namespace HW3.Pages
{
    public class XmlPageModel : PageModel
    {
        public List<string> Titles { get; set; }

        public async Task OnGetAsync()
        {
            var xmlUrl = "http://scripting.com/rss.xml";
            using (var httpClient = new HttpClient())
            {
                var xmlContent = await httpClient.GetStringAsync(xmlUrl);
                Titles = ParseXml(xmlContent);
            }
        }

        private List<string> ParseXml(string xmlContent)
        {
            var titles = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);

            var items = xmlDoc.GetElementsByTagName("item");
            foreach (XmlNode item in items)
            {
                var titleNode = item.SelectSingleNode("title");
                if (titleNode != null)
                {
                    titles.Add(titleNode.InnerText);
                }
            }

            return titles;
        }

    }
}
