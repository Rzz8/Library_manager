using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_manager
{
    public class MovieCollection
    {
        private const int MaxMovies = 100000;
        private const int TableSize = 100003; // Table size is chosen as a prime number that is close to 100000
        private List<KeyValuePair<string, List<Movie>>>[] table;  // An array table to store lists of movies

        public MovieCollection()
        {
            table = new List<KeyValuePair<string, List<Movie>>>[TableSize];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new List<KeyValuePair<string, List<Movie>>>();
            }
        }

        // A hash function for a string input (movie title)
        public int GetHash(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash = (hash * 29 + c) % TableSize;
            }
            return hash;
        }

        // Method to count the number of movies
        public int GetMovieCount()
        {
            int count = 0;
            for (int i = 0; i < TableSize; i++)
            {
                foreach (var kvp in table[i])
                {
                    count += kvp.Value.Count;
                }
            }
            return count;
        }

        // Method to add a new movie copy
        public bool AddMovie(Movie movie)
        {
            if (GetMovieCount() >= MaxMovies)
            {
                return false;
            }

            int hash = GetHash(movie.Title); // Obtain the hash value from movie title
            var bucket = table[hash]; // find the bucket that holds the movies with the hash index

            // for the bucket, check if the movie list already exists for the title
            foreach (var kvp in bucket)
            {
                if (kvp.Key == movie.Title)
                {
                    kvp.Value.Add(movie);
                    return true;
                }
            }

            // If the title is not found, create a new entry
            bucket.Add(new KeyValuePair<string, List<Movie>>(movie.Title, new List<Movie> { movie }));
            return true;
        }

        // Method to add copies of a movie
        public bool AddCopies(string movieTitle, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }

            int hash = GetHash(movieTitle);
            var bucket = table[hash];

            foreach (var kvp in bucket)
            {
                if (kvp.Key == movieTitle)
                {
                    kvp.Value[0].AddCopies(count);
                    return true;
                }
            }

            return false;
        }

        // Method to remove a movie's copies
        public bool RemoveMovieCopies(string movieTitle, int copies)
        {
            int hash = GetHash(movieTitle);
            var bucket = table[hash];

            for (int i = 0; i < bucket.Count; i++)   // loop through the bucket to find the movie
            {
                if (bucket[i].Key == movieTitle)
                {
                    var movieList = bucket[i].Value;

                    if (movieList.Count > 0)
                    {
                        Movie movie = movieList[0];

                        if (movie.Copies >= copies)
                        {
                            movie.RemoveCopies(copies);
                            return true;
                        }

                        if (movie.Copies == 0)
                        {
                            bucket.RemoveAt(i);  // remove the movie if no copies are left
                            return true;
                        }
                        return false;
                    }
                }
            }

            return false;
        }

        // Method to search for a movie by title
        public Movie SearchMovie(string movieTitle)
        {
            int hash = GetHash(movieTitle);
            var bucket = table[hash];

            foreach (var kvp in bucket)
            {
                if (kvp.Key == movieTitle)
                {
                    // Assuming we return the first movie found with the matching title
                    return kvp.Value.FirstOrDefault();
                }
            }

            return null; // Movie not found
        }

        // Method to sort all movies by title
        public List<Movie> GetAllMoviesSortedByTitle()
        {
            var allMovies = new List<Movie>();

            for (int i = 0; i < TableSize; i++)
            {
                foreach (var kvp in table[i])
                {
                    allMovies.AddRange(kvp.Value);
                }
            }

            allMovies.Sort((m1, m2) => m1.Title.CompareTo(m2.Title));

            return allMovies;
        }

        // Method to sort all movies by borrow count
        public List<Movie> GetTopThreeMovies()
        {
            List<Movie> allMovies = new List<Movie>();

            for (int i = 0; i < TableSize; i++)
            {
                foreach (var kvp in table[i])
                {
                    allMovies.AddRange(kvp.Value);
                }
            }

            QuickSort(allMovies, 0, allMovies.Count - 1);

            List<Movie> topThreeMovies = new List<Movie>();
            for (int i = 0; i < Math.Min(3, allMovies.Count); i++)
            {
                topThreeMovies.Add(allMovies[i]);
            }

            return topThreeMovies;
        }

        // Quick sort algorithm to sort movies by borrow count
        private void QuickSort(List<Movie> movies, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(movies, low, high);

                QuickSort(movies, low, pi - 1);
                QuickSort(movies, pi + 1, high);
            }
        }

        // Partition method for quick sort
        private int Partition(List<Movie> movies, int low, int high)
        {
            Movie pivot = movies[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (movies[j].BorrowCount > pivot.BorrowCount)
                {
                    i++;
                    Movie temp = movies[i];
                    movies[i] = movies[j];
                    movies[j] = temp;
                }
            }

            Movie temp1 = movies[i + 1];
            movies[i + 1] = movies[high];
            movies[high] = temp1;

            return i + 1;
        }
    }

}
