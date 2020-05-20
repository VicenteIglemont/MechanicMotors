using Mechanic_Motors.Modelo;
using Mechanic_Motors.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace Mechanic_Motors.Vista
{
    /// <summary>
    /// Lógica de interacción para ResponderConsultaWindow.xaml
    /// </summary>
    public partial class ResponderConsultaWindow : Window
    {

        Modelo.Consulta consultaElegida;

        public ResponderConsultaWindow(Modelo.Consulta consultaElegida)
        {
            this.consultaElegida = consultaElegida;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();
            EmailTextBlock.Text += $"{consultaElegida.Email}";
            ConsultaTextBox.Text = consultaElegida.Descripcion;
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResponderButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("¿Desea enviar esta respuesta?", "Enviar respuesta", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MailMessage emailAutomatico = new MailMessage();
                emailAutomatico.To.Add(new MailAddress(consultaElegida.Email.ToString()));
                emailAutomatico.From = new MailAddress("mechanicmotors.reparaciones@gmail.com");
                emailAutomatico.Subject = "Consulta de Mechanic Motors";
                emailAutomatico.Body = $"Saludos. Nos ponemos en contacto con usted para responder a la duda que usted nos envio mediante nuetras app. <br /> " +
                    $"CONSULTA: {consultaElegida.Descripcion} <br />" +
                    $"RESPUESTA: {RespuestaTextBox.Text} <br />" +
                    $"Cualquier duda, no dude en ponerse en contacto con nosotros. Un saludo y muchas gracias por su confianza.";
                emailAutomatico.IsBodyHtml = true;
                emailAutomatico.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("mechanicmotors.reparaciones@gmail.com", "mmadmin123.");

                try
                {
                    smtp.Send(emailAutomatico);
                    emailAutomatico.Dispose();
                    MessageBox.Show("El correo se ha enviado de manera satisfactoria", "Enviar email", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido enviar el email... Error: " + ex.Message, "Enviar email", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }
}
