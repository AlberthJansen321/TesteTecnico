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
          public RelayCommand<Produto> GetProdutosCommand { get; private set; }
        public ObservableCollection<Produto> Produtos { get; } = new();

        public HomeViewModel(IHomeService homeService, IConnectivity connectivity)
        {
            _homeService = homeService;
            _connectivity = connectivity;
            Task.Run(async () =>
            {
                await GetProdutos();
            });
            SelectionProdutoCommand = new RelayCommand<Produto>(async (param) => await SelectProduto(param));
            DeleteProdutoCommand = new RelayCommand<Produto>(async (param) => await DeleteProduto(param));
            GetProdutosCommand = new RelayCommand<Produto>(async (param) => await GetProdutos());
        }
        public async Task GetProdutos()
        {
            await App.Current.Dispatcher.DispatchAsync(async () =>
            {
                try
                {
                    if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await Shell.Current.DisplayAlert("Atenção", "Verifique sua conexão com a internet", "Ok");
                        return;
                    }

                    IsBusy = true;

                    await Task.Run(async () =>
                    {
                        var produtos = await _homeService.GetAllProdutosAsync();

                        await Application.Current.Dispatcher.DispatchAsync(() =>
                        {
                            if (Produtos.Count != 0)
                            {
                                Produtos?.Clear();
                            }

                            if (produtos?.Count() > 0)
                            {
                                foreach (var dados in produtos)
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
                    await Shell.Current.DisplayAlert("Error!", $"Não possível retornar os produtos. Erro: {ex.Message}", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        private async Task SelectProduto(Produto param)
        {
            try
            {
                await Shell.Current.DisplayAlert("Error!", "Selecionou o produto", "OK");
            }
            catch
            {

            }
        }

        private async Task DeleteProduto(Produto param)
        {

            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Atenção", "Verifique sua conexão com a internet", "Ok");
                    return;
                }

                bool delete = await _homeService.Delete(param.Id);

                if(delete == true)
                {
                    await Shell.Current.DisplayAlert("Sucesso", "Produto deletado", "Ok");
                    await GetProdutos();
                }
            }
            catch
            {

            }
        }

    }
}
