using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _007_WebAPI_Client.Models
{
    
    class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
    }

    class CharacterCollection
    {
        public string Count { get; set; }
        public Uri Next { get; set; }
        public Uri Previous { get; set; }
        public List<Character> Results { get; set; }

        public int cPC = 0;

        private CharacterCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allCharacterResponse = client.GetAsync($"characters{pageNumber}").Result;
                return allCharacterResponse.Content.ReadAsAsync<CharacterCollection>().Result;
                
            }
            return this;
            
        }
        
        public CharacterCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, Previous);
        }

        public CharacterCollection GetNext(HttpClient client)
        {
            return GetPage(client, Next);
        }
    }
}
