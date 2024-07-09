using System;
using System.Collections.Generic;

namespace Library_manager
{
    public class Movie
    {
        private static readonly List<string> AllowedGenres = new List<string>
        {
            "Drama", "Adventure", "Family", "Action", "SciFi", "Comedy", "Animated", "Thriller", "Other"
        };

        private static readonly List<string> AllowedClassifications = new List<string>
        {
            "G", "PG", "M15", "MA15"
        };

        public string Title { get; set; }
        public string MovieGenre { get; set; }
        public string MovieClassification { get; set; }
        public int Duration { get; set; }
        public int Copies { get; set; }
        public int BorrowCount { get; private set; }

        public Movie(string title, string genre, string classification, int duration, int copies)
        {
            if (!AllowedGenres.Contains(genre))
            {
                throw new ArgumentException("Invalid genre.");
            }

            if (!AllowedClassifications.Contains(classification))
            {
                throw new ArgumentException("Invalid classification.");
            }

            if (duration <= 0)
            {
                throw new ArgumentException("Movie duration must be positive.");
            }

            if (copies < 0)
            {
                throw new ArgumentException("Number of copies cannot be negative.");
            }

            Title = title;
            MovieGenre = genre;
            MovieClassification = classification;
            Duration = duration;
            Copies = copies;
            BorrowCount = 0;
        }

        public void AddCopies(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }

            Copies += count;
        }

        public void RemoveCopies(int count)
        {
            if (count < 0)
            {
                throw new ArgumentException("Count must be positive.");
            }

            if (Copies - count < 0)
            {
                throw new InvalidOperationException("Not enough copies to remove.");
            }

            Copies -= count;
        }

        public void IncrementBorrowCount()
        {
            BorrowCount++;
        }
    }
}
