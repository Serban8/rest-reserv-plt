using Microsoft.OpenApi.Models;
using reserv_plt.Server.Settings;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//for testing on localhost only
builder.Services.AddCors(corsBuilder =>
{
    corsBuilder.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
//

// Add services to the container.
Dependencies.Inject(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();

//for testing on localhost only
app.UseCors("AllowAll");
//

app.MapControllers();

app.Run();
