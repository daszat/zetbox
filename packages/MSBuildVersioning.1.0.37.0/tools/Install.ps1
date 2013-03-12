param($installPath, $toolsPath, $package, $project)

function Get-RelativePath {
<#
.SYNOPSIS
   Get a path to a file (or folder) relative to another folder
.DESCRIPTION
   Converts the FilePath to a relative path rooted in the specified Folder
.PARAMETER Folder
   The folder to build a relative path from
.PARAMETER FilePath
   The File (or folder) to build a relative path TO
.PARAMETER Resolve
   If true, the file and folder paths must exist
.Example
   Get-RelativePath ~\Documents\WindowsPowerShell\Logs\ ~\Documents\WindowsPowershell\Modules\Logger\log4net.xslt
   
   ..\Modules\Logger\log4net.xslt
   
   Returns a path to log4net.xslt relative to the Logs folder
#>
[CmdletBinding()]
param(
   [Parameter(Mandatory=$true, Position=0)]
   [string]$Folder
, 
   [Parameter(Mandatory=$true, Position=1, ValueFromPipelineByPropertyName=$true)]
   [Alias("FullName")]
   [string]$FilePath
,
   [switch]$Resolve
)
process {
   Write-Verbose "Resolving paths relative to '$Folder'"
   $from = $Folder = split-path $Folder -NoQualifier -Resolve:$Resolve
   $to = $filePath = split-path $filePath -NoQualifier -Resolve:$Resolve

   while($from -and $to -and ($from -ne $to)) {
      if($from.Length -gt $to.Length) {
         $from = split-path $from
      } else {
         $to = split-path $to
      }
   }

   $filepath = $filepath -replace "^"+[regex]::Escape($to)+"\\"
   $from = $Folder
   while($from -and $to -and $from -gt $to ) {
      $from = split-path $from
      $filepath = join-path ".." $filepath
   }
   Write-Output $filepath
}
}

Write-Host "Installing MSBuildVersioning"

$origProj = Get-Project
$buildProject = $origProj | Get-MSBuildProject

$buildProject.Items | Where-Object { $_.Key -eq 'Reference' } | Select-Object -ExpandProperty Value |  Select-Object -ExpandProperty EvaluatedInclude | Write-Host
# remove reference
$ref = $buildProject.Items | Where-Object { $_.Key -eq 'Reference' } | Select-Object -ExpandProperty Value | Where-Object { $_.EvaluatedInclude.StartsWith('MSBuildVersioning') }
if ($ref) {
    $buildProject.RemoveItem($ref)

    # figure out our installation path
    $csprojpath = [System.IO.PATH]::GetFullPath([System.IO.PATH]::GetDirectoryName($origProj.FullName))
    Write-Host "Using csprojpath=$csprojpath"

    $hintpath = $ref | Select-Object -ExpandProperty Metadata | Where-Object {$_.Name -eq 'HintPath' } | Select-Object -ExpandProperty EvaluatedValue
    Write-Host "Detected hintpath=$hintpath"
    if (-not [System.IO.PATH]::IsPathRooted($hintpath)) {
        $hintpath = [System.IO.PATH]::Combine($csprojpath, $hintpath)
    }
    $hintpath = [System.IO.PATH]::GetDirectoryName([System.IO.PATH]::GetFullPath($hintpath))
    Write-Host "Using hintpath=$hintpath"

    $location = Get-RelativePath $csprojpath $hintpath
    Write-Host "Using location=$location"

    # add MSBuild targets
    $buildProject.Xml.AddImport("$location\MsBuildVersioning.targets")

    # avoid compiling the template
    $base = $buildProject.Items | Where-Object { $_.Key -eq 'Compile' } | Select-Object -ExpandProperty Value | Where-Object { $_.EvaluatedInclude -eq 'Properties\VersionInfo.cs' } 
    $base.ItemType = "None"

    # do compile the result
    $designer = $buildProject.Xml.AddItem("Compile", 'Properties\VersionInfo.Designer.cs')
    $designer.AddMetadata("DependentUpon", "VersionInfo.cs")

    # figure out which scm we're using and add a proper task
    $path = Get-Location
    $path = [System.IO.PATH]::GetFullPath($path)
    while ($path -ne [System.IO.PATH]::GetPathRoot($path)) {
        Write-Host "Testing $path"
        $type = @((Get-ChildItem $path -filter ".git" -force), (Get-ChildItem $path -filter ".hg" -force), (Get-ChildItem $path -filter ".svn" -force))
        if ($type.Count -gt 0) {
            $scm = $type[0].Name
            Write-Host "Adding BeforeBuild Target for $path: $scm"
            $target = $buildProject.Xml.AddTarget("BeforeBuild")
            switch($scm) {
                ".git" { $task = $target.AddTask("GitVersionFile") }
                ".hg" { $task = $target.AddTask("HgVersionFile") }
                ".svn" { $task = $target.AddTask("SvnVersionFile") }
            }
            $task.SetParameter("TemplateFile", 'Properties\VersionInfo.cs')
            $task.SetParameter("DestinationFile", 'Properties\VersionInfo.Designer.cs')
            break
        }
        $path = [System.IO.PATH]::GetFullPath([System.IO.PATH]::Combine($path, ".."))
    }
}

$origProj.Save()
