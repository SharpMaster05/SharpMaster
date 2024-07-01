using BLL.Services;
using DAL.Abstractions;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpMaster.ViewModels.Pages;
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
        builder.AddTransient<PersonViewModel>();
        builder.AddTransient<BuildViewModel>();
        builder.AddTransient<RegionViewModel>();
        builder.AddTransient<AddOrUpdateViewModel>();
        builder.AddTransient<AddOrUpdateBuildViewModel>();
        builder.AddTransient<PeopleFromBuildViewModel>();
        builder.AddTransient<BuildListFromSelectedRegionViewModel>();

        builder.AddTransient<IRepository<Person>, PersonRepository>();
        builder.AddTransient<PersonService>();

        builder.AddTransient<IRepository<Build>, BuildRepository>();
        builder.AddTransient<BuildService>();

        builder.AddTransient<IRepository<Region>, RegionRepository>();
        builder.AddTransient<RegionService>();

        builder.AddTransient<Animation>();
        builder.AddSingleton<Navigation>();

        _provider = builder.BuildServiceProvider();
    }

    public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
    public PersonViewModel PersonViewModel => _provider.GetRequiredService<PersonViewModel>();
    public BuildViewModel BuildViewModel => _provider.GetRequiredService<BuildViewModel>();
    public RegionViewModel RegionViewModel => _provider.GetRequiredService<RegionViewModel>();
    public AddOrUpdateViewModel AddOrUpdateViewModel => _provider.GetRequiredService<AddOrUpdateViewModel>();
    public AddOrUpdateBuildViewModel AddOrUpdateBuildViewModel => _provider.GetRequiredService<AddOrUpdateBuildViewModel>();
    public PeopleFromBuildViewModel PeopleFromBuildViewModel => _provider.GetRequiredService<PeopleFromBuildViewModel>();
    public BuildListFromSelectedRegionViewModel BuildListFromSelectedRegionViewModel => _provider.GetRequiredService<BuildListFromSelectedRegionViewModel>();
}
