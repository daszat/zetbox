pipeline {
    agent {
	    // Not yet, missing windows 10/11 machine 
        // docker { 
		// 	image 'mcr.microsoft.com/dotnet/sdk:5.0' 
		// }
		label 'Windows' 
    }
    environment {
        HOME = '/tmp'
		DASZ_NUGET_KEY = credentials('dasz-nuget-key')
    } 
    stages {
        stage('Build') {
            steps {
				sh  '''
				set +x
				dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/ Zetbox.Core.sln
				dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/HttpService Zetbox.Server.HttpService/Zetbox.Server.HttpService.csproj
				cp -r ./bin/Release/Common ./bin/Release/HttpService
				cp -r ./bin/Release/Server ./bin/Release/HttpService
				cp -r ./Configs ./bin/Release
				'''

				archiveArtifacts artifacts: 'bin/Release/**, publish/**', fingerprint: true
            }        
        }
		stage('Build Nuget Packages') {
			steps {
				sh  '''
				set +x
				version="$(gitversion -nofetch -showvariable NuGetVersionV2)"
				echo "Version = $version"

				echo "@nuget install ZetboxBasic -Version $version -OutputDirectory \"%~dp0\bin\"" > publish/DownloadZetbox.cmd

				# publish
				rm publish/*.nupkg || true
				rm publish/*.nuspec || true
				cp publish/* ./bin/Release

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
				'''
				
				archiveArtifacts artifacts: 'publish/*.nupkg', fingerprint: true
			}        
		}	
		stage('Publish Nuget Packages') {
			when {
				branch 'master'
			}
			steps {
				sh  '''
				set +x
				dotnet nuget push publish/*.nupkg --skip-duplicate -k $DASZ_NUGET_KEY -s https://office.dasz.at/ngf/api/v2/package
				'''
			}        
		}			
        stage('Description') {
            steps {
                sh 'GitVersion /nofetch'
                sh "GitVersion /nofetch | sed -n 's/\"FullSemVer\": \"\\(.*\\)\",/\\1/p' > VERSION.txt"
                script {
                    def ver = readFile 'VERSION.txt'
                    currentBuild.description = ver
                }
            }        
        }       
    }
}