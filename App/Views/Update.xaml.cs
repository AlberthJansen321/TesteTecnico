using App.Models;
using App.ViewModels;

namespace App.Views;

public partial class Update : ContentPage
{
    HomeViewModel _vm;
    public Update(HomeViewModel vm)
	{
        _vm = vm;
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override bool OnBackButtonPressed()
    {
        _vm.ModelProduto = null;
        return base.OnBackButtonPressed();
    }
}