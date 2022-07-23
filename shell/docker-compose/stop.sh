export $(egrep -v '^#' .env | xargs -0)
cd $PROJECT_SRC_DIR
docker-compose stop
docker stop