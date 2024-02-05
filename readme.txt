Rental App
Overview
The Rental App is a clean architecture-based application developed using ASP.NET Core Web API. It facilitates the interaction between brokers and house seekers, allowing brokers to manage property details and users to search and view properties.

Project Structure
The project follows a clean architecture pattern with four main layers:

Core Layer:

Entities: Define the core data structures.
Interfaces: Contain contracts for interacting with data and services.
Infrastructure Layer:

Implementations: Implement the interfaces defined in the core layer.
Application Layer:

Services: Contain business logic and services.
Services are injected into the Web API.
Web API:

Exposes endpoints for user and broker interactions.
Supports user registration, property management, and property search.

Features
Broker Actions

Register as a Broker:

Endpoint: /api/account/Register-as-broker
Allows users to register as a broker.
Login as a Broker:

Endpoint: /api/account/Login
Allows registered users, including brokers, to log in.
Logout:

Endpoint: /api/account/Logout
Allows users to log out.


Add Property Detail:

Endpoint: /api/broker/addproperty
Enables brokers to add property details.
Update Property:

Endpoint: /api/broker/updateproperty/{propertyId}
Allows brokers to update property information.
Delete Property:

Endpoint: /api/broker/deleteproperty/{propertyId}
Enables brokers to delete a property.


User Actions
Register as a House Seeker:

Endpoint: /api/user/register

Register as a  House Seeker:

Endpoint: /api/account/Register-house-seeker
Allows users to register as a House Seeker.
Login as a Broker:

Endpoint: /api/account/Login
Allows registered users, to log in.
Logout:

Endpoint: /api/account/Logout
Allows users to log out.


Search and View Property:

Endpoint: /api/user/searchproperty
Enables house seekers to search and view available properties.
Setup and Usage
Clone the Repository:

bash
Copy code
git clone https://github.com/anantakhanal16/ProshoreRentalAppCleanArch.git
cd rental-app
Restore Packages and Build:

Copy code
dotnet restore
dotnet build
Run the Application:

arduino
Copy code
dotnet run
Access the API:

API Base URL: http://localhost:5000
Dependencies
ASP.NET Core
Entity Framework Core

Contribution
Feel free to contribute to the project by submitting issues or pull requests.

License
This project is licensed under the MIT License.










1)getting started 

clone the repostiory and open visual studio

2)do the migrations by running following  command in terminal 

dotnet ef migrations add InitialCreate2    -p Infrastructure -s RentalApi

dotnet ef database update -p Infrastructure -s RentalApi --context ApplicationDbContext


