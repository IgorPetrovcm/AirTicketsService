#!/bin/sh

ENV_FILE="./container.env"
DUMP_FILE="./dump.sql"

DMBS_PASSWORD=root


if [ -f "$ENV_FILE" ]
then 
    source $ENV_FILE
else
    exit 1
fi

if [ ! -f "$DUMP_FILE" ]
then 
    exit 1
fi

PGPASSWORD=$PASSWORD psql -h localhost -p $PORT -U $USERNAME < $DUMP_FILE

echo "Dump produced!"