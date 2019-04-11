using Microsoft.Win32;

namespace Gensler.Revit.FamilyLoader.Views.Commands
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Input;

    internal class FolderCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var folderBrowserDialog = new FolderBrowserDialog {SelectedPath = @"\\gensler.ad\Content\Revit"};
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            _viewModel.FolderPath = folderBrowserDialog.SelectedPath;
            _viewModel.IsLoaded = true;
        }

        public FolderCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void ConfigureWindowsRegistry()
        {
            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64); //here you specify where exactly you want your entry

            var reg = localMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true) ??
                      localMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);

            if (reg.GetValue("EnableLinkedConnections") != null) return;
            reg.SetValue("EnableLinkedConnections", "1", RegistryValueKind.DWord);
            MessageBox.Show(
                "Your configuration is now created,you have to restart your device to let app work perfektly");
        }
    }
}
