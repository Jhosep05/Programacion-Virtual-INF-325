using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Ahorcado_Xamarin
{
    public partial class MainPage : ContentPage
    {
        readonly string [] PALABRAS = { "LIBRO", "BOLIVIA", "PERRO", "INFORMATICA" };
        readonly string[] LETRAS = {
            "A", "B","C","D","E","F","G",
            "H","I","J","K","L","M","N",
            "Ñ","O","P","Q","R","S","T",
            "U","V","W","X","Y","Z","*",
        };
        string palabraAAdivinar;
        string palabraAdivinadaPorAhora;
        int numFallos;
        int numAciertos;
        public MainPage()
        {
            InitializeComponent();
            Inicializar();
        }

        private void Inicializar()
        {
            numAciertos = 0;
            numFallos = 0;
            Img_Ahorcado.Source = ImageSource.FromResource("Ahorcado_Xamarin.images.0.png");
            InicializarPalabraSecreta();
            InicializarBotonera();

        }
        private void InicializarPalabraSecreta()
        {
            Random rnd = new Random();
            int numeroPalabra = rnd.Next(0, PALABRAS.Length);
            palabraAAdivinar = PALABRAS[numeroPalabra];
            palabraAdivinadaPorAhora = "";

            for (int i = 0; i < palabraAAdivinar.Length; i++)
            {
                palabraAdivinadaPorAhora += "- ";

            }
            Lbl_Palabra.Text = palabraAdivinadaPorAhora;

        }
        private void InicializarBotonera()
        {
            for (var fila = 0; fila < 4; fila++)
            {
                for (var columna = 0; columna < 7; columna++)
                {
                    Button boton = new Button
                    {
                        Text = LETRAS[fila * 7 + columna]
                    };
                    Grd_botonera.Children.Add(boton, columna, fila);
                    boton.Clicked += Boton_ClickedAsync;
                }
            }
        }

        private async void Boton_ClickedAsync(object sender, EventArgs e)
        {
            Button botonPulsado = (Button)sender;
            string letraPulsada = botonPulsado.Text;
            if (LetraAcertada(letraPulsada))
            {
                Lbl_Palabra.Text = palabraAdivinadaPorAhora;
                botonPulsado.BackgroundColor = Color.Green;
                
                if (numAciertos == palabraAAdivinar.Length)
                {
                    await DisplayAlert("Has ganado", "pulsa Ok para jugar otra vez", "Ok");
                    Inicializar();
                }
            }
            else {
                botonPulsado.BackgroundColor = Color.Red;
                Img_Ahorcado.Source = ImageSource.FromResource("Ahorcado_Xamarin.images."+numFallos+".png");
                numFallos++; 
                if (numFallos==7) {
                    await DisplayAlert("Has perdido","pulsa Ok para jugar otra vez","Ok");
                    Inicializar();
                   
                }
            }
            
        }

        private bool LetraAcertada(string letraPulsada)
        {
            bool hayAcierto = false;
            for (var i = 0; i < palabraAAdivinar.Length; i++)
            {
                if (palabraAAdivinar.Substring(i,1)==letraPulsada)
                {
                    palabraAdivinadaPorAhora = palabraAdivinadaPorAhora.Substring(0, i *2) +
                        letraPulsada + palabraAdivinadaPorAhora.Substring(i*2+1);
                    hayAcierto = true;
                    numAciertos++;
                }

            }

            return hayAcierto;
        }
    }
}
