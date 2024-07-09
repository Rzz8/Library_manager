using Library_manager;
using LibraryManager;
using System;

class Program
{
    // Some golbal variables.
    // The authenticatedMember is used to keep track of the member who is currently logged in.
    static MemberCollection? memberCollection;
    static MovieCollection? movieCollection;
    static Member? authenticatedMember;

    static void Main(string[] args)
    {
        memberCollection = new MemberCollection();
        movieCollection = new MovieCollection();
        string message;

        // ===============================================================================================================
        // =====   Start of testing section. This section provides some initial data   ===================================
        // =====   Commenting the following lines can remove all initial data          ===================================

        // Add some initial movies
        Movie movie1 = new Movie("Lion King", "Adventure", "G", 89, 4);
        Movie movie2 = new Movie("Frozen", "Animated", "G", 102, 1);
        Movie movie3 = new Movie("Jumanji", "Adventure", "PG", 119, 2);
        Movie movie4 = new Movie("The Dark Knight", "Action", "M15", 152, 6);
        Movie movie5 = new Movie("The Avengers", "Action", "M15", 143, 4);
        Movie movie6 = new Movie("The Incredibles", "Animated", "PG", 115, 2);

        movieCollection.AddMovie(movie1);
        movieCollection.AddMovie(movie2);
        movieCollection.AddMovie(movie3);
        movieCollection.AddMovie(movie4);
        movieCollection.AddMovie(movie5);
        movieCollection.AddMovie(movie6);

        // Add some initial members
        Member member1 = new Member("John", "Doe", "123456789", 1234);
        Member member2 = new Member("Jane", "Smith", "987654321", 5678);
        Member member3 = new Member("Laura", "Brown", "123476789", 2234);
        Member member4 = new Member("Michael", "Johnson", "977654321", 5578);
        Member member5 = new Member("David", "Williams", "122456789", 2234);
        Member member6 = new Member("Emma", "Jones", "887654321", 5578);
        Member member7 = new Member("Olivia", "Brown", "998877665", 3345);
        Member member8 = new Member("Liam", "Taylor", "556677889", 6678);
        Member member9 = new Member("Sophia", "Wilson", "445566778", 1122);
        Member member10 = new Member("Noah", "Moore", "334455667", 8899);


        memberCollection.RegisterMember(member1, out message);
        memberCollection.RegisterMember(member2, out message);
        memberCollection.RegisterMember(member3, out message);
        memberCollection.RegisterMember(member4, out message);
        memberCollection.RegisterMember(member5, out message);
        memberCollection.RegisterMember(member6, out message);
        memberCollection.RegisterMember(member7, out message);
        memberCollection.RegisterMember(member8, out message);
        memberCollection.RegisterMember(member9, out message);
        memberCollection.RegisterMember(member10, out message);

        // Perform some borrow operations
        member1.BorrowMovie(movie4, out message);
        member2.BorrowMovie(movie5, out message);
        member3.BorrowMovie(movie3, out message);
        member4.BorrowMovie(movie1, out message);
        member5.BorrowMovie(movie6, out message);
        member6.BorrowMovie(movie4, out message);
        member7.BorrowMovie(movie3, out message);
        member8.BorrowMovie(movie5, out message);
        member9.BorrowMovie(movie2, out message);
        member10.BorrowMovie(movie4, out message);

        // ============================================================================================================
        // ============================================================================================================
        // ==== End of testing section. Commenting the above lines can remove initial data. ===========================


        // The main program that shows the welcome screen and main menu
        bool running = true;
        while (running)
        {
            ShowWelcomeScreen();
            Console.Write("Enter your choice ==> ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (StaffLogin())
                    {
                        ShowStaffMenu();
                        HandleStaffMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Returning to the main menu.");
                    }

                    break;
                case "2":
                    if (MemberLogin())
                    {
                        ShowMemberMenu();
                        HandleMemberMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password. Returning to the main menu.");
                    }

                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Ending the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        // Method to show the main menu
        void ShowWelcomeScreen()
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
            Console.WriteLine("===================================================");
            Console.WriteLine("Main Menu");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Select from the following:");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. End the program");
            Console.WriteLine();
        }

        // Method to handle staff login. The username is "staff" and the password is "today123"
        bool StaffLogin()
        {
            Console.WriteLine("Please enter username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            string password = Console.ReadLine();

            return username == "staff" && password == "today123";
        }

        // Method to handle member login.
        // Validation inclues checking the first name, last name, and password (four-digit number).
        bool MemberLogin()
        {
            Console.WriteLine("Please enter first name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Please enter last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Please enter password: ");
            int password = int.Parse(Console.ReadLine());

            string message;

            return memberCollection.Login(firstName, lastName, password, out authenticatedMember, out message);
        }


        // Method to show the staff menu
        // The user will then select an option from the staff menu
        void ShowStaffMenu()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Staff Menu");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1. Add DVDs of a new movie to the system");
            Console.WriteLine("2. Add new DVDs of an existing movie to the system");
            Console.WriteLine("3. Remove a DVD from the system");
            Console.WriteLine("4. Register a new member to the system");
            Console.WriteLine("5. Remove a registered member from the system");
            Console.WriteLine("6. Find a member's contact phone number, given the member's name");
            Console.WriteLine("7. Find members who are currently renting a particular movie");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine();
        }

        // Method to handle the staff menu
        void HandleStaffMenu()
        {
            bool inStaffMenu = true;
            while (inStaffMenu)
            {
                Console.Write("Enter your choice for the staff menu ==> ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add DVDs of a new movie
                        AddNewMovie();
                        break;
                    case "2":
                        // Add new DVDs of an existing movie
                        AddExistingMovie();
                        break;
                    case "3":
                        // Remove DVDs of a movie from the system
                        RemoveMovieDVD();
                        break;
                    case "4":
                        // Register a new member
                        RegisterNewMember();
                        break;
                    case "5":
                        // Remove a registered member
                        RemoveMember();
                        break;
                    case "6":
                        // Find a member's contact phone number
                        SearchMember();
                        break;
                    case "7":
                        // Find all members that are currently renting a particular movie
                        FindMembersRentingMovie();
                        break;
                    case "0":
                        inStaffMenu = false;
                        ShowWelcomeScreen();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Method to add a new movie to the system (Option 1)
        void AddNewMovie()
        {
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine();

            Console.Write("Enter the genre of the movie (\"Drama\", \"Adventure\", \"Family\", \"Action\", \"SciFi\", \"Comedy\", \"Animated\", \"Thriller\", \"Other\"): ");
            string genre = Console.ReadLine();

            Console.Write("Enter the classification of the movie (\"G\", \"PG\", \"M15\", \"MA15\"): ");
            string classification = Console.ReadLine();

            Console.Write("Enter the duration of the movie (in minutes): ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter the number of copies: ");
            int copies = int.Parse(Console.ReadLine());

            try
            {
                Movie newMovie = new Movie(title, genre, classification, duration, copies);
                if (movieCollection.AddMovie(newMovie))
                {
                    Console.WriteLine(copies + " copies of " + title + " added to the system.");
                }
                else
                {
                    Console.WriteLine("Failed to add movie to the system.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Method to add new copies of an existing movie to the system (Option 2)
        void AddExistingMovie()
        {
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine();

            Console.Write("Enter the number of copies to add: ");
            int copies = int.Parse(Console.ReadLine());

            if (movieCollection.AddCopies(title, copies))
            {
                Console.WriteLine(copies + " copies of " + title + " added to the system.");
            }
            else
            {
                Console.WriteLine("Failed to add copies to the system.");
            }
        }

        // Method to remove copies of a movie from the system (Option 3)
        void RemoveMovieDVD()
        {
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine();

            Console.Write("Enter the number of copies to remove: ");
            int copies = int.Parse(Console.ReadLine());

            if (movieCollection.RemoveMovieCopies(title, copies))
            {
                Console.WriteLine(copies + " copies of " + title + " removed from the system.");
            }
            else
            {
                Console.WriteLine("Failed to remove copies from the system.");
            }
        }

        // Method to register a new member (Option 4)
        void RegisterNewMember()
        {
            Console.Write("New member's first name: ");
            string firstName = Console.ReadLine();

            Console.Write("New member's last name: ");
            string lastName = Console.ReadLine();

            Console.Write("New member's phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Set new member's initial four-digit password: ");
            int password = int.Parse(Console.ReadLine());

            Member newMember = new Member(firstName, lastName, phoneNumber, password);
            string message;
            if (memberCollection.RegisterMember(newMember, out message))
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Failed to register the new member.");
            }
        }

        // Method to remove a registered member (Option 5)
        void RemoveMember()
        {
            Console.Write("Enter member's first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter member's last name: ");
            string lastName = Console.ReadLine();

            string message;
            if (memberCollection.RemoveMember(firstName, lastName, out message))
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine("Failed to remove the member.");
            }
        }

        // Method to search for a member by name (Option 6)
        void SearchMember()
        {
            Console.Write("Enter member's first name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter member's last name: ");
            string lastName = Console.ReadLine();

            string message;
            Member? member = memberCollection.SearchMember(firstName, lastName, out message);

            if (member != null)
            {
                Console.WriteLine("Member found: " + member.FirstName + " " + member.LastName + ", phone number is " + member.PhoneNumber);
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        // Method to find all members renting a particular movie (Option 7)
        void FindMembersRentingMovie()
        {
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine();

            var rentingMembers = memberCollection.FindMembersRentingMovie(title);

            if (rentingMembers.Count == 0)
            {
                Console.WriteLine("No members are currently renting " + title);
            }

            Console.WriteLine(title + " is rented by these members:");

            foreach (var member in rentingMembers)
            {
                Console.WriteLine(member.FirstName + " " + member.LastName);
            }
        }

        // Method to show the member menu
        // The user will then select an option from the member menu
        void ShowMemberMenu()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Member Menu");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine();
        }

        // Method to handle the member menu
        void HandleMemberMenu()
        {
            bool inMemberMenu = true;
            while (inMemberMenu)
            {
                Console.Write("Enter your choice for the member menu ==> ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllMovies();
                        break;
                    case "2":
                        // Display all the information about a movie
                        DisplayMovie();
                        break;
                    case "3":
                        // Borrow a movie DVD
                        BorrowMovie();
                        break;
                    case "4":
                        // Return a movie DVD
                        ReturnMovie();
                        break;
                    case "5":
                        // List current borrowing movies
                        DisplayCurrentBorrowedMovies();
                        break;
                    case "6":
                        // Display the top 3 movies rented by the members
                        DisplayTopThreeMovies();
                        break;
                    case "0":
                        inMemberMenu = false;
                        ShowWelcomeScreen();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Method to display all movies (option 1)
        void DisplayAllMovies()
        {
            var movies = movieCollection.GetAllMoviesSortedByTitle();

            if (movies.Count == 0)
            {
                Console.WriteLine("No movies found.");
            }
            else
            {
                foreach (var movie in movies)
                {
                    Console.WriteLine(movie.Title + " (" + movie.MovieGenre + ", " + movie.MovieClassification + ", " + movie.Duration + " minutes) - " + movie.Copies + " copies");
                }
            }
        }

        // Method to display all information about a movie (option 2)
        void DisplayMovie()
        {
            Console.Write("Enter the title of the movie: ");
            string title = Console.ReadLine();

            Movie? movie = movieCollection.SearchMovie(title);
            Console.WriteLine(movie?.Title + " (" + movie?.MovieGenre + ", " + movie?.MovieClassification + ", " + movie?.Duration + " minutes) - " + movie?.Copies + " copies");
        }

        // Method to borrow a movie (option 3)
        void BorrowMovie()
        {
            if (authenticatedMember == null)
            {
                Console.WriteLine("You need to login first.");
                return;
            }

            Console.Write("Enter the title of the movie you want to borrow: ");
            string title = Console.ReadLine();

            var movie = movieCollection.SearchMovie(title);

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            string message;

            if (authenticatedMember.BorrowMovie(movie, out message))
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        // Method to return a movie (option 4)
        void ReturnMovie()
        {
            if (authenticatedMember == null)
            {
                Console.WriteLine("You need to login first.");
                return;
            }

            Console.Write("Enter the title of the movie you want to return: ");
            string title = Console.ReadLine();

            var movie = movieCollection.SearchMovie(title);

            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            string message;

            if (authenticatedMember.ReturnMovie(movie, out message))
            {
                movie.AddCopies(1);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        // Method to display the current borrowed movies (option 5)
        void DisplayCurrentBorrowedMovies()
        {
            if (authenticatedMember == null)
            {
                Console.WriteLine("You need to login first.");
                return;
            }

            var borrowedMovies = authenticatedMember.GetBorrowedMovies();
            Console.WriteLine("You have borrowed the following movies:");
            foreach (Movie borrowedMovie in borrowedMovies)
            {
                Console.WriteLine(borrowedMovie.Title);
            }
        }

        // Method to display the top three movies rented by the members (option 6)
        void DisplayTopThreeMovies()
        {
            List<Movie> topThreeMovies = movieCollection.GetTopThreeMovies();

            if (topThreeMovies.Count == 0)
            {
                Console.WriteLine("No movies have been borrowed yet.");
            }

            Console.WriteLine("Top 3 movies rented by the members:");
            foreach (Movie movie in topThreeMovies)
            {
                Console.WriteLine(movie.Title + " (" + movie.MovieGenre + ", " + movie.MovieClassification + ", " + movie.Duration + " minutes) - " + movie.BorrowCount + " times");
            }
        }
    }
}