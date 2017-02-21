using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _007_WebAPI_Client.Models
{
    class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public List<Character> Characters { get; set; }
    }

    class MovieCollection
    {
        public string Count { get; set; }
        public Uri Next { get; set; }
        public Uri Previous { get; set; }
        public List<Movie> Results { get; set; }

        private MovieCollection GetPage(HttpClient client, Uri page)
        {
            if (page != null)
            {
                string pageNumber = page.Query;
                var allMovieResponse = client.GetAsync($"movies{pageNumber}").Result;
                return allMovieResponse.Content.ReadAsAsync<MovieCollection>().Result;
                
            }
            return this;
            
        }

        public MovieCollection GetPrevious(HttpClient client)
        {
            return GetPage(client, Previous);
        }

        public MovieCollection GetNext(HttpClient client)
        {
            return GetPage(client, Next);
        }
    }
}
