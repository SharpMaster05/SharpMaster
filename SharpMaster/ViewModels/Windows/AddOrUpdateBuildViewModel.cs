using System.Windows.Controls;
using System.Windows.Input;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;

namespace SharpMaster.ViewModels.Windows;

internal class AddOrUpdateBuildViewModel : BaseViewModel<BuildDTO>
{
    private readonly BuildService _buildService;
    private readonly RegionService _regionService;
    private readonly Animation _animation;
    private bool _isUpdating;

    public AddOrUpdateBuildViewModel(BuildService buildService, RegionService regionService)
        : base(buildService)
    {
        _buildService = buildService;
        _regionService = regionService;
        _animation = new Animation();
        Build = new();

        InitializeRegions();

        WindowTitle = "Add new Building";
    }

    public AddOrUpdateBuildViewModel(
        BuildService buildService,
        RegionService regionService,
        BuildDTO build = null,
        bool isUpdating = false
    )
        : this(buildService, regionService)
    {
        if (isUpdating && build != null)
        {
            try
            {
                Build = build;
                _isUpdating = isUpdating;
                WindowTitle = "Updating the build";
                Task.Run(async () => SelectedRegion = (await _regionService.GetAllAsync()).FirstOrDefault(x => x.RegionId == build.RegionId).Title);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }

    private async void InitializeRegions()
    {
        var regions = (await _regionService.GetAllAsync()).Select(x => x.Title);
        Regions = new(regions);
    }

    private async void SaveBuilding()
    {
        var regionId = (await _regionService.GetAllAsync())
            .FirstOrDefault(x => x.Title == SelectedRegion)
            .RegionId;

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

    public ICommand SaveCommand =>
        new Command(x =>
        {
            var border = x as Border;
            SaveBuilding();
            CloseCommand.Execute(border);
        });
    public ICommand CloseCommand =>
        new Command(x =>
        {
            if (x is Border border)
            {
                _animation.CloseAnimation(border, "AddOrUpdateBuildView");
            }
        });
}
