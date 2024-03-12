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
- Main
    - Navigate through different sections such as library, active exchanges, and user's book listings
    - (Additional) Access news, updates, or advertisements
- Library
    - Browse available books in the library
    - Sort and filter books based on various criteria (genre, language, price)
    - Search for specific books within the library (by title/author name)
    - View information about the book and its seller
- PreOrder
    - Initiate a purchase request with book owner
    - View detailed information about the book, seller
    - Initiate chat with seller
- Order
    - View detailed information about the basket items
    - Choose convenient delivery and payent options
    - View other seller's products
    - Confirm order
    - Leave feedback about the seller
- My books
    - Add new books for sell/exchange
    - Modify existing book details
- Book
    - Manage book parameters and details
- History
    - View information about actual, completed orders
- Active (actual raffles/auctions/chats/meetings)
    - Raffles/Auctions
        - Join or organize events to exchange books for donations
    - Chats
        - View and manage private chats with sellers
        - Participate in or create discussion channels for books or series
    - (Additional) Meetings
        - (Additional) Organize or participate in video conferences and literary events
- Additional general features
    - Enable anonymous book exchanges based on value and description
    - Enable book crossing initiatives

## Entity class descriptions

- User: Stores user information provided during registration
- Book: Stores parameters of books added to the library
- Order: Manages order parameters and status
- OrderList: Maintains a list of order objects
- ProfileComment: Allows users to leave comments about other users after successful transactions
- Chat: Facilitates user-to-user communication through chat creation or joining
- ChatSubscribersList: Manages a list of subscribers for each chat channel

![schema drawio](https://github.com/SergioKostyukov/BookShareHub/assets/107180627/c7cf19ef-07e8-4b85-8a1f-3d010c471420)


