using App.ViewModels;
namespace App.Views;

public partial class Home : ContentPage
{
    HomeViewModel _vm;
    List<SwipeView> swipeViews { set; get; }
    public Home(HomeViewModel vm)
    {
        _vm = vm;
        InitializeComponent();
        swipeViews = new List<SwipeView>();
        this.BindingContext = vm;
    }
    protected async override void OnAppearing()
    {
        await Task.Run(async () =>
        {
            await App.Current.Dispatcher.DispatchAsync(() =>
            {
                if (collectionview.EmptyView == null)
                {
                    collectionview.EmptyView = Resources["BasicEmptyView"];
                }

                if (App.updateview == true)
                {
                    collectionview.ItemsSource = null;
                    collectionview.EmptyView = null;

                    _vm.GetProdutos();

                    collectionview.ItemsSource = _vm.Produtos;
                    collectionview.EmptyView = Resources["BasicEmptyView"];

                    App.updateview = false;
                }
            });
        });
    }

    private async void collectionview_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (swipeViews.Count() == 1)
        {
            foreach (var s in swipeViews)
            {
                s.Close();
            }
            swipeViews.Clear();
        }
        //if (e.VerticalDelta == 9)
        //{
        //    await Task.Delay(2000);
        //    _vm..Execute(null);
        //    e.LastVisibleItemIndex = 0;
        //}
        // await Shell.Current.DisplayAlert("Sucesso",e.LastVisibleItemIndex.ToString(), "Ok");
    }


    private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
    {
        if (swipeViews.Count() == 1)
        {
            foreach (var s in swipeViews)
            {
                s.Close();
            }
            swipeViews.Clear();
        }
    }
    private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
    {
        var swipView = (SwipeView)sender;
        swipeViews.Add(swipView);
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        _vm.ModelProduto = new Models.Produto();
        await Shell.Current.GoToAsync($"/{nameof(Add)}");
    }
}