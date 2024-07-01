using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BLL.Abstractions;
using SharpMaster.Infrastucture;

namespace SharpMaster.ViewModels;

internal partial class BaseViewModel<T> : Notifier where T : class, new()
{
    private readonly IService<T> _service;

    public BaseViewModel(IService<T> service)
    {
        _service = service;
    }

    public async Task InitializeAsync()
    {
        await InitializeAllItemsAsync();
    }

    protected async Task InitializeAllItemsAsync()
    {
        var items = await _service.GetAllAsync();
        Items = new(items);
    }

    protected void InitializeCommands()
    {
        DeleteCommand = new Command(x => Delete(), x => SelectedItem != null);
        SelectedItemCommand = new Command(x => SelectedItem = x as T);
        SearchCommand = new Command(x => Search(), x => SelectedSearchProperty != null);
        ReloadCommand = new Command(async x => await InitializeAllItemsAsync());
    }

    protected void Add(Window view) => view.ShowDialog();

    protected void Edit(Window view, BaseViewModel<T> viewModel)
    {
        view.DataContext = viewModel;
        view.ShowDialog();
    }

    protected async void Delete()
    {
        await _service.DeleteAsync(SelectedItem);

        var items = await _service.GetAllAsync();

        Items = new(items);
        SelectedItem = null;
    }

    protected async void Search()
    {
        var searchProperty = typeof(T).GetProperty(SelectedSearchProperty);

        var itmes = await _service.GetAllAsync();

        var filteredPeople = itmes.Where(x =>
        {
            var propertyValue = searchProperty.GetValue(x)?.ToString().ToLower();
            return propertyValue.Contains(SearchingText, StringComparison.CurrentCultureIgnoreCase);
        });
        Items = new(filteredPeople);
    }
}
