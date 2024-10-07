using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PipelineTask.PipelineTask.Api.Accessors;
using PipelineTask.PipelineTask.Api.Setups;
using PipelineTask.PipelineTask.Domain.DataAccess;
using PipelineTask.PipelineTask.Domain.Models;
using PipelineTask.PipelineTask.Infrastructure.Accessors;
using PipelineTask.PipelineTask.Infrastructure.Setups;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PipelineTaskContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityPipelineTaskContext>();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddScoped<IUserCacheSetup, UserCacheSetup>();
builder.Services.AddScoped<IUserCacheAccessor, UserCacheAccessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var userCacheSetup = scope.ServiceProvider.GetRequiredService<IUserCacheSetup>();
        Task.WaitAll(userCacheSetup.InitializeAsync());

        app.Run();
    }
    catch (Exception ex)
    {

    }
}


