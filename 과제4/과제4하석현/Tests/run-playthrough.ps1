$ErrorActionPreference = 'Stop'
$projectRoot = Split-Path -Parent $PSScriptRoot
$compilerPath = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
$testBinary = Join-Path $PSScriptRoot 'Playthrough.exe'
$sourceFiles = @('World.cs','Monster.cs','GameSession.cs') | ForEach-Object { Join-Path $projectRoot ('Source\' + $_) }
$sourceFiles += Join-Path $PSScriptRoot 'Playthrough.cs'
& $compilerPath /nologo /codepage:65001 "/out:$testBinary" /reference:System.Drawing.dll /reference:System.Core.dll $sourceFiles
if ($LASTEXITCODE -ne 0) { throw 'Test build failed.' }
& $testBinary | Tee-Object -FilePath (Join-Path $PSScriptRoot 'playthrough-results.txt')
if ($LASTEXITCODE -ne 0) { throw 'Playthrough did not reach the ending.' }
