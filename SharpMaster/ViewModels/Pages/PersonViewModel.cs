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

    public ICommand SelectedItemCommand => new Command(x => SelectedItem = x as PersonDTO);
    
    public ICommand EditPersonCommand => new Command(x =>
    {
        AddOrUpdateView view = new AddOrUpdateView();
        AddOrUpdateViewModel editPersonViewModel = new AddOrUpdateViewModel(_personService, _buildService, SelectedItem, true);
        view.DataContext = editPersonViewModel;
        view.ShowDialog();
    }, x => SelectedItem != null);

    public ICommand ReloadCommand => new Command(async x =>
    {
        var items = await _personService.GetAllAsync();
        Items = new(items);
    });
    
    public ICommand DeleteCommand => new Command(async x =>
    {
        await _personService.DeleteAsync(SelectedItem);
        var items = await _personService.GetAllAsync();
        Items = new(items);
        SelectedItem = null;
    },
    x => SelectedItem != null);

    public ICommand AddNewPersonCommand => new Command(x => 
    {
        AddOrUpdateView view = new AddOrUpdateView();
        view.ShowDialog();
    });
}
