@echo off

set name=zetbox - dev
set assembly=Zetbox.Client.Bootstrapper
set version=1.0.0.0
set publisher=dasz.at OG
set providerurl=http://dasz.at/foobar

set out_path=.\publish\
set src_path=..\bin\Debug\Bootstrapper

set certfile=tempcert
set mage_path="%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\mage.exe"

xcopy /y /s "%src_path%\*.exe" "%out_path%\%version%\"
xcopy /y /s "%src_path%\*.exe.config" "%out_path%\%version%\"
xcopy /y /s "%src_path%\*.dll" "%out_path%\%version%\"

IF NOT EXIST "%~dp0\%certfile%.pfx" ( 
	echo Creating tempcert
	makecert -sv "%~dp0\%certfile%.pvk" -n "CN=Temp-Cert" "%~dp0\%certfile%.cer"
	pvk2pfx -pvk "%~dp0\%certfile%.pvk" -spc "%~dp0\%certfile%.cer" -pfx "%~dp0\%certfile%.pfx"
)
cd "%out_path%\%version%\"
%mage_path% -New Application -Processor msil -ToFile "%assembly%.exe.manifest" -name "%name%" -Version "%version%" -FromDirectory . 
%mage_path% -Sign "%assembly%.exe.manifest" -CertFile "%~dp0\%certfile%.pfx"

cd ..
%mage_path% -New Deployment -Processor msil -Version "%version%" -Install true -Publisher "%publisher%" -ProviderUrl "%providerurl%" -AppManifest "%version%\\%assembly%.exe.manifest" -ToFile "%name%.application"
%mage_path% -Sign "%name%.application" -CertFile "%~dp0\%certfile%.pfx"
