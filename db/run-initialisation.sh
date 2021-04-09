#!/bin/bash

echo "Sleeping 30 seconds to make sure SQL is up and running"

sleep 30s

echo "----------------"
echo "Running DB Setup"
echo "----------------"

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P SuperS3cure -d master -i scripts/DBSetup.sql