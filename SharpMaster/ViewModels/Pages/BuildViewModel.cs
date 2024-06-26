using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Pages;

internal class BuildViewModel : BaseViewModel<BuildDTO>
{
    private readonly BuildService _buildService;
    private readonly RegionService _regionService;
    public BuildViewModel(BuildService buildService, RegionService regionService) : base(buildService)
    {
        _buildService = buildService;
        _regionService = regionService;

        InitializeAsync(_buildService);
    }

    public ICommand AddCommand => new Command(x => Add(new AddOrUpdateBuildView()));
    public ICommand EditCommand => new Command(x => Edit(new AddOrUpdateBuildView(), new AddOrUpdateBuildViewModel(_buildService, _regionService, SelectedItem, true))
                                               , x => SelectedItem != null);
    public ICommand DeleteCommand => new Command(x => Delete(_buildService), x => SelectedItem != null);
    public ICommand SelectedItemCommand => new Command(x => SelectedItem = x as BuildDTO);
    public ICommand SearchCommand => new Command(x => Search(_buildService));
    public ICommand ReloadCommand => new Command(x => InitializeAsync(_buildService));
}
