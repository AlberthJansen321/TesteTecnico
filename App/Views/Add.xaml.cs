using App.ViewModels;

namespace App.Views;

public partial class Add : ContentPage
{
	public Add(HomeViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}