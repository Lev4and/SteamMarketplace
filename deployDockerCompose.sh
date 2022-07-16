folders=(\
    "/var/projects/SteamMarketplace/" \
      )

for folder in ${folders[*]}
do
    echo "update " $folder
    cd $folder && git fetch
    cd $folder && git reset --hard origin/master
done

cd "/var/projects/SteamMarketplace/docker-compose/" && docker-compose up -d --build