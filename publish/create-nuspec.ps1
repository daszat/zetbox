"Creating *.nuspec from template with a version from GitVersion" | out-host

# don't forget to 
# msbuild ..\Zetbox.Complete.sln /p:Configuration=Debug
# if creating the package locally

rm *.nupkg
cp *.* ..\bin\Debug

$str = (..\packages\GitVersion.CommandLine.3.0.0-beta0002\Tools\GitVersion.exe) | out-string
$json = ConvertFrom-Json $str

$version = $json.NuGetVersionV2
"  Version = $version" | out-host

"" | out-host
"Converting files" | out-host
ls *.nuspec.template | % {
	$file = $_.Name.Split('.')[0]
	$file | out-host
	Get-Content "$file.nuspec.template" | Foreach-object { 
		$_ -replace '##version##', $version 
	} | Set-Content -encoding "UTF8" "..\bin\Debug\$file.nuspec"
}

pushd
cd ..\bin\Debug

"" | out-host
"packing files" | out-host
ls *.nuspec | % {
	$file = $_.Name
	$file | out-host
	..\..\.nuget\nuget.exe pack $file
}

popd

mv ..\bin\Debug\*.nupkg .