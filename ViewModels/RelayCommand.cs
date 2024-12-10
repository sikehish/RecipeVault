using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute ?? (() => true); // Default to always executable
    }

    public bool CanExecute(object parameter) => _canExecute();

    public void Execute(object parameter) => _execute();

    public event EventHandler CanExecuteChanged
    {
        add { }  // No need for CommandManager, Avalonia handles this differently
        remove { }  // No need for CommandManager
    }
}
