using System.Windows.Input;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Pages;
using SharpMaster.Views.Windows;

namespace SharpMaster.ViewModels.Pages;

internal class BuildViewModel : BaseViewModel<BuildDTO>
{
    private readonly PersonService _personService;
    private readonly BuildService _buildService;
    private readonly RegionService _regionService;
    private readonly Navigation _navigation;

    public BuildViewModel(BuildService bs, RegionService rs, PersonService ps, Navigation nav)
        : base(bs)
    {
        _buildService = bs;
        _regionService = rs;
        _personService = ps;
        _navigation = nav;

        InitializeCommands();
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
    public ICommand NavigateToPeoplePageCommand => new Command(async x =>
    {
        SelectedItem = x as BuildDTO;

        var people = (await _personService.GetAllAsync()).Where(person =>
               person.BuildId == SelectedItem.Id
           );
        var viewModel = new PeopleFromBuildViewModel(people, _personService, _buildService);
        var view = new PersonView();

        view.DataContext = viewModel;

        _navigation.ChangePage(view);
    });
}
