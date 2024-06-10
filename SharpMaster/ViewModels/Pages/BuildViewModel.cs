using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;

namespace SharpMaster.ViewModels.Pages;

internal class BuildViewModel : BaseViewModel<BuildDTO>
{
    private readonly BuildService _buildService;
    public BuildViewModel(BuildService buildService) : base(buildService)
    {
        _buildService = buildService;

        Items = new(_buildService.GetAll());
    }
}
