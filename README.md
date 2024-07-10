To run this application locally, download all files and in Visual Studio 2022 run "Library manager.csproj” file. Alternatively, you can use Visual Studio Code.

This application is developed for a community library to manage its movie DVDs. The community can lend movie DVDs to its members. The library staff can add/remove movie DVDs, manage members, and find members renting specific movies. Members can find/display movies, borrow/return movies, and list their currently rented movies. 

Upon starting, the program presents the main menu with two options: Staff (Option 1) or Member (Option 2).
-	Selecting Option 1 prompts the user to enter a username and password ("staff" and "today123"). If the credentials are correct, the staff menu is displayed, providing access to various operations.
-	Selecting Option 2 prompts the user to enter their first name, last name, and a 4-digit password. These credentials are verified against the members in the “memberCollection“. If the credentials are correct, the user is assigned as “authenticatedMember“, and the member menu is displayed, offering options such as borrowing, returning, and displaying movies.

There are some initial data at the top of the program (lines 19-76 in the Program class). Uncommenting these lines can run the application without any initial data. 

# Program Class Structure

**Global Variables** <br>
|-- `MemberCollection? memberCollection` <br>
|-- `MovieCollection? movieCollection` <br>
|-- `Member? authenticatedMember` <br>

**Main Method** <br>
|-- Initialize collections <br>
|-- Add initial data (Can comment out) <br>
|-- Main loop <br>
|   |-- ShowWelcomeScreen <br>
|   |-- Handle user input <br>
|       |-- StaffLogin and menu <br>
|           |-- ShowStaffMenu <br>
|           |-- HandleStaffMenu <br>
|       |-- MemberLogin and menu <br>
|           |-- ShowMemberMenu <br>
|           |-- HandleMemberMenu <br>

**ShowWelcomeScreen Method** <br>

**StaffLogin Method** <br>

**MemberLogin Method** <br>

**ShowStaffMenu Method** <br>

**HandleStaffMenu Method** <br>
|-- AddNewMovie <br>
|-- AddExistingMovie <br>
|-- RemoveMovieDVD <br>
|-- RegisterNewMember <br>
|-- RemoveMember <br>
|-- SearchMember <br>
|-- FindMembersRentingMovie <br>

**ShowMemberMenu Method** <br>

**HandleMemberMenu Method** <br>
|-- DisplayAllMovies <br>
|-- DisplayMovie <br>
|-- BorrowMovie <br>
|-- ReturnMovie <br>
|-- DisplayCurrentBorrowedMovies <br>
|-- DisplayTopThreeMovies <br>

