
# RabbitListener

This project is a sample project related to RabbitMQ.




## What Does the Project Include?

- Frontend: ReactJs
- Backend: .Net 6.0
- Docker, Docker Compose
- RabbitMQ
  
## Run

Clone project

```bash
  git clone https://github.com/ahmetturanozturk/RabbitListener.git
```

Go to the project

```bash
  cd RabbitListener/Services
```

Create a network

```bash
  docker network create my-network
```

Run docker-compose.yml

```bash
  docker-compose up -d
```

View from browser

```bash
  http://localhost:5285/
```



## Application ScreenShots

![ScreenShots](https://arogames.net/wp-content/uploads/2023/03/rabbitmq.gif)

![ScreenShots](https://arogames.net/wp-content/uploads/2023/03/test1.PNG)

![ScreenShots](https://arogames.net/wp-content/uploads/2023/03/test2.PNG)


## View Logs

http://localhost:5285/urls.json

or
```bash
docker exec -it {CONTEINER_ID} /bin/sh
cd wwwroot
cat urls.json
```
  
 [Game Play](https://arogames.net)

