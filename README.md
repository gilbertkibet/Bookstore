# Bookstore

## Live Testing On Azure
Navigate to: 
[Live Bookstore Api On Azure](https://bookstore-api.azurewebsites.net/swagger/index.html) 
Or copy paste this Url ```sh https://bookstore-api.azurewebsites.net/swagger/index.html``` . 

Either of the above will navigate you to the swagger documentation page of the Bookstore API.
The app uses JWT token for authentication. Some actions might require you to be Authenticated. Use 
``` superadmin@bookstore.com ```  as the username and ``` Pa$$w0rd ``` as password to login through the login endpoint to get the Token. Click on authorize button and paste the token then you can test the actions.

This can also be tested using Postman.

### Built With

* [.NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

# Installing .NET 5

- [Downloads](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Installation docs](https://docs.microsoft.com/dotnet/core/install/)
- [Install Visual studio](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2022)


## Getting Started

## Installation

1. Clone the repo
   ```sh
   git clone https://github.com/MuthomiEric/Bookstore
   ```
2. Make sure you have followed the above steps to install .NET 5 and visual studio on your machine

3. Navigate to the .sln file and click on it it will open the solution on visual studio

4. Restore the dependancies 

5. Add a connection string to your database in appsetting.json one for Identity and another one for StoreContext. Copy this connection   string and edit accordingly
 ```sh
 "ConnectionStrings": {
    "AppIdentityCS": "Server=(local);Initial Catalog=AppIdentity;Persist Security Info=False;User ID=sa;Password={yourpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "BookstoreCS": "Server=Server=(local);Initial Catalog=BookStore;Persist Security Info=False;User ID=sa;Password={yourpassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  ```
6. Also add a token section like this to your appsetting.json 
 ```sh
"Token": {
    "Key": "el gran secreto",
    "Issuer": "https://localhost:5001"
  }
```
7. Start the application. Navigate to your URL/swagger and you will have the UI documenting the API

## Usage

The app uses JWT token for authentication. Some actions might require you to be Authenticated. Use superadmin@bookstore.com as the username and Pa$$w0rd to login through the login endpoint to get the Token. Click on authorize button and paste the token then you can test the actions.



## Contact

Erick Muthomi - erick.muthomi@outlook.com

Project Link: [https://github.com/MuthomiEric/Bookstore](https://github.com/MuthomiEric/Bookstore)

<p align="right">(<a href="#top">back to top</a>)</p>

