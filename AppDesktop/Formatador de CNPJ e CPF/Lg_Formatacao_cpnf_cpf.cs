public class Lg_Formatacao_cpnf_cpf {
    public string FormatarCpf(string text) {
        
        text = new string(text.Where(char.IsDigit).ToArray()); // Remove caracteres não numéricos

        if (text.Length > 11) text = text.Substring(0, 11); // Limita a 11 caracteres

        if (text.Length <= 3) return text;
        if (text.Length <= 6) return string.Format("{0}.{1}", text.Substring(0, 3), text.Substring(3));
        if (text.Length <= 9) return string.Format("{0}.{1}.{2}", text.Substring(0, 3), text.Substring(3, 3), text.Substring(6));
        return string.Format("{0}.{1}.{2}-{3}", text.Substring(0, 3), text.Substring(3, 3), text.Substring(6, 3), text.Substring(9));
    }

    public string FormatarCnpj(string text) {
        text = new string(text.Where(char.IsDigit).ToArray()); // Remove caracteres não numéricos

        if (text.Length > 14) text = text.Substring(0, 14); // Limita a 14 caracteres

        if (text.Length > 2) text = text.Insert(2, "."); // Adiciona o primeiro ponto após os dois primeiros dígitos

        if (text.Length > 6) text = text.Insert(6, "."); // Adiciona o segundo ponto após o quinto dígito

        if (text.Length > 10) text = text.Insert(10, "/"); // Adiciona a barra após o oitavo dígito

        if (text.Length > 15) text = text.Insert(15, "-"); // Adiciona o hífen após o décimo segundo dígito

        return text;
    }
}
