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
gendarme -config $SolutionDir\gendarmerules.xml --xml $SolutionDir\bin\gendarme.xml --ignore $SolutionDir\gendarmeignore.txt --severity high+ $SolutionDir\bin\Debug\bin\Common\Kistl.API.dll $SolutionDir\bin\Debug\bin\Common\Kistl.App.Projekte.Common.dll $SolutionDir\bin\Debug\bin\Common\TempAppHelpers.dll $SolutionDir\bin\Debug\bin\Client\Kistl.API.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.App.Projekte.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.ASPNET.Toolkit.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.Forms.exe $SolutionDir\bin\Debug\bin\Client\Kistl.Client.WPF.exe $SolutionDir\bin\Debug\bin\Client\Kistl.DalProvider.ClientObjects.dll $SolutionDir\bin\Debug\bin\Server\Kistl.API.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.App.Projekte.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.DalProvider.EF.dll $SolutionDir\bin\Debug\bin\Common\Kistl.DalProvider.Memory.dll $SolutionDir\bin\Debug\bin\Server\Kistl.DalProvider.Memory.Generator.dll $SolutionDir\bin\Debug\bin\Server\Kistl.DalProvider.NHibernate.dll $SolutionDir\bin\Debug\bin\Server\Kistl.DalProvider.NHibernate.Generator.dll $SolutionDir\bin\Debug\bin\Server\Kistl.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.Server.Service.exe $SolutionDir\bin\Debug\bin\Server\Kistl.Server.Service32.exe $SolutionDir\bin\Debug\bin\Server\Ini50.Migrate.exe $SolutionDir\bin\Debug\bin\Common\Ini50.App.Common.dll $SolutionDir\bin\Debug\bin\Client\Ini50.App.Client.dll

convert-withxslt $SolutionDir\bin\gendarme.xml $SolutionDir\gendarme-visualstudio.xslt $SolutionDir\bin\gendarme.txt
(get-content $SolutionDir\bin\gendarme.txt) -replace '\(\D?(\d+)\)', ' ($1,1)' | set-content $SolutionDir\bin\gendarme.txt
exit 0
