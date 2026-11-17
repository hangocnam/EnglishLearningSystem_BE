using EnglishLearningSystem.API;
using EnglishLearningSystem.API.Errors;
using EnglishLearningSystem.Application;
using EnglishLearningSystem.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddWebAPI();

// Error handling: every unhandled exception → ProblemDetails with a Domain error code
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();   // first in the pipeline so it wraps everything after it
app.UseStatusCodePages();    // 401/403/404 without a body → ProblemDetails

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
