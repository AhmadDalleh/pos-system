using Pos.Api.Health;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var connectionString =  builder.Configuration.GetConnectionString("Pos") 
    ?? throw new InvalidOperationException("Connection string 'Pos' not found.");
builder.Services.AddNpgsqlDataSource(connectionString);
builder.Services.AddHealthChecks()
    .AddCheck<PostgresHealthCheck>("postgres");

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health"); 
app.Run();


