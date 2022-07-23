export $(egrep -v '^#' .env | xargs -0)
cd $PROJECT_SRC_DIR
docker rm -vf $(docker ps -aq)
docker rmi -f $(docker images -aq)
docker-compose ps
docker-compose images
docker ps
docker images