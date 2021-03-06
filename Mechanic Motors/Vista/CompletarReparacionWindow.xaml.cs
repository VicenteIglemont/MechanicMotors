﻿using Mechanic_Motors.Modelo;
using Mechanic_Motors.ServiciosBD;
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
    /// Lógica de interacción para CompletarReparacionWindow.xaml
    /// </summary>
    public partial class CompletarReparacionWindow : Window
    {

        Modelo.Reparacion reparacionCompletada;

        public CompletarReparacionWindow(Modelo.Reparacion reparacionCompletada)
        {
            this.reparacionCompletada = reparacionCompletada;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            IdClienteTextBlock.Text = reparacionCompletada.IdReparacion.ToString();
            NombreClienteTextBlock.Text = reparacionCompletada.NombreCliente;
            EmailClienteTextBlock.Text = reparacionCompletada.EmailCliente;
            TelefonoClienteTextBlock.Text = reparacionCompletada.TelefonoCliente;
            VehiculoTextBlock.Text = reparacionCompletada.Vehiculo;
            ProblemaTextBox.Text = reparacionCompletada.Descripcion;
        }

        // Cancelar dar por completada una reparacion
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Dar por completada una reparacion
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            string[] fecha = reparacionCompletada.HoraEntrada.ToString().Split(' ');
            string dia = fecha[0];
            string hora = fecha[1];

            if (EnviarEmailCheckBox.IsChecked == true)
            {

                MailMessage emailAutomatico = new MailMessage();
                emailAutomatico.To.Add(new MailAddress(reparacionCompletada.EmailCliente.ToString()));
                emailAutomatico.From = new MailAddress("mechanicmotors.reparaciones@gmail.com");
                emailAutomatico.Subject = "Reparación de Mechanic Motors";
                emailAutomatico.Body = $"Saludos, {reparacionCompletada.NombreCliente}. <br/> Nos ponemos en contacto con usted para informarle de que su reparación pendiente para el vehículo {reparacionCompletada.Vehiculo} que nos dejó el {dia} ha sido completada. <br/>" +
                    $"Si ha recibido este mensaje significa que su vehículo ya está disponible para que usted lo recoja. <br/> Cualquier duda puede escribirnos mediante nuestra app 'MechanicMotorsClientes' en la seccion de consultas. <br/> <br/>" +
                    $"Un saludo y muchas gracias por su confianza.";
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha podido enviar el email... Error: " + ex.Message, "Enviar email", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            PasarAHistorial(reparacionCompletada);

            this.Close();
        }

        // Metodo mediante el cual pasamos la reparacion al historial de reparaciones
        private void PasarAHistorial(Reparacion r)
        {
            Historial historial = new Historial();
            historial.NombreCliente = r.NombreCliente;
            historial.EmailCliente = r.EmailCliente;
            historial.TelefonoCliente = r.TelefonoCliente;
            historial.Vehiculo = r.Vehiculo;
            historial.Descripcion = r.Descripcion;
            historial.HoraEntrada = r.HoraEntrada;
            historial.HoraFinalizacion = DateTime.Now;

            BDServicios.AddHistorial(historial);
            BDServicios.DeleteReparacion(r);
        }
    }
}
