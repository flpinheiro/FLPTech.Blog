using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

//cache
var cache = builder.AddRedis("cache");

//sql server
var password = builder.AddParameter("password", "Chageme@123", secret: true);
var sql = builder.AddSqlServer("sql")
    //.WithPassword(password)
    .WithHostPort(14330)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("database");

// migration sevice
var migrationService = builder.AddProject<Projects.FLPTech_Blog_Infraestructure_MigrationService>("migrationservice")
    .WithReference(db)
    .WaitFor(db);

//backend
var apiService = builder.AddProject<Projects.FLPTech_Blog_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db)
    .WithReference(migrationService)
    .WaitFor(migrationService)
    .WithHttpHealthCheck("/health");

//frontend
builder.AddProject<Projects.FLPTech_Blog_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(apiService)
    .WaitFor(apiService);

//open api documentation
builder.AddScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.Purple);
})
    .WithApiReference(apiService);

//start application
builder.Build().Run();
