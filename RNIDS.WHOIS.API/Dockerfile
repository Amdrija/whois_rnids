FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RNIDS.WHOIS/RNIDS.WHOIS.csproj", "RNIDS.WHOIS/"]
RUN dotnet restore "RNIDS.WHOIS/RNIDS.WHOIS.csproj"
COPY . .
WORKDIR "/src/RNIDS.WHOIS"
RUN dotnet build "RNIDS.WHOIS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RNIDS.WHOIS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RNIDS.WHOIS.dll"]
