param($installPath, $toolsPath, $package, $project)
# $installPath is packages\zetboxBasic...\
# $toolsPath is $installPath\tools\

$zetbox = [System.IO.Path]::Combine($installPath, "..\..\.zetbox\")

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp "$toolsPath\*.targets" $zetbox
cp "$toolsPath\env.xml" $zetbox
cp "$toolsPath\PrepareEnv.exe" $zetbox