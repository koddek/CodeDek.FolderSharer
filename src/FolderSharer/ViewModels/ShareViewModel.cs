using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodeDek.Lib;
using CodeDek.Lib.Mvvm;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FolderSharer.ViewModels
{
    public sealed class ShareViewModel : ObservableObject
    {
        private string _selectedPath;
        private string _name;
        private string _description;
        private readonly MainViewModel _mainViewModel;

        public ShareViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public string SelectedPath
        {
            get => _selectedPath;
            set => Set(ref _selectedPath, value)
                .Alert(nameof(CreateShareCmd));
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value)
                .Alert(nameof(CreateShareCmd));
        }

        public string Description
        {
            get => _description;
            set => Set(ref _description, value)
                .Alert(nameof(CreateShareCmd));
        }

        public Cmd SelectPathCmd => new Cmd(() =>
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Select a folder to share.";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dlg.EnsureValidNames = true;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedPath = dlg.FileName;
            }
        });

        public Cmd CreateShareCmd => new Cmd(() =>
        {
            var result = Storage.ShareDirectory(SelectedPath, Name, Description);
            if (result.success)
            {
                _mainViewModel.Status = $"({SelectedPath}) was successfully shared. 😎";
                _mainViewModel.Passage = "";
                _mainViewModel.PassageUrl = "";

                SelectedPath = "";
                Name = "";
                Description = "";
            }
            else
            {
                MessageBox.Show(result.message, "Error 😢", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, () => !string.IsNullOrWhiteSpace(SelectedPath));
    }
}
