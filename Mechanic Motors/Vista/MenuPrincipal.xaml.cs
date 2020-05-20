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
            CitasDataGrid.DataContext = (DataContext as MenuPrincipalViewModel).GetCitas();
            ConsultasDataGrid.DataContext = (DataContext as MenuPrincipalViewModel).GetDudas();
            FechaActualTextBlockMain.Text = $"{idioma.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek).ToUpper()}, {DateTime.Now.Day} de {DateTime.Now.ToString("MMMM").ToUpper()} de {DateTime.Now.Year}";
            HoraActualTextBlockMain.Text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";

            // Timer que ira actualizando la hora del Main Window
            DispatcherTimer actualizarHora = new DispatcherTimer();
            actualizarHora.Interval = TimeSpan.FromSeconds(1);
            actualizarHora.Tick += ActualizarHora_Tick;
            actualizarHora.Start();
        }

        // Controla cual es el logo mostrado, el de tema claro u oscuro
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

        // Boton que maneja el tema claro y el tema oscuro
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

        // Evento que controla que se actualice el timer de horas
        private void ActualizarHora_Tick(object sender, EventArgs e)
        {
            string hora = "";

            if(DateTime.Now.Hour.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Hour}:";
            }
            else
            {
                hora += $"{DateTime.Now.Hour}:";
            }

            if(DateTime.Now.Minute.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Minute}:";
            }
            else
            {
                hora += $"{DateTime.Now.Minute}:";
            }

            if(DateTime.Now.Second.ToString().Length == 1)
            {
                hora += $"0{DateTime.Now.Second}";
            }
            else
            {
                hora += $"{DateTime.Now.Second}";
            }

            HoraActualTextBlockMain.Text = hora;
        }

        // Boton para completar una reparacion y enviar un email en caso de asi quererlo
        private void CompletarButton_Click(object sender, RoutedEventArgs e)
        {
            Reparacion reparacionCompletada = ReparacionesDataGrid.SelectedItem as Modelo.Reparacion;
            CompletarReparacionWindow completarReparacionWindow = new CompletarReparacionWindow(reparacionCompletada);
            completarReparacionWindow.Owner = this;
            completarReparacionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            completarReparacionWindow.ShowDialog();
            while (completarReparacionWindow.IsActive)
            {

            }
            ReparacionesDataGrid.Items.Refresh();
        }

        // Boton para editar una reparacion
        private void EditarReparacionButton_Click(object sender, RoutedEventArgs e)
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

        // Boton para cancelar y eliminar una reparacion
        private void CancelarReparacionButton_Click(object sender, RoutedEventArgs e)
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

        // Comando para registrar una nueva reparación
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

        // Comando para crear una nueva pieza
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

        // Boton que aumenta en 1 la cantidad de unidades de una pieza. 
        private void AñadirCantidadButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaIncrementada = AlmacenDataGrid.SelectedItem as Modelo.Pieza;
            if ((DataContext as MenuPrincipalViewModel).IncrementarPieza(piezaIncrementada) == 1)
            {
                AlmacenDataGrid.Items.Refresh();
            }
        }

        // Boton que disminuye en 1 la cantidad de unidades de una pieza. 
        private void RestarCantidadButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaDecrementada = AlmacenDataGrid.SelectedItem as Modelo.Pieza;
            if ((DataContext as MenuPrincipalViewModel).DecrementarPieza(piezaDecrementada) == 1)
            {
                AlmacenDataGrid.Items.Refresh();
            }
        }

        // Boton para proceder a la eliminacion de una pieza
        private void EliminarPiezaButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaCancelada = AlmacenDataGrid.SelectedItem as Modelo.Pieza;

            if(System.Windows.MessageBox.Show("¿Desea eliminar esta pieza?", "Eliminar Pieza", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(BDServicios.DeletePieza(piezaCancelada) == 1)
                {
                    System.Windows.MessageBox.Show("Pieza eliminada con éxito", "Eliminar pieza", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("No se ha podido eliminar la pieza... Compruebe su conexión a internet", "Eliminar pieza", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        // Boton para proceder a la modificacion de una pieza
        private void EditarPiezaButton_Click(object sender, RoutedEventArgs e)
        {
            Pieza piezaElegida = AlmacenDataGrid.SelectedItem as Modelo.Pieza;
            EditarPiezaWindow editarPiezaWindow = new EditarPiezaWindow(piezaElegida);
            editarPiezaWindow.Owner = this;
            editarPiezaWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editarPiezaWindow.ShowDialog();
            while (editarPiezaWindow.IsActive)
            {

            }
            AlmacenDataGrid.Items.Refresh();
        }

        // Boton para cerrar la app
        private void MenuItemSalir_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        // Boton para volver al salvapantallas
        private void MiniLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        // Evento para controlar que solo haya un expander como máximo desplegado en cualquier momento
        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandUnicamente(sender as Expander);
        }

        private void ExpandUnicamente(Expander expander)
        {
            foreach(var hijo in ContenedorExpanders.Children)
            {
                if(hijo is Expander && hijo != expander)
                {
                    ((Expander)hijo).IsExpanded = false;
                }
            }
        }

        // Boton para responder a una consulta
        private void ResponderDudaButton_Click(object sender, RoutedEventArgs e)
        {
            Consulta consultaElegida = ConsultasDataGrid.SelectedItem as Modelo.Consulta;
            ResponderConsultaWindow responderConsultaWindow = new ResponderConsultaWindow(consultaElegida);
            responderConsultaWindow.Owner = this;
            responderConsultaWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            responderConsultaWindow.ShowDialog();
            while (responderConsultaWindow.IsActive)
            {

            }
            ConsultasDataGrid.Items.Refresh();
        }

        // Boton para eliminar una consulta
        private void EliminarDudaButton_Click(object sender, RoutedEventArgs e)
        {
            Consulta consultaElegida = ConsultasDataGrid.SelectedItem as Modelo.Consulta;

            if (System.Windows.MessageBox.Show("¿Desea eliminar esta consulta?", "Eliminar Consulta", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (BDServicios.DeleteConsulta(consultaElegida) == 1)
                {
                    System.Windows.MessageBox.Show("Consulta eliminada con éxito", "Eliminar Consulta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("No se ha podido eliminar la consulta... Compruebe su conexión a internet", "Eliminar Consulta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
