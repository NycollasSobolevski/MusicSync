using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DotNetEnv.Env.Load();

builder.Services.AddTransient(p =>
{
    WebProxy proxy = new WebProxy("http://disrct:etstech31415@rb-proxy-ca1.bosch.com:8080/");
    HttpClientHandler clientHandler = new HttpClientHandler(){
        Proxy = proxy
    };

    return new HttpClient(clientHandler)
    {
        BaseAddress = new Uri("http://accounts.spotify.com/api")
    };
}
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
