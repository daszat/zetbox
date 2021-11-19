param($installPath, $toolsPath, $package, $project)
# $installPath is packages\zetboxBasic...\
# $toolsPath is $installPath\tools\

$pname=$project.ProjectName
"Hello from install-zetboxBasic.ps1 in $pname" | Out-Host

$solutionPath = [IO.Path]::GetDirectoryName($dte.Solution.Properties.Item("Path").Value)
$zetbox = [System.IO.Path]::Combine($solutionPath, ".zetbox\")

"installPath = $installPath" | Out-Host
"toolsPath = $toolsPath" | Out-Host
"solutionPath = $solutionPath" | Out-Host
"zetbox = $zetbox" | Out-Host

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
cp -Force "$toolsPath\*.targets" $zetbox
cp -Force "$toolsPath\env.xml" $zetbox
cp -Force "$toolsPath\PrepareEnv.*" $zetbox
robocopy /e "$toolsPath\runtimes" "$zetbox\runtimes" | Out-Null
cp -Force "$toolsPath\..\zetbox\Server\Npgsql.*" $zetbox
cp -Force "$toolsPath\..\zetbox\Server\Mono.Security.*" $zetbox
cp -Force "$toolsPath\..\zetbox\System.Security.*" $zetbox

robocopy /e "$toolsPath\..\zetbox" "$zetbox\bin" | Out-Null
robocopy /e "$toolsPath\runtimes" "$zetbox\bin\runtimes" | Out-Null

robocopy /e "$toolsPath\..\zetbox\Common" "$zetbox\bin\HttpService" | Out-Null
robocopy /e "$toolsPath\..\zetbox\Common" "$zetbox\bin\HttpService\Common" | Out-Null
robocopy /e "$toolsPath\..\zetbox\Server" "$zetbox\bin\HttpService\Server" | Out-Null
robocopy /e "$toolsPath\..\zetbox\Client" "$zetbox\bin\HttpService\Client" | Out-Null
cp -Force "$toolsPath\..\zetbox\Microsoft.*" "$zetbox\bin\HttpService"
cp -Force "$toolsPath\..\zetbox\System.*" "$zetbox\bin\HttpService"
robocopy /e "$toolsPath\runtimes" "$zetbox\bin\HttpService\runtimes" | Out-Null
