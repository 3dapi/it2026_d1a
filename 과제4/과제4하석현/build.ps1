$ErrorActionPreference = 'Stop'
$projectRoot = $PSScriptRoot
$compilerPath = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
if (-not (Test-Path -LiteralPath $compilerPath)) { $compilerPath = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe' }
$sourceFiles = @(Get-ChildItem -LiteralPath (Join-Path $projectRoot 'Source') -Filter '*.cs' | ForEach-Object FullName)
& $compilerPath /nologo /target:winexe /platform:anycpu /optimize+ /codepage:65001 "/out:$projectRoot\Scruple.exe" /reference:System.dll /reference:System.Core.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll $sourceFiles
if ($LASTEXITCODE -ne 0) { throw 'C# build failed.' }
Write-Output 'Build succeeded: Scruple.exe'
