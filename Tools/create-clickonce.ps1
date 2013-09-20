<# 
.SYNOPSIS 
    create click-once 
.DESCRIPTION 
    creates a click once application for the zetbox boot strapper
.NOTES 
    Author     : dasz.at OG, office@dasz.at
	License    : GNU General Public License (GPL)
.LINK 
    http://dasz.at
#> 

Param(
	[string]$Name = "zetbox", 
	[string]$DBUrl = "http://demo.dasz.at/zetbox", 
	[string]$Version = "1.0.0.0",
	[string]$Publisher = "dasz.at OG",
	[string]$ProviderUrl = "http://demo.dasz.at/zetbox/client",
	[string]$SourcePath = "..\bin\Debug\Bootstrapper",
	[string]$CertFile = (get-location).Path + "\tempcert"
)
$ErrorActionPreference = "Stop"

# http://rkeithhill.wordpress.com/2009/08/03/effective-powershell-item-16-dealing-with-errors/
function CheckLastExitCode {
    param ([int[]]$SuccessCodes = @(0), [scriptblock]$CleanupScript=$null)

    if ($SuccessCodes -notcontains $LastExitCode) {
        if ($CleanupScript) {
            "Executing cleanup script: $CleanupScript"
            &$CleanupScript
        }
        $msg = @"
EXE RETURNED EXIT CODE $LastExitCode
CALLSTACK:$(Get-PSCallStack | Out-String)
"@
        throw $msg
    }
}


$assembly = "Zetbox.Client.Bootstrapper"
$outPath = (get-location).Path + "\publish\"

$magePath = "${env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\mage.exe"

if (!(Test-Path -path "$outPath\$Version")) {
	mkdir "$outPath\$Version" | out-null
}	

cp -Recurse "$SourcePath\*.*" "$outPath\$Version" -exclude "*.pdb" -force

$configFile = "$outPath\$Version\$assembly.exe.config"
[xml]$cfgXML = Get-Content $configFile
$addressTag = ($cfgXML.Configuration.userSettings.{Zetbox.Client.Bootstrapper.Properties.Settings}.setting | ? { $_.name -eq "Address" })
$addressTag.value = $DBUrl
$cfgXML.Save($configFile)

if (!(Test-Path "$CertFile.pfx")) {
	"Creating temp cert" | out-host
	makecert -sv "$CertFile.pvk" -n "CN=Temp-Cert" "$CertFile.cer"
	pvk2pfx -pvk "$CertFile.pvk" -spc "$CertFile.cer" -pfx "$CertFile.pfx"
}

Push-Location
try {
	cd "$outPath\$Version\"
	&($magePath) -New Application -Processor msil -ToFile "$assembly.exe.manifest" -name "$Name" -Version "$Version" -FromDirectory . 
	CheckLastExitCode
	&($magePath) -Sign "$assembly.exe.manifest" -CertFile "$CertFile.pfx"
	CheckLastExitCode

	cd .. 
	&($magePath) -New Deployment -Processor msil -Version "$Version" -Install true -Publisher "$Publisher" -ProviderUrl "$ProviderUrl" -AppManifest "$Version\$assembly.exe.manifest" -ToFile "$Name.application"
	CheckLastExitCode
	&($magePath) -Sign "$Name.application" -CertFile "$CertFile.pfx"
	CheckLastExitCode
} finally {
	Pop-Location
}