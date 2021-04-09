#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["IGS.TechTest/IGS.TechTest.csproj", "IGS.TechTest/"]
RUN dotnet restore "IGS.TechTest/IGS.TechTest.csproj"
COPY . .
WORKDIR "/src/IGS.TechTest"
RUN dotnet build "IGS.TechTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IGS.TechTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "IGS.TechTest.dll"]
