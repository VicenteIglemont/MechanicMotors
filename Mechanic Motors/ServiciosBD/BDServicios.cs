using Mechanic_Motors.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic_Motors.ServiciosBD
{
    class BDServicios
    {

        
        private static readonly MechanicMotorsEntities3 _contexto;

        // Cargamos en el programa las tablas
        static BDServicios()
        {
            _contexto = new MechanicMotorsEntities3();
            _contexto.Reparacions.Load();
            _contexto.Piezas.Load();
        }

        // Obtenemos todos los registros de las reparaciones
        public static ObservableCollection<Modelo.Reparacion> GetReparaciones()
        {
            _contexto.Reparacions.Load();
            return _contexto.Reparacions.Local;
        }

        public static ObservableCollection<Modelo.Historial> GetHistorial()
        {
            _contexto.Historials.Load();
            return _contexto.Historials.Local;
        }

        // Obtenemos todos los registros de las piezas
        public static ObservableCollection<Modelo.Pieza> GetAlmacen()
        {
            _contexto.Piezas.Load();
            return _contexto.Piezas.Local;
        }

        // Obtenemos todos los registros de las citas del dia
        public static ObservableCollection<Modelo.Cita> GetCitas()
        {
            _contexto.Citas.Load();
            ObservableCollection<Cita> citas = _contexto.Citas.Local;
            ObservableCollection<Cita> citasDelDia = new ObservableCollection<Cita>();

            foreach(Cita x in citas)
            {
                if(x.HoraCita.Day == DateTime.Now.Day && x.HoraCita.Month == DateTime.Now.Month && x.HoraCita.Year == DateTime.Now.Year)
                {
                    citasDelDia.Add(x);
                }
            }

            return citasDelDia;
        }

        // Obtenemos todos los registros de las consultas
        public static ObservableCollection<Modelo.Consulta> GetDudas()
        {
            _contexto.Consultas.Load();
            return _contexto.Consultas.Local;
        }



        // Crea la nueva reparacion
        public static int AddReparacion(Modelo.Reparacion reparacion)
        {
            _contexto.Reparacions.Add(reparacion);
            return _contexto.SaveChanges();
        }

        // Guarda los cambios en una reparacion
        public static int SaveReparacion(Reparacion reparacionElegida)
        {
            Modelo.Reparacion reparacion = BuscaReparacion(reparacionElegida.IdReparacion);

            reparacion.TelefonoCliente = reparacionElegida.TelefonoCliente;
            reparacion.EmailCliente = reparacionElegida.EmailCliente;
            reparacion.Descripcion = reparacionElegida.Descripcion;

            return _contexto.SaveChanges();
        }

        // Borra la reparacion
        public static int DeleteReparacion(Reparacion reparacionCancelada)
        {
            _contexto.Reparacions.Remove(reparacionCancelada);
            return _contexto.SaveChanges();
        }



        // Crea la nueva pieza
        public static int AddPieza(Modelo.Pieza pieza)
        {
            _contexto.Piezas.Add(pieza);
            return _contexto.SaveChanges();
        }

        // Incrementa en 1 la cantidad de la pieza
        public static int IncrementarPieza(Pieza piezaIncrementada)
        {
            foreach(Pieza x in _contexto.Piezas)
            {
                if(x.IdPieza == piezaIncrementada.IdPieza)
                {
                    x.Cantidad++;
                    break;
                }
            }
            return _contexto.SaveChanges();
        }

        // Decrementa en 1 la cantidad de la pieza
        public static int DecrementarPieza(Pieza piezaDecrementada)
        {
            foreach (Pieza x in _contexto.Piezas)
            {
                if (x.IdPieza == piezaDecrementada.IdPieza)
                {
                    x.Cantidad--;
                    break;
                }
            }
            return _contexto.SaveChanges();
        }

        // Guarda los cambios en una pieza
        public static int SavePieza(Modelo.Pieza piezaElegida)
        {
            Modelo.Pieza pieza = BuscaPieza(piezaElegida.IdPieza);

            pieza.NombrePieza = piezaElegida.NombrePieza;
            pieza.VehiculoPerteneciente = piezaElegida.VehiculoPerteneciente;
            pieza.Color = piezaElegida.Color;
            pieza.Cantidad = piezaElegida.Cantidad;
            pieza.PrecioUnitario = piezaElegida.PrecioUnitario;

            return _contexto.SaveChanges();
        }

        // Borra una pieza
        public static int DeletePieza(Pieza piezaCancelada)
        {
            _contexto.Piezas.Remove(piezaCancelada);
            return _contexto.SaveChanges();
        }



        // Para asegurarnos de que actualizamos la BD correctamente buscamos la reparacion con el mismo id, se usa en las ediciones de reparaciones. 
        private static Reparacion BuscaReparacion(int idReparacion)
        {
            foreach (Modelo.Reparacion x in _contexto.Reparacions)
            {
                if (x.IdReparacion == idReparacion)
                    return x;
            }
            return null;
        }

        // Para asegurarnos de que actualizamos la BD correctamente buscamos la reparacion con el mismo id, se usa en las ediciones de piezas.
        private static Pieza BuscaPieza(int idPieza)
        {
            foreach (Modelo.Pieza x in _contexto.Piezas)
            {
                if (x.IdPieza == idPieza)
                    return x;
            }
            return null;
        }




        // Para borrar la consulta una vez respondida
        public static int DeleteConsulta(Consulta consultaCancelada)
        {
            _contexto.Consultas.Remove(consultaCancelada);
            return _contexto.SaveChanges();
        }

        // Para borrar la cita
        public static int DeleteCita(Cita citaElegida)
        {
            _contexto.Citas.Remove(citaElegida);
            return _contexto.SaveChanges();
        }



        // Para añadir una reparacion al historial 
        public static int AddHistorial(Modelo.Historial reparacionAGuardar)
        {
            _contexto.Historials.Add(reparacionAGuardar);
            return _contexto.SaveChanges();
        }
    }
}
