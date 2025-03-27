using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

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

builder
    .Services.AddMarten(option =>
    {
        option.Connection(builder.Configuration.GetConnectionString("Database")!);
        option.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();

// Repositories
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

// Exceotion Handling
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline. (after building app)
app.MapCarter();
app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app.Run();
