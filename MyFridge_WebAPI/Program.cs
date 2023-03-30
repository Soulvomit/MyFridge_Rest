using Microsoft.EntityFrameworkCore;
using MyFridge_Library_Data.Data;
using MyFridge_WebAPI.UoW;
using MyFridge_WebAPI.UoW.Interface;

namespace MyFridge_WebAPI
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //add services to the container
            //add db context service
            builder.Services.AddDbContext<ApplicationDbContext>(options => options
                                                            .UseLazyLoadingProxies(true)
                                                            .UseInMemoryDatabase("InMemoryDatabase"));
            //add unit of work service
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //add controllers
            builder.Services.AddControllers();

            //learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}