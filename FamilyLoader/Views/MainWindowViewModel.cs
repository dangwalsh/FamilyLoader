using System;

namespace Gensler.Revit.FamilyLoader.Views
{
    using System.ComponentModel;
    using Autodesk.Revit.DB;
    using Commands;

    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _folderPath;
        private bool _isLoaded;
        private XYZ _startPoint;
        private double _width;
        private double _height;
        private int _count;
        private TimeSpan _time;

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
                Arrangement.Basis = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(StartPoint)));
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                Quad.Width = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Width)));
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                Quad.Height = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Height)));
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        public TimeSpan Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }

        public FolderCommand FolderCommand { get; set; }

        public RunCommand RunCommand { get; set; }

        public PointCommand PointCommand { get; set; }

        public UpCommand UpCommand { get; set; }

        public DownCommand DownCommand { get; set; }

        public CloseCommand CloseCommand { get; set; }

        public MainWindowViewModel()
        {
            FolderCommand = new FolderCommand(this);
            RunCommand = new RunCommand(this);
            PointCommand = new PointCommand(this);
            UpCommand = new UpCommand(this);
            DownCommand = new DownCommand(this);
            CloseCommand = new CloseCommand(this);

            StartPoint = new XYZ();
            Width = 5.0;
            Height = 5.0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

    }
}
