param($installPath, $toolsPath, $package, $project)
# $installPath is packages\zetboxBasic...\
# $toolsPath is $installPath\tools\

$pname=$project.ProjectName
"Hello from install-zetboxBasic.ps1 in $pname" | Out-Host

$solutionPath = [IO.Path]::GetDirectoryName($dte.Solution.Properties.Item("Path").Value)
$zetbox = [System.IO.Path]::Combine($solutionPath, ".zetbox\")

"solutionPath = $solutionPath" | Out-Host
"zetbox = $zetbox" | Out-Host

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp "$toolsPath\*.targets" $zetbox
cp "$toolsPath\env.xml" $zetbox
cp "$toolsPath\PrepareEnv.exe" $zetbox
# Required to copy Postgres databases
cp "$toolsPath\..\zetbox\Server\Npgsql.*" $zetbox
# Required to create click once packages
cp "$toolsPath\..\zetbox\Server\Mono.Security.*" $zetbox

cp -Recurse "$toolsPath\..\zetbox" "$zetbox\bin"
