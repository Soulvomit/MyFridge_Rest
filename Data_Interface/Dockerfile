#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyFridge_WebAPI/MyFridge_WebAPI.csproj", "MyFridge_WebAPI/"]
RUN dotnet restore "MyFridge_WebAPI/MyFridge_WebAPI.csproj"
COPY . .
WORKDIR "/src/MyFridge_WebAPI"
RUN dotnet build "MyFridge_WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyFridge_WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyFridge_WebAPI.dll"]