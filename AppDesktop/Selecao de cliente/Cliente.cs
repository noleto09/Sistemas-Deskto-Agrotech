// Classe modelo para Cliente
public class Cliente
{
    public int ID_Cliente { get; set; }
    public string Nome_Cliente { get; set; }
    public string CpfOuCnpj { get; set; }
    public string TipoCliente { get; set; } // 'PF' ou 'PJ'
}