param($installPath, $toolsPath, $package, $project)

$zetbox = [System.IO.Path]::Combine($installPath, "..\..\.zetbox\")

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp "$toolsPath\*.targets" $zetbox
cp "$toolsPath\Env.xml" $zetbox
cp "$toolsPath\PrepareEnv.exe" $zetbox