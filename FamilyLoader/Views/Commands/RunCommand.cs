namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Input;

    internal class RunCommand : ICommand
    {
        private MainWindowViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = parameter as MainWindow;
            var path = _viewModel.FolderPath;
            //var document = RevitCommand.Document;
            //var loader = new Loader(document, path);
            var request = new RequestData()
            {
                Id = RequestId.Load,
                Text = path
            };
            RevitCommand.RequestHandler.Request.Make(request);
            RevitCommand.ExternalEvent.Raise();
        }

        public RunCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
