dotnet ef database update 0 --context DevTvDbContext --project "..\..\src\DevTv.Api\DevTv.Api.csproj"
dotnet ef migrations remove --context DevTvDbContext --project "..\..\src\DevTv.Api\DevTv.Api.csproj"
dotnet ef migrations add InitialCreate --context DevTvDbContext --project "..\..\src\DevTv.Api\DevTv.Api.csproj"

dotnet ef database update 0 --context EventStore --project "..\..\src\DevTv.Api\DevTv.Api.csproj"
dotnet ef migrations remove --context EventStore --project "..\..\src\DevTv.Api\DevTv.Api.csproj"
dotnet ef migrations add InitialCreate --context EventStore --project "..\..\src\DevTv.Api\DevTv.Api.csproj"

dotnet run ci --project "..\..\src\DevTv.Api\DevTv.Api.csproj"
