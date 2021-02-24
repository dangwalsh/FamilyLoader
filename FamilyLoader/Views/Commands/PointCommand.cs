﻿namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Input;

    internal class PointCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var window = parameter as MainWindow;
            window?.Hide();

            var ui = RevitCommand.UiDocument;
            var point = ui.Selection.PickPoint();

            _viewModel.StartPoint = point;

            window?.ShowDialog();
        }

        public PointCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
    }
}
