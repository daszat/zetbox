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
				dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/ Zetbox.Core.sln
				dotnet publish --disable-parallel --ignore-failed-sources --configuration Release --output ./bin/Release/HttpService Zetbox.Server.HttpService/Zetbox.Server.HttpService.csproj
				cp -r ./bin/Release/Common ./bin/Release/HttpService
				cp -r ./bin/Release/Server ./bin/Release/HttpService
				cp -r ./Configs ./bin/Release
				'''

				archiveArtifacts artifacts: './bin', fingerprint: true
            }        
        }
        stage('Artifacts') {
            failFast true
            parallel {
				stage('Build Nuget Packages') {
					when {
						branch 'master'
					}
					steps {
						
						archiveArtifacts artifacts: 'JET.Angebotsportal.Setup/bin/Release/*.msi', fingerprint: true
					}        
				}			
            }
        }
        stage('Description') {
            agent { label 'Windows' }
            steps {
                sh 'GitVersion /nofetch'
                sh "GitVersion /nofetch | sed -n 's/\"FullSemVer\":\"\\(.*\\)\",/\\1/p' > VERSION.txt"
                script {
                    def ver = readFile 'VERSION.txt'
                    currentBuild.description = ver
                }
            }        
        }       
    }
}