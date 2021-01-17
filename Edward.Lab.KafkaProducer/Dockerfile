#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Edward.Lab.KafkaProducer/Edward.Lab.KafkaProducer.csproj", "Edward.Lab.KafkaProducer/"]
RUN dotnet restore "Edward.Lab.KafkaProducer/Edward.Lab.KafkaProducer.csproj"
COPY . .
WORKDIR "/src/Edward.Lab.KafkaProducer"
RUN dotnet build "Edward.Lab.KafkaProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Edward.Lab.KafkaProducer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Edward.Lab.KafkaProducer.dll"]