:: --------------------------------------------
:: Start up the Rollup.js bundler 
:: --------------------------------------------

:: cd C:\xampp\htdocs\files\deadit\src\dotnet\Deadit\Deadit.WebGui\wwwroot\js
cd C:\xampp\htdocs\files\trivi\src\dotnet\Trivi\Trivi.WebGui\wwwroot\js

rollup -c rollup.config.js --watch --strict --bundleConfigAsCjs
