using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. DI (before building app)
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LogginBehaviour<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline. (after building app)
app.MapCarter();

app.UseHttpsRedirection();

app.Run();
