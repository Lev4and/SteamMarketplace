#!/bin/sh
#!/usr/bin/env bash
which sh
cd /var
mkdir projects
cd projects
git clone https://github.com/Lev4and/SteamMarketplace.git
cd SteamMarketplace
cd /shell/portainer
bash createData.sh
cd ..
cd grafana
bash createData.sh
cd ..
cd docker-compose
bash build.sh
cd ..
cd postgres
bash init.sh