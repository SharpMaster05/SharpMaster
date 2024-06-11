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
    public PersonViewModel(PersonService personService) : base(personService)
    {
        _personService = personService;

        Items = new(_personService.GetAll());
    }

    public ICommand SelectedItemCommand => new Command(x => { if (x is PersonDTO person) SelectedItem = person; });
    public ICommand EditPersonCommand => new Command(x =>
    {
        if (SelectedItem != null)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }
    }, x => SelectedItem != null);

    public ICommand ReloadCommand => new Command(x => Items = new(_personService.GetAll()));
    public ICommand DeleteCommand => new Command(x =>
    {
            _personService.Delete(SelectedItem);
            Items = new(_personService.GetAll());
    }, x => SelectedItem != null);
    public ICommand AddNewPersonCommand => new Command(x => 
    {
        AddPersonView view = new();
        view.ShowDialog();
    });
}
