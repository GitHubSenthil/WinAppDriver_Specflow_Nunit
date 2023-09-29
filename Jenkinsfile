node("TeamsAutomationNode3") {
    def workspace = WORKSPACE
    Branch = Branch.replaceAll('origin/','')
    stage('Build') {
        echo "${Branch}"
        //def msbuild =  "\"%ProgramFiles%\\Microsoft Visual Studio\"\\2022\\Professional\\MSBuild\\Current\\Bin\\amd64\\MSBuild.exe"
        def msbuild = "dotnet build"
        git branch: "${Branch}", credentialsId: '69ed4382-ca01-4abe-93d2-870f2881496d', url: 'https://gitlab.nomura.com/eis-microsoft_365-test_automation/eis-m365-teams-functional-test.git'
        def projectSln = "eis-m365-teams-functional-test.sln";
        def exitStatus = bat(returnStatus: true, script: "${msbuild} ${projectSln} /p:Configuration=Debug")
        if (exitStatus != 0){
          currentBuild.result = 'FAILURE'
          error 'build failed'
      }
    }
    stage('Test') {
        def nunitConsolePath = "C:\\Temp\\NUnit3Console\\bin\\net35\\nunit3-console.exe"
        def dllFile = "\\bin\\Debug\\net6.0\\TeamsWindowsApp.dll"
        def executionStatus = bat(returnStatus: true, script: "${nunitConsolePath} ${workspace}${dllFile} --where \"cat==MIB\" --result=junit-results.xml;transform=nunit3-junit.xslt")
        if (executionStatus != 0){
            currentBuild.result = 'FAILURE'
        }
    }
    
    stage('ReportPreparation') {
        def dllFile = "${workspace}\\bin\\Debug\\net6.0\\TeamsWindowsApp.dll"
        def executionJson = "${workspace}\\bin\\Debug\\net6.0\\TestExecution.json"
        
        def executionStatus = bat(returnStatus: true, script: "livingdoc test-assembly --output-type JSON ${dllFile} -o feature.json")
        if (executionStatus != 0){
            currentBuild.result = 'FAILURE'
        }
        
        def exePwr = powershell returnStatus: true, script: "${workspace}\\Resources\\LivingDoc_Scenarios.ps1 ${executionJson} feature.json pruned.json"
        if (exePwr != 0){
            currentBuild.result = 'FAILURE'
        }
        
        def finalReportStatus = bat(returnStatus: true, script: "livingdoc feature-data  pruned.json --output-type HTML -t ${executionJson} -o TestHtmlReport.html")
        if (finalReportStatus != 0){
            currentBuild.result = 'FAILURE'
        }
        
    }
    
    stage('PublishTestReport') {
        //nunit healthScaleFactor: 1, testResultsPattern: 'TestResult.xml'
        junit skipPublishingChecks: true, skipMarkingBuildUnstable: true, testResults: 'junit-results.xml'
        publishHTML([allowMissing: false, alwaysLinkToLastBuild: false, keepAll: true, reportDir: '', reportFiles: 'TestHtmlReport.html', reportName: 'HTML Report', reportTitles: 'MIB_Regression'])
        
        //Email Report
        emailext subject: 'MIB Regression QA $DEFAULT_SUBJECT',
                    body: 'MIB Regression Build url: $BUILD_URL',
                    recipientProviders: [
                        [$class: 'CulpritsRecipientProvider'],
                        [$class: 'DevelopersRecipientProvider'],
                        [$class: 'RequesterRecipientProvider']
                    ],
                    attachmentsPattern: '**/*.html',
                    to: 'senthilkumar.sivanandam@nomura.com',
                    mimeType: 'text/html'
    }
    
    stage('Rerun') {
        def sRerunFile = "${workspace}\\TestData\\RerunLog.txt"
        def sFile = fileExists sRerunFile
        def value
        if (sFile) 
        {
            echo 'Rerun in Progress'
            def file = readFile sRerunFile
            def lines = file.readLines()
            lines.each()
            {
                value = value + "cat==" + it + "||"
            }
            value = value.replaceAll("null", "")
            if (value.length() > 2)
            {
                value = value.substring(0,value.length()-2)
                echo value
                build job: 'Rerun_FailedScenarios', parameters: [text(name: 'RerunTags', value: value), text(name: 'BranchName', value: Branch)]
            }
            else{
                echo "To rerun, please follow the format of tag 'Rerun<TypeofTestwithNumbers>'. It should be unique for each scenario. RerunReg001"
            }
            
        }
        else
        {
            echo 'No failures to rerun'
        }
    }
}
