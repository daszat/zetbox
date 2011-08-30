
if .%1. == .-xml. GOTO XML

del bin\gendarme.html
set output=--html bin\gendarme.html

GOTO EXECUTE

:XML

del bin\gendarme.xml
set output=--xml bin\gendarme.xml

:EXECUTE

gendarme.exe --config gendarmerules.xml %output% --ignore gendarmeignore.txt --severity "high+" bin\Debug\Common\Core\Kistl.API.dll bin\Debug\Common\App.ZBox\Kistl.App.Projekte.Common.dll bin\Debug\Common\Core\Kistl.API.Common.dll bin\Debug\Client\Core\Kistl.API.Client.dll bin\Debug\Client\App.ZBox\Kistl.App.Projekte.Client.dll bin\Debug\Client\ASP.NET\Kistl.Client.ASPNET.Toolkit.dll bin\Debug\Client\Core\Kistl.Client.dll bin\Debug\Kistl.Client.Forms.exe bin\Debug\Kistl.Client.WPF.exe bin\Debug\Client\Core\Kistl.DalProvider.ClientObjects.dll bin\Debug\Server\Core\Kistl.API.Server.dll bin\Debug\Server\App.ZBox\Kistl.App.Projekte.Server.dll bin\Debug\Server\EF\Kistl.DalProvider.EF.dll bin\Debug\Server\EF\Kistl.DalProvider.EF.Generator.dll bin\Debug\Common\Core\Kistl.DalProvider.Memory.dll bin\Debug\Server\Core\Kistl.DalProvider.Memory.Generator.dll bin\Debug\Server\NH\Kistl.DalProvider.NHibernate.dll bin\Debug\Server\NH\Kistl.DalProvider.NHibernate.Generator.dll bin\Debug\Server\Core\Kistl.Server.dll bin\Debug\Kistl.Server.Service.exe bin\Debug\Kistl.Server.Service32.exe

if .%1. == .-xml. GOTO EXIT

bin\gendarme.html

:EXIT
