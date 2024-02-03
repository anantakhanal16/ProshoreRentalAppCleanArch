1)getting started 

clone the repostiory and open visual studio

2)do the migrations by running following  command in terminal 

dotnet ef migrations add InitialCreate2    -p Infrastructure -s RentalApi

dotnet ef database update -p Infrastructure -s RentalApi --context ApplicationDbContext


