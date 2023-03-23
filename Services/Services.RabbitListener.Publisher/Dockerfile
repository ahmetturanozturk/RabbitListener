FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app


COPY *.csproj ./
COPY ["Services.RabbitListener.RabbitMQ/RabbitListener.Shared.csproj", "Services.RabbitListener.RabbitMQ/"]

RUN dotnet restore	"Services.RabbitListener.RabbitMQ/RabbitListener.Shared.csproj"

COPY . ./
RUN dotnet publish Services.RabbitListener.Publisher/Services.RabbitListener.Publisher.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime

WORKDIR /app

COPY --from=build /app/out .
ENV DOTNET_RUNNING_IN_CONTAINER=true
#ASPNETCORE_URLS

ENTRYPOINT ["dotnet", "Services.RabbitListener.Publisher.dll"]