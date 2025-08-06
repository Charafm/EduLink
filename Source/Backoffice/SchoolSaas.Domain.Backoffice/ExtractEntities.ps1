$domainPath = "G:\repos\SchoolSaas\SchoolSaas\Source\Backoffice\SchoolSaas.Domain.Backoffice"
$outputFile = "Entities.json"

$entities = Get-ChildItem -Path $domainPath -Filter "*.cs" -Recurse | ForEach-Object {
    $content = Get-Content $_.FullName -Raw
    $className = [regex]::Match($content, 'public\s+(?:sealed\s+)?class\s+(\w+)\s*:\s*BaseEntity<.*?>').Groups[1].Value

    if ($className) {
        $properties = [regex]::Matches($content, '(?m)^\s*(\[.*?\])?\s*(public\s+virtual\s+)?(.*?)\s+(\w+)\s*{\s*get;\s*set;\s*}(?:\s*=\s*.*?;)?') |
            ForEach-Object {
                $attributes = if ($_.Groups[1].Success) { @($_.Groups[1].Value.Trim()) } else { @() }
                $type = $_.Groups[3].Value.Trim()
                $isNullable = $type.EndsWith('?') -or $type.StartsWith('IEnumerable') -or $type.StartsWith('ICollection')

                [PSCustomObject]@{
                    Name = $_.Groups[4].Value.Trim()
                    Type = $type.Replace('?', '')
                    IsNullable = $isNullable
                    Attributes = $attributes
                }
            }

        [PSCustomObject]@{
            Entity = $className
            Properties = $properties
            BaseEntityProperties = [PSCustomObject]@{
                Created = "DateTime"
                LastModified = "DateTime?"
                CreatedBy = "string?"
                LastModifiedBy = "string?"
                IsDeleted = "bool?"
                RowVersion = "byte[]?"
            }
        }
    }
}

$entities | ConvertTo-Json -Depth 4 | Set-Content $outputFile
Write-Host "Entity data exported to $outputFile"