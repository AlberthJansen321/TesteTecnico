using System.ComponentModel.DataAnnotations;

namespace Api.Application.DTO;

public class ProdutoDTO 
{
    [Required(ErrorMessage = "Campo nome é obrigatório")]
    [MaxLength(255,ErrorMessage = "Deve conter no máximo 255 caracteres")]
    [MinLength(2,ErrorMessage = "Deve conter no mínimo 2 caracteres")]
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Informe o valor do produto")]
    public decimal Preco { get; set; }
}

