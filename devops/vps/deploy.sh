#!/bin/bash

cd /var/www/trivi

printf "\n\n\n"
echo "------------------------------------------------"
echo 'Pulling down latest code from GitHub...'
echo "------------------------------------------------"
printf "\n\n\n"

git pull





printf "\n\n\n"
echo "------------------------------------------------"
echo 'Building dotnet Project'
echo "------------------------------------------------"
printf "\n\n\n"


cd /var/www/trivi/src/dotnet/Trivi
dotnet publish Trivi.WebGui -r linux-x64 --self-contained false -c Release





printf "\n\n\n"
echo "------------------------------------------------"
echo 'Compiling css'
echo "------------------------------------------------"
printf "\n\n\n"

cd /var/www/trivi/src/dotnet/Trivi/Trivi.WebGui/wwwroot/css
sass custom/style.scss dist/style.css



printf "\n\n\n"
echo "------------------------------------------------"
echo 'Compiling typescript'
echo "------------------------------------------------"
printf "\n\n\n"
cd /var/www/trivi/src/dotnet/Trivi/Trivi.WebGui/wwwroot/js
node --max-old-space-size=8192 /usr/bin/rollup -c rollup.config.js --bundleConfigAsCjs


printf "\n\n\n"
echo "------------------------------------------------"
echo 'Starting up gui server...'
echo "------------------------------------------------"
printf "\n\n\n"

cd /etc/systemd/system
systemctl daemon-reload
systemctl reload-or-restart trivi.gui.service
service apache2 restart
