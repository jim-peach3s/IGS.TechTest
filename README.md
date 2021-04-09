# IGS Tech Test
## Introduction
This is a solution to the marketplace tech test given by IGS.

It uses docker and docker compose for spinning the projects up but if you would prefer not to use these then you will need to install a version of Microsoft SQL Server which can be found [here](https://www.microsoft.com/en-gb/sql-server/sql-server-downloads).

If you are not going to use docker to install then you will need to manually run the DBSetup file found in the db folder to seed the database.

If you are using the docker version there is a 30s sleep after spinup before the seeding begins to allow for the server to start properly before creating the db.