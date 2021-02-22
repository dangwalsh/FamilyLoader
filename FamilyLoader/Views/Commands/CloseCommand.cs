namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Input;

    internal class CloseCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = parameter as MainWindow;

            window?.Close();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        
        public CloseCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
    }
}
