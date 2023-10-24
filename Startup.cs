using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContaCorrenteAPI.Models;
using Microsoft.EntityFrameworkCore.SqlServer;

public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    // ...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<ErrorHandlerMiddleware>();
    // ...
}
