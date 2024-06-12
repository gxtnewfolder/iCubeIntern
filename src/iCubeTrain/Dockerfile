FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/iCubeTrain/iCubeTrain.csproj", "src/iCubeTrain/"]
RUN dotnet restore "src/iCubeTrain/iCubeTrain.csproj"
COPY . .
WORKDIR "/src/src/iCubeTrain"
RUN dotnet build "iCubeTrain.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "iCubeTrain.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iCubeTrain.dll"]
