ECHO off

rem batch file to run a script to create a db
rem 09/17/2020

rem sqlcmd -S localhost -E -i gen_one_db.sql
sqlcmd -S localhost\sqlexpress -E -i gen_one_db.sql
rem sqlcmd -S localhost\mssqlserver -E -i gen_one_db.sql


ECHO .
ECHO if no error messages appear DB was created
PAUSE