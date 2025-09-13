using Expense.Tracker.Application;
using Expense.Tracker.Infrastructure;
using Expense.Tracker.Persistence;
using Expense.Tracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAPIServices(builder);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

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