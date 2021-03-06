﻿FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app


COPY sisventas_authentication_ms.sln ./
COPY SysVentas.Authentication.Application/SysVentas.Authentication.Application.csproj SysVentas.Authentication.Application/
COPY SysVentas.Authentication.Domain/SysVentas.Authentication.Domain.csproj SysVentas.Authentication.Domain/
COPY SysVentas.Authentication.Infrastructure.Data/SysVentas.Authentication.Infrastructure.Data.csproj SysVentas.Authentication.Infrastructure.Data/
COPY SysVentas.Authentication.Infrastructure.Ldap/SysVentas.Authentication.Infrastructure.Ldap.csproj SysVentas.Authentication.Infrastructure.Ldap/
COPY SysVentas.Authentication.WebApi/SysVentas.Authentication.WebApi.csproj SysVentas.Authentication.WebApi/

RUN dotnet restore 

COPY . ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "SysVentas.Authentication.WebApi.dll"]
