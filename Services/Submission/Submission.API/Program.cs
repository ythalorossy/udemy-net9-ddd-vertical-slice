using Submission.API;
using Submission.API.Endpoints;
using Submission.Application;
using Submission.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add Services
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Use Services
app
    .UseSwagger()
    .UseSwaggerUI()
    .UseRouting()       // Match the HTTP Request to an endpoint (route) based on the URL
    ;

app.MapAllEndpoints();

// TODO: Migrate Database - Create the first Migration
if (app.Environment.IsDevelopment())
{
    // TODO: Apply Migrations
    // TODO: Seed Test Data
}
#endregion


app.Run();

