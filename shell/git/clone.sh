cd ..
export $(egrep -v '^#' .env | xargs -0)
cd $PROJECT_SRC_DIR
git clone $PROJECT_GIT_REPOSITORY -b origin/$PROJECT_BRANCH