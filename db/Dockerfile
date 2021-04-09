FROM mcr.microsoft.com/mssql/server:2017-latest-ubuntu

RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

COPY . /usr/src/app

RUN chmod +x /usr/src/app/run-initialisation.sh

ENV SA_PASSWORD SuperS3cure
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh