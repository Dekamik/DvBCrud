FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DvBCrud.API.Tests.Mongo/DvBCrud.API.Tests.Mongo.csproj", "DvBCrud.API.Tests.Mongo/"]
RUN dotnet restore "DvBCrud.API.Tests.Mongo/DvBCrud.API.Tests.Mongo.csproj"
COPY . .
WORKDIR "/src/DvBCrud.API.Tests.Mongo"
RUN dotnet build "DvBCrud.API.Tests.Mongo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DvBCrud.API.Tests.Mongo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DvBCrud.API.Tests.Mongo.dll"]
