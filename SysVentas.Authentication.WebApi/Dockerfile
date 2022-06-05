FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SysVentas.Authentication.WebApi/SysVentas.Authentication.WebApi.csproj", "SysVentas.Authentication.WebApi/"]
RUN dotnet restore "SysVentas.Authentication.WebApi/SysVentas.Authentication.WebApi.csproj"
COPY . .
WORKDIR "/src/SysVentas.Authentication.WebApi"
RUN dotnet build "SysVentas.Authentication.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SysVentas.Authentication.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SysVentas.Authentication.WebApi.dll"]
