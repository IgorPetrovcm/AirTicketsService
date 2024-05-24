#!/bin/sh

source ./container.env

PGPASSWORD=$DMBS_PASSWORD psql -h localhost -p $PORT -U $DMBS_USERNAME < ./dump.sql

echo "Dump produced!"