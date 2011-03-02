#!/bin/bash -e

echo '********************************************************************************'
echo 'Reset the database to what is defined in the database.xml'
echo '********************************************************************************'

mono --debug bin/Debug/Kistl.Server.Service.exe Configs/$zenv/Kistl.Server.Service.xml -wipe
mono --debug bin/Debug/Kistl.Server.Service.exe Configs/$zenv/Kistl.Server.Service.xml -updateschema Kistl.Server/Database/Database.xml
mono --debug bin/Debug/Kistl.Server.Service.exe Configs/$zenv/Kistl.Server.Service.xml -deploy Kistl.Server/Database/Database.xml -checkdeployedschema
mono --debug bin/Debug/Kistl.Server.Service.exe Configs/$zenv/Kistl.Server.Service.xml -repairschema -syncidentities -generate

echo '********************************************************************************'
echo '************************************ Success ***********************************'
echo '********************************************************************************'
