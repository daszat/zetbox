@echo off

pushd
cd ..\
bin\debug\bin\server\Kistl.Server.Service.exe Kistl.Server.Service\DefaultConfig.xml -checkschema Kistl.Server\Database\Database.xml
popd
