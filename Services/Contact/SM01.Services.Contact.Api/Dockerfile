#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Contact/SM01.Services.Contact.Api/SM01.Services.Contact.Api.csproj", "Services/Contact/SM01.Services.Contact.Api/"]
RUN dotnet restore "Services/Contact/SM01.Services.Contact.Api/SM01.Services.Contact.Api.csproj"
COPY . .
WORKDIR "/src/Services/Contact/SM01.Services.Contact.Api"
RUN dotnet build "SM01.Services.Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SM01.Services.Contact.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SM01.Services.Contact.Api.dll"]