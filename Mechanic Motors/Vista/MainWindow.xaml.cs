using MaterialDesignThemes.Wpf;
using Mechanic_Motors.Vista;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mechanic_Motors
{
    
    public partial class MainWindow : Window
    {
        // Esto nos permitira transoformar toda la informacion aportada por el sistema a español.
        CultureInfo idioma = new CultureInfo("es-ES");

        // Estas propiedades nos permitiran la correcta gestion de las imagenes
        List<String> cochesIzquierda = new List<String> { "/Resources/CochesMain/Camaro.png", "/Resources/CochesMain/Mustang.png", "/Resources/CochesMain/Charger.png" };
        List<String> cochesDerecha = new List<String> { "/Resources/CochesMain/Veneno.png", "/Resources/CochesMain/Bugatti.png", "/Resources/CochesMain/FerrariFXX.png" };
        Boolean cambioIzquierda = true;
        int contadorImagenesIzquierda = 1;
        int contadorImagenesDerecha = 1;
        DispatcherTimer nuevaImagenIzquierda;
        DispatcherTimer nuevaImagenDerecha;

        public MainWindow()
        {
            InitializeComponent();
            FechaActualTextBlockMain.Text = $"{idioma.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).ToUpper()}, {DateTime.Now.Day} de {DateTime.Now.ToString("MMMM").ToUpper()} de {DateTime.Now.Year}";
            HoraActualTextBlockMain.Text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

            CochesIzquierdaImagen.Source = new BitmapImage(new Uri(cochesIzquierda.ElementAt(0), UriKind.Relative));
            CochesDerechaImagen.Source = new BitmapImage(new Uri(cochesDerecha.ElementAt(0), UriKind.Relative));

            // Timer que ira actualizando la hora del Main Window
            DispatcherTimer actualizarHora = new DispatcherTimer();
            actualizarHora.Interval = TimeSpan.FromSeconds(1);
            actualizarHora.Tick += ActualizarHora_Tick;
            actualizarHora.Start();

            // Timer que ira cambiando las imagenes del Main Window
            DispatcherTimer cambiarImagen = new DispatcherTimer();
            cambiarImagen.Interval = TimeSpan.FromSeconds(3);
            cambiarImagen.Tick += CambiarImagen_Tick;
            cambiarImagen.Start();
        }

        // Este evento de timer nos permitira que las imagenes de la ventana principal se cambien cada X segundos
        private void CambiarImagen_Tick(object sender, EventArgs e)
        {
            DoubleAnimation ocultar = new DoubleAnimation();
            ocultar.From = 1;
            ocultar.To = 0;
            ocultar.Duration = TimeSpan.FromSeconds(1);

            if (cambioIzquierda)
            {
                CochesIzquierdaImagen.BeginAnimation(Image.OpacityProperty, ocultar);

                nuevaImagenIzquierda = new DispatcherTimer();
                nuevaImagenIzquierda.Interval = TimeSpan.FromSeconds(1);
                nuevaImagenIzquierda.Tick += NuevaImagenIzquierda_Tick;
                nuevaImagenIzquierda.Start();

                contadorImagenesIzquierda++;
                if (contadorImagenesIzquierda >= 3)
                    contadorImagenesIzquierda = 0;
                cambioIzquierda = false;
            }
            else
            {
                CochesDerechaImagen.BeginAnimation(Image.OpacityProperty, ocultar);

                nuevaImagenDerecha = new DispatcherTimer();
                nuevaImagenDerecha.Interval = TimeSpan.FromSeconds(1);
                nuevaImagenDerecha.Tick += NuevaImagenDerecha_Tick;
                nuevaImagenDerecha.Start();

                contadorImagenesDerecha++;
                if (contadorImagenesDerecha >= 3)
                    contadorImagenesDerecha = 0;
                cambioIzquierda = true;
            }
        }

        // Este evento nos permitira una correcta transicion de imagenes
        private void NuevaImagenIzquierda_Tick(object sender, EventArgs e)
        {
            CochesIzquierdaImagen.Source = new BitmapImage(new Uri(cochesIzquierda.ElementAt(contadorImagenesIzquierda), UriKind.Relative));
            DoubleAnimation mostrar = new DoubleAnimation();
            mostrar.From = 0;
            mostrar.To = 1;
            mostrar.Duration = TimeSpan.FromSeconds(1);
            CochesIzquierdaImagen.BeginAnimation(Image.OpacityProperty, mostrar);

            nuevaImagenIzquierda.Stop();
        }

        // Este evento nos permitira una correcta transicion de imagenes
        private void NuevaImagenDerecha_Tick(object sender, EventArgs e)
        {
            CochesDerechaImagen.Source = new BitmapImage(new Uri(cochesDerecha.ElementAt(contadorImagenesIzquierda), UriKind.Relative));
            DoubleAnimation mostrar = new DoubleAnimation();
            mostrar.From = 0;
            mostrar.To = 1;
            mostrar.Duration = TimeSpan.FromSeconds(1);
            CochesDerechaImagen.BeginAnimation(Image.OpacityProperty, mostrar);

            nuevaImagenDerecha.Stop();
        }

        // Este evento de timer nos permitira que la hora se vaya actualizando de manera correcta
        private void ActualizarHora_Tick(object sender, EventArgs e)
        {
            string hora = "";

            if (DateTime.Now.Hour.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Hour}:";
            }
            else
            {
                hora += $"{DateTime.Now.Hour}:";
            }

            if (DateTime.Now.Minute.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Minute}:";
            }
            else
            {
                hora += $"{DateTime.Now.Minute}:";
            }

            if (DateTime.Now.Second.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Second}";
            }
            else
            {
                hora += $"{DateTime.Now.Second}";
            }

            HoraActualTextBlockMain.Text = hora;
        }

        // Aqui falta implementar el metodo que hara el cambio de tema de claro a oscuro y viceversa. 
        private void DarkModeToggle_Click(object sender, RoutedEventArgs e)
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                bundled.BaseTheme = BaseTheme.Light;
                LogoImage.Source = new BitmapImage(new Uri("/Resources/LogoClaro.png", UriKind.Relative));
            }
            else
            {
                bundled.BaseTheme = BaseTheme.Dark;
                LogoImage.Source = new BitmapImage(new Uri("/Resources/LogoOscuro.png", UriKind.Relative));
            }
        }

        // Este evento nos permitira acceder al menu principal
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuPrincipal menu = new MenuPrincipal();
            menu.Owner = this;
            menu.ShowInTaskbar = true;
            menu.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            menu.Show();
        }
    } 
}