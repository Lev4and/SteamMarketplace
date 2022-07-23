#!/bin/sh
#!/usr/bin/env bash
which sh
cd ..
cd ..
cd /db/$POSTGRES_DB_NAME/data
docker exec $DOCKER_POSTGRES_CONTAINER_NAME pg_dump -U $POSTGRES_USER --column-inserts --data-only -t '"public"."Applications"' -t '"public"."Currencies"' -t '"public"."ExchangeRates"' -t '"public"."TransactionTypes"' -t '"public"."AspNetRoles"' -t '"public"."AspNetUsers"' -t '"public"."Transactions"'  $POSTGRES_DB_NAME > data-lite-$(date +"%d.%m.%Y %H:%M:%S").sql