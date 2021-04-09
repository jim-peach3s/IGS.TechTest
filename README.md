# IGS Tech Test
## Introduction
This is a solution to the marketplace tech test given by IGS.

It uses docker and docker compose for spinning the projects up but if you would prefer not to use these then you will need to install a version of Microsoft SQL Server which can be found [here](https://www.microsoft.com/en-gb/sql-server/sql-server-downloads).

If you are not going to use docker to install then you will need to manually run the DBSetup file found in the db folder to seed the database.

If you are using the docker version there is a 30s sleep after spinup before the seeding begins to allow for the server to start properly before creating the db.

## Installation
### Local or hosted db
If you want to install using your own local SQL database or a hosted SQL db then you can either build the app by going to the IGS.TechTest folder and running

```
dotnet build
dotnet run
```

If you want to build it as a docker container then in the same folder (\<root\>/IGS.TechTest) run the following commands

```
docker build -t igstechtest .
docker run -p 5000:80 igstechtest
```

You will need to make sure that you have no other docker container running that is using the port 5000.

If you are going to use your own DB then you will need to update the connection string in appsettings.json to reflect the connection details of your server. The setting is:

```json
{
  "ConnectionStrings:MarketplaceContext": "<your-server-connection-string>"
}
```

### Docker compose
You can also spin up this project using docker compose. This has a mssql database service rolled into it for the project.

You can do this by opening a terminal or command window at the root of the project and running the following commands.

```
docker-compose up -d --build
```

To tear down the containers then run:

```
docker-compose down
```

## Extra
A hosted version of this project can be found [here](https://igs-tech-test-jamie.azurewebsites.net).