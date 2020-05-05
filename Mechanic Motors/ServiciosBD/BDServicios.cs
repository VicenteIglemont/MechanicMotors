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
            return _contexto.Reparaciones.Local;
        }

        public static ObservableCollection<Modelo.Pieza> GetAlmacen()
        {
            return _contexto.Almacen.Local;
        }
        
        public static int AddReparacion(Modelo.Reparacion reparacion)
        {
            _contexto.Reparaciones.Add(reparacion);
            return _contexto.SaveChanges();
        }

        public static int SaveReparacion(Reparacion reparacionElegida)
        {

            Modelo.Reparacion reparacion = BuscaReparacion(reparacionElegida.IdReparacion);

            reparacion.TelefonoCliente = reparacionElegida.TelefonoCliente;
            reparacion.EmailCliente = reparacionElegida.EmailCliente;
            reparacion.Descripcion = reparacionElegida.Descripcion;

            return _contexto.SaveChanges();
        }

        public static int DeleteReparacion(Reparacion reparacionCancelada)
        {
            _contexto.Reparaciones.Remove(reparacionCancelada);
            return _contexto.SaveChanges();
        }

        private static Reparacion BuscaReparacion(int idReparacion)
        {
            foreach(Modelo.Reparacion x in _contexto.Reparaciones)
            {
                if (x.IdReparacion == idReparacion)
                    return x;
            }
            return null;
        }

        public static int AddPieza(Modelo.Pieza pieza)
        {
            _contexto.Almacen.Add(pieza);
            return _contexto.SaveChanges();
        }

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

        internal static int DecrementarPieza(Pieza piezaDecrementada)
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
    }
}
