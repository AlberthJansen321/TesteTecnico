namespace Api.Application.Helpers;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
    }
}
