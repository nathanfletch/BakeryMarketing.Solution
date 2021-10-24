# Bakery Marketing


#### An app to market delicious sweets.

#### _By Nathan Fletcher_

* This app uses a many to many relationship in Entity Framework Core to model treats and flavors. Routes are only authorized for the specific type of user that should have permission to access them.

## Technologies Used

* C#
* ASP.NET Core MVC
* Restful Routing Conventions
* Entity Framework Core 
* Identity Framework

## Setup
<details>
<summary>Setup & Installation Instructions</summary>

#### Installations (if necessary)
* Install C# and .NET using the [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-5.0.100-macos-x64-installer)
* Install [MySql Community Server](https://dev.mysql.com/downloads/file/?id=484914)

#### Setup
* Clone this repository to your local machine
* Navigate to the BakeryMarketing.Solution folder and create a file named "appsettings.json" 
* Add the following code to the file:
  ```
  {
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=bakery_marketing;uid=root;pwd=[YOUR-PASSWORD-HERE];"
    }
  }
  ```
* Navigate to the BakeryMarketing folder and run the following commands:
* `dotnet restore` to install the necessary dependencies
* `dotnet build` to compile the project.
* `dotnet tool install --global dotnet-ef`
* `dotnet ef migrations add Initial`
* `dotnet ef database update`

#### Start
* From the terminal in the BakeryMarketing folder, run the following commands:

* `dotnet run` to start the server.
* Enter localhost:5000 in your browser to start using the app. 
</details>

## How to Use This App
There are 3 ways to use this app: sign in as a chef, sign in as a manager, or don't sign in (this represents a customer). These roles can use the following routes:
* Not signed in: Index and Details
* Chef: Index, Details, and Create
* Manager: Index, Details, Create, Edit, and Delete

## Known Issues
* Anyone can create a manager or chef account, so account creation should include more authorization.
* Please contact me if you find any bugs or have suggestions. 

## Future Plans
* Add another login for customers with ability to order treats.
* Add error messages if a user in a role tries to access an unauthorized route.
* Improve styling.

## License

_[MIT](https://opensource.org/licenses/MIT)_  

Copyright (c) 2021 Nathan Fletcher 

## Contact Information

_Nathan Fletcher @ github.com/nathanfletch_  