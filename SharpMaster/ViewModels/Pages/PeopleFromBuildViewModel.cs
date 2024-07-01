using System.Windows;
using System.Windows.Input;
using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;
using Microsoft.EntityFrameworkCore;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;

namespace SharpMaster.ViewModels.Pages;

internal class PeopleFromBuildViewModel : BaseViewModel<PersonDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;

    public PeopleFromBuildViewModel(PersonService person, BuildService buildService)
        : base(person)
    {
        _personService = person;
        _buildService = buildService;
        SelectedSearchProperty = "Name";
    }

    public PeopleFromBuildViewModel(
        IEnumerable<PersonDTO> people,
        PersonService person,
        BuildService buildService
    )
        : this(person, buildService)
    {
        Items = new(people);
        InitializeCommands();
        BackButton = Visibility.Visible;
        SearchCommand = new Command(x =>
        {
            try
            {
                var searchProperty = typeof(PersonDTO).GetProperty(SelectedSearchProperty);

                var filteredPeople = Items.Where(x =>
                {
                    var propertyValue = searchProperty.GetValue(x)?.ToString().ToLower();
                    return propertyValue.Contains(
                        SearchingText,
                        StringComparison.CurrentCultureIgnoreCase
                    );
                });
                Items = new(filteredPeople);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        });

        ReloadCommand = new Command(async x => 
        {
            var personBuildId = people.First().BuildId;
            var reloadedPeople = (await _personService.GetAllAsync()).Where(x => x.BuildId == personBuildId);
            Items = new(reloadedPeople);
        });

    }

    public Visibility BackButton { get; set; }

    public override ICommand AddCommand => new Command(x => Add(new AddOrUpdateView()));
    public override ICommand EditCommand =>
        new Command(
            x =>
            {
                var viewModel = new AddOrUpdateViewModel(_personService, _buildService, SelectedItem, true);
                var view = new AddOrUpdateView();

                Edit(view, viewModel);
            },
            x => SelectedItem != null
        );
}
