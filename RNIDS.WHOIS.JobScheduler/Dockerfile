FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RNIDS.WHOIS.JobScheduler/RNIDS.WHOIS.JobScheduler.csproj", "RNIDS.WHOIS.JobScheduler/"]
RUN dotnet restore "RNIDS.WHOIS.JobScheduler/RNIDS.WHOIS.JobScheduler.csproj"
COPY . .
WORKDIR "/src/RNIDS.WHOIS.JobScheduler"
RUN dotnet build "RNIDS.WHOIS.JobScheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RNIDS.WHOIS.JobScheduler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RNIDS.WHOIS.JobScheduler.dll"]
