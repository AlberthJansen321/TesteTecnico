using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        [ObservableProperty]
        bool isLoading;

        public bool IsNotBusy => !IsBusy;
    }
}
