
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

//Fetch news api configuration
builder.Services.Configure<ExternalApiOptions>(
    builder.Configuration.GetSection("ExternalApi"));

builder.Services.AddHttpClient<ArticlesController>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

// Use CORS policy for dev environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
    app.UseCors(AllowLocalHost);
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();

