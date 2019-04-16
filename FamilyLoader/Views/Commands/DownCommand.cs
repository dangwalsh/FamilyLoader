namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Input;

    internal class DownCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var name = parameter as string;
            switch (name)
            {
                case "Height":
                    _viewModel.Height -= 1.0;
                    if (_viewModel.Height < 0) _viewModel.Height = 0;
                    break;
                case "Width":
                    _viewModel.Width -= 1.0;
                    if (_viewModel.Width < 0) _viewModel.Width = 0;
                    break;
                default:
                    throw new ArgumentException("Invalid field name.");
            }
        }

        public event EventHandler CanExecuteChanged;

        public DownCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
