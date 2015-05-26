param($installPath, $toolsPath, $package, $project)
# $installPath is packages\zetboxBasic...\
# $toolsPath is $installPath\tools\

$zetbox = [System.IO.Path]::Combine($installPath, "..\..\.zetbox\")

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp "$toolsPath\*.targets" $zetbox
cp "$toolsPath\env.xml" $zetbox
cp "$toolsPath\PrepareEnv.exe" $zetbox
# required to work around https://bugzilla.xamarin.com/show_bug.cgi?id=15347
# which will be fixed in mono 3.2.something
cp "$toolsPath\..\zetbox\Common\Mono.Cecil.*" $zetbox