using Mechanic_Motors.VistaModelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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

        private void ActualizarHora_Tick(object sender, EventArgs e)
        {
            HoraActualTextBlockMain.Text = $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NuevaReparacionWindow nuevaReparacionWindow = new NuevaReparacionWindow();
            nuevaReparacionWindow.Owner = this;
            nuevaReparacionWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            nuevaReparacionWindow.Show();
        }
    }
}
