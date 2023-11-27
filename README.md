# docker-aspnet-angular-phone-book-app

## Docker Commands

```bash
sudo -s
docker rm -vf $(sudo docker ps -aq)
docker rm -f $(sudo docker ps -aq)
docker rmi -f $(sudo docker images -aq)
docker system prune -y

docker-compose build
docker network create app_network
docker images -a
docker ps -a
docker create --network=app_network --name db --hostname db -p 1433:1433 [image_id]
docker start db
docker create --network=app_network --name api --hostname api -p 5057:8080 [image_id]
docker start api
docker create --network=app_network --name client --hostname client -p 4200:80 [image_id]
docker start client

docker exec -it db "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123"
```

```sql
--https://discourse.ubuntu.com/t/microsoft-sql-server-2019-on-ubuntu-20-04/21943
SELECT Name from sys.Databases;
GO
USE PhoneBookApp;
GO

--https://chartio.com/resources/tutorials/sql-server-list-tables-how-to-show-all-tables/
SELECT * FROM INFORMATION_SCHEMA.TABLES;
GO

--https://learn.microsoft.com/en-us/sql/t-sql/functions/serverproperty-transact-sql?view=sql-server-ver16
SELECT SERVERPROPERTY('ServerName') AS ComputerName
GO
```
