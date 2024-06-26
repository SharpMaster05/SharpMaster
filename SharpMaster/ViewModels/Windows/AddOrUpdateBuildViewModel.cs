using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Windows;
internal class AddOrUpdateBuildViewModel : BaseViewModel<BuildDTO>
{

    private readonly BuildService _buildService;
    private readonly RegionService _regionService;
    private readonly Animation _animation;
    private bool _isUpdating;
    public AddOrUpdateBuildViewModel(BuildService buildService, RegionService regionService) : base(buildService)
    {
        _buildService = buildService;
        _regionService = regionService;
        _animation = new Animation();
        Build = new BuildDTO();

        InitializeRegions();

        WindowTitle = "Add new building";
    }

    public AddOrUpdateBuildViewModel(BuildService buildService, RegionService regionService, BuildDTO build = null, bool isUpdating = false)
           : this(buildService, regionService)
    {
        if (isUpdating) 
        {
            Build = build;
            _isUpdating = isUpdating;
            WindowTitle = "Updating the build";
            Task.Run(async () => SelectedRegion = (await _regionService.GetAllAsync()).FirstOrDefault(x => x.RegionId == build.RegionId).Title);
        }
    }

    private async void InitializeRegions()
    {
        var regions = await _regionService.GetAllAsync();
        Regions = new(regions.Select(x => x.Title));
    }

    private async void SaveBuilding()
    {
        var regionId = (await _regionService.GetAllAsync()).FirstOrDefault(x => x.Title == SelectedRegion).RegionId;
        Build.RegionId = regionId;

        if (_isUpdating)
            await _buildService.UpdateAsync(Build);
        else
            await _buildService.AddAsync(Build);
    }

    public BuildDTO Build { get; set; }
    public string WindowTitle { get; set; }
    public List<string> Regions { get; set; }
    public string SelectedRegion { get; set; }

    public ICommand AddCommand => new Command(x =>
    {
        var border = x as Border;
        SaveBuilding();
        CloseCommand.Execute(border);

    });
    public ICommand CloseCommand => new Command(x =>
    {
        if(x is Border border)
        {
            _animation.CloseAnimation(border, "AddOrUpdateBuildView");
        }
    });
}
