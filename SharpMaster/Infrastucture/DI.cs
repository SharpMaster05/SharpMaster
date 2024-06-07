﻿using BLL.Services;
using DAL.Abstractions;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpMaster.ViewModels.Windows;
using System.IO;

namespace SharpMaster.Infrastucture;

internal class DI
{
    private static ServiceProvider _provider;

    public static void Init()
    {
        var builder = new ServiceCollection();
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true);

        IConfiguration configuration = config.Build();

        string connectionString = configuration.GetConnectionString("SharpMasterConnection");

        builder.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.AddAutoMapper(typeof(MappingProfile));
        builder.AddTransient<MainViewModel>();

        builder.AddScoped<IRepository<Person>, PersonRepository>();
        builder.AddScoped<PersonService>();

        builder.AddScoped<IRepository<Build>, BuildRepository>();
        builder.AddScoped<BuildService>();

        builder.AddScoped<IRepository<Region>, RegionRepository>();
        builder.AddScoped<RegionService>();

        _provider = builder.BuildServiceProvider();
    }

    public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
}
