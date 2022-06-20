$ErrorActionPreference = "Stop"
echo "Reading DotNetToolkit project version..."
$DotNetToolkitProjectXml = [Xml] (Get-Content ".\src\DotNetToolkit\DotNetToolkit.csproj")
$DotNetToolkitProjectVersion = [String] $DotNetToolkitProjectXml.Project.PropertyGroup.Version
$DotNetToolkitProjectVersion = $DotNetToolkitProjectVersion.Trim()
echo "DotNetToolkit project version is $DotNetToolkitProjectVersion"
echo "Reading DotNetToolkit published package version..."
$DotNetToolkitPublishedPackageVersion = (nuget list OSSILab.NetToolkit).Trim()
echo "DotNetToolkit published package version is $DotNetToolkitPublishedPackageVersion"
if($DotNetToolkitPublishedPackageVersion -match "^OSSILab.NetToolkit \d+.*")
{
    if($DotNetToolkitPublishedPackageVersion -notmatch "OSSILab.NetToolkit $DotNetToolkitProjectVersion")
    {
		echo "ReleaseDotNetToolkit build tag is set"
        Write-Host "##vso[build.addbuildtag]ReleaseDotNetToolkit"
    }
}
else
{
    Throw "Unable to retrieve latest NetToolkit version from nuget"
}