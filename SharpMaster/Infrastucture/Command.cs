using System.Windows.Input;

namespace SharpMaster.Infrastucture;

internal class Command : ICommand
{
    private Action<object> _execute;
    private Func<object, bool> _canExecute;

    public Command(Action<object> execute, Func<object, bool> canExecute = null) => (_execute, _canExecute) = (execute, canExecute); 

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) => _execute(parameter);
}
