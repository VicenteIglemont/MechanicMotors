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

        
        private static readonly MechanicMotorsEntity _contexto;

        // Cargamos en el programa las tablas
        static BDServicios()
        {
            _contexto = new MechanicMotorsEntity();
            _contexto.Reparaciones.Load();
            _contexto.Almacen.Load();
        }

        // Obtenemos todos los registros de la base de datos
        public static ObservableCollection<Modelo.Reparacion> GetReparaciones()
        {
            _contexto.Reparaciones.Load();
            return _contexto.Reparaciones.Local;
        }

        public static ObservableCollection<Modelo.Pieza> GetAlmacen()
        {
            _contexto.Almacen.Load();
            return _contexto.Almacen.Local;
        }
        
        // Crea la nueva reparacion
        public static int AddReparacion(Modelo.Reparacion reparacion)
        {
            _contexto.Reparaciones.Add(reparacion);
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
            _contexto.Reparaciones.Remove(reparacionCancelada);
            return _contexto.SaveChanges();
        }

        // Crea la nueva pieza
        public static int AddPieza(Modelo.Pieza pieza)
        {
            _contexto.Almacen.Add(pieza);
            return _contexto.SaveChanges();
        }

        // Incrementa en 1 la cantidad de la pieza
        public static int IncrementarPieza(Pieza piezaIncrementada)
        {
            foreach(Pieza x in _contexto.Almacen)
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
            foreach (Pieza x in _contexto.Almacen)
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
            _contexto.Almacen.Remove(piezaCancelada);
            return _contexto.SaveChanges();
        }


        // Para asegurarnos de que actualizamos la BD correctamente buscamos la reparacion con el mismo id, se usa en las ediciones de reparaciones. 
        private static Reparacion BuscaReparacion(int idReparacion)
        {
            foreach (Modelo.Reparacion x in _contexto.Reparaciones)
            {
                if (x.IdReparacion == idReparacion)
                    return x;
            }
            return null;
        }

        // Para asegurarnos de que actualizamos la BD correctamente buscamos la reparacion con el mismo id, se usa en las ediciones de piezas. 

        private static Pieza BuscaPieza(int idPieza)
        {
            foreach (Modelo.Pieza x in _contexto.Almacen)
            {
                if (x.IdPieza == idPieza)
                    return x;
            }
            return null;
        }
    }
}
