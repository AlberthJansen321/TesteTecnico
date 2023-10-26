namespace Api.Application.DTO;

public class ProdutoDTO 
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo nome é obrigatório")]
    [MaxLength(255,ErrorMessage = "Deve conter no máximo 255 caracteres")]
    [MinLength(2,ErrorMessage = "Deve conter no mínimo 2 caracteres")]
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Informe o valor do produto")]
    [Range(1,99999999999999999,ErrorMessage ="Valor do produto deve ver ser maior que 0")]
    public decimal Preco { get; set; }
}

