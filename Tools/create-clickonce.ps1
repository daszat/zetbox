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
	[string]$ProviderUrl = "http://dasz.at/foobar",
	[string]$SourcePath = "..\bin\Debug\Bootstrapper",
	[string]$CertFile = (get-location).Path + "\tempcert"
)

$assembly = "Zetbox.Client.Bootstrapper"
$out_path = ".\publish\"

$mage_path="${env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\mage.exe"

if (!(Test-Path -path "$out_path\$Version")) {
	mkdir "$out_path\$Version" | out-null
}	

cp -Recurse "$SourcePath\*.*" "$out_path\$Version" -exclude "*.pdb" -force

if (!(Test-Path "$CertFile.pfx")) {
	"Creating tempcert" | out-host
	makecert -sv "$CertFile.pvk" -n "CN=Temp-Cert" "$CertFile.cer"
	pvk2pfx -pvk "$CertFile.pvk" -spc "$CertFile.cer" -pfx "$CertFile.pfx"
}

Push-Location
cd "$out_path\$Version\"
&($mage_path) -New Application -Processor msil -ToFile "$assembly.exe.manifest" -name "$Name" -Version "$Version" -FromDirectory . 
&($mage_path) -Sign "$assembly.exe.manifest" -CertFile "$CertFile.pfx"

cd ..
&($mage_path) -New Deployment -Processor msil -Version "$Version" -Install true -Publisher "$Publisher" -ProviderUrl "$ProviderUrl" -AppManifest "$Version\$assembly.exe.manifest" -ToFile "$Name.application"
&($mage_path) -Sign "$Name.application" -CertFile "$CertFile.pfx"

Pop-Location