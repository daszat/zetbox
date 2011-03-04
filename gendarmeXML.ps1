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

"." > bin\gendarme.txt
cd $SolutionDir

.\gendarme.bat -xml

convert-withxslt bin\gendarme.xml gendarme-visualstudio.xslt bin\gendarme.txt
(get-content bin\gendarme.txt) -replace '\(\D?(\d+)\)', ' ($1,1)' | set-content bin\gendarme.txt
exit 0
