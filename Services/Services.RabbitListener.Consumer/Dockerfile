FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

COPY *.csproj ./
COPY ["Services.RabbitListener.RabbitMQ/RabbitListener.Shared.csproj", "Services.RabbitListener.RabbitMQ/"]

RUN dotnet restore	"Services.RabbitListener.RabbitMQ/RabbitListener.Shared.csproj"

COPY . ./
RUN dotnet publish Services.RabbitListener.Consumer/Services.RabbitListener.Consumer.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Services.RabbitListener.Consumer.dll"]