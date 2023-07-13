using API_AdressSearch.Infra.CrossCutting.Requests;
using API_AdressSearch.Infra.CrossCutting.Requests.Interfaces;
using API_AdressSearch.Service.Validators;
using API_AdressSearch.Service.Validators.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRequests, Requests>();
builder.Services.AddScoped<IRequestValidator, RequestValidator>();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var OpenCors = "_openCors";

// Configuring cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OpenCors,
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                            builder.WithMethods("PUT", "DELETE", "GET", "POST");
                            builder.AllowAnyHeader();
                        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(OpenCors);

app.MapControllers();

app.Run();
