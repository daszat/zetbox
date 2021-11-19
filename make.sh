#!/bin/bash

dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/ Zetbox.Core.sln
dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/HttpService Zetbox.Server.HttpService/Zetbox.Server.HttpService.csproj
cp -r ./bin/Release/Common ./bin/Release/HttpService
cp -r ./bin/Release/Server ./bin/Release/HttpService
cp -r ./Configs ./bin/Release

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
