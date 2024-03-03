# BookShareHub

*It`s a web application built on ASP.NET Core, facilitating book exchanges among users. 
It provides a user-friendly interface for registration, book listing, search, and exchange functionalities.
The backend is powered by ASP.NET Core with MSSQL as the database system. 
Overall, the platform offers a seamless solution for book enthusiasts to connect, exchange books, and engage in literary discussions.*

## Application functionality

Below is a description of the functionality, divided into the corresponding pages of the application
- User Login/SignUp
    - User registration and login functionality
- Account
    - Access basic user information
    - View exchange history and profile comments
    - (Additional) View additional statistics
- Main page
    - Navigate through different sections such as library, active exchanges, and user's book listings
    - (Additional) Access news, updates, or advertisements
- Books list
    - Browse available books in the library
    - Sort and filter books based on various criteria
    - Search for specific books within the library
    - Initiate exchange requests with book owners
    - (Additional) Enable anonymous book exchanges based on value and description
- Order page
    - Facilitate book exchanges based on agreed-upon terms
    - View detailed information about the book, seller, and delivery parameters
    - Initiate chats with sellers
    - Send and respond to exchange requests
    - Leave feedback about the seller
- My shared books
    - Add new books for exchange
    - Modify existing book details
    - (Additional) Determine the value of books based on specified parameters
- Book page
    - Manage book parameters and details
- Active (actual raffles/auctions/chats/meetings)
    - View and manage private chats with sellers
    - Participate in or create discussion channels for books or series
    - Join or organize auctions and raffles for book donations
    - (Additional) Organize or participate in video conferences and literary events
- Additional general features
    - Enable book crossing initiatives

## Entity class descriptions

- User: Stores user information provided during registration
- Book: Stores parameters of books added to the library
- Order: Manages order parameters and status
- OrderList: Maintains a list of order objects
- ProfileComment: Allows users to leave comments about other users after successful transactions
- Chat: Facilitates user-to-user communication through chat creation or joining
- ChatSubscribersList: Manages a list of subscribers for each chat channel