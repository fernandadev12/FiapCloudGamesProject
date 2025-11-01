FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FCG.API/FCG.API.csproj", "FCG.API/"]
COPY ["FCG.Application/FCG.Application.csproj", "FCG.Application/"]
COPY ["FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["FCG.Infra.Data/FCG.Infra.Data.csproj", "FCG.Infra.Data/"]
RUN dotnet restore "FCG.API/FCG.API.csproj"
COPY . .
WORKDIR "/src/FCG.API"
RUN dotnet build "FCG.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FCG.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.API.dll"]
