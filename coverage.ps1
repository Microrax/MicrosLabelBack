# Parameters - Solution
$SolutionFileName = "LogisAssistedPicking.sln"
$CoverageCoverletDir = ".coverage"
$CoverageReportDir = ".coverage-report"	

# Parameters - Coverlet
$CoverletOutputFormat = "cobertura"
$CoverletOutputExtension = ".xml"
$CoverageFileName = "coverage.$CoverletOutputFormat$CoverletOutputExtension"
	
# Parameters - Report Generator
$ReportTypes = "HTML;cobertura;"
$HtmlReportIndexFileName = "index.html"	

# Calculated Parameters - Coverlet - CHANGE WITH EXTREME CAUTION
$CoverageRunIdentifier = [GUID]::NewGuid().ToString()
$CoverletOutput = [IO.Path]::Combine($CoverageCoverletDir, $CoverageRunIdentifier)	

# Install report generator
dotnet tool install --global dotnet-reportgenerator-globaltool

# Build
dotnet build

# Test
dotnet test --settings coverlet.runsettings --results-directory:"$CoverletOutput"

# Calculated Parameters - Report Generator - CHANGE WITH EXTREME CAUTION
$TargetDir = [IO.Path]::Combine($CoverageReportDir, $CoverageRunIdentifier)
$ReportHtmlFile = [IO.Path]::Combine($CoverageReportDir, $CoverageRunIdentifier, $HtmlReportIndexFileName)
$TestResultsDirs = Join-Path $CoverletOutput -ChildPath **\*
$CoverageFiles = (Split-Path $TestResultsDirs -Resolve | ForEach-Object -Process {[IO.Path]::Combine($_, $CoverageFileName)}) -join ";"

# Generate Report
reportgenerator.exe "-reports:$CoverageFiles" "-targetdir:$TargetDir" "-reporttypes:$ReportTypes"

# Open Report
Invoke-Item $ReportHtmlFile