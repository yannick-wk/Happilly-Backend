#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Happilly.Presentation/Happilly.Presentation.csproj", "Happilly.Presentation/"]
COPY ["Happilly.Persistence/Happilly.Persistence.csproj", "Happilly.Persistence/"]
COPY ["Happilly.Application/Happilly.Application.csproj", "Happilly.Application/"]
COPY ["Happilly.Domain/Happilly.Domain.csproj", "Happilly.Domain/"]
RUN dotnet restore "Happilly.Presentation/Happilly.Presentation.csproj"
COPY . .
WORKDIR "/src/Happilly.Presentation"
RUN dotnet build "Happilly.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Happilly.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Happilly.Presentation.dll"]