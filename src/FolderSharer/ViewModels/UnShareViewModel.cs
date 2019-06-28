using System.Windows;
using CodeDek.Lib;
using CodeDek.Lib.Mvvm;

namespace FolderSharer.ViewModels
{
    public sealed class UnShareViewModel : ObservableObject
    {
        private Share _selectedSharedPath;
        private readonly MainViewModel _mainViewModel;

        public UnShareViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public ObservableSet<Share> SharedPaths { get; } = new ObservableSet<Share>();

        public Share SelectedSharedPath
        {
            get => _selectedSharedPath;
            set => Set(ref _selectedSharedPath, value)
                .Alert(nameof(UnShareCmd))
                .Alert(nameof(ClearItemsCmd));
        }

        public Cmd GetSharedPathsCmd => new Cmd(() =>
        {
            SharedPaths.Empty();
            foreach (var item in Storage.GetSharedPaths())
            {
                SharedPaths.Add(new Share { Path = item.path, Name = item.name, Type = item.type });
            }

            OnPropertyChanged(nameof(ClearItemsCmd));
        });

        public Cmd UnShareCmd => new Cmd(() =>
        {
            var ask = MessageBox.Show("Proceed with unshare?", "Confirmation 😕", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ask == MessageBoxResult.Yes)
            {
                var result = Storage.UnshareDirectory(SelectedSharedPath.Path);
                if (result.success)
                {
                    _mainViewModel.Status = $"({SelectedSharedPath.Path}) was successfully unshared. 😎";
                    _mainViewModel.Passage = "";
                    _mainViewModel.PassageUrl = "";

                    SelectedSharedPath = null;
                    GetSharedPathsCmd.Execute();
                }
                else
                {
                    MessageBox.Show(result.message, "Error 😢", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }, () => SelectedSharedPath != null);

        public Cmd ClearItemsCmd => new Cmd(() =>
        {
            SelectedSharedPath = null;
            SharedPaths.Empty();
            OnPropertyChanged(nameof(ClearItemsCmd));
        }, () => SelectedSharedPath != null || SharedPaths.Count > 0);

        public class Share
        {
            public string Path { get; set; }

            public string Name { get; set; }

            public string Type { get; set; }
        }
    }
}
