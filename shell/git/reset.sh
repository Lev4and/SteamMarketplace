export $(egrep -v '^#' .env | xargs -0)
cd $PROJECT_SRC_DIR
git reset --hard origin/$PROJECT_BRANCH