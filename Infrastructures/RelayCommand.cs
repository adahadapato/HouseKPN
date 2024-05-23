
namespace HouseKPN.Infrastructures;

using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object?> execute, Predicate<object> canExecute)
    {
        if (execute == null)
        {
            throw new ArgumentNullException("execute");
        }
        _canExecute = canExecute;
        _execute = execute;
    }

    public RelayCommand(Action<object> execute)
           : this(execute, null)
    {
        _execute = execute;
    }

    // Ensures WPF commanding infrastructure asks all RelayCommand objects whether their
    // associated views should be enabled whenever a command is invoked 
    public event EventHandler? CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
            //CanExecuteChangedInternal += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
            //CanExecuteChangedInternal -= value;
        }
    }
    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }


    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    //private event EventHandler CanExecuteChangedInternal;

    //public void RaiseCanExecuteChanged()
    //{
    //    CanExecuteChangedInternal.Raise(this);
    //}
}

public class RelayCommand<T> : ICommand
{
    private readonly Predicate<T> _canExecute;
    private readonly Action<T> _execute;

    public RelayCommand(Action<T> execute)
       : this(execute, null)
    {
        _execute = execute;
    }

    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
        if (execute == null)
        {
            throw new ArgumentNullException("execute");
        }
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute == null || _canExecute((T)parameter);
    }

    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
