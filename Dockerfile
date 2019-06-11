FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
ADD NuGet.Config .
WORKDIR /src
COPY ["src/App.CheckIn.Subscribe.Web/App.CheckIn.Subscribe.Web.csproj", "src/App.CheckIn.Subscribe.Web/"]
COPY ["src/App.CheckIn.Subscribe.Application/App.CheckIn.Subscribe.Application.csproj", "src/App.CheckIn.Subscribe.Application/"]
COPY ["src/App.CheckIn.EntityFrameworkCore/App.CheckIn.EntityFrameworkCore.csproj", "src/App.CheckIn.EntityFrameworkCore/"]
COPY ["src/App.CheckIn.Domain/App.CheckIn.Domain.csproj", "src/App.CheckIn.Domain/"]
RUN dotnet restore "src/App.CheckIn.Subscribe.Web/App.CheckIn.Subscribe.Web.csproj"
COPY . .
WORKDIR "/src/src/App.CheckIn.Subscribe.Web"
RUN dotnet build "App.CheckIn.Subscribe.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.CheckIn.Subscribe.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "App.CheckIn.Subscribe.Web.dll"]
