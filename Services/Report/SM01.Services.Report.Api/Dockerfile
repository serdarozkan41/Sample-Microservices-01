#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Report/SM01.Services.Report.Api/SM01.Services.Report.Api.csproj", "Services/Report/SM01.Services.Report.Api/"]
RUN dotnet restore "Services/Report/SM01.Services.Report.Api/SM01.Services.Report.Api.csproj"
COPY . .
WORKDIR "/src/Services/Report/SM01.Services.Report.Api"
RUN dotnet build "SM01.Services.Report.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SM01.Services.Report.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SM01.Services.Report.Api.dll"]