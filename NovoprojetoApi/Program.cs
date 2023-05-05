
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NovoprojetoApi.percistencia;

namespace NovoprojetoApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //conexão de banco de dados na memoria
            //builder.Services.AddDbContext<DevEventDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));
           
            
            string connectionString = builder.Configuration.GetConnectionString("TestServerSql");
            builder.Services.AddDbContext<DevEventDbContext>(
              e => e.UseSqlServer(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NovoProjetoEventos.Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Thiago de souza",
                        Email = "thiaguinhosouza73@gmail.com",
                        Url = new Uri("https://github.com/thiago-sso90")
                    }
                });

                var xmlPasta = "NovoProjetoApi.xml";
                var xmlCaminho = Path.Combine(AppContext.BaseDirectory, xmlPasta);
                c.IncludeXmlComments(xmlCaminho);
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


            app.MapControllers();

            app.Run();
        }
    }
}