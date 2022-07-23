which sh
#!/bin/sh
#!/usr/bin/env bash
cd /var/projects/${PROJECT_NAME}/shell/git
bash sync.sh
cd /var/projects/${PROJECT_NAME}/shell/docker-compose
bash build.sh