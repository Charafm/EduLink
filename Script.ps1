<#
.SYNOPSIS
    Extracts detailed solution structure for diagramming.

.DESCRIPTION
    - Finds all .csproj projects under the specified root.
    - Classifies each project into Web, Application, Infrastructure (by name).
    - For Web:
        • Lists Controllers and their classes.
    - For Application:
        • Lists Commands and Queries and their classes.
    - For Infrastructure:
        • Lists DbContext classes, Migrations, and Service implementations.
    - Also lists immediate subdirs, general classes, JSON files, Dockerfiles.
    - Outputs to console and writes JSON to solution-structure.json.

.PARAMETER SolutionRoot
    Root folder of your solution. Defaults to current directory.

.EXAMPLE
    PS> .\Extract-SolutionStructure.ps1 -SolutionRoot "G:\repos\SchoolSaas"
#>

param(
    [string]$SolutionRoot = (Get-Location).Path
)

if (-not (Test-Path $SolutionRoot)) {
    Write-Error "Solution root '$SolutionRoot' not found."
    exit 1
}

# 1) discover all projects
$projects = Get-ChildItem -Path $SolutionRoot -Recurse -Filter *.csproj -File
if (-not $projects) {
    Write-Warning "No .csproj files under '$SolutionRoot'."
    exit 0
}

$result = foreach ($proj in $projects) {
    $projDir  = Split-Path $proj.FullName -Parent
    $projName = [IO.Path]::GetFileNameWithoutExtension($proj.Name)

    # classify layer by project name
    $isWeb           = $projName -match '\.Web\.'
    $isApp           = $projName -match '\.Application\.'
    $isInfra         = $projName -match '\.Infrastructure\.'

    # common collectors
    $dirs            = Get-ChildItem $projDir -Directory | Select-Object -Expand Name
    $jsonFiles       = Get-ChildItem $projDir -Recurse -Include *.json -File |
                       ForEach-Object { $_.FullName.Substring($projDir.Length + 1) }
    $dockerFiles     = Get-ChildItem $projDir -Recurse -File |
                       Where-Object { $_.Name -ieq 'Dockerfile' } |
                       ForEach-Object { $_.FullName.Substring($projDir.Length + 1) }

    # Web: Controllers
    if ($isWeb) {
        $ctrlPath    = Join-Path $projDir 'Controllers'
        $controllerClasses = @()
        if (Test-Path $ctrlPath) {
            $controllerClasses = Get-ChildItem $ctrlPath -Recurse -Filter *.cs -File |
                Select-String -Pattern 'class\s+(\w+Controller)' |
                ForEach-Object { $_.Matches[0].Groups[1].Value } |
                Sort-Object -Unique
        }
    } else { $controllerClasses = @() }

    # Application: Commands & Queries
    if ($isApp) {
        $commands = Get-ChildItem $projDir -Recurse -Include *Command.cs -File |
            Select-String -Pattern 'class\s+([A-Za-z_]\w*Command)' |
            ForEach-Object { $_.Matches[0].Groups[1].Value } |
            Sort-Object -Unique

        $queries  = Get-ChildItem $projDir -Recurse -Include *Query.cs -File |
            Select-String -Pattern 'class\s+([A-Za-z_]\w*Query)' |
            ForEach-Object { $_.Matches[0].Groups[1].Value } |
            Sort-Object -Unique
    } else {
        $commands = @(); $queries = @()
    }

    # Infrastructure: DbContexts, Migrations, Services
    if ($isInfra) {
        # DbContext classes (by name or inheritance)
        $dbContexts = Get-ChildItem $projDir -Recurse -Filter *.cs -File |
            Select-String -Pattern 'class\s+([A-Za-z_]\w*Context)' |
            ForEach-Object { $_.Matches[0].Groups[1].Value } |
            Sort-Object -Unique

        # Migrations folder files
        $migrationFiles = Get-ChildItem $projDir -Recurse -File |
            Where-Object { $_.DirectoryName -match '[\\/]Migrations([\\/]|$)' } |
            ForEach-Object { $_.FullName.Substring($projDir.Length + 1) } |
            Sort-Object

        # Service implementations (classes ending in Service)
        $serviceImpls = Get-ChildItem $projDir -Recurse -Filter *Service.cs -File |
            Select-String -Pattern 'class\s+([A-Za-z_]\w*Service)' |
            ForEach-Object { $_.Matches[0].Groups[1].Value } |
            Sort-Object -Unique
    } else {
        $dbContexts     = @()
        $migrationFiles = @()
        $serviceImpls   = @()
    }

    # general C# classes if you still want them
    $allClasses = Get-ChildItem $projDir -Recurse -Filter *.cs -File |
        Select-String -Pattern '^\s*(public|internal)\s+class\s+([A-Za-z_]\w*)' |
        ForEach-Object { $_.Matches[0].Groups[2].Value } |
        Sort-Object -Unique

    [PSCustomObject]@{
        ProjectName           = $projName
        ProjectPath           = $projDir
        Layer                 = if ($isWeb) { 'Web' } elseif ($isApp) { 'Application' } elseif ($isInfra) { 'Infrastructure' } else { 'Other' }
        Directories           = $dirs
        JsonFiles             = $jsonFiles
        Dockerfiles           = $dockerFiles
        ControllerClasses     = $controllerClasses
        ApplicationCommands   = $commands
        ApplicationQueries    = $queries
        DbContextClasses      = $dbContexts
        MigrationFiles        = $migrationFiles
        ServiceImplementations= $serviceImpls
        AllClasses            = $allClasses
    }
}

#  Display in console
$result | Format-List

#  Always export to JSON for your diagram tooling
$out = Join-Path $SolutionRoot 'solution-structure.json'
$result | ConvertTo-Json -Depth 10 | Set-Content $out
Write-Host "✅ Exported detailed structure to $out"
