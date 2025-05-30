# Используем базовый образ для ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Используем SDK образ для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем всё сразу
COPY . .
# Восстанавливаем зависимости
RUN dotnet restore "XboxLiveApiCsharp.sln"
# Копируем остальные исходники
COPY . .
# Сборка всех проектов
WORKDIR "/src/XblApp.UI"
RUN dotnet build "XblApp.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "XblApp.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XblApp.UI.dll"]
