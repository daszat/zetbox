param($SolutionDir)

function Convert-WithXslt($originalXmlFilePath, $xslFilePath, $outputFilePath) 
{
   ## Simplistic error handling
   $xslFilePath = resolve-path $xslFilePath
   if( -not (test-path $xslFilePath) ) { throw "Can't find the XSL file" } 
   $originalXmlFilePath = resolve-path $originalXmlFilePath
   if( -not (test-path $originalXmlFilePath) ) { throw "Can't find the XML file" } 
   $outputFilePath = resolve-path $outputFilePath
   if( -not (test-path (split-path $originalXmlFilePath)) ) { throw "Can't find the output folder" } 

   ## Get an XSL Transform object (try for the new .Net 3.5 version first)
   $EAP = $ErrorActionPreference
   $ErrorActionPreference = "SilentlyContinue"
   $script:xslt = new-object system.xml.xsl.xslcompiledtransform
   trap [System.Management.Automation.PSArgumentException] 
   {  # no 3.5, use the slower 2.0 one
      $ErrorActionPreference = $EAP
      $script:xslt = new-object system.xml.xsl.xsltransform
   }
   $ErrorActionPreference = $EAP
   
   ## load xslt file
   $xslt.load( $xslFilePath )
     
   ## transform 
   $xslt.Transform( $originalXmlFilePath, $outputFilePath )
}

if(-not $SolutionDir) 
{
	$SolutionDir = ".";
}

"." > $SolutionDir\bin\gendarme.txt
gendarme -config $SolutionDir\gendarmerules.xml --xml $SolutionDir\bin\gendarme.xml --ignore $SolutionDir\gendarmeignore.txt --severity high+ $SolutionDir\bin\Debug\bin\Client\Kistl.API.dll $SolutionDir\bin\Debug\bin\Client\Kistl.App.Projekte.Common.dll $SolutionDir\bin\Debug\bin\Client\Kistl.DalProvider.Frozen.dll $SolutionDir\bin\Debug\bin\Client\TempAppHelpers.dll $SolutionDir\bin\Debug\bin\Client\Kistl.API.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.App.Projekte.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.ASPNET.Toolkit.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.dll $SolutionDir\bin\Debug\bin\Client\Kistl.Client.Forms.exe $SolutionDir\bin\Debug\bin\Client\Kistl.Client.WPF.exe $SolutionDir\bin\Debug\bin\Client\Kistl.DalProvider.ClientObjects.dll $SolutionDir\bin\Debug\bin\Server\Kistl.API.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.App.Projekte.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.DalProvider.EF.dll $SolutionDir\bin\Debug\bin\Server\Kistl.Server.dll $SolutionDir\bin\Debug\bin\Server\Kistl.Server.Service.exe
convert-withxslt $SolutionDir\bin\gendarme.xml $SolutionDir\gendarme-visualstudio.xslt $SolutionDir\bin\gendarme.txt
(get-content $SolutionDir\bin\gendarme.txt) -replace "\(.(.+)\)", ' ($1,1)' | set-content $SolutionDir\bin\gendarme.txt
exit 0