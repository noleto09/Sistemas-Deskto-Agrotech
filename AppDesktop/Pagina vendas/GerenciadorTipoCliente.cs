
public class GerenciadorTipoCliente {
    public void OnClientTypeChanged(RadioButton radioButtonPF, RadioButton radioButtonPJ, Entry cpfCnpjEntry, CheckBox checkBoxMale, CheckBox checkBoxFemale, CheckBox checkBoxOtherSex) {
        string clientType = string.Empty;

        if (radioButtonPF.IsChecked) {
            // Se PF estiver selecionado, habilita os CheckBox para sexo
            checkBoxMale.IsEnabled = true;
            checkBoxFemale.IsEnabled = true;
            checkBoxOtherSex.IsEnabled = true;

            // Configura a máscara de formatação para CPF
            cpfCnpjEntry.TextChanged -= CpfCnpjEntry_TextChanged;
            cpfCnpjEntry.TextChanged += CpfCnpjEntry_TextChanged;

            // Guarda o nome correspondente em uma variável
            clientType = "Pessoa Física";
        } else if (radioButtonPJ.IsChecked) {
            // Se PJ estiver selecionado, desabilita os CheckBox para sexo
            checkBoxMale.IsEnabled = false;
            checkBoxFemale.IsEnabled = false;
            checkBoxOtherSex.IsEnabled = false;

            // Desmarca todos os CheckBox para evitar seleção acidental
            checkBoxMale.IsChecked = false;
            checkBoxFemale.IsChecked = false;
            checkBoxOtherSex.IsChecked = false;

            // Configura a máscara de formatação para CNPJ
            cpfCnpjEntry.TextChanged -= CpfCnpjEntry_TextChanged;
            cpfCnpjEntry.TextChanged += CpfCnpjEntry_TextChanged;

            // Guarda o nome correspondente em uma variável
            clientType = "Pessoa Jurídica";
        }

        // Aqui você pode usar a variável clientType conforme necessário
        Console.WriteLine($"Tipo de cliente selecionado: {clientType}");
    }


    private void CpfCnpjEntry_TextChanged(object sender, TextChangedEventArgs e) {
        // Lógica para aplicar a máscara de CPF ou CNPJ
    }
}
