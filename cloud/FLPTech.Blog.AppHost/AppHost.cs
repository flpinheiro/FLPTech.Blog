using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

//parameters
var username = builder.AddParameter("username", "admin");
var password = builder.AddParameter("password", "Chageme@123", secret: true);

//cache
var cache = builder.AddRedis("cache");

//sql server
var sqlserver = builder.AddSqlServer("sqlserver")
    //.WithPassword(password)
    .WithHostPort(14330)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("database");

// migration sevice
var migrationService = builder.AddProject<Projects.FLPTech_Blog_Infraestructure_MigrationService>("migrationservice")
    .WithReference(sqlserver)
    .WaitFor(sqlserver);

//postgree for keycloak
var pgsql = builder.AddPostgres("pgsql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("keycloak-db");

// Identity Provider (Keycloak)
var keycloak = builder.AddKeycloak("keycloak", 8080, username, password)
    .WithReference(pgsql)
    .WaitFor(pgsql)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

//backend
var apiService = builder.AddProject<Projects.FLPTech_Blog_ApiService>("apiservice")
    .WithReference(sqlserver)
    .WaitFor(sqlserver)
    .WithReference(migrationService)
    .WaitFor(migrationService)
    .WithHttpHealthCheck("/health");

//web frontend bff
var webbff = builder.AddProject<Projects.FLPTech_Blog_Web_BFF>("webbff")
    .WithReference(keycloak)
    //.WaitFor(keycloak)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithHttpHealthCheck("/health")
    .WithExternalHttpEndpoints();

//web frontend
builder.AddProject<Projects.FLPTech_Blog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(sqlserver)
    .WaitFor(sqlserver)
    .WithReference(webbff)
    .WaitFor(webbff);

//open api documentation
builder.AddScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.Purple);
})
    .WithApiReference(keycloak)
    .WithApiReference(apiService)
    .WithApiReference(webbff);

//start application
builder.Build().Run();
