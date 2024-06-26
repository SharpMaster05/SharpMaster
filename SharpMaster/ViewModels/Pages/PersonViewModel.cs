using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;
using System.Windows.Input;

namespace SharpMaster.ViewModels.Pages;

internal class PersonViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;
    public PersonViewModel(PersonService personService, BuildService buildService)
    {
        _personService = personService;
        _buildService = buildService;

        InitializeAsync(_personService);
    }
    public ICommand AddNewPersonCommand => new Command(x => Add(new AddOrUpdateView()));
    public ICommand DeleteCommand => new Command(x => Delete(_personService), x => SelectedItem != null);
    public ICommand EditPersonCommand => new Command(x => Edit(new AddOrUpdateView(), new AddOrUpdateViewModel(_personService, _buildService, SelectedItem, true))
                                                     , x => SelectedItem != null);
    public ICommand SelectedItemCommand => new Command(x => SelectedItem = x as PersonDTO);
    public ICommand SearchCommand => new Command(x => Search(_personService));
    public ICommand ReloadCommand => new Command(x => InitializeAsync(_personService));
}
