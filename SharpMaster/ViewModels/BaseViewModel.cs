using BLL.Abstractions;
using SharpMaster.Infrastucture;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SharpMaster.ViewModels;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    private readonly IService<T> _service;

    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }

    public BaseViewModel(IService<T> service)
    {
        _service = service;
    }

    public void Add(T item)
    {
        _service.Add(item);
    }

}
