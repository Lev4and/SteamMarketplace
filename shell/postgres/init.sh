#!/bin/sh
#!/usr/bin/env bash
which sh
cd ..
cd ..
cd /db/$POSTGRES_DB_NAME/schema
docker exec $DOCKER_POSTGRES_CONTAINER_NAME psql -U $POSTGRES_USER -d $POSTGRES_DB_NAME -a -f schema.sql
cd ..
cd data
docker exec $DOCKER_POSTGRES_CONTAINER_NAME psql -U $POSTGRES_USER -d $POSTGRES_DB_NAME -a -f data.sql