using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swagger.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Swagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SwaggerDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionString"]);
            });
            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("productV1", new Info
                {
                    
                    version = "V1",
                    title = "Poduct API"
                    description = "Ürün Ekleme/Silme/Güncelleme iþlemlerini gerçekleþtiren api",
                    contact = new Contact
                    {
                     email ="celikberkan1@outlook.com",
                     name ="Berkan Çelik",
                     url = "www.berkan.com"
                    }
                });
                var xmlFiles = $"{ Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //Combine path leri birleþtirmektedir.
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFiles);
                gen.IncludeXmlComments(xmlPath); 
            });

            var xmlFiles = $"{ Assembly.GetExecutingAssembly().GetName()}.xml";
            //Combine path leri birleþtirmektedir.
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFiles);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/productV1/swagger.json", "Swagger v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","Product API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
