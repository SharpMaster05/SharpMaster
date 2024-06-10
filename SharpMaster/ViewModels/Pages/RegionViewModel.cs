using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;

namespace SharpMaster.ViewModels.Pages;

internal class RegionViewModel : BaseViewModel<RegionDTO>
{
    private readonly RegionService _regionService;
    public RegionViewModel(RegionService regionService) : base(regionService)
    {
        _regionService = regionService;

        Items = new(_regionService.GetAll());
    }
}
