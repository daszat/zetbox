@echo off
"%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe" /t:BeforeBuild .\$safesolutionname$.Common\$safesolutionname$.Common.csproj /v:minimal
echo Done