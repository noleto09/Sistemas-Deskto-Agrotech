public class VendaItem
{
    public string Produto { get; set; }
    public string Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; } // Alterado para decimal
    public decimal Desconto { get; set; } // Alterado para decimal
    public decimal ValorTotal { get; set; } // Alterado para decimal
}
