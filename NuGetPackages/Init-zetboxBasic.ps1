param($installPath, $toolsPath, $package, $project)

$zetbox = [System.IO.Path]::Combine($installPath, "..\..\.zetbox\")

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp "$toolsPath\*.targets" $zetbox
cp "$toolsPath\env.xml" $zetbox
cp "$toolsPath\PrepareEnv.exe" $zetbox