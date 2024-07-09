using Library_manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManager
{
    public class MemberCollection
    {
        private const int MaxMembers = 100000;
        private const int TableSize = 100003; // Table size is chosen as a prime number that is close to 100000
        private List<KeyValuePair<string, List<Member>>>[] table;  // An array table to store lists of members

        public MemberCollection()
        {
            table = new List<KeyValuePair<string, List<Member>>>[TableSize];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new List<KeyValuePair<string, List<Member>>>();
            }
        }

        // A hash function for a string input (full name from the member)
        public int GetHash(string key)
        {
            int hash = 0;
            foreach (char c in key)
            {
                hash = (hash * 29 + c) % TableSize;
            }
            return hash;
        }

        // Method to obtain the number of members
        public int GetMemberCount()
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

        // Method to register a new member
        public bool RegisterMember(Member member, out string message)
        {
            if (GetMemberCount() >= MaxMembers)
            {
                message = "Cannot add more members because the collection is full.";
                return false;
            }

            string fullName = member.FirstName + member.LastName;
            int hash = GetHash(fullName); // Obtain the hash value from member's full name
            var bucket = table[hash]; // find the bucket that holds the members with the hash index

            // for the bucket, check if the member already exists for the name
            foreach (var kvp in bucket)
            {
                if (kvp.Key == fullName)
                {
                    message = "The user has been registered!";
                    return false;
                }
            }

            // If the member is not found, create a new member
            bucket.Add(new KeyValuePair<string, List<Member>>(fullName, new List<Member> { member }));
            message = $"The member {member.FirstName} {member.LastName} is successfully registered to the system.";
            return true;
        }

        // Method to search for a member by full name
        public Member SearchMember(string firstName, string lastName, out string message)
        {
            string fullName = firstName + lastName;
            int hash = GetHash(fullName);
            var bucket = table[hash];

            foreach (var kvp in bucket)
            {
                if (kvp.Key == fullName)
                {
                    // Assuming we return the first member found with the matching name
                    message = "Member found.";
                    return kvp.Value.FirstOrDefault();
                }
            }

            message = "Cannot find the member!";
            return null; // Member not found
        }

        // Method to remove a member
        public bool RemoveMember(string firstName, string lastName, out string message)
        {
            string fullName = firstName + lastName;
            int hash = GetHash(fullName);
            var bucket = table[hash];

            for (int i = 0; i < bucket.Count; i++)
            {
                if (bucket[i].Key == fullName)
                {
                    bucket.RemoveAt(i);
                    message = $"The member {firstName} {lastName} is successfully removed.";
                    return true;
                }
            }

            message = "Cannot find the member to remove!";
            return false; // Member not found
        }

        // Method to login a member
        public bool Login(string firstName, string lastName, int passWord, out Member authenticatedMember, out string message)
        {
            authenticatedMember = SearchMember(firstName, lastName, out message);
            if (authenticatedMember != null && authenticatedMember.PassWord == passWord)
            {
                message = "Login successful!";
                return true;
            }

            message = "Login failed!";
            return false;
        }

        // Method to find members renting a specific movie
        public List<Member> FindMembersRentingMovie(string movieTitle)
        {
            List<Member> rentingMembers = new List<Member>();

            for (int i = 0; i < TableSize; i++)
            {
                if (table[i] != null) // Ensure table[i] is not null before iterating
                {
                    foreach (var kvp in table[i])
                    {
                        foreach (Member member in kvp.Value)
                        {
                            foreach (Movie movie in member.GetBorrowedMovies())
                            {
                                if (movie.Title == movieTitle)
                                {
                                    rentingMembers.Add(member);
                                    break; // Exit inner loop once the movie is found
                                }
                            }
                        }
                    }
                }
            }
            return rentingMembers;
        }
    }
}
