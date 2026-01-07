using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using AiWPFDemo.Models;
using AiWPFDemo.Services;

namespace AiWPFDemo.ViewModels;

public sealed class MainViewModel : INotifyPropertyChanged
{
    private readonly WorkListService _workListService;
    private string _rootFolderPath = string.Empty;
    private FolderItem? _selectedFolder;
    private WorkItem? _selectedWorkItem;
    private bool _isImageSelected = true;
    private bool _isDrawingSelected;
    private bool _isModelSelected;
    private Uri? _pdfSource;

    public MainViewModel()
    {
        _workListService = new WorkListService();
        FolderItems = new ObservableCollection<FolderItem>();
        WorkItems = new ObservableCollection<WorkItem>();
        BrowseRootFolderCommand = new RelayCommand(_ => BrowseRootFolder());
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<FolderItem> FolderItems { get; }
    public ObservableCollection<WorkItem> WorkItems { get; }

    public ICommand BrowseRootFolderCommand { get; }

    public string RootFolderPath
    {
        get => _rootFolderPath;
        set
        {
            if (value == _rootFolderPath)
            {
                return;
            }

            _rootFolderPath = value;
            OnPropertyChanged();
            LoadFolders();
        }
    }

    public FolderItem? SelectedFolder
    {
        get => _selectedFolder;
        set
        {
            if (Equals(value, _selectedFolder))
            {
                return;
            }

            _selectedFolder = value;
            OnPropertyChanged();
            LoadWorkItems();
        }
    }

    public WorkItem? SelectedWorkItem
    {
        get => _selectedWorkItem;
        set
        {
            if (Equals(value, _selectedWorkItem))
            {
                return;
            }

            _selectedWorkItem = value;
            OnPropertyChanged();
            UpdatePdfSource();
        }
    }

    public bool IsImageSelected
    {
        get => _isImageSelected;
        set
        {
            if (value == _isImageSelected)
            {
                return;
            }

            _isImageSelected = value;
            if (value)
            {
                _isDrawingSelected = false;
                _isModelSelected = false;
                OnPropertyChanged(nameof(IsDrawingSelected));
                OnPropertyChanged(nameof(IsModelSelected));
            }

            OnPropertyChanged();
            UpdatePdfSource();
        }
    }

    public bool IsDrawingSelected
    {
        get => _isDrawingSelected;
        set
        {
            if (value == _isDrawingSelected)
            {
                return;
            }

            _isDrawingSelected = value;
            if (value)
            {
                _isImageSelected = false;
                _isModelSelected = false;
                OnPropertyChanged(nameof(IsImageSelected));
                OnPropertyChanged(nameof(IsModelSelected));
            }

            OnPropertyChanged();
            UpdatePdfSource();
        }
    }

    public bool IsModelSelected
    {
        get => _isModelSelected;
        set
        {
            if (value == _isModelSelected)
            {
                return;
            }

            _isModelSelected = value;
            if (value)
            {
                _isImageSelected = false;
                _isDrawingSelected = false;
                OnPropertyChanged(nameof(IsImageSelected));
                OnPropertyChanged(nameof(IsDrawingSelected));
            }

            OnPropertyChanged();
            UpdatePdfSource();
        }
    }

    public Uri? PdfSource
    {
        get => _pdfSource;
        private set
        {
            if (Equals(value, _pdfSource))
            {
                return;
            }

            _pdfSource = value;
            OnPropertyChanged();
        }
    }

    private void BrowseRootFolder()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Select root folder",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = false
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            RootFolderPath = dialog.SelectedPath;
        }
    }

    private void LoadFolders()
    {
        FolderItems.Clear();
        WorkItems.Clear();
        SelectedWorkItem = null;
        PdfSource = null;
        SelectedFolder = null;

        if (string.IsNullOrWhiteSpace(RootFolderPath) || !Directory.Exists(RootFolderPath))
        {
            return;
        }

        foreach (var directory in Directory.GetDirectories(RootFolderPath))
        {
            FolderItems.Add(new FolderItem(Path.GetFileName(directory), directory));
        }
    }

    private void LoadWorkItems()
    {
        WorkItems.Clear();
        SelectedWorkItem = null;
        PdfSource = null;

        if (_selectedFolder is null)
        {
            return;
        }

        var workListPath = Path.Combine(_selectedFolder.FullPath, "WorkList.xml");
        if (!File.Exists(workListPath))
        {
            return;
        }

        foreach (var item in _workListService.LoadWorkItems(workListPath))
        {
            WorkItems.Add(item);
        }
    }

    private void UpdatePdfSource()
    {
        if (_selectedWorkItem is null || _selectedFolder is null)
        {
            PdfSource = null;
            return;
        }

        var fileName = GetSelectedFileName(_selectedWorkItem);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            PdfSource = null;
            return;
        }

        var fullPath = Path.Combine(_selectedFolder.FullPath, fileName);
        if (!File.Exists(fullPath))
        {
            PdfSource = null;
            return;
        }

        PdfSource = new Uri(fullPath, UriKind.Absolute);
    }

    private string GetSelectedFileName(WorkItem item)
    {
        if (_isDrawingSelected)
        {
            return item.Drawing;
        }

        if (_isModelSelected)
        {
            return item.Model;
        }

        return item.Image;
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
