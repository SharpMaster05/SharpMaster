using BLL.Abstractions;
using BLL.DTO;
using BLL.Services;
using SharpMaster.Infrastucture;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SharpMaster.ViewModels;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }

    protected async void InitializeAsync(IService<T> service)
    {
        var items = await service.GetAllAsync();
        Items = new(items);
    }

}
