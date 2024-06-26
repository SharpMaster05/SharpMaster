using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SharpMaster.ViewModels;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }
    public string SearchingText { get; set; }
    public string SelectedSearchProperty { get; set; }

    protected async void InitializeAsync(IService<T> service)
    {
        var items = await service.GetAllAsync();
        Items = new(items);
    }

    protected void Add(Window view)
    {
        view.ShowDialog();
    }

    protected void Edit(Window view, BaseViewModel<T> viewModel)
    {
        view.DataContext = viewModel;
        view.ShowDialog();
    }

    protected async void Delete(IService<T> service)
    {
        await service.DeleteAsync(SelectedItem);

        var items = await service.GetAllAsync();
        
        Items = new(items);
        SelectedItem = null;
    }

    protected async void Search(IService<T> service)
    {
        var searchProperty = typeof(T).GetProperty(SelectedSearchProperty);

        var itmes = await service.GetAllAsync();

        var filteredPeople = itmes.Where(x =>
        {
            var propertyValue = searchProperty.GetValue(x)?.ToString().ToLower();
            return propertyValue.Contains(SearchingText, StringComparison.CurrentCultureIgnoreCase);
        });
        Items = new(filteredPeople);
    }
}
