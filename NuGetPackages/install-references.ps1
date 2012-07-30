param($installPath, $toolsPath, $package, $project)
# $installPath is the path to the folder where the package is installed
# $toolsPath is the path to the tools directory in the folder where the package is installed
# $package is a reference to the package object.
# $project is a reference to the EnvDTE project object and represents the project the package is installed into.

$ourReferences = (
   "Zetbox.API",
   "Zetbox.API.Client",
   "Zetbox.API.Common",
   "Zetbox.API.Server",
   "Zetbox.Client",
   "Zetbox.Client.WPF",
   "Zetbox.Client.WPF.Toolkit"
)

foreach ($reference in $project.Object.References)
{
    if ($ourReferences -contains $reference.Name)
    {
        $reference.CopyLocal = $false;
    }
}
