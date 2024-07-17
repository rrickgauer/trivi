mysqldump -u main -h 104.225.208.163 -p ^
--databases Trivi_Dev ^
--column-statistics=FALSE ^
--no-create-db ^
--routines ^
--events ^
--triggers ^
--skip-quote-names ^
--no-data ^
--result-file "C:\xampp\htdocs\files\trivi\sql\ddl\.schemas.sql"


mysqldump -u main -h 104.225.208.163 -p ^
--column-statistics=FALSE ^
--no-create-db ^
--no-create-info ^
--replace ^
--order-by-primary ^
--skip-quote-names ^
--result-file "C:\xampp\htdocs\files\trivi\sql\ddl\.data.sql" ^
Trivi_Dev Error_Message_Groups Error_Messages Question_Types


@echo off
cd "C:\xampp\htdocs\files\trivi\sql\ddl"

type ".schemas.sql" ".data.sql" > "dump.sql"

del ".schemas.sql" /Q
del ".data.sql" /Q

pause
