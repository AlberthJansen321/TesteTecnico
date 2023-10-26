using App.Models;
using App.Service.Interfaces;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly IHomeService _homeService;
        private readonly IConnectivity _connectivity;
        public RelayCommand<Produto> SelectionProdutoCommand { get; private set; }
        public RelayCommand<Produto> DeleteProdutoCommand { get; private set; }
        public ObservableCollection<Produto> Produtos { get; } = new();

        public HomeViewModel(IHomeService homeService, IConnectivity connectivity)
        {
            _homeService = homeService;
            _connectivity = connectivity;
            Task.Run(async () =>
            {
                await GetProdutos();
            });
            SelectionProdutoCommand = new RelayCommand<Produto>(async (param) => SelectProduto(param));
            DeleteProdutoCommand = new RelayCommand<Produto>(async (param) => DeleteProduto(param));
        }

        public async Task GetProdutos()
        {
            await App.Current.Dispatcher.DispatchAsync(async () =>
            {
                try
                {
                    if (IsBusy)
                        return;

                    if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await Shell.Current.DisplayAlert("Atenção", "Verifique sua conexão com a internet", "Ok");
                        return;
                    }

                    IsBusy = true;

                    await Task.Run(async () =>
                    {
                        var produtos = await _homeService.GetAllProdutosAsync();

                        await Application.Current.Dispatcher.DispatchAsync(async () =>
                        {
                            if (Produtos.Count != 0)
                            {
                                Produtos?.Clear();
                            }

                            if (produtos?.Count() > 0)
                            {
                                foreach (var dados in produtos.OrderBy(x => x.Id))
                                {
                                    var produto = new Produto();
                                    produto.Nome = dados.Nome;
                                    produto.Descricao = dados.Descricao;
                                    produto.Id = dados.Id;
                                    produto.Preco = dados.Preco;

                                    var result = Produtos.FirstOrDefault(x => x.Id == dados.Id);

                                    if (result != null)
                                        result = dados;
                                     else 
                                        Produtos?.Add(produto);

                                }
                            }
                        });
                    });
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        private async void SelectProduto(Produto param)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void DeleteProduto(Produto param)
        {

            try
            {

            }
            catch
            {

            }
        }

    }
}
