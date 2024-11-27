using System;
using Microsoft.Maui.Dispatching;
using System.Timers; // Adicione este `using` se já não estiver incluído.

public class GeradorDataHora
{
    private readonly IDispatcher dispatcher;
    private readonly System.Timers.Timer timer; // Use o namespace completo para evitar ambiguidade

    public string DataAtual { get; private set; }
    public string HoraAtual { get; private set; }

    // Evento para notificar quando a hora ou data for atualizada
    public event Action<string, string> DataHoraAtualizada;

    public GeradorDataHora(IDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;

        // Inicializa o Timer para atualizar a hora a cada segundo
        timer = new System.Timers.Timer(1000); // 1000 milissegundos = 1 segundo
        timer.Elapsed += AtualizarDataHora;
        timer.Start();

        // Inicializa a data e a hora
        AtualizarDataHora(null, null);
    }

    private void AtualizarDataHora(object sender, ElapsedEventArgs e)
    {
        // Obtém a data e a hora atual
        DateTime now = DateTime.Now;
        DataAtual = now.ToString("dd/MM/yyyy");
        HoraAtual = now.ToString("HH:mm:ss");

        // Atualiza na thread principal
        dispatcher.Dispatch(() =>
        {
            // Notifica que a data e a hora foram atualizadas
            DataHoraAtualizada?.Invoke(DataAtual, HoraAtual);
        });
    }

    // Método para parar o Timer quando não for mais necessário
    public void PararAtualizacao()
    {
        timer.Stop();
        timer.Dispose();
    }
}
