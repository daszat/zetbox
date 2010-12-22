#!/bin/bash -e

echo '********************************************************************************'
echo 'Reset the database to what is defined in the database.xml'
echo '********************************************************************************'

mono bin/Debug/bin/Server/Kistl.Server.Service.exe Kistl.Server.Service/DefaultConfig${zenv}.xml -wipe
mono bin/Debug/bin/Server/Kistl.Server.Service.exe Kistl.Server.Service/DefaultConfig${zenv}.xml -updateschema Kistl.Server/Database/Database.xml
mono bin/Debug/bin/Server/Kistl.Server.Service.exe Kistl.Server.Service/DefaultConfig${zenv}.xml -deploy Kistl.Server/Database/Database.xml -checkdeployedschema
mono bin/Debug/bin/Server/Kistl.Server.Service.exe Kistl.Server.Service/DefaultConfig${zenv}.xml -repairschema -syncidentities

echo '********************************************************************************'
echo '************************************ Success ***********************************'
echo '********************************************************************************'
