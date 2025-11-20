var builder = DistributedApplication.CreateBuilder(args);
var api = builder.AddProject<Projects.UniHealth_Api>("unihealth-api");
builder.Build().Run();
