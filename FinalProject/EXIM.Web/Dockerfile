FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
RUN apt-get update && apt-get install -y nodejs
COPY ["EXIM.Web/*.csproj", "EXIM.Web/"]
COPY ["EXIM.Data/*.csproj", "EXIM.Data/"]
COPY ["EXIM.Common/*.csproj", "EXIM.Common/"]
COPY ["EXIM.Membership/*.csproj", "EXIM.Membership/"]
COPY ["EXIM.DataImporter/*.csproj", "EXIM.Training/"]
RUN dotnet restore "EXIM.Web/EXIM.Web.csproj"
COPY . .
WORKDIR "/src/EXIM.Web"
RUN dotnet build "EXIM.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "EXIM.Web.csproj" -c Release -o /app

FROM build AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EXIM.Web.dll"]