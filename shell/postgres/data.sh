#!/bin/sh
#!/usr/bin/env bash
which sh
cd ..
cd ..
cd /db/$POSTGRES_DB_NAME/data
docker exec $DOCKER_POSTGRES_CONTAINER_NAME pg_dump -U $POSTGRES_USER --column-inserts --data-only  $POSTGRES_DB_NAME > data-$(date +"%d.%m.%Y %H:%M:%S").sql