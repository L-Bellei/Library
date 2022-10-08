# Library 1.0.0

- This project is about a library management, Users and employees control, loan control of books and print reports on operations.

### Application requirements:

 - .NET 6 SDK.
 - Sql Server

### For Execution:

- Clone this repository on your visual studio 
  - (tutorial is here: "https://learn.microsoft.com/pt-br/visualstudio/get-started/tutorial-open-project-from-repo?view=vs-2022").

- On AppSettings: Change the connection string for your local Sql Server Connection.
  - Example("Data Source=YOUR-SQL-SERVER;Initial Catalog=Library-LocalDb;Integrated Security=True").
  
- On cmd: execute command "dotnet-ef database update" for create tables in your database.

### Packages:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Design
- Microsoft.AspNetCore.Authentication
- Microsoft.AspNetCore.Authentication.JwtBearer
- Swashbuckle.AspNetcore
