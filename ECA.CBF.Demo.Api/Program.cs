using ECA.CBF.Demo.Api;
using ECA.CBF.Demo.Process;
using ECA.CBF.Demo.Process.Interface;
using ECA.CBF.Demo.Repository.Db;
using ECA.CBF.Demo.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(config =>
{
    config.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddXmlSerializerFormatters();

builder.Services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new ApiVersion(1, 0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
});

#region Dependencies Injection

builder.Services.AddScoped<ITeamProcess, TeamProcess>();
builder.Services.AddScoped<ITransferProcess, TransferProcess>();
builder.Services.AddScoped<IPlayerProcess, PlayerProcess>();
builder.Services.AddScoped<ITournmentProcess, TournmentProcess>();
builder.Services.AddScoped<IMatchProcess, MatchProcess>();
builder.Services.AddScoped<IMatchEventsProcess, MatchEventsProcess>();

builder.Services.AddScoped<ITeamDbRepository, TeamBdRepository>();
builder.Services.AddScoped<IPlayerDbRepository, PlayerDbRepository>();
builder.Services.AddScoped<ITransferDbRepository, TransferDbRepository>();
builder.Services.AddScoped<ITournmentDbRepository, TournmentDbRepository>();
builder.Services.AddScoped<IMatchDbRepository, MatchDbRepository>();
builder.Services.AddScoped<IGoalDbRepository, GoalDbRepository>();
builder.Services.AddScoped<ICardDbRepository, CardDbRepository>();
builder.Services.AddScoped<IReplacementDbRepository, ReplacementDbRepository>();

#endregion Dependencies Injection



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
