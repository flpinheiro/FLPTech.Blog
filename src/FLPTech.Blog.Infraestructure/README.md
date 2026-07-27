# Migration

to apply the migration, run the following command:

```sh
Add-Migration -name "InitialMigration" -Context AppDbContext -Project FLPTech.Blog.Infraestructure -OutputDir Data/Migrations -StartupProject FLPTech.Blog.ApiService -verbose
```

to update the database, run the following command:
```sh
Update-Database -Context AppDbContext -Project FLPTech.Blog.Infraestructure -StartupProject FLPTech.Blog.ApiService -verbose
```

# Database

The database used in this project is SQL Server. To change the database, you need to change the connection string in the `appsettings.json` file.