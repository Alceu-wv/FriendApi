using amigos_dev.Infrastructure;
using amigos_dev.Infrastructure.InversionOfControl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FriendDB"));
});

DependencyInjection.Inject(builder.Services, builder.Configuration);

// Add controllers, endpoints, swagger, etc.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
