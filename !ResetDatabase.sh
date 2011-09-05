#!/bin/bash -e

echo '********************************************************************************'
echo 'Reset the database to what is defined in the database.xml'
echo '********************************************************************************'

mono --debug bin/Debug/Kistl.Server.Service.exe Configs/$zenv/Kistl.Server.Service.xml \
	-updateschema Modules/KistlBasic.xml;Modules/KistlUtils.xml;Modules/TestModules.xml \
	-deploy Modules/KistlBasic.xml \
	-deploy Modules/KistlUtils.xml \
	-deploy Modules/TestModules.xml \
	-updatedeployedschema \
	-repairschema

echo '********************************************************************************'
echo '************************************ Success ***********************************'
echo '********************************************************************************'
