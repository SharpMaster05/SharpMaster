using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;

namespace SharpMaster.ViewModels.Pages;

internal class PersonViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;

    public PersonViewModel(PersonService ps, BuildService bs)
        : base(ps)
    {
        _personService = ps;
        _buildService = bs;
        InitializeCommands();
        BackButton = Visibility.Hidden;
    }

    public Visibility BackButton { get; set; }
    
    public override ICommand AddCommand => new Command(x => Add(new AddOrUpdateView()));
    public override ICommand EditCommand =>
        new Command(
            x =>
                Edit(
                    new AddOrUpdateView(),
                    new AddOrUpdateViewModel(_personService, _buildService, SelectedItem, true)
                ),
            x => SelectedItem != null
        );
}
