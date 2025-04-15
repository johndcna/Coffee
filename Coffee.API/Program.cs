using Coffee.API.Services.Implementation;
using Coffee.API.Services.Interfaces;
using Microsoft.AspNetCore.RateLimiting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("LimitOnFifthCall", opt =>
            {

                opt.AutoReplenishment = true;
                opt.PermitLimit = 4;
                opt.QueueLimit = 0;
                opt.Window = TimeSpan.FromMinutes(1);
            })


            .OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 503;
                context.HttpContext.Response.ContentType = "text/plain";
            };
        });


        //Repositories
        builder.Services.AddScoped<ICoffeeService, CoffeeService>();
        builder.Services.AddTransient<ICoffeeMessage, AprilCoffeeMessageProvider>();
        builder.Services.AddTransient<ICoffeeMessage, DefaultCoffeeMessageProvider>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRateLimiter();
        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}