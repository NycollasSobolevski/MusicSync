using System.Net;
using music_api;
using music_api.Auxi;
using music_api.Model;
using Security_jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DotNetEnv.Env.Load();
//! tirar duvida com trevis
builder.Services.AddTransient(p =>
{
    return new HttpClient()
    {
        BaseAddress = new Uri("http://accounts.spotify.com/api")
    };
});

builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IMailService, SendEmail>();
builder.Services.AddTransient<IRepository<Token>, TokenRepository>();
builder.Services.AddTransient<IJwtService>( p => 
    new JwTService(new PasswordProvider(
        Environment.GetEnvironmentVariable("PASSWORD_PROVIDER")
        )
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MainPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
        });
});


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

app.UseCors();

app.Run();
