add-migration Init -Context EdulinkContext -OutputDir Migrations

Update-database -Context BackofficeContext 

Remove-Migration -Context BackofficeContext