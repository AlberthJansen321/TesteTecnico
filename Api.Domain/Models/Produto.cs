﻿using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Models;

public class Produto
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
}

