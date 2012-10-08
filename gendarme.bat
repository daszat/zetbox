
if .%1. == .-xml. GOTO XML

del bin\gendarme.html
set output=--html bin\gendarme.html

GOTO EXECUTE

:XML

del bin\gendarme.xml
set output=--xml bin\gendarme.xml

:EXECUTE

gendarme.exe --config gendarmerules.xml %output% --ignore gendarmeignore.txt --severity "high+" bin\Debug\Common\Core\Zetbox.API.dll bin\Debug\Common\App.Zetbox\Zetbox.App.Projekte.Common.dll bin\Debug\Common\Core\Zetbox.API.Common.dll bin\Debug\Client\Core\Zetbox.API.Client.dll bin\Debug\Client\App.Zetbox\Zetbox.App.Projekte.Client.dll bin\Debug\Client\Core\Zetbox.Client.dll bin\Debug\Zetbox.Client.Forms.exe bin\Debug\Zetbox.Client.WPF.dll bin\Debug\Client\Core\Zetbox.DalProvider.ClientObjects.dll bin\Debug\Server\Core\Zetbox.API.Server.dll bin\Debug\Server\App.Zetbox\Zetbox.App.Projekte.Server.dll bin\Debug\Server\EF\Zetbox.DalProvider.EF.dll bin\Debug\Server\EF\Zetbox.DalProvider.EF.Generator.dll bin\Debug\Common\Core\Zetbox.DalProvider.Memory.dll bin\Debug\Server\Core\Zetbox.DalProvider.Memory.Generator.dll bin\Debug\Server\NH\Zetbox.DalProvider.NHibernate.dll bin\Debug\Server\NH\Zetbox.DalProvider.NHibernate.Generator.dll bin\Debug\Server\Core\Zetbox.Server.dll bin\Debug\Zetbox.Server.Service.exe bin\Debug\Zetbox.Server.Service32.exe

if .%1. == .-xml. GOTO EXIT

bin\gendarme.html

:EXIT
