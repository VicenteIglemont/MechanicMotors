using MaterialDesignThemes.Wpf;
using Mechanic_Motors.Modelo;
using Mechanic_Motors.ServiciosBD;
using Mechanic_Motors.VistaModelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Mechanic_Motors.Vista
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        // Esto nos permitira transoformar toda la informacion aportada por el sistema a español.
        CultureInfo idioma = new CultureInfo("es-ES");

        public MenuPrincipal()
        {
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();
            ComprobarLogo();
            ReparacionesDataGrid.DataContext = (DataContext as MenuPrincipalViewModel).GetReparaciones();
            AlmacenDataGrid.DataContext = (DataContext as MenuPrincipalViewModel).GetAlmacen();
            FechaActualTextBlockMain.Text = $"{idioma.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).ToUpper()}, {DateTime.Now.Day} de {DateTime.Now.ToString("MMMM").ToUpper()} de {DateTime.Now.Year}";
            HoraActualTextBlockMain.Text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

            // Timer que ira actualizando la hora del Main Window
            DispatcherTimer actualizarHora = new DispatcherTimer();
            actualizarHora.Interval = TimeSpan.FromSeconds(1);
            actualizarHora.Tick += ActualizarHora_Tick;
            actualizarHora.Start();
        }

        private void ComprobarLogo()
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                MiniLogo.Source = new BitmapImage(new Uri("/Resources/LogoOscuro.png", UriKind.Relative));
            }
            else
            {
                MiniLogo.Source = new BitmapImage(new Uri("/Resources/LogoClaro.png", UriKind.Relative));
            }
        }

        private void ActualizarHora_Tick(object sender, EventArgs e)
        {
            HoraActualTextBlockMain.Text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            Reparacion reparacionElegida = ReparacionesDataGrid.SelectedItem as Modelo.Reparacion;
            EditarReparacionWindow editarReparacionWindow = new EditarReparacionWindow(reparacionElegida);
            editarReparacionWindow.Owner = this;
            editarReparacionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editarReparacionWindow.ShowDialog();
            while (editarReparacionWindow.IsActive)
            {

            }
            ReparacionesDataGrid.Items.Refresh();
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Reparacion reparacionCancelada = ReparacionesDataGrid.SelectedItem as Modelo.Reparacion;
            CancelarReparacionWindow cancelarReparacionWindow = new CancelarReparacionWindow(reparacionCancelada);
            cancelarReparacionWindow.Owner = this;
            cancelarReparacionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cancelarReparacionWindow.ShowDialog();
            while (cancelarReparacionWindow.IsActive)
            {

            }
            ReparacionesDataGrid.Items.Refresh();
        }

        private void AñadirCantidadButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaIncrementada = AlmacenDataGrid.SelectedItem as Modelo.Pieza;
            if ((DataContext as MenuPrincipalViewModel).IncrementarPieza(piezaIncrementada) == 1) 
            {
                AlmacenDataGrid.Items.Refresh();
            }
        }

        private void RestarCantidadButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaDecrementada = AlmacenDataGrid.SelectedItem as Modelo.Pieza;
            if ((DataContext as MenuPrincipalViewModel).DecrementarPieza(piezaDecrementada) == 1)
            {
                AlmacenDataGrid.Items.Refresh();
            }
        }

        private void DarkModeToggle_Click(object sender, RoutedEventArgs e)
        {
            BundledTheme bundled = (BundledTheme)App.Current.Resources.MergedDictionaries.First();

            if (bundled.BaseTheme == BaseTheme.Dark)
            {
                bundled.BaseTheme = BaseTheme.Light;
                MiniLogo.Source = new BitmapImage(new Uri("/Resources/LogoClaro.png", UriKind.Relative));
            }
            else
            {
                bundled.BaseTheme = BaseTheme.Dark;
                MiniLogo.Source = new BitmapImage(new Uri("/Resources/LogoOscuro.png", UriKind.Relative));
            }
        }



        // Comandos
        private void CommandBinding_NuevaReparacionExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            NuevaReparacionWindow nuevaReparacionWindow = new NuevaReparacionWindow();
            nuevaReparacionWindow.Owner = this;
            nuevaReparacionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nuevaReparacionWindow.ShowDialog();
        }

        private void CommandBinding_NuevaReparacionCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_NuevaPiezaExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            NuevaPiezaWindow nuevaPiezaWindow = new NuevaPiezaWindow();
            nuevaPiezaWindow.Owner = this;
            nuevaPiezaWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nuevaPiezaWindow.ShowDialog();
        }

        private void CommandBinding_NuevaPiezaCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
