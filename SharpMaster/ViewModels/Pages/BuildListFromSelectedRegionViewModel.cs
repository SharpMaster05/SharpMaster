using System.Windows.Input;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Pages;
using SharpMaster.Views.Windows;

namespace SharpMaster.ViewModels.Pages;

internal class BuildListFromSelectedRegionViewModel : BaseViewModel<BuildDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;
    private readonly RegionService _regionService;
    private readonly Navigation _navigation;

    public BuildListFromSelectedRegionViewModel(
        BuildService bs,
        RegionService rs,
        PersonService ps,
        Navigation nav
    )
        : base(bs)
    {
        _buildService = bs;
        _regionService = rs;
        _personService = ps;
        _navigation = nav;
        SelectedSearchProperty = "Title";
    }

    public BuildListFromSelectedRegionViewModel(
        IEnumerable<BuildDTO> buildings,
        BuildService bs,
        RegionService rs,
        PersonService ps,
        Navigation nav
    )
        : this(bs, rs, ps, nav)
    {
        Items = new(buildings);
        InitializeCommands();

        SearchCommand = new Command(x =>
        {
            try
            {
                var searchProperty = typeof(BuildDTO).GetProperty(SelectedSearchProperty);

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
            var buildRegionId = buildings.First().RegionId;
            var newBuildings = (await _buildService.GetAllAsync()).Where(x => x.RegionId == buildRegionId);
            Items = new(newBuildings);
        });
    }

    public override ICommand AddCommand => new Command(x => Add(new AddOrUpdateBuildView()));
    public override ICommand EditCommand =>
        new Command(
            x =>
                Edit(
                    new AddOrUpdateBuildView(),
                    new AddOrUpdateBuildViewModel(_buildService, _regionService, SelectedItem, true)
                ),
            x => SelectedItem != null
        );
    public new ICommand SelectedItemCommand => new Command(x => SelectedItem = x as BuildDTO);

    public ICommand NavigateToPeoplePageCommand => new Command(async x =>
    {
        SelectedItemCommand.Execute(x);

        var people = (await _personService.GetAllAsync()).Where(person =>
               person.BuildId == SelectedItem.Id
           );
        var viewModel = new PeopleFromBuildViewModel(people, _personService, _buildService);
        var view = new PersonView();

        view.DataContext = viewModel;

        _navigation.ChangePage(view);
    });
}
