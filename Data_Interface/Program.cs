using Microsoft.EntityFrameworkCore;
using Data_Library.Context;
using Data_Interface.Service.Mapper;
using Data_Interface.Service.Mapper.Interface;
using Data_Interface.Service.Data;
using Data_Interface.Service.Data.Interface;

namespace Data_Interface
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
            //add mapper service
            builder.Services.AddSingleton<IMapperService, MapperService>();
            //add unit of work service
            builder.Services.AddScoped<IDataService, DataService>();

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