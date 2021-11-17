#!/bin/bash

dotnet build --disable-parallel --ignore-failed-sources --configuration Release Zetbox.Core.sln

# publish
rm publish/*.nupkg || true
rm publish/*.nuspec || true
cp publish/* ./bin/Release


version="3.0.0-alpha"
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
	nuget pack $f -OutputDirectory ./publish/
done
