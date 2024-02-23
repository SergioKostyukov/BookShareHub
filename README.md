# BookShareHub

*It`s a web application built on ASP.NET Core, facilitating book exchanges among users. 
It provides a user-friendly interface for registration, book listing, search, and exchange functionalities.
The backend is powered by ASP.NET Core with MSSQL as the database system. 
Overall, the platform offers a seamless solution for book enthusiasts to connect, exchange books, and engage in literary discussions.*

## Application functionality

Below is a description of the functionality, divided into the corresponding pages of the application
- User Login/SignUp
    - Registration and login to the system
- Account
    - Basic user information
    - View exchange history
    - View profile comments
    - (Additional) Other statistics
- Main page
    - Navigation through the pages (library, active, myBooks)
    - (Additional) Possibility to view news/updates/advertisements
- Books list
    - Possibility to view the books available in the library
    - Search for books within the library
    - Possibility to offer the order terms to the book owner
    - (Additional) Sorting and filtering by criteria
    - (Additional) Anonymous book exchange based on value and description
- Order page
    - Exchange books according to their value
    - Possibility to view information about the book, seller, delivery params
    - Possibility to create a chat with the seller
    - Possibility to send a request for exchange with a proposal (the other party must accept or reject)
    - Possibility to place an order if agreed upon
    - Possibility to leave a comment about the seller
- My shared books
    - Add own book for exchange
    - Change exist book params
    - (Additional) Determining the value of the book according to specified parameters
- Book page
    - Setting/changing book parameters
- Active (actual raffles/auctions/chats/meetings)
    - Possibility to view/delete private chats with sellers
    - Possibility to create/join/exit a discussion channel for a book/series of books
    - Possibility to create/join/exit an auction/raffle for donations
    - (Additional) Possibility to create/join video conferences and literary evenings
- Additional general features
    - book crossing posibility

## Entity classes description

- User - each user enters his own information during registration
- Book - parameters of the book are set when it is added to the general library
- Order - parameters at checkout
- OrderList - list of order objects
- ProfileComment - the user has the opportunity to leave a comment about the user after a successful (or not) order.
- Chat - the user can create a chat or join an existing one.
- ChatSubscribersList - each chat contains a list of subscribed users
