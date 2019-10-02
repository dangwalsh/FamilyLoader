namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Input;

    internal class UpCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

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
                    _viewModel.Height = Math.Truncate(_viewModel.Height) + 1.0;
                    break;
                case "Width":
                    _viewModel.Width = Math.Truncate(_viewModel.Width) + 1.0;
                    break;
                default:
                    throw new ArgumentException("Invalid field name.");
            }

        }

        public event EventHandler CanExecuteChanged;

        public UpCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
