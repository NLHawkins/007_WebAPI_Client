using _007_WebAPI_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _007_WebAPI_Client
{
    class Program
    {
        public static HttpClient client = new HttpClient();

        private static void SetUpClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://007api.co/api/");
        }

        static Character GetCharacter(string id)
        {
            var response = client.GetAsync($"characters/{id}").Result;
            return response.Content.ReadAsAsync<Character>().Result;
        }

        static CharacterCollection GetCharCollection()
        {
            var response = client.GetAsync($"characters").Result;
            return response.Content.ReadAsAsync<CharacterCollection>().Result;
        }

        static Movie GetMovie(string id)
        {
            var response = client.GetAsync($"movies/{id}").Result;
            return response.Content.ReadAsAsync<Movie>().Result;
        }

        static MovieCollection GetMovieCollection()
        {
            var response = client.GetAsync("movies/").Result;
            MovieCollection Movies = response.Content.ReadAsAsync<MovieCollection>().Result;
            return Movies;
        }

        private static MovieCollection GetAllMovies()
        {
            var allMovieResponse = client.GetAsync("movies/").Result;
            MovieCollection allMovies = allMovieResponse.Content.ReadAsAsync<MovieCollection>().Result;
            return allMovies;

        }

        private static MovieCollection getNextMovies(MovieCollection collection)
        {
            var nextMovies = collection.GetNext(client);
            return nextMovies;

        }

        private static GadgetCollection getNextGadgets(GadgetCollection collection)
        {
            var nextGagdets = collection.GetNext(client);
            return nextGagdets;

        }

        private static CharacterCollection getNextChars(CharacterCollection collection)
        {
            var nextChars = collection.GetNext(client);
            return nextChars;

        }


        private static MovieCollection getPrevMovies()
        {
            var prevMovies = GetAllMovies().GetPrevious(client);
            return prevMovies;
        }

        private static CharacterCollection getPrevChars()
        {
            var prevChars = GetCharCollection().GetPrevious(client);
            return prevChars;
        }

        private static GadgetCollection getPrevGadgets()
        {
            var prevGadget = GetGadgetCollection().GetPrevious(client);
            return prevGadget;
        }
        static void ShowAllChars(CharacterCollection charCollection)
        {
            var allChars = charCollection.Results.ToList();
            foreach (var character in allChars)
            {
                Console.WriteLine($"Id: {character.Id}");
                Console.WriteLine($"Name: {character.Name}");
            }
        }

        static void ShowAllGadgets(GadgetCollection gCollection)
        {
            var allGadgets = gCollection.Results.ToList();
            foreach (var gadget in allGadgets)
            {
                Console.WriteLine($"Id: {gadget.Id}");
                Console.WriteLine($"Name: {gadget.Name}");
            }
        }
        static Gadget GetGadget(string id)
        {
            var response = client.GetAsync($"gadgets/{id}").Result;
            return response.Content.ReadAsAsync<Gadget>().Result;
        }

        static GadgetCollection GetGadgetCollection()
        {
            var response = client.GetAsync($"gadgets").Result;
            GadgetCollection allGadgets = response.Content.ReadAsAsync<GadgetCollection>().Result;
            return allGadgets;
        }



        static string Read(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        public static bool inMainMenu;
        public static bool inMovieListMenu;
        public static bool inMovieDetailMenu;
        public static bool inMovieCharDetailMenu;

        public static bool inCharListMenu;
        public static bool inCharDetailMenu;

        public static bool inGadgetListMenu;
        public static bool inGadgetDetailMenu;
        public static bool inGadgetOwnerDetailMenu;

        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to The World of James Bond");
            Console.WriteLine();
            SetUpClient();
            inMainMenu = true;
            while (inMainMenu)
            {

                ShowMainMenu();
                string choiceString = Read("> ");
                Console.WriteLine();
                int choiceTry;
                int.TryParse(choiceString, out choiceTry);
                if (choiceTry > 0)
                {
                    int choice = int.Parse(choiceString);

                    switch (choice)
                    {
                        case 1:
                            var movieCollection = GetMovieCollection();
                            ShowAllMovies(movieCollection);
                            inMovieListMenu = true;
                            ShowMovieChoiceSC(ref choiceString, ref choiceTry, movieCollection);
                            break;
                        case 2:
                            var charCollection = GetCharCollection();
                            ShowAllChars(charCollection);
                            inCharListMenu = true;
                            ShowCharChoiceSC(ref choiceString, ref choiceTry, charCollection);
                            break;
                        case 3:
                            var gadgetCollection = GetGadgetCollection();
                            ShowAllGadgets(gadgetCollection);
                            inGadgetListMenu = true;
                            ShowGadgetChoiceSC(ref choiceString, ref choiceTry, gadgetCollection);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ShowGadgetChoiceSC(ref string choiceString, ref int choiceTry, GadgetCollection gadgetCollection)
        {
            while (inGadgetListMenu)
            {
                var nextCollection = gadgetCollection;
                ShowGadgetListMenu();
                choiceString = Read("> ");
                int.TryParse(choiceString, out choiceTry);
                if (choiceTry > 0)
                {
                    int gadgetMenuChoice = int.Parse(choiceString);

                    switch (gadgetMenuChoice)
                    {
                        case 1:
                            Console.WriteLine("Type the ID of the Gadget for which to its view detials");
                            var gadgetId = Console.ReadLine();
                            var gadget = GetGadget(gadgetId);
                            ShowGadgetDetails(gadget);
                            inGadgetDetailMenu = true;
                            while (inGadgetDetailMenu)
                            {
                                ShowGadgetDetailsMenu();
                                choiceString = Read("> ");
                                int.TryParse(choiceString, out choiceTry);
                                if (choiceTry > 0)
                                {
                                    int gadgetDetailsMenuChoice = int.Parse(choiceString);

                                    switch (gadgetDetailsMenuChoice)
                                    {
                                        case 1:
                                            Console.WriteLine("Type the ID of the Gadget Owner for which to view detials");
                                            var charId = Console.ReadLine();
                                            var character = GetCharacter(charId);
                                            inGadgetOwnerDetailMenu = true;
                                            while (inGadgetOwnerDetailMenu)
                                            {
                                                ShowCharDetails(character);
                                                Console.WriteLine("1) Back to Movie Details");
                                                Console.WriteLine("2) Main Menu");
                                                choiceString = Read("> ");
                                                Console.WriteLine();
                                                int.TryParse(choiceString, out choiceTry);
                                                if (choiceTry > 0)
                                                {
                                                    int gadgetOwnerDetailsChoice = int.Parse(choiceString);

                                                    switch (gadgetOwnerDetailsChoice)
                                                    {
                                                        case 1:
                                                            inGadgetOwnerDetailMenu = false;
                                                            break;
                                                        case 2:
                                                            inGadgetOwnerDetailMenu = false;
                                                            inGadgetDetailMenu = false;
                                                            inGadgetListMenu = false;
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        case 2:
                                            inGadgetDetailMenu = false;
                                            break;
                                        case 3:
                                            inGadgetDetailMenu = false;
                                            inGadgetListMenu = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        case 2:
                            var nextGadgets = getNextGadgets(nextCollection);
                            ShowAllGadgets(nextGadgets);
                            break;
                        case 3:
                            var prevGadgets = getPrevGadgets();
                            ShowAllGadgets(prevGadgets);
                            break;
                        case 4:
                            inGadgetListMenu = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ShowCharChoiceSC(ref string choiceString, ref int choiceTry, CharacterCollection charCollection)
        {
            while (inCharListMenu)
            {
                var nextCollection = charCollection;
                ShowCharListMenu();
                choiceString = Read("> ");
                int.TryParse(choiceString, out choiceTry);
                if (choiceTry > 0)
                {
                    int charMenuChoice = int.Parse(choiceString);

                    switch (charMenuChoice)
                    {
                        case 1:
                            Console.WriteLine("Type the ID of the Character for which to view detials");
                            var charId = Console.ReadLine();
                            var character = GetCharacter(charId);
                            inCharDetailMenu = true;
                            while (inCharDetailMenu)
                            {
                                ShowCharDetails(character);
                                Console.WriteLine("1) Back to Character List");
                                Console.WriteLine("2) Main Menu");
                                choiceString = Read("> ");
                                Console.WriteLine();
                                int.TryParse(choiceString, out choiceTry);
                                if (choiceTry > 0)
                                {
                                    int charDetailsChoice = int.Parse(choiceString);

                                    switch (charDetailsChoice)
                                    {
                                        case 1:
                                            inCharDetailMenu = false;
                                            break;
                                        case 2:
                                            inCharDetailMenu = false;
                                            inCharListMenu = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        case 2:
                            var nextChars = getNextChars(nextCollection);
                            ShowAllChars(nextChars);
                            ShowCharChoiceSC(ref choiceString, ref choiceTry, nextChars);
                            break;
                        case 3:
                            var prevMovies = getPrevMovies();
                            ShowAllMovies(prevMovies);
                            break;
                        case 4:
                            inMovieListMenu = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ShowMovieChoiceSC(ref string choiceString, ref int choiceTry, MovieCollection Collection)
        {
            while (inMovieListMenu)
            {
                var nextCollection = Collection;
                ShowMovieListMenu();
                choiceString = Read("> ");
                int.TryParse(choiceString, out choiceTry);
                if (choiceTry > 0)
                {
                    int movieMenuChoice = int.Parse(choiceString);

                    switch (movieMenuChoice)
                    {
                        case 1:
                            Console.WriteLine("Type the ID of the Movie for which to view detials");
                            var movieId = Console.ReadLine();
                            var movie = GetMovie(movieId);
                            ShowMovieDetails(movie);
                            inMovieDetailMenu = true;
                            while (inMovieDetailMenu)
                            {
                                ShowMovieDetailsMenu();
                                choiceString = Read("> ");
                                int.TryParse(choiceString, out choiceTry);
                                if (choiceTry > 0)
                                {
                                    int movieDetailsMenuChoice = int.Parse(choiceString);

                                    switch (movieDetailsMenuChoice)
                                    {
                                        case 1:
                                            Console.WriteLine("Type the ID of the Character for which to view detials");
                                            var charId = Console.ReadLine();
                                            var character = GetCharacter(charId);
                                            inMovieCharDetailMenu = true;
                                            while (inMovieCharDetailMenu)
                                            {

                                            }
                                            ShowCharDetails(character);
                                            Console.WriteLine("1) Back to Movie Details");
                                            Console.WriteLine("2) Main Menu");
                                            choiceString = Read("> ");
                                            Console.WriteLine();
                                            int.TryParse(choiceString, out choiceTry);
                                            if (choiceTry > 0)
                                            {
                                                int movieCharDetailsChoice = int.Parse(choiceString);

                                                switch (movieCharDetailsChoice)
                                                {
                                                    case 1:
                                                        inMovieCharDetailMenu = false;
                                                        break;
                                                    case 2:
                                                        inMovieCharDetailMenu = false;
                                                        inMovieDetailMenu = false;
                                                        inMovieListMenu = false;
                                                        break;
                                                    default:
                                                        break;
                                                }
                                            }
                                            break;
                                        case 2:
                                            inMovieDetailMenu = false;
                                            break;
                                        case 3:
                                            inMovieDetailMenu = false;
                                            inMovieListMenu = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;

                        case 2:

                            var nextMovies = getNextMovies(nextCollection);
                            ShowAllMovies(nextMovies);
                            ShowMovieChoiceSC(ref choiceString, ref choiceTry, nextMovies);
                            break;
                        case 3:
                            var prevMovies = getPrevMovies();
                            ShowAllMovies(prevMovies);
                            break;
                        case 4:
                            inMovieListMenu = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ShowAllMovies(MovieCollection movies)
        {
            var movielist = movies.Results.ToList();
            foreach (var m in movielist)
            {
                Console.WriteLine($"Id: {m.Id}");
                Console.WriteLine($"Title: {m.Title}");
            }
        }

        private static void ShowCharDetails(Character character)
        {
            Console.WriteLine($"Here are the details of {character.Name}");
            Console.WriteLine($"Name: {character.Name}");
            Console.WriteLine($"ReleaseDate: {character.Bio}");
            Console.WriteLine();
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine("1) Movies");
            Console.WriteLine("2) Characters");
            Console.WriteLine("3) Gadgets");
            Console.WriteLine();
        }

        private static void ShowMovieDetailsMenu()
        {
            Console.WriteLine("1) View Movie Character Details");
            Console.WriteLine("2) Back to Movie List");
            Console.WriteLine("3) Main Menu");
            Console.WriteLine();
        }

        private static void ShowGadgetDetailsMenu()
        {
            Console.WriteLine("1) View Gadget Owner Details");
            Console.WriteLine("2) Back to Gadget List");
            Console.WriteLine("3) Main Menu");
            Console.WriteLine();
        }

        private static void ShowCharDetailsMenu()
        {
            Console.WriteLine("1) Back to Character List");
            Console.WriteLine("2) Back to Main Menu");

            Console.WriteLine();
        }

        private static void ShowMovieDetails(Movie movie)
        {
            Console.WriteLine($"Here are the details of {movie.Title}");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"ReleaseDate: {movie.ReleaseDate}");
            Console.WriteLine($"Characters: ");
            Console.WriteLine();
            foreach (var character in movie.Characters)
            {
                Console.WriteLine($"Name: {character.Name}");
                Console.WriteLine($"Id: {character.Id}");
                Console.WriteLine();
            }
        }

        private static void ShowGadgetDetails(Gadget gadget)
        {
            Console.WriteLine($"Here are the details of {gadget.Name}");
            Console.WriteLine($"Title: {gadget.Name}");
            Console.WriteLine($"ReleaseDate: {gadget.Description}");
            Console.WriteLine($"Owner: {gadget.Owner}");
        }

        private static void ShowMovieListMenu()
        {
            Console.WriteLine("1) View Movie Details (Remember the ID)");
            Console.WriteLine("2) Next Movies");
            Console.WriteLine("3) Previous Movies");
            Console.WriteLine("4) Main Menu");
            Console.WriteLine();
        }

        private static void ShowGadgetListMenu()
        {
            Console.WriteLine("1) View Gadget Details (Remember the ID)");
            Console.WriteLine("2) Next Gadgets");
            Console.WriteLine("3) Previous Gadgets");
            Console.WriteLine("4) Main Menu");
            Console.WriteLine();
        }

        private static void ShowCharListMenu()
        {
            Console.WriteLine("1) View Character Details (Remember the ID)");
            Console.WriteLine("2) Next Characters");
            Console.WriteLine("3) Previous Characters");
            Console.WriteLine("4) Main Menu");
            Console.WriteLine();
        }
    }
}
