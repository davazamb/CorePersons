﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CorePersons.Data;
using CorePersons.Models;
using CorePersons.Services;
using Microsoft.AspNetCore.Identity;

namespace CorePersons
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //invoca el servicio createroles
            //await CreateRoles(serviceProvider);
        }
        //Crear roles de usuario
        //private async Task CreateRoles(IServiceProvider serviceProvider)
        //{
        //    var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    string[] rolesNames = { "Admin", "User" };
        //    IdentityResult result;
        //    foreach (var rolesName in rolesNames)
        //    {
        //        var roleExist = await RoleManager.RoleExistsAsync(rolesName);
        //        if (!roleExist)
        //        {
        //            result = await RoleManager.CreateAsync(new IdentityRole(rolesName));
        //        }       
        //    }
        //    //id para el usuario admin 
        //    var user = await UserManager.FindByIdAsync("d42f147f-56a5-41ab-8cba-b670bc48e9c0");
        //    await UserManager.AddToRoleAsync(user, "Admin");
        }         
    }

