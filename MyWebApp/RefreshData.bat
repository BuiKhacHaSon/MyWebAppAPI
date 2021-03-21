@echo off
dotnet ef database drop -f
dotnet ef migrations remove
dotnet ef migrations add mywebapp
dotnet ef database update