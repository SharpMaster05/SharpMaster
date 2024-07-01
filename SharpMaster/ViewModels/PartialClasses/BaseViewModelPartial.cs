using BLL.Services;
using SharpMaster.Infrastucture;
using SharpMaster.ViewModels.Windows;
using SharpMaster.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SharpMaster.ViewModels;

internal partial class BaseViewModel<T> : Notifier where T : class, new()
{
    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }
    public string SearchingText { get; set; }
    public string SelectedSearchProperty { get; set; }
    public virtual ICommand AddCommand {  get; protected set; }
    public ICommand DeleteCommand { get; protected set; }
    public virtual ICommand EditCommand { get; protected set; }
    public ICommand ReloadCommand { get; protected set; }
    public ICommand SelectedItemCommand { get; protected set; }
    public ICommand SearchCommand { get; protected set; }
}
