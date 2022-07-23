#!/bin/sh
#!/usr/bin/env bash
which sh
cd ..
cd ..
cd /db/$POSTGRES_DB_NAME/schema
docker exec $DOCKER_POSTGRES_CONTAINER_NAME pg_dump -U $POSTGRES_USER --schema-only $POSTGRES_DB_NAME > schema-$(date +"%d.%m.%Y %H:%M:%S").sql