using Library_manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager
{
    public class Member
    {
        private static int lastMemberId = 0;
        public int MemberId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int PassWord { get; set; }   
        private List<Movie> BorrowedMovies { get; set; }

        public Member(string firstName, string lastName, string phoneNumber, int passWord)
        {
            MemberId = ++lastMemberId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PassWord = passWord;
            BorrowedMovies = new List<Movie>();
        }

        // Method to borrow a movie for a registered member
        public bool BorrowMovie(Movie movie, out string message)
        {
            if (BorrowedMovies.Count >= 5)
            {
                message = "You cannot borrow more than 5 movies at a time.";
                return false;
            }

            if (BorrowedMovies.Contains(movie))
            {
                message = "You have already borrowed the movie.";
                return false;
            }

            BorrowedMovies.Add(movie);
            movie.RemoveCopies(1);
            movie.IncrementBorrowCount();
            message = $"You have successfully borrowed the movie {movie.Title}.";
            return true;
        }

        // Method to return a movie for a registered member
        public bool ReturnMovie(Movie movie, out string message)
        {
            if (!BorrowedMovies.Contains(movie))
            {
                message = "You haven't borrowed this movie.";
                return false;
            }

            BorrowedMovies.Remove(movie);
            message = $"You have successfully returned the movie {movie.Title}.";
            return true;
        }

        // Method to obtain member info and borrowed books
        public string GetMemberInfo()
        {
            return $"Member ID: {MemberId}\nFirst Name: {FirstName}\nLast Name: {LastName}\nPhone Number: {PhoneNumber}\nBorrowed Movies: {string.Join(", ", BorrowedMovies.Select(m => m.Title))}";
        }

        // Method to return the current member's borrowed books only
        public List<Movie> GetBorrowedMovies()
        {
            return BorrowedMovies;
        }
    }
}
