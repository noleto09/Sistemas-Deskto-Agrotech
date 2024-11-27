using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;


public class Ajuste_Paginas {
    public void MostrarPagina(ContentView pagina, Layout layout) {
        pagina.IsVisible = true;

        if (layout is AbsoluteLayout absoluteLayout) {
            AbsoluteLayout.SetLayoutBounds(absoluteLayout, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(absoluteLayout, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }
        
    }
}

