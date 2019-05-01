param($installPath, $toolsPath, $package, $project)

$analyzersPath = join-path $toolsPath "analyzers"

# Uninstall the language agnostic analyzers.
foreach ($analyzerFilePath in Get-ChildItem $analyzersPath -Filter *.dll)
{
    if($project.Object.AnalyzerReferences)
    {
        $project.Object.AnalyzerReferences.Remove($analyzerFilePath.FullName)
    }
}
