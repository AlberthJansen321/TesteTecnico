using App.Models;
using App.ViewModels;

namespace App.Views;

public partial class Update : ContentPage
{
    public Update(HomeViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}