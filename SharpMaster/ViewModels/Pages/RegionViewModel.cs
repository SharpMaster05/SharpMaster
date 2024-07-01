using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.Views.Pages;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Pages;

internal class RegionViewModel : BaseViewModel<RegionDTO>
{
    private readonly PersonService _personService;
    private readonly RegionService _regionService;
    private readonly BuildService _buildService;
    private readonly Navigation _navigation;
    public RegionViewModel(PersonService personService, RegionService service, BuildService buildService, Navigation navigation) : base(service)
    {
        _regionService = service;
        _buildService = buildService;
        _personService = personService;
        _navigation = navigation;
    }

    public ICommand SelectedRegionCommand => new Command(async x =>
    {
        SelectedItem = x as RegionDTO;

        var buildings = (await _buildService.GetAllAsync()).Where(build => build.RegionId == SelectedItem.RegionId);
        var viewModel = new BuildListFromSelectedRegionViewModel(buildings, _buildService, _regionService, _personService, _navigation);
        var view = new BuildView();

        view.DataContext = viewModel;

        _navigation.ChangePage(view);
    });
}
