param($installPath, $toolsPath, $package, $project)
# $installPath is the path to the folder where the package is installed
# $toolsPath is the path to the tools directory in the folder where the package is installed
# $package is a reference to the package object.
# $project is a reference to the EnvDTE project object and represents the project the package is installed into.

$pname=$project.ProjectName
"Hello from install-clientblazor.ps1 in $pname" | Out-Host

# add the clientaspnet.targets relative to project dir
$relativeSolutionPath = [NuGet.PathUtility]::GetRelativePath($project.FullName, $dte.Solution.Properties.Item("Path").Value)
$relativeSolutionPath = [IO.Path]::GetDirectoryName($relativeSolutionPath)
$relativeSolutionPath = [NuGet.PathUtility]::EnsureTrailingSlash($relativeSolutionPath)

$importPath = ($relativeSolutionPath + ".zetbox\clientblazor.targets")
if (! (((Get-MSBuildProject).Imports | %{ $_.ImportingElement.Project.contains(".zetbox\clientaspnet.targets")}) -contains $true)) {
	Add-Import $importPath $project.ProjectName
}
