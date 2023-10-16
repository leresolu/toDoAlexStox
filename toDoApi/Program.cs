using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Hosting;
using toDoApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Adds Cross Origin Requests
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\todoalexstox-firebase-adminsdk-uwj1l-728f14f1fc.json");

builder.Services.AddSingleton<IFirestoreService>(s => new FirestoreService(
    FirestoreDb.Create("todoalexstox")
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS must come AFTER routing and BEFORE Authentication and Authorization
// See https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-7.0#middleware-order
// for proper middle ware order
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
