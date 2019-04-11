namespace Gensler.Revit.FamilyLoader.Views
{
    using System.ComponentModel;
    using Autodesk.Revit.DB;
    using Commands;
    using ValueConverters;

    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _folderPath;
        private bool _isLoaded;
        private XYZ _startPoint;
        private FeetInches _width;
        private FeetInches _height;

        public string FolderPath
        {
            get => _folderPath;
            set
            {
                _folderPath = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(FolderPath)));
            }
        }

        public bool IsLoaded
        {
            get => _isLoaded;
            set
            {
                _isLoaded = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(IsLoaded)));
            }
        }

        public XYZ StartPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(StartPoint)));
            }
        }

        public FeetInches Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Width)));
            }
        }

        public FeetInches Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Height)));
            }
        }

        public FolderCommand FolderCommand { get; set; }

        public RunCommand RunCommand { get; set; }

        public PointCommand PointCommand { get; set; }

        public MainWindowViewModel()
        {
            FolderCommand = new FolderCommand(this);
            RunCommand = new RunCommand(this);
            PointCommand = new PointCommand(this);

            StartPoint = new XYZ();
            Width = new FeetInches();
            Height = new FeetInches();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }
    }
}
