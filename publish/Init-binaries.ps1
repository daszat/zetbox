param($installPath, $toolsPath, $package, $project)
# $installPath is packages\zetboxBasic...\
# $toolsPath is $installPath\tools\

$pname=$project.ProjectName
"Hello from install-binaries.ps1 in $pname" | Out-Host

$solutionPath = [IO.Path]::GetDirectoryName($dte.Solution.Properties.Item("Path").Value)
$zetbox = [System.IO.Path]::Combine($solutionPath, ".zetbox\")

"installPath = $installPath" | Out-Host
"toolsPath = $toolsPath" | Out-Host
"solutionPath = $solutionPath" | Out-Host
"zetbox = $zetbox" | Out-Host

if (!(Test-Path -path $zetbox)) { mkdir $zetbox }
robocopy /e "$toolsPath\..\zetbox" "$zetbox\bin" | Out-Null

