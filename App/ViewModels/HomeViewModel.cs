using App.Models;
using App.Service.Interfaces;
using App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using System.Collections.ObjectModel;

namespace App.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private bool _isLock;
        private bool _isAllItemLoaded;
        private int pageSize = 10;
        [ObservableProperty]
        public Produto modelProduto = new Produto();

        private Produto[] _allProdutoList;

        private readonly IHomeService _homeService;
        private readonly IConnectivity _connectivity;
        public RelayCommand<Produto> SelectionProdutoCommand { get; private set; }
        public RelayCommand<Produto> DeleteProdutoCommand { get; private set; }
        public RelayCommand<Produto> GetProdutosCommand { get; private set; }
        public RelayCommand<Produto> UpdateProdutocommand { get; private set; }
        public RelayCommand<Produto> AddProdutocommand { get; private set; }
        public ObservableRangeCollection<Produto> Produtos { get; } = new();

        public HomeViewModel(IHomeService homeService, IConnectivity connectivity)
        {
            _homeService = homeService;
            _connectivity = connectivity;
            GetProdutos();

            SelectionProdutoCommand = new RelayCommand<Produto>(async (param) => await SelectProduto(param));
            DeleteProdutoCommand = new RelayCommand<Produto>(async (param) => await DeleteProduto(param));
            GetProdutosCommand = new RelayCommand<Produto>((param) => GetProdutos());
            UpdateProdutocommand = new RelayCommand<Produto>(async (param) => await UpdateProduto());
            AddProdutocommand = new RelayCommand<Produto>(async (param) => await AddProduto());
        }
        private async Task AddProduto()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Atenção", "Verifique sua conexão com a internet", "Ok");
                    return;
                }

                var result = await _homeService.Add(ModelProduto);

                if (result != null)
                {
                    App.updateview = true;
                    await Shell.Current.DisplayAlert("Sucesso", "Produto cadastrado com sucesso", "OK");
                }
                else await Shell.Current.DisplayAlert("Sucesso", "Erro ao cadastrar o produto", "OK");
            }
            catch
            {

            }
            finally
            {
                GetProdutos();
            }
        }
        private async Task UpdateProduto()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Atenção", "Verifique sua conexão com a internet", "Ok");
                    return;
                }

                var result = await _homeService.Update(ModelProduto.Id, ModelProduto);

                if (result != null)
                {
                    App.updateview = true;
                    await Shell.Current.DisplayAlert("Sucesso", "Produto alterado com sucesso", "OK");
                }
                else await Shell.Current.DisplayAlert("Erro", "Erro ao alterar o produto", "OK");
            }
            catch
            {

            }
            finally
            {

            }
        }

        public async void GetProdutos()
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

                _isAllItemLoaded = false;
                _isLock = false;
                IsBusy = true;

                await Task.Run(async () =>
                {
                    _allProdutoList = await _homeService.GetAllProdutosAsync();

                    await Application.Current.Dispatcher.DispatchAsync(() =>
                    {
                        if (Produtos?.Count > 0)
                        {
                            Produtos?.Clear();
                        }
                        if (_allProdutoList?.Count() > 0)
                        {
                            Produtos.ReplaceRange(_allProdutoList.Take(pageSize).ToList());
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
        }

        private async Task SelectProduto(Produto param)
        {
            try
            {
                if (param != null)
                {
                    ModelProduto = param;
                    await Shell.Current.GoToAsync($"/{nameof(Update)}");
                }
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

                if (delete == true)
                {
                    App.updateview = true;
                    await Shell.Current.DisplayAlert("Sucesso", "Produto deletado", "Ok");
                }
                else await Shell.Current.DisplayAlert("Erro", "Erro ao deletar o produto", "Ok");

            }
            catch
            {

            }
            finally
            {
                await Task.Delay(1000);
                GetProdutos();
            }
        }
        [RelayCommand]
        private async Task LoadMoreProdutos()
        {
            if (Produtos.Count > 0)
            {
                if (!_isAllItemLoaded && !_isLock)
                {
                    IsLoading = _isLock = true;
                    await Task.Run(async () =>
                    {
                        await Task.Delay(2000);
                        var produtoslist = _allProdutoList.Skip(Produtos.Count).Take(pageSize).ToList();

                        await Application.Current.Dispatcher.DispatchAsync(() =>
                        {
                            if (produtoslist.Count < pageSize)
                                _isAllItemLoaded = true;

                            if (produtoslist.Count > 0)
                            {
                                Produtos.AddRange(produtoslist);
                            }
                            _isLock = IsLoading = false;
                        });
                    });
                }
            }
        }
    }
}
