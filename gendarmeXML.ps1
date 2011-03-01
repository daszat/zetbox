param($SolutionDir)

function Convert-WithXslt($xmlFilePath, $xsltFilePath, $outputFilePath) 
{
	$xsltFilePath = resolve-path $xsltFilePath
	$xmlFilePath = resolve-path $xmlFilePath
	$outputFilePath = resolve-path $outputFilePath

	$xslt = new-object system.xml.xsl.xslcompiledtransform
	$xslt.load( $xsltFilePath )
	$xslt.Transform( $xmlFilePath, $outputFilePath )
}

if(-not $SolutionDir) 
{
	$SolutionDir = ".";
}

"." > $SolutionDir\bin\gendarme.txt
gendarme -config $SolutionDir\gendarmerules.xml --xml $SolutionDir\bin\gendarme.xml --ignore $SolutionDir\gendarmeignore.txt --severity high+ $SolutionDir\bin\Debug\Common\Core\Kistl.API.dll $SolutionDir\bin\Debug\Common\App.ZBox\Kistl.App.Projekte.Common.dll $SolutionDir\bin\Debug\Common\Core\Kistl.API.Common.dll $SolutionDir\bin\Debug\Client\Core\Kistl.API.Client.dll $SolutionDir\bin\Debug\Client\App.ZBox\Kistl.App.Projekte.Client.dll $SolutionDir\bin\Debug\Client\ASP.NET\Kistl.Client.ASPNET.Toolkit.dll $SolutionDir\bin\Debug\Client\Core\Kistl.Client.dll $SolutionDir\bin\Debug\Kistl.Client.Forms.exe $SolutionDir\bin\Debug\Kistl.Client.WPF.exe $SolutionDir\bin\Debug\Client\Core\Kistl.DalProvider.ClientObjects.dll $SolutionDir\bin\Debug\Server\Core\Kistl.API.Server.dll $SolutionDir\bin\Debug\Server\App.ZBox\Kistl.App.Projekte.Server.dll $SolutionDir\bin\Debug\Server\EF\Kistl.DalProvider.EF.dll $SolutionDir\bin\Debug\Common\Core\Kistl.DalProvider.Memory.dll $SolutionDir\bin\Debug\Server\Core\Kistl.DalProvider.Memory.Generator.dll $SolutionDir\bin\Debug\Server\NH\Kistl.DalProvider.NHibernate.dll $SolutionDir\bin\Debug\Server\NH\Kistl.DalProvider.NHibernate.Generator.dll $SolutionDir\bin\Debug\Server\Core\Kistl.Server.dll $SolutionDir\bin\Debug\Kistl.Server.Service.exe $SolutionDir\bin\Debug\Kistl.Server.Service32.exe $SolutionDir\bin\Debug\Ini50.Migrate.exe $SolutionDir\bin\Debug\Common\App.Ini50\Ini50.App.Common.dll $SolutionDir\bin\Debug\Client\App.Ini50\Ini50.App.Client.dll

convert-withxslt $SolutionDir\bin\gendarme.xml $SolutionDir\gendarme-visualstudio.xslt $SolutionDir\bin\gendarme.txt
(get-content $SolutionDir\bin\gendarme.txt) -replace '\(\D?(\d+)\)', ' ($1,1)' | set-content $SolutionDir\bin\gendarme.txt
exit 0
