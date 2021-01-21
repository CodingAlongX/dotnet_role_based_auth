### DotNet Role Based Authentication

###

##### Add required NuGet packages

1. `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

2. `dotnet add package Microsoft.EntityFrameworkCore`

3. `dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore`

4. `dotnet add package Microsoft.EntityFrameworkCore.Design`

5. `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`

##### Install EntityFramework command line tool

* `dotnet tool install --global dotnet-ef`

##### Create migrations

* `dotnet ef migrations add InitialCreate`

##### Apply migrations to database

* `dotnet ef database update`

##### Run

* `dotnet watch run`