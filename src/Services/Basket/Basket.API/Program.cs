var builder = WebApplication.CreateBuilder(args);

// Add services to the container. DI (before building app)

var app = builder.Build();

// Configure the HTTP request pipeline. (after building app)

app.UseHttpsRedirection();

app.Run();
