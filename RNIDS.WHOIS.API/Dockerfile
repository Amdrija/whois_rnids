FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RNIDS.WHOIS.API/RNIDS.WHOIS.API.csproj", "RNIDS.WHOIS.API/"]
COPY ["RNIDS.WHOIS.Application/RNIDS.WHOIS.Application.csproj", "RNIDS.WHOIS.Application/"]
COPY ["RNIDS.WHOIS.Core/RNIDS.WHOIS.Core.csproj", "RNIDS.WHOIS.Core/"]
COPY ["RNIDS.WHOIS.TCP/RNIDS.WHOIS.TCP.csproj", "RNIDS.WHOIS.TCP/"]
COPY ["RNIDS.WHOIS.SerilogLogger/RNIDS.WHOIS.SerilogLogger.csproj", "RNIDS.WHOIS.SerilogLogger/"]
COPY ["RNIDS.WHOIS.MongoDB/RNIDS.WHOIS.MongoDB.csproj", "RNIDS.WHOIS.MongoDB/"]
RUN dotnet restore "RNIDS.WHOIS.API/RNIDS.WHOIS.API.csproj"
COPY . .
WORKDIR "/src/RNIDS.WHOIS.API"
RUN dotnet build "RNIDS.WHOIS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RNIDS.WHOIS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RNIDS.WHOIS.API.dll"]
