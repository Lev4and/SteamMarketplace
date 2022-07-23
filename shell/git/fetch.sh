export $(egrep -v '^#' .env | xargs -0)
cd $PROJECT_SRC_DIR
git fetch