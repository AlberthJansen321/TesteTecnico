namespace Api.Domain.Models;

public class Produto
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public DateTime DtCadastro { get; set; }
    public DateTime? DtAlteracao { get; set; }
}

