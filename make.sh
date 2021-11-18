#!/bin/bash

dotnet build --disable-parallel --ignore-failed-sources --configuration Release Zetbox.Core.sln
dotnet publish --disable-parallel --ignore-failed-sources --configuration Release  Zetbox.Server.HttpService/Zetbox.Server.HttpService.csproj --output ./bin/Release/HttpService

# publish
rm publish/*.nupkg || true
rm publish/*.nuspec || true
cp publish/* ./bin/Release


version="$(gitversion -nofetch -showvariable NuGetVersionV2)"
echo "Version = $version"

echo ""
echo "Converting files"

for f in publish/*.nuspec.template; do

	baseName=`echo $f | cut -d "." -f 1`
	newExtension=".new"

	cp -f $f $baseName.nuspec
	sed -i "s/##version##/$version/g" $baseName.nuspec

done

cp ./publish/*.nuspec ./bin/Release

echo "packing files"
for f in ./bin/Release/*.nuspec; do
	nuget pack -NoPackageAnalysis $f -OutputDirectory ./publish/
done
