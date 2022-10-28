#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WordPrediction.Api/WordPrediction.Api.csproj", "WordPrediction.Api/"]
RUN dotnet restore "WordPrediction.Api/WordPrediction.Api.csproj"
COPY . .
WORKDIR "/src/WordPrediction.Api"
RUN dotnet build "WordPrediction.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WordPrediction.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WordPrediction.Api.dll"]