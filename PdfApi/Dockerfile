#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
RUN apt-get update && apt-get install -y --allow-unauthenticated apt-utils libgdiplus libc6-dev libx11-dev
RUN ln -s /usr/lib/libgdiplus.so/usr/lib/gdiplus.dll
RUN apt update -y && apt install wkhtmltopdf -y

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY . .

RUN dotnet restore "PdfApi.csproj"

FROM build AS publish
RUN dotnet publish "PdfApi.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PdfApi.dll"]
