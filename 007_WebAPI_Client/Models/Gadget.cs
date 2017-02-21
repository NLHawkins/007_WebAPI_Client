using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _007_WebAPI_Client.Models
{
    class Gadget
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Character Owner { get; set; }
    }

    class GadgetCollection
    {
        public string Count { get; set; }
        public Uri Next { get; set; }
        public Uri Previous { get; set; }
        public List<Gadget> Results { get; set; }

        private GadgetCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allGadgetResponse = client.GetAsync($"gadgets{pageNumber}").Result;
                return allGadgetResponse.Content.ReadAsAsync<GadgetCollection>().Result;
            }
            return this;
        }
        public GadgetCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, Previous);
        }

        public GadgetCollection GetNext(HttpClient client)
        {
            return GetPage(client, Next);
        }
    }
}
