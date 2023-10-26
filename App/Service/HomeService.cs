using App.Models;
using App.Service.Interfaces;
using static Android.Graphics.ColorSpace;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using System.Text;

namespace App.Service;

public class HomeService : IHomeService
{
    private readonly HttpClient _httpClient;
    public HomeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Produto> Add(Produto model)
    {

        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await
                   _httpClient.PostAsync(App.url_base + "/produto/cadastrar", new StringContent(
                       JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            var conteudo = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Produto>(conteudo);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"{ex.Message}", "Ok");
        }

        return null;
    }

    public async Task<bool> Delete(int CodProduto)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            var response = await _httpClient.DeleteAsync(App.url_base + $"/produto/deletar/{CodProduto}");

            var conteudo = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"{ex.Message}", "Ok");
        }

        return false;
    }

    public async Task<Produto[]> GetAllProdutosAsync()
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(App.url_base + "/produto/todos");

            var conteudo = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Produto[]>(conteudo);
            }
        }
        catch(Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"{ex.Message}", "Ok");
        }

        return null;
    }

    public async Task<Produto> GetByIdProduto(int CodProduto)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync(App.url_base + $"/produto/{CodProduto}");

            var conteudo = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Produto>(conteudo);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"{ex.Message}", "Ok");
        }

        return null;
    }

    public async Task<Produto> Update(int CodProduto, Produto model)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await
                   _httpClient.PutAsync(App.url_base + $"/produto/alterar/{CodProduto}", new StringContent(
                       JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            var conteudo = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Produto>(conteudo);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro", $"{ex.Message}", "Ok");
        }

        return null;
    }
}
