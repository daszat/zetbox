
if .%1. == .-xml. GOTO XML

del bin\gendarme.html
set output=--html bin\gendarme.html

GOTO EXECUTE

:XML

del bin\gendarme.xml
set output=--xml bin\gendarme.xml

:EXECUTE

gendarme.exe --config gendarmerules.xml %output% --ignore gendarmeignore.txt --severity "high+" bin\Debug\Common\Zetbox.API.dll bin\Debug\Common\Zetbox.App.Projekte.Common.dll bin\Debug\Common\Zetbox.API.Common.dll bin\Debug\Client\Zetbox.API.Client.dll bin\Debug\Client\Zetbox.App.Projekte.Client.dll bin\Debug\Client\Zetbox.Client.dll bin\Debug\Zetbox.Client.Forms.exe bin\Debug\Client\Zetbox.Client.WPF.dll bin\Debug\Client\Zetbox.DalProvider.ClientObjects.dll bin\Debug\Server\Zetbox.API.Server.dll bin\Debug\Server\Zetbox.App.Projekte.Server.dll bin\Debug\Server\Zetbox.DalProvider.EF.dll bin\Debug\Server\Zetbox.DalProvider.EF.Generator.dll bin\Debug\Common\Zetbox.DalProvider.Memory.dll bin\Debug\Server\Zetbox.DalProvider.Memory.Generator.dll bin\Debug\Server\Zetbox.DalProvider.NHibernate.dll bin\Debug\Server\Zetbox.DalProvider.NHibernate.Generator.dll bin\Debug\Server\Zetbox.Server.dll bin\Debug\Zetbox.Cli.exe bin\Debug\Zetbox.Server.Service.exe bin\Debug\Zetbox.Server.Service32.exe

if .%1. == .-xml. GOTO EXIT

bin\gendarme.html

:EXIT
