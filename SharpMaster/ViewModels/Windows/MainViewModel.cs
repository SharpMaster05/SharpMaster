using BLL.DTO;
using BLL.Services;
using System.Collections.ObjectModel;

namespace SharpMaster.ViewModels.Windows;

internal class MainViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;
    private readonly RegionService _regionService;

    public MainViewModel(PersonService personService, BuildService buildService, RegionService regionService)
    {
        _personService = personService;
        _buildService = buildService;
        _regionService = regionService;

        Persons = new ObservableCollection<PersonDTO>(_personService.GetAll());
        Builds = new ObservableCollection<BuildDTO>(_buildService.GetAll());
        Regions = new ObservableCollection<RegionDTO>(_regionService.GetAll());
    }

    public ObservableCollection<PersonDTO> Persons { get; set; }
    public ObservableCollection<BuildDTO> Builds { get; set; }
    public ObservableCollection<RegionDTO> Regions { get; set; }
}
