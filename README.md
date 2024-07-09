Staff can add/remove movie DVDs, manage members, and find members renting specific movies. Members can find/display movies, borrow/return movies, and list their currently rented movies. 

# Program Structure

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

