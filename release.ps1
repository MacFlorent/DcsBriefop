<#
.SYNOPSIS
    Starts a DcsBriefop release: sets the version, commits, tags and pushes.
    The GitHub "Release" workflow then builds the zip and publishes the GitHub Release.

.PARAMETER Version
    Full version Y.M.D.N (e.g. 26.11.13.0). When omitted, uses today's date and the first
    subrelease N (from 0) that has no tag yet, locally or on origin.

.PARAMETER WhatIf
    Runs all checks and prints the chosen version without changing anything.

.EXAMPLE
    .\release.ps1
    .\release.ps1 26.11.13.1
    .\release.ps1 -WhatIf
#>
param(
    [Parameter(Position = 0)]
    [string]$Version,
    [switch]$WhatIf
)

$ErrorActionPreference = 'Stop'
Set-Location -Path $PSScriptRoot

$sProjectFile = Join-Path $PSScriptRoot 'DcsBriefop.csproj'
$sVersionPattern = '^\d{2}\.\d{1,2}\.\d{1,2}\.\d+$'

function Fail([string]$sMessage) {
    Write-Host "ERROR: $sMessage" -ForegroundColor Red
    exit 1
}

function Invoke-Git {
    $sOutput = & git @args 2>&1
    if ($LASTEXITCODE -ne 0) { throw "git $($args -join ' ') failed:`n$sOutput" }
    return $sOutput
}

function Test-TagExists([string]$sTag) {
    return [bool](& git tag --list $sTag)
}

# Checks on the version argument first: they need no git access.
if ($Version -and $Version -notmatch $sVersionPattern) {
    Fail "Invalid version '$Version'. Expected Y.M.D.N, e.g. 26.11.13.0 (no leading 'v')."
}

# Repository state
if (& git status --porcelain) {
    Fail 'Working tree has uncommitted changes. Commit or stash them first.'
}
$sBranch = Invoke-Git rev-parse --abbrev-ref HEAD
if ($sBranch -ne 'master') {
    Fail "Releases are made from 'master' (current branch: '$sBranch')."
}

# Local tags are refreshed from origin so a tag pushed elsewhere is not reused.
try { Invoke-Git fetch --tags --quiet origin | Out-Null }
catch { Fail "Could not fetch tags from origin.`n$_" }

if (-not $Version) {
    $sDate = Get-Date -Format 'yy.M.d'
    $iSubRelease = 0
    while (Test-TagExists "v$sDate.$iSubRelease") { $iSubRelease++ }
    $Version = "$sDate.$iSubRelease"
}
$sTag = "v$Version"
if (Test-TagExists $sTag) {
    Fail "Tag '$sTag' already exists."
}

Write-Host "Release version: $Version"
if ($WhatIf) {
    Write-Host 'WhatIf: nothing changed.'
    exit 0
}

# Set <Version> in the csproj, preserving encoding (BOM) and line endings.
[byte[]]$aBytes = [System.IO.File]::ReadAllBytes($sProjectFile)
$bHasBom = $aBytes.Length -ge 3 -and $aBytes[0] -eq 0xEF -and $aBytes[1] -eq 0xBB -and $aBytes[2] -eq 0xBF
$sContent = [System.IO.File]::ReadAllText($sProjectFile)
$oRegex = [regex]'<Version>[^<]*</Version>'
$iMatches = $oRegex.Matches($sContent).Count
if ($iMatches -ne 1) {
    Fail "Expected exactly one <Version> element in DcsBriefop.csproj, found $iMatches."
}
$sContent = $oRegex.Replace($sContent, "<Version>$Version</Version>")
[System.IO.File]::WriteAllText($sProjectFile, $sContent, [System.Text.UTF8Encoding]::new($bHasBom))

# Commit, tag, push. On failure, say what was already done locally.
$sDone = 'nothing'
try {
    Invoke-Git commit --quiet -m "release $Version" -- DcsBriefop.csproj | Out-Null
    $sDone = "commit 'release $Version'"
    Invoke-Git tag $sTag | Out-Null
    $sDone += " and tag '$sTag'"
    Invoke-Git push --quiet origin master | Out-Null
    $sDone += ', pushed master'
    Invoke-Git push --quiet origin $sTag | Out-Null
}
catch {
    Write-Host "ERROR: $_" -ForegroundColor Red
    Write-Host "Done locally before the failure: $sDone." -ForegroundColor Yellow
    Write-Host "Fix the problem, then finish with: git push origin master; git push origin $sTag" -ForegroundColor Yellow
    Write-Host "Or undo with: git tag -d $sTag; git reset --hard HEAD~1 (only if the commit was not pushed)" -ForegroundColor Yellow
    exit 1
}

Write-Host "Pushed $sTag. The GitHub 'Release' workflow now builds and publishes the release:"
Write-Host '  https://github.com/MacFlorent/DcsBriefop/actions'
