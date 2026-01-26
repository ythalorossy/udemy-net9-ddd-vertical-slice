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
    .UseRouting()       // Match the HTTP Request to an endpoint (route) based on the URL
    ;

app.MapAllEndpoints();

if (app.Environment.IsDevelopment())
{
    // TODO: Migrate Database - Create the first Migration
    // TODO: Apply Migrations
    // TODO: Seed Test Data

    // Enable Swagger in Development environment
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Submission API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

#endregion

app.Run();