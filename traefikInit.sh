mkdir -p data/configurations
touch data/acme.json
chmod 600 data/acme.json
cp traefik.yml data/configurations/traefik.yml
cp dynamic.yml data/configurations/dynamic.yml