using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;

namespace SharpMaster.ViewModels.Pages;

internal class RegionViewModel : BaseViewModel<RegionDTO>
{
    private readonly RegionService _regionService;
    public RegionViewModel(RegionService regionService)
    {
        _regionService = regionService;

        InitializeAsync(_regionService);
    }
}
