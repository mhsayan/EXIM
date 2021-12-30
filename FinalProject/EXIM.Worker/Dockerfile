FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["EXIM.Worker/*.csproj", "EXIM.Worker/"]
COPY ["EXIM.Data/*.csproj", "EXIM.Data/"]
COPY ["EXIM.Common/*.csproj", "EXIM.Common/"]
COPY ["EXIM.DataImporter/*.csproj", "EXIM.DataImporter/"]
RUN dotnet restore "EXIM.Worker/EXIM.Worker.csproj"
COPY . .
WORKDIR "/src/EXIM.Worker"
RUN dotnet build "EXIM.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EXIM.Worker.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EXIM.Worker.dll"]