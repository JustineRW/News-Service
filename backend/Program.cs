
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// CORS configuration
var AllowLocalHost = "allowLocalHost";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowLocalHost,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.Configure<ExternalApiOptions>(
    builder.Configuration.GetSection("ExternalApi"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

// Enable CORS for development
app.UseCors(AllowLocalHost);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

